using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using FlameRestaurant.Domain.Models.Entities;

namespace FlameRestaurant.Domain.Models.DbContexts.Configurations
{
    public class ReservationEntityTypeConfiguration : IEntityTypeConfiguration<Reservation>
    {
        public void Configure(EntityTypeBuilder<Reservation> builder)
        {
            builder.HasIndex(c => c.Id);

            builder.Property(c => c.Id)
                .UseIdentityColumn(1, 1);
            builder.Property(c => c.Name)
                .HasMaxLength(30)
                .IsRequired();
            builder.Property(c => c.NumberofPeople)
                .HasMaxLength(50)
                .IsRequired();
            builder.Property(c => c.Date)
                .IsRequired();
        }
    }
}
