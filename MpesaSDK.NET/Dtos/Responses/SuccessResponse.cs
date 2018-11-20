using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace MpesaSDK.NET.Dtos.Responses
{

    public class BaseSuccessResponse
    {
        [JsonProperty("ResponseDescription")]
        public string ResponseDescription { get; set; }
        [JsonProperty("ResponseCode")]
        public string ResponseCode { get; set; }

        public override string ToString()
        {
            return JsonConvert.SerializeObject(this);
        }
    }

    public class CommonSuccessResponse :BaseSuccessResponse
    {
        [JsonProperty("ConversationID")]
        public string ConversationID { get; set; }

        [JsonProperty("OriginatorCoversationID")]
        public string OriginatorConversationID { get; set; }
        public override string ToString()
        {
            return JsonConvert.SerializeObject(this);
        }
    }

    public class StkPushQuerySuccessResponse : StkPushSuccessResponse
    {
        [JsonProperty("ResultDesc")]
        public string ResultDesc { get; set; }
        [JsonProperty("ResultCode")]
        public string ResultCode { get; set; }

        public override string ToString()
        {
            return JsonConvert.SerializeObject(this);
        }
    }

    public class StkPushSuccessResponse:StkPushBaseSuccessResponse
    {
        [JsonProperty("CustomerMessage")]
        public string CustomerMessage { get; set; }

        public override string ToString()
        {
            return JsonConvert.SerializeObject(this);
        }
    }

    public class StkPushBaseSuccessResponse : BaseSuccessResponse
    {
        [JsonProperty("MerchantRequestID")]
        public string MerchantRequestID { get; set; }
        [JsonProperty("CheckoutRequestID")]
        public string CheckoutRequestID { get; set; }
        public override string ToString()
        {
            return JsonConvert.SerializeObject(this);
        }
    }

}

