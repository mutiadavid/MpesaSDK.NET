using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace MpesaSDK.NET.Dtos.Responses
{
    public class SuccessResponse
    {
        [JsonProperty("ConversationID")]
        public string ConversationID { get; set; }

        #region stkpush extra paramas
        [JsonProperty("MerchantRequestID")]
        public string MerchantRequestID { get; set; }
        [JsonProperty("CheckoutRequestID")]
        public string CheckoutRequestID { get; set; }
        #endregion

        [JsonProperty("OriginatorCoversationID")]
        public string OriginatorConversationID { get; set; }
        [JsonProperty("ResponseCode")]
        public string ResponseCode { get; set; }
        [JsonProperty("ResponseDescription")]
        public string ResponseDescription { get; set; }
        public override string ToString()
        {
            return JsonConvert.SerializeObject(this);
        }
    }
}
