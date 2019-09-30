using BDDExample.Models;
using BDDExample.Services;

namespace BDDExample
{
    public interface IMembership
    {
        bool AssociateOpenAuth(string email, string provider, string providerid);
        AuthenticationResult Authenticate(string email, string password, string ip = "192.168.64.1");
        AuthenticationResult AuthenticateByOpenAuth(string providerid, string ip = "192.168.64.1");
        AuthenticationResult AuthenticateByToken(string authenticationtoken, string ip = "192.168.64.1");
        bool DisassociateOpenAuth(string email, string providerid);
        User GetUser(string email);
        RegistrationResult Register(string email, string password, string confirm, string publicname = null, string ip = "192.168.64.1");
        ResetResult ResetUsersPassword(string resettoken, string newpassword);
        ReminderResult SendResetTokenToUser(string email, string reseturl = "http://localhost/reminder/");
    }
}