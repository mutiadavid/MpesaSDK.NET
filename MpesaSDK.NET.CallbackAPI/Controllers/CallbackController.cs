using Microsoft.AspNetCore.Mvc;
using MpesaSDK.NET.Dtos.Callbacks;

namespace MpesaSDK.NET.CallbackAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CallbackController : ControllerBase
    {
        private readonly ILogger<CallbackController> _logger;
        private readonly MpesaClient _client;

        public CallbackController(ILogger<CallbackController> logger, MpesaClient client)
        {
            this._logger = logger;
            _client = client;
        }

        [HttpGet("Test")]
        public async Task<IActionResult> TestAsync()
        {
            await _client.STKPushAsync(null);
            return Ok();
        }


        [HttpPost("StkPushCallback")]
        public IActionResult StkPushCallback([FromBody] LipaNaMpesaCallback callback)
        {
            _logger.LogInformation(callback.ToString());
            return Ok();
        }

        public IActionResult C2BCallback([FromBody] C2BCallback callback)
        {
            _logger.LogInformation(callback.ToString());
            return Ok();
        }

        [HttpPost("C2BValidation")]
        public IActionResult C2BValidation([FromBody] C2BValidationCallback callback)
        {
            _logger.LogInformation(callback.ToString());
            return Ok();
        }

        [HttpPost("C2BConfirmation")]
        public IActionResult C2BConfirmation([FromBody] C2BConfirmationCallback callback)
        {
            _logger.LogInformation(callback.ToString());
            return Ok();
        }
    }
}