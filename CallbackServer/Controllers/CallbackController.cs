using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using MpesaSDK.NET.Dtos.Callbacks;

namespace CallbackServer.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CallbackController : ControllerBase
    {
        private readonly ILogger<CallbackController> _logger;

        public CallbackController(ILogger<CallbackController> logger)
        {
            _logger = logger;
        }
        [HttpPost("StkPushCallback")]
        public async Task<IActionResult> StkPushCallbackAsync([FromBody] LipaNaMpesaCallback callback)
        {
            //do something with the call databack
            
            _logger.LogInformation(callback.ToString());
            return Ok();
        }
        [HttpPost("C2BValidation")]
        public async Task<IActionResult> C2BValidationAsync([FromBody] C2BValidationCallback callback)
        {
            _logger.LogInformation(callback.ToString());
            return Ok();
        }
        [HttpPost("C2BConfirmation")]
        public async Task<IActionResult> C2BConfirmationAsync([FromBody] C2BConfirmationCallback callback)
        {
            _logger.LogInformation(callback.ToString());
            return Ok();
        }




        [HttpGet("")]
        public IActionResult Get()
        {
            
            return Ok("Home");
        }

    }
}