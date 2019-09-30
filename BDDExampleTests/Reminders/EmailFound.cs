using BDDExample.Services;
using BDDExample.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;
using System.IO;

namespace BDDExampleTests.Reminders
{
    [Trait("Reminders", "Email found")]
    public class ValidEmail : TestBase
    {
        ReminderResult _result;
        public ValidEmail()
        {
            var app = new Application("test@test.com", "password", "password");
            var result = new Registrator().ApplyForMembership(app);
            if(!Directory.Exists(@"C:\temp"))
            {
                Directory.CreateDirectory(@"C:\temp");
            }
            if (!Directory.Exists(@"C:\temp"))
            {
                Directory.CreateDirectory(@"C:\temp\maildrop");
            }
            foreach(FileInfo file in new DirectoryInfo(@"C:\temp\maildrop").GetFiles())
            {
                file.Delete();
            }
            _result = new Reminder().SendReminderTokenToUser("test@test.com", "");
        }

        [Fact(DisplayName = "Sets the reminder token")]
        public void SetsReminderToken()
        {
            Assert.NotNull(_result.User.ReminderToken);
        }
    }



    public class EmailFound
    {
    }
}
