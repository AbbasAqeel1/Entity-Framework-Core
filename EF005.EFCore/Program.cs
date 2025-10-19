using System;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.Extensions.Configuration;


namespace EF005.EFCore
{


    public class Program
    {
        public static void Main(string[] args)
        {

            //RetrieveAllWallets();
            //RetrieveSigleWallet();
            //AddWallet();
            //UpdateWallet();
            //DeleteWallet();
            //QueryData();
            TransactionDemo();
            Console.ReadKey();

        }

        public static void RetrieveSigleWallet()
        {
            var walletID = 2;

            using (var context = new AppDbContext())
            {
                var result = context.Wallets.FirstOrDefault(x => x.Id == walletID);
                Console.WriteLine(result);
            }
        }

        public static void RetrieveAllWallets()
        {
            using (var context = new AppDbContext())
            {
                foreach (var wallet in context.Wallets)
                {
                    Console.WriteLine(wallet);
                }
            }
        }

        public static void AddWallet()
        {
            var wallet = new Wallet
            {
                Holder = "Jack",
                Balance = 444
            };

            using (var context = new AppDbContext())
            {
                context.Wallets.Add(wallet);
                context.SaveChanges();
                Console.WriteLine("Wallet Added successfully.");
            }
        }

        public static void UpdateWallet()
        {
            using (var context = new AppDbContext())
            {
                var walletToUpdate = context.Wallets.FirstOrDefault(x => x.Id == 23);
                walletToUpdate.Balance += 1000;

                context.SaveChanges();
            }

        }

        public static void DeleteWallet()
        {
            using (var context = new AppDbContext())
            {
                var walletToDelete = context.Wallets.FirstOrDefault(x => x.Id == 23);
                context.Wallets.Remove(walletToDelete);

                context.SaveChanges();
            }

        }

        public static void QueryData()
        {
            using (var context = new AppDbContext())
            {
                var wallets = context.Wallets.Where(x => x.Balance >= 5000).OrderByDescending(x => x.Balance);

                foreach (var wallet in wallets)
                {
                    Console.WriteLine(wallet);
                }
            }
        }

        public static void TransactionDemo()
        {
            using (var context = new AppDbContext())
            {
                using(var transaction = context.Database.BeginTransaction())
                {
                    //transfer 2000 dollar from abbas3 to Noah 

                    var FromWallet = context.Wallets.FirstOrDefault(x => x.Id == 14);
                    var ToWallet = context.Wallets.FirstOrDefault(x => x.Id == 1);

                    FromWallet.Balance -= 2000;
                    ToWallet.Balance += 2000;

                    transaction.Commit();

                }
                context.SaveChanges();
            }
        }
}    }