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
                    var authToken = context.HttpContext.GetAuthToken();

                    if (_validationService.IsValidHost(context.HttpContext.Request.Host.ToString()) 
                        && _validationService.IsValidAccessToken(authToken))
                    {
                        context.HttpContext
                            .AddToken(authToken)
                            .AddStatus("Authorized")
                            .AddAccessibility("Authorized");
                    }
                    else
                    {

                        context.HttpContext
                            .AddToken(authToken)
                            .AddStatus("NotAuthorized")
                            .AddStatusCode(HttpStatusCode.Forbidden)
                            .AddReasonPhrase("Not Authorized");

                        context.Result = new JsonResult("NotAuthorized")
                        {
                            
                        };
                    }
                }
            }
        }

    }
}
