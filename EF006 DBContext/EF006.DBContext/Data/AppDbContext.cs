using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EF006.DBContext.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace EF006.DBContext.Data
{
    internal class AppDbContext : DbContext
    {
        public DbSet<Wallet> Wallets { get; set; } = null!;

        //DbContext Internal Configuration 
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);

            var config = new ConfigurationBuilder().AddJsonFile("appsettings3.json").Build();
            var connectionstr = config.GetSection("constr"). Value;
            optionsBuilder.UseSqlServer(connectionstr);
        }
    }
}
