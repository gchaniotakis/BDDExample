using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BDDExample.DB;

namespace BDDExampleTests
{
    public class TestBase : IDisposable
    {
        public TestBase()
        {
            new ApplicationDbContext().Database.ExecuteSqlCommand("DELETE FROM USERS");
        }

        public void Dispose()
        {
            new ApplicationDbContext().Database.ExecuteSqlCommand("DELETE FROM USERS");
        }
    }
}
