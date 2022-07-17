using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using api.Models.Notifications.Enums;
using api.Models.Notifications.Responses;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Features;
using Microsoft.AspNetCore.Mvc.Filters;

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

        public static HttpContext AddToken(this HttpContext httpContext, string authToken)
        {
            httpContext.Response.Headers.Add("authToken", authToken);
            return httpContext;
        }

        public static HttpContext AddStatus(this HttpContext httpContext, string status)
        {
            httpContext.Response.Headers.Add("AuthStatus", status);
            return httpContext;
        }

        public static HttpContext AddAccessibility(this HttpContext httpContext, string status)
        {
            httpContext.Response.Headers.Add("storeAccessibility", status);
            return httpContext;
        }

        public static HttpContext AddStatusCode(this HttpContext httpContext, HttpStatusCode httpStatus)
        {
            httpContext.Response.StatusCode = (int)httpStatus;
            return httpContext;
        }

        public static HttpContext AddReasonPhrase(this HttpContext httpContext, string reason)
        {
            httpContext.Response.HttpContext.Features.Get<IHttpResponseFeature>().ReasonPhrase = reason;
            return httpContext;
        }

        public static string GetAuthToken(this HttpContext httpContext)
        {
            httpContext.Request.Headers.TryGetValue("sms-api-key", out var authTokens);
            var authToken = authTokens.FirstOrDefault();
            return authToken;
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
