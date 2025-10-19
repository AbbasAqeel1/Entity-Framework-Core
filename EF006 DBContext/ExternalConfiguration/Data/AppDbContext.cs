using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace ExternalConfiguration.Data
{
    public class AppDbContext : DbContext
    {
        public DbSet<Wallet> wallets { get; set; } = null!;

        public AppDbContext(DbContextOptions options) : base(options)
        {
        }

    }
}
