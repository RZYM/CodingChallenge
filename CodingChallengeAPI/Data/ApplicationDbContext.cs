using CodingChallengeAPI.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;
using System.Reflection.Metadata;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace CodingChallengeAPI.Data
{
    public class ApplicationDbContext : DbContext
    {
        public virtual DbSet<Account> Account { get; set; }
        public virtual DbSet<Outstanding> Outstanding { get; set; }
        public virtual DbSet<Deposit> Deposit { get; set; }
        public virtual DbSet<TransferTransactions> TransferTransactions { get; set; }
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {

        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //modelBuilder.Entity<Deposit>()
            //.Property<int>("AccountsForeignKey");

            //modelBuilder.Entity<Deposit>()
            //        .HasOne(p => p.Accounts)
            //        .WithMany(b => b.Deposits)
            //        .HasForeignKey("AccountsForeignKey");

            //modelBuilder.Entity<Account>()
            //  .HasMany(c => c.TransferTransactions)
            //  .WithOne(e => e.Account)
            //  .HasForeignKey<Account, TransferTransactions>(p => new { p.TransferFrom, p.TransferTo });
            //.WithRequired() // or .WithOptional()
            //.HasForeignKey(e => new { e., e.DocID2 });

            //modelBuilder.Entity<Account>()
            //.HasMany(p => p.TransferTransactions)
            //.HasForeignKey(c => new { c.TransferFrom, c.TransferTo });
        }
    }
}
