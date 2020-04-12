using CarRent.Contexts.Models.Core;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CarRent.Contexts.SQLiteContext.ModelConfigurations
{
    internal sealed class UserConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.HasKey(u => u.Id);

            builder.Property(u => u.FirstName)
                .HasMaxLength(50)
                .IsRequired();

            builder.Property(u => u.LastName)
                .HasMaxLength(50)
                .IsRequired();

            builder.Property(u => u.Address)
                .HasMaxLength(300)
                .IsRequired();


            builder
                .HasOne(u => u.RentedCar)
                .WithOne(c => c.Tenant)
                .HasForeignKey<Car>(c => c.TenantId)
                .IsRequired(false)
                .OnDelete(DeleteBehavior.SetNull);


        }
    }
}
