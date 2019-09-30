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
            
            foreach(var log in dbcontext.ActivityLogs)
            {
                Console.WriteLine(log.Entry);
            }


            Console.WriteLine("Done!");
            Console.Read();
        }
    }
}
