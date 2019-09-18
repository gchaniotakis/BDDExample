using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace BDDExampleTests.Registration
{
    [Trait("An Invalid Application was received", "")]
    public class ApplicationValidations
    {
        [Fact(DisplayName = "Application is Denied")]
        public void UserDenied()
        {
            throw new NotImplementedException("Implement me");
        }

        [Fact(DisplayName = "A message is shown explaining why")]
        public void MessageIsShown()
        {
            throw new NotImplementedException("Implement me");
        }
    }
}
