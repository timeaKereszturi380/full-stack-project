using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Models.Paging;

namespace api.Models.Notifications.Responses
{
    public class NotificationActionResult
    {
        public ICollection<string> ErrorMessages { get; set; }
        public bool Success { get; set; }
    }

    public class NotificationActionResults<TModel> where TModel : class
    {
        public ICollection<TModel> Items { get; set; }
        public ICollection<string> ErrorMessages { get; set; }
        public bool Success { get; set; }
    }
}
