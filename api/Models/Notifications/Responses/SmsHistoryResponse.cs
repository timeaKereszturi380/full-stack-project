using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace api.Models.Notifications.Responses
{
    public class SmsHistoryResponse
    {
        public ICollection<SmsItem> Items { get; set; }
    }
}
