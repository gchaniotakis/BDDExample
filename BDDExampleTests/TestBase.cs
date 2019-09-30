using System;
using System.Collections.Generic;
using System.IO;
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
            new ApplicationDbContext().Database.ExecuteSqlCommand("delete from useractivitylogs;delete from usermail");
            if (!Directory.Exists(@"C:\temp"))
            {
                Directory.CreateDirectory(@"C:\temp");
            }
            if (!Directory.Exists(@"C:\temp"))
            {
                Directory.CreateDirectory(@"C:\temp\maildrop");
            }
            foreach (FileInfo file in new DirectoryInfo(@"C:\temp\maildrop").GetFiles())
            {
                file.Delete();
            }
        }

        public void Dispose()
        {
            new ApplicationDbContext().Database.ExecuteSqlCommand("DELETE FROM USERS");
        }
    }
}
