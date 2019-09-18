using System;
using System.Collections.Generic;
using System.Text;

namespace BDDExample.Models
{
    public class UserMailerLog
    {
        public Guid Id { get; set; }
        public Guid UserId { get; set; }
        public string Subject { get; set; }
        public string Body { get; set; }
        public DateTime CreatedAt { get; set; }

        public UserMailerLog()
        {
            CreatedAt = DateTime.Now;
            Id = Guid.NewGuid();
        }
    }
}
