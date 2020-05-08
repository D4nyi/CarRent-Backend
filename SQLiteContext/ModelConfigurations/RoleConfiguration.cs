using CarRent.Contexts.Models.Core;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CarRent.Contexts.SQLiteContext.ModelConfigurations
{
    internal class RoleConfiguration : IEntityTypeConfiguration<Role>
    {
        public void Configure(EntityTypeBuilder<Role> builder)
        {
            builder
                .HasKey(r => r.Id);
            
            builder
                .HasIndex(r => r.Name)
                .IsUnique();

            builder
                .Property(r => r.Name)
                .IsRequired();
        }
    }
}
