using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Models.Notifications.Requests;
using api.Models.Notifications.Responses;

namespace api.Services.Notifications
{
    public interface ISmsNotificationService
    {
        Task<NotificationActionResult> SendSmsAsync(SmsRequest smsRequest);
        Task<NotificationActionResults<SmsItem>> GetMessagesAsync(SmsHistoryRequest historyRequest);
    }
}
