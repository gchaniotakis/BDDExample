using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace BDDExampleTests.Authentication
{
    [Trait("Authentication", "Invalid Login")]
    public class InvalidLogin
    {
        [Fact(DisplayName ="Not authenticated")]
        public void NotAuthenticated()
        {

        }

        [Fact(DisplayName ="MessageProvided")]
        public void MessageProvided()
        {

        }
    }

    [Trait("Authentication", "Email was not found")]
    public class EmailNotFound
    {
        [Fact(DisplayName = "Not authenticated")]
        public void NotAuthenticated()
        {

        }

        [Fact(DisplayName = "MessageProvided")]
        public void MessageProvided()
        {

        }
    }

    [Trait("Authentication", "Password doesn't match")]
    public class PasswordDoesntMatch
    { 
        [Fact(DisplayName = "Not authenticated")]
        public void NotAuthenticated()
        {

        }

        [Fact(DisplayName = "MessageProvided")]
        public void MessageProvided()
        {

        }
    }
}
