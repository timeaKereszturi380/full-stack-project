using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace api.Models.Notifications.Responses
{
    public class ValidationResponse
    {
        public ICollection<string> Errors { get; set; }
        public bool IsValid { get; set; }
    }
}
