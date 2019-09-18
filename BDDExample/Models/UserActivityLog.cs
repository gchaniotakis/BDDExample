using System;
using System.Collections.Generic;
using System.Text;

namespace BDDExample.Models
{
    public class UserActivityLog
    {
        public string Subject { get; set; }
        public string Entry { get; set; }
        public Guid UserId { get; set; }
        public Guid Id { get; set; }
        public DateTime CreatedAt { get; set; }

        public UserActivityLog()
        {
            CreatedAt = DateTime.Now;
        }
    }
}
