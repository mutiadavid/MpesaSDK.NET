using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace MpesaSDK.NET.Dtos.Responses
{
    public class MpesaResponse
    {
        [JsonProperty("IsSuccess")]
        public bool IsSuccess { get; set; }
        [JsonProperty("SuccessResponse")]
        public SuccessResponse SuccessResponse { get; set; }
        [JsonProperty("ErrorResponse")]
        public ErrorResponse ErrorResponse { get; set; }

        public override string ToString()
        {
            return JsonConvert.SerializeObject(this);
        }
    }
}
