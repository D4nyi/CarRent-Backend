using CarRent.Contexts.Models.Core;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CarRent.Contexts.SQLiteContext.ModelConfigurations
{
    internal sealed class CarConfiguration : IEntityTypeConfiguration<Car>
    {
        public void Configure(EntityTypeBuilder<Car> builder)
        {
            builder.HasKey(c => c.Id);

            builder.Property(c => c.Colour)
                .HasConversion<int>()
                .HasDefaultValue(Colour.None)
                .IsRequired();

            builder.Property(c => c.Brand)
                .HasMaxLength(60)
                .IsRequired();

            builder.Property(c => c.Model)
                .HasMaxLength(60)
                .IsRequired();

            builder.Property(c => c.Brand)
                .HasMaxLength(60)
                .IsRequired();

            builder.Property(c => c.LicensePlate)
                .HasMaxLength(10)
                .HasDefaultValue("AAA-000")
                .IsRequired();

            builder.Property(c => c.Mileage)
                .HasDefaultValue(default(double))
                .IsRequired();

            builder.Property(c => c.EngineDescription)
                .HasMaxLength(200)
                .IsRequired();

            builder
                .HasOne(c => c.Renting)
                .WithOne(u => u.Car)
                .HasForeignKey<Renting>(u => u.CarId)
                .IsRequired(false);

            builder
                .HasOne(c => c.Premise)
                .WithMany(p => p.Cars)
                .HasForeignKey(u => u.PremiseId)
                .IsRequired(false)
                .OnDelete(DeleteBehavior.SetNull);
        }
    }
}
