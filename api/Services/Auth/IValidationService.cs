using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Models.Notifications.Requests;
using api.Models.Notifications.Responses;

namespace api.Services.Auth
{
    public interface IValidationService
    {
        bool IsValidAccessToken(string token);
        bool IsValidHost(string host);
        ValidationResponse ValidateRequest(SmsRequest request);
    }
}
