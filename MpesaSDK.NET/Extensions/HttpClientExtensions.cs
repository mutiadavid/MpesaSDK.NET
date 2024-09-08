using MpesaSDK.NET.Dtos.Responses;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace MpesaSDK.NET.Extensions
{
    internal static class HttpClientExtensions
    {
        public static void WithBasicAuthentication(this HttpClient client, MpesaClientOptions options)
        {
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", Convert.ToBase64String(Encoding.ASCII.GetBytes($"{options.ConsumerKey}:{options.ConsumerSecret}")));
        }

        public static async Task WithTokenAuthenticationAsync(this HttpClient client, MpesaClientOptions options, CancellationToken cancellationToken)
        {
            // Check if Authorization header exists and contains a Bearer token
            if (client.DefaultRequestHeaders.Authorization != null && client.DefaultRequestHeaders.Authorization.Scheme == "Bearer")
                if (client.DefaultRequestHeaders.TryGetValues("TokenExpiryInUtc", out IEnumerable<string> tokenExpiresInValues))
                {
                    if (DateTimeOffset.TryParse(tokenExpiresInValues.FirstOrDefault(), out DateTimeOffset tokenExpiresIn) && tokenExpiresIn > DateTimeOffset.UtcNow)
                    {
                        // Token exists & has not expired
                        return;
                    }
                }

            // Renew the token using Basic Authentication
            client.WithBasicAuthentication(options);
            AccessTokenResponse tokenResult = await client.GetJsonAsync<AccessTokenResponse>("oauth/v1/generate?grant_type=client_credentials", cancellationToken);
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", tokenResult.AccessToken);
            // Set Token Expiry header
            client.DefaultRequestHeaders.Remove("TokenExpiryInUtc");
            client.DefaultRequestHeaders.Add("TokenExpiryInUtc", DateTimeOffset.UtcNow.AddSeconds(tokenResult.ExpiresIn).AddMinutes(-1).ToString("o"));
        }

        public static async Task<T> GetJsonAsync<T>(this HttpClient client, string url, CancellationToken cancellationToken)
        {
            HttpResponseMessage response = await client.GetAsync($"{client.BaseAddress}/{url}", cancellationToken);
            if (response.IsSuccessStatusCode)
            {
                string res = await response.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<T>(res);
            }
            throw new HttpRequestException(await GetSimplifiedMessageAsync(response));
        }

        public static async Task<T> PostJsonAsync<T>(this HttpClient client, string url, object data, MpesaClientOptions options, CancellationToken cancellationToken)
        {
            await client.WithTokenAuthenticationAsync(options, cancellationToken);
            string jsonData = JsonConvert.SerializeObject(data);
            HttpResponseMessage response = await client.PostAsync($"{client.BaseAddress}/{url}", new StringContent(jsonData, Encoding.UTF8, "application/json"), cancellationToken);
            if (response.IsSuccessStatusCode)
            {
                string res = await response.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<T>(res);
            }
            throw new HttpRequestException(await GetSimplifiedMessageAsync(response));
        }

        private static async Task<string> GetSimplifiedMessageAsync(HttpResponseMessage response)
        {
            try
            {
                string responseMsg = await response.Content.ReadAsStringAsync();
                if (response.StatusCode == HttpStatusCode.Forbidden)
                {
                    return $"Access Denied: {responseMsg}";
                }
                else if (response.StatusCode == HttpStatusCode.Unauthorized)
                {
                    return $"Unauthorized: {responseMsg}";
                }
                else if (response.StatusCode == HttpStatusCode.InternalServerError)
                {
                    ApiErrorResponse errInfo = JsonConvert.DeserializeObject<ApiErrorResponse>(responseMsg);
                    return $"Error: {errInfo.ErrorCode}, {errInfo.ErrorMessage}";
                }
            }
            catch (Exception ex) { Debug.WriteLine(ex.Message); }
            return $"Server Responded with a {response.StatusCode} Status Code, Reason: {response.ReasonPhrase}.";
        }
    }
}
