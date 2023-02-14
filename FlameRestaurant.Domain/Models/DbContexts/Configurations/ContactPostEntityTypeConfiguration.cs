using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using FlameRestaurant.Domain.Models.Entities;

namespace FlameRestaurant.Domain.Models.DbContexts.Configurations
{
    public class ContactPostEntityTypeConfiguration : IEntityTypeConfiguration<ContactPost>
    {
        public void Configure(EntityTypeBuilder<ContactPost> builder)
        {
            builder.HasIndex(c => c.Id);

            builder.Property(c => c.Id)
                .UseIdentityColumn(1, 1);
            builder.Property(c => c.Name)
                .IsRequired();
            builder.Property(c => c.Email)
                .IsRequired();
            builder.Property(c => c.Message)
                .IsRequired();
        }
    }
}
