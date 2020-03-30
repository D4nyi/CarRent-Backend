using CarRent.Contexts.Models.Core;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics.CodeAnalysis;

namespace CarRent.Contexts.SQLiteContext
{
    public class SQLiteDbContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Premise> Premises { get; set; }
        public DbSet<Car> Cars { get; set; }

        public SQLiteDbContext([NotNull] DbContextOptions options) : base(options) { }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            _ = optionsBuilder.UseSqlite(@"Data Source=CarRentDB.db;");

            base.OnConfiguring(optionsBuilder);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }
    }
}
