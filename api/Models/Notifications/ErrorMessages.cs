using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace api.Models.Notifications
{
    public static class ErrorMessages
    {
        public static string Null_Request => "Request was null";
        public static string Phone_Validation_Error => "Not a valid UK phone number";
        public static string Empty_Content => "Empty sms content";
    }
}
