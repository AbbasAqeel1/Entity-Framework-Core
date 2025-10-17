using System;
using Microsoft.Extensions.Configuration;




namespace ConnectionString
{
    public class Program
    {
        public static void Main(string[] args)
        {

                var configuration1 = new ConfigurationBuilder().AddJsonFile("appsettings.json").Build();

            Console.WriteLine(configuration1.GetSection("constr").Value);
            Console.ReadKey();
        }
    }
}