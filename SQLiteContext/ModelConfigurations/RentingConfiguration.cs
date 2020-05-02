using CarRent.Contexts.Models.Core;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;


namespace CarRent.Contexts.SQLiteContext.ModelConfigurations
{
    internal sealed class RentingConfiguration : IEntityTypeConfiguration<Renting>
    {
        public void Configure(EntityTypeBuilder<Renting> builder)
        {
            builder.HasKey(r => r.Id);

            builder.Property(r => r.ReturnDate)
                .IsRequired();

            builder.Property(r => r.PickupDate)
                .IsRequired();

            builder.Property(r => r.Rented)
                .IsRequired();

            builder.Property(r => r.State)
                .IsRequired();

            builder
                .HasOne(r => r.Car)
                .WithOne(c => c.Renting)
                .HasForeignKey<Car>(c => c.RentingId)
                .IsRequired(false)
                .OnDelete(DeleteBehavior.SetNull);

            builder
                .HasOne(r => r.User)
                .WithOne(u => u.Renting)
                .HasForeignKey<User>(u => u.RentingId)
                .IsRequired(false)
                .OnDelete(DeleteBehavior.SetNull);
        }
    }
}
