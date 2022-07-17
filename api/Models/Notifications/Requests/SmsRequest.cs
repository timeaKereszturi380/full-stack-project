using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace api.Models.Notifications.Requests
{
    public class SmsRequest
    {
        [DataType(DataType.PhoneNumber)]
        public string SMSRecipient { get; set; }
        [DataType(DataType.PhoneNumber)]
        public string SMSFrom { get; set; }
        public string Content { get; set; }
    }
}
