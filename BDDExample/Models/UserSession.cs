using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BDDExample.Models
{
    public class UserSession
    {
        public Guid Id { get; set; }
        public Guid UserId { get; set; }
        public DateTime StartedAt { get; set; }
        public DateTime FinishedAt { get; set; }

        public UserSession(Guid userid)
        {
            StartedAt = DateTime.Now;
            Id = Guid.NewGuid();
            UserId = userid;
            FinishedAt = DateTime.Today.AddDays(30);
        }
    }
}
