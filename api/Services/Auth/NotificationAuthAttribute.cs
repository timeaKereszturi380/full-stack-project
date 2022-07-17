using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using api.Services.CoreFunctions;
using Microsoft.AspNetCore.Http.Features;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace api.Services.Auth
{
    public class NotificationAuthAttribute : TypeFilterAttribute
    {
        
        public NotificationAuthAttribute(Type type) : base(typeof(NotificationAuth))
        {
        }

        public class NotificationAuth : IAuthorizationFilter
        {

            private readonly IValidationService _validationService;

            public NotificationAuth(IValidationService validationService)
            {
                _validationService = validationService;
            }

            public void OnAuthorization(AuthorizationFilterContext context)
            {
                if (context == null)
                {
                }
                else
                {
                    context.HttpContext.Request.Headers.TryGetValue("sms-api-key", out var authTokens);
                    var authToken = authTokens.FirstOrDefault();

                    var test = _validationService.IsValidHost(context.HttpContext.Request.Host.ToString());

                    if (_validationService.IsValidHost(context.HttpContext.Request.Host.ToString()) 
                        && _validationService.IsValidAccessToken(authToken))
                    {
                        context.HttpContext.Response.Headers.Add("authToken", authToken);
                        context.HttpContext.Response.Headers.Add("AuthStatus", "Authorized");
                        context.HttpContext.Response.Headers.Add("storeAccessiblity", "Authorized");
                    }
                    else
                    {
                        context.HttpContext.Response.Headers.Add("authToken", authToken);
                        context.HttpContext.Response.Headers.Add("AuthStatus", "NotAuthorized");

                        context.HttpContext.Response.StatusCode = (int)HttpStatusCode.Forbidden;
                        context.HttpContext.Response.HttpContext.Features.Get<IHttpResponseFeature>().ReasonPhrase = "Not Authorized";
                        context.Result = new JsonResult("NotAuthorized")
                        {
                            
                        };
                    }
                }
            }
        }

    }
}
