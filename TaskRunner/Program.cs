using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BDDExample.DB;
using BDDExample.Models;

namespace TaskRunner
{
    class Program
    {
        static void Main(string[] args)
        {
            var dbcontext = new ApplicationDbContext();
            var user = new User { Email="g.chaniotakis@bewise.gr"};
            Console.WriteLine(user.Id);

            dbcontext.Users.Add(user);
            dbcontext.SaveChanges();
            Console.WriteLine("Done!");
            Console.Read();
        }
    }
}
