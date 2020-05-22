using Microsoft.EntityFrameworkCore;
using MoveMoney.API.Models;

namespace MoveMoney.API.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options) { }

        public DbSet<Agency> Agency { get; set; }
        public DbSet<ClosingCashAgent> ClosingCashAgents { get; set; }
        public DbSet<ClosingCashManager> ClosingCashManagers { get; set; }
        public DbSet<ClosingCashManangerDetail> ClosingCashManangerDetails { get; set; }
        public DbSet<Country> Countries { get; set; }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<TypeIdentification> TypeIdentifications { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<UserLogs> UserLogs { get; set; }
        public DbSet<UserRole> UserRoles { get; set; }
        public DbSet<Comission> Comissions { get; set; }
        public DbSet<ComissionRange> ComissionRanges { get; set; }


        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<ClosingCashAgent>() //Setting Up 1 to 1 relationship
            .HasOne(a => a.ClosingCashManangerDetail)
            .WithOne(b => b.ClosingCashAgent)
            .HasForeignKey<ClosingCashManangerDetail>(b => b.ClosingCashAgentId);

            builder.Entity<Order>()
            .HasOne(a => a.ClosingCashAgentDetail)
            .WithOne(b => b.Order)
            .HasForeignKey<ClosingCashAgentDetail>(b => b.OrderId);
        }
    }
}