using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace api.Models.Notifications.Requests
{
    public class SmsHistoryRequest
    {
        public DateTime? FromDateUtc { get; set; }
        public DateTime? ToDateUtc { get; set; }
        public int Page { get; set; }
        public int PageSize { get; set; }
    }
}
