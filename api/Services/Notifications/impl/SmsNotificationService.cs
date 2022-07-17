using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Models.Notifications.Enums;
using api.Models.Notifications.Requests;
using api.Models.Notifications.Responses;
using api.Models.Notifications.Sms;
using api.Services.Auth;
using api.Services.CoreFunctions;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using Twilio.Rest.Api.V2010.Account;
using Twilio.Rest.Api.V2010.Account.Call;
using Twilio.TwiML.Voice;
using static api.Services.CoreFunctions.Core;

namespace api.Services.Notifications.impl
{
    public class SmsNotificationService : ISmsNotificationService
    {
        private readonly SmsSettings _smsSettings;
        private readonly IValidationService _validationService;

        public SmsNotificationService(IOptions<SmsSettings> smsOptions, IValidationService validationService)
        {
            _smsSettings = smsOptions.Value;
            _validationService = validationService;
        }

        public async Task<NotificationActionResult> SendSmsAsync(SmsRequest smsRequest)
        {
            smsRequest.SMSFrom = _smsSettings.TrialPhoneNumber;
            smsRequest.SMSRecipient = "+447709694375";
            var validationResponse = _validationService.ValidateRequest(smsRequest);

            if (ShouldSendSms(validationResponse.IsValid))
            {
                var isSent = await SendAsync(smsRequest);

                return ActionResult(isSent, null);
            }

            return ActionResult(false, validationResponse.Errors);
        }

        private bool ShouldSendSms(bool isValidRequest)
            => !string.IsNullOrWhiteSpace(_smsSettings.AccountSid)
               && !string.IsNullOrWhiteSpace(_smsSettings.AuthToken)
               && isValidRequest;

        private async Task<bool> SendAsync(SmsRequest smsRequest)
        {
            try
            {
                var message = await MessageResource.CreateAsync(
                    body: smsRequest.Content,
                    from: new Twilio.Types.PhoneNumber(smsRequest.SMSFrom.SanitizeNumber()),
                    to: new Twilio.Types.PhoneNumber(smsRequest.SMSRecipient.SanitizeNumber())
                );
               
                return HasMessageBeenSent(message?.Status?.ToString());
            }
            catch (Exception exception)
            {
                return false;
            }
        }


        public async Task<NotificationActionResults<SmsItem>> GetMessagesAsync(SmsHistoryRequest historyRequest)
        {
            var errorMessages = new List<string>();
            var smsList = new List<SmsItem>();

            if (historyRequest.PageSize == 0)
            {
                historyRequest.PageSize = 20;
            }

            try
            {
                var messages = await MessageResource
                    .ReadAsync(limit: historyRequest.PageSize, dateSent: historyRequest.FromDateUtc, dateSentBefore: historyRequest.ToDateUtc);
                
                if (messages != null && messages.Any())
                {
                    smsList.AddRange(messages
                        .Select(m => new SmsItem() 
                            { Content = m.Body, Recipient = m.To, SentDateUtc = m.DateSent }));
                }

                return ActionResults(smsList, true, new List<string>());
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                errorMessages.Add(e.Message);
            }

            return ActionResults(new List<SmsItem>(), false, errorMessages); ;
        }

    }
}

