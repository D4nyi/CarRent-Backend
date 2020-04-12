using CarRent.Contexts.Models.Core;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CarRent.Contexts.SQLiteContext.ModelConfigurations
{
    internal sealed class PremiseConfiguration : IEntityTypeConfiguration<Premise>
    {
        public void Configure(EntityTypeBuilder<Premise> builder)
        {
            builder.HasKey(p => p.Id);

            builder.Property(p => p.Name)
                .HasMaxLength(50)
                .IsRequired();

            builder.Property(p => p.Address)
                .HasMaxLength(300)
                .IsRequired();

            builder.HasMany(p => p.Cars)
                .WithOne(c => c.Premise)
                .IsRequired(false)
                .OnDelete(DeleteBehavior.SetNull);
        }
    }
}
