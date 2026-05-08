using Microsoft.Extensions.DependencyInjection;

namespace MpesaSDK.NET.Extensions
{
    public static class MpesaClientExtension
    {
        public static void AddMpesaClient(this IServiceCollection services, MpesaClientOptions mpesaClientOptions)
        {
            services.AddHttpClient();
            services.AddSingleton(mpesaClientOptions);
            services.AddSingleton<MpesaClient>();
        }
    }
}
