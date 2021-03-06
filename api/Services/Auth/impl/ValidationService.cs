using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using api.Models.Notifications.Requests;
using api.Models.Notifications.Responses;
using api.Services.CoreFunctions;
using PhoneNumbers;
using static api.Models.Notifications.ErrorMessages;

namespace api.Services.Auth.impl
{
    public class ValidationService : IValidationService
    {
        private static Guid accessToken => Guid.Parse("ff44db7e-5088-4a1f-8c2c-1b75edea62e6");
        private static ICollection<string> validHosts => new ReadOnlyCollection<string>(new List<string>()
        {
            "localhost:44391"
        });

        
        public bool IsValidAccessToken(string token)
        {
            var tokenToValidate = token.GuidTryParse();
            return tokenToValidate != Guid.Empty
                   && tokenToValidate == accessToken;
        }

        public bool IsValidHost(string host)
            => validHosts.Contains(host);

        public ValidationResponse ValidateRequest(SmsRequest request)
        {
            var errorList = new List<string>();

            if (request == null)
            {
                errorList.Add(Null_Request);
            }

            if (!IsValidUKMobileNumber(request?.SMSRecipient))
            {
                errorList.Add(Phone_Validation_Error);
            }

            if (EmptyContent(request?.Content))
            {
                errorList.Add(Empty_Content);
            }

            return new ValidationResponse(){Errors = errorList, IsValid = errorList.Count == 0};
        }

        private bool EmptyContent(string content)
            => string.IsNullOrEmpty(content);

        private bool IsValidUKMobileNumber(string mobileNumber)
        {
            var phoneUtils = PhoneNumberUtil.GetInstance();
           
            try
            {
              var isValidNumber = phoneUtils.IsValidNumber(phoneUtils.Parse(mobileNumber,
                    "UK"));
                return isValidNumber;
            }
            catch (Exception e)
            {
                return false;
            }
        }
    }
}

