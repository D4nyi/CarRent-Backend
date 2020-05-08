using CarRent.Contexts.Models.Core;
using CarRent.Contexts.SQLiteContext.ModelConfigurations;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics.CodeAnalysis;

namespace CarRent.Contexts.SQLiteContext
{
    public class SQLiteDbContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<Premise> Premises { get; set; }
        public DbSet<Car> Cars { get; set; }
        public DbSet<Renting> Rentings { get; set; }

        public SQLiteDbContext([NotNull] DbContextOptions options) : base(options)
        {
            Database.EnsureCreated();
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new UserConfiguration())
                .ApplyConfiguration(new PremiseConfiguration())
                .ApplyConfiguration(new CarConfiguration())
                .ApplyConfiguration(new RentingConfiguration())
                .ApplyConfiguration(new RoleConfiguration());
            
            base.OnModelCreating(modelBuilder);
        }
    }
}
