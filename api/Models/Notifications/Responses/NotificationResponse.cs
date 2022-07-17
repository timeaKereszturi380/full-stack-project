using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace api.Models.Notifications.Responses
{
    public class NotificationResponse
    {
        public bool Success { get; set; }
        public ICollection<string> ErrorMessages { get; set; }
    }
}
