using System;
using EF006.DBContext.Data;



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
            using (var context = new AppDbContext())
            {
                foreach(var wallet in context.Wallets)
                {
                    Console.WriteLine(wallet);
                }
            }
        }
    
    }

}