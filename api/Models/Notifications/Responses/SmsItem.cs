using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace api.Models.Notifications.Responses
{
    public class SmsItem
    {
        public Guid Id { get; set; }
        public string Content { get; set; }
        public string Recipient { get; set; }
        public DateTime? SentDateUtc { get; set; }
    }
}
