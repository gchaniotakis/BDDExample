using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BDDExample.Models
{
    public class UserSession
    {
        public Guid Id { get; set; }
        public User User { get; set; }
        [Required]
        public DateTime StartedAt { get; set; }
        public DateTime FinishedAt { get; set; }
        [MaxLength(55)]
        public string IP { get; set; }

        public UserSession(Guid userid)
        {
            StartedAt = DateTime.Now;
            Id = Guid.NewGuid();            
            FinishedAt = DateTime.Today.AddDays(30);
        }
    }
}
