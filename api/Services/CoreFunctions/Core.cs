using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Models.Notifications.Enums;
using api.Models.Notifications.Responses;

namespace api.Services.CoreFunctions
{
    public static class Core
    {
        public static Guid GuidTryParse(this string value)
        {
            try
            {
                return Guid.Parse(value);
            }
            catch (Exception e)
            {
                return Guid.Empty;
            }
        }

        public static bool HasMessageBeenSent(string status)
        {
            if (string.IsNullOrEmpty(status))
            {
                return false;
            }

            return status.Equals(DeliveryStatus.COMPLETED.ToString(), StringComparison.CurrentCultureIgnoreCase)
                   || status.Equals(DeliveryStatus.QUEUED.ToString(), StringComparison.CurrentCultureIgnoreCase);
        }

        public static DateTime ToDateTime(this string value)
        {
            var success = DateTime.TryParse(value, out var dateTime);
            return success ? dateTime : DateTime.MinValue;
        }

        public static string SanitizeNumber(this string number)
            => number
                .Replace(" ", string.Empty)
                .Replace("-", string.Empty)
                .Replace("(", string.Empty)
                .Replace(")", string.Empty);

        public static NotificationActionResults<TModel> ActionResults<TModel>(ICollection<TModel> items, bool success, ICollection<string> messages) 
            where TModel : class
        {
            return new NotificationActionResults<TModel>()
            {
                ErrorMessages = messages,
                Success = success,
                Items = items
            };
        }

        public static NotificationActionResult ActionResult(bool success, ICollection<string> messages)
        {
            return new NotificationActionResult()
            {
                ErrorMessages = messages,
                Success = success,
            };
        }
    }
}
