using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BDDExample.Services;

namespace BDDExample
{
    public class Configuration
    {
        public string ResetUrl { get; set; }
        public string ConfirmationUrl { get; set; }
        public string ConnectionStringName { get; set; }
        public bool RequireEmailConfirmation { get; set; }
        public int MinPasswordLength { get; set; }
        public int DefaultUserSessionDays { get; set; }
        public Authenticator AuthenticationService { get; set; }
        public Registrator RegistrationService { get; set; }
        public Reminder ReminderService { get; set; }

        public Configuration()
        {
            ResetUrl = "http://localhost/reminder/";
            ConfirmationUrl = "http://localhost/email/confirm";
            ConnectionStringName = "BDDExample";
            MinPasswordLength = 4;
            RequireEmailConfirmation = false;
            DefaultUserSessionDays = 30;
            AuthenticationService = new Authenticator();
            RegistrationService = new Registrator();
            ReminderService = new Reminder();
        }
    }
}
