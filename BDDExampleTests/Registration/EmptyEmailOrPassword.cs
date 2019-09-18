using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace BDDExampleTests.Registration
{
    [Trait("An application is received with empty email or password", "")]
    public class EmptyEmailOrPassword
    {
        [Fact(DisplayName = "Application considered invalid")]
        public void ApplicationInvalid()
        {
            throw new NotImplementedException("Implement me");
        }

        [Fact(DisplayName = "A message is returned")]
        public void MessageReturned()
        {
            throw new NotImplementedException("Implement me");
        }
    }
}
