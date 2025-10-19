using System;
using ExternalConfiguration.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;



namespace EF006.DBContext
{
    public class Program
    {
        public static void Main(string[] args)
        {
            RunMethod1();




            Console.ReadKey();
        }

        public static void RunMethod1()
        {
            var config = new ConfigurationBuilder().AddJsonFile("appsettings3.json").Build();
            var connectionstr = config.GetSection("constr").Value;

            
            
            
            
            var OptionBuilder = new DbContextOptionsBuilder();

            OptionBuilder.UseSqlServer(connectionstr);



            var options = OptionBuilder.Options;

            using (var context = new AppDbContext(options))
            {
                foreach (var wallet in context.wallets)
                {
                    Console.WriteLine(wallet);
                }
            }
        }

    }

}