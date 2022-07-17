using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using api.Models.Notifications.Requests;
using api.Models.Notifications.Responses;
using api.Services.Auth;
using api.Services.Notifications;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using static api.Services.CoreFunctions.Core;

namespace api.Controllers
{
    [Route("api/[controller]")]
    [AllowAnonymous]
    [Produces("application/json")]
    [ApiController]
    public class SmsController : ControllerBase
    {
        private ISmsNotificationService _smsNotificationService;


        public SmsController(ISmsNotificationService smsNotificationService)
        {
            _smsNotificationService = smsNotificationService;
        }


       // [NotificationAuth(typeof(NotificationAuthAttribute.NotificationAuth))]
        [HttpGet("/history")]
        [ProducesResponseType(typeof(NotificationResponse), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.Forbidden)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> GetSmsHistoryAsync([FromQuery] string from, [FromQuery] string to)
        {
            var historyRequest = new SmsHistoryRequest()
            {
                FromDateUtc = from.ToDateTime(),
                ToDateUtc = to.ToDateTime()
            };
            var response = await _smsNotificationService.GetMessagesAsync(historyRequest);

            if (response.Success)
            {
                return Ok(response);
            }

            return BadRequest(response);
        }



        [NotificationAuth(typeof(NotificationAuthAttribute.NotificationAuth))]
        [HttpPost("/send")]
        [ProducesResponseType(typeof(NotificationResponse), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.Forbidden)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> SendSmsAsync([FromBody] SmsRequest request)
        {
           var response = await _smsNotificationService.SendSmsAsync(request);

            if (response.Success)
            {
                return Ok(response);
            }

            return BadRequest(response);
        }
    }
}
