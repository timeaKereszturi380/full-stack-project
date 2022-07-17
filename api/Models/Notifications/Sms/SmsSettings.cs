using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace api.Models.Notifications.Sms
{
    public class SmsSettings
    {
        public string AccountSid { get; set; } = "ACXXXXXXXXXXXXXXXXXXXXXXXXXXXX";
        public string AuthToken { get; set; } = "aXXXXXXXXXXXXXXXXXXXXXXXXXXXXXX";
        public string TrialPhoneNumber { get; set; }
    }
}
