using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
using BDDExample.Models;

namespace BDDExample.DB
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext() :base (nameOrConnectionString: "BDDExample")
        {
            
        }

        public DbSet<User> Users { get; set; }
        public DbSet<UserActivityLog> ActivityLogs { get; set; }
        public DbSet<UserMailerMessage> MailerMessages { get; set; }
        public DbSet<UserSession> Sessions { get; set; }
        public DbSet<UserMailerTemplate> Mailers { get; set; }
    }
}
