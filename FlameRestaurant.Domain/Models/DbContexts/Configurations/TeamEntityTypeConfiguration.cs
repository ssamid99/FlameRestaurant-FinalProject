using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using FlameRestaurant.Domain.Models.Entities;

namespace FlameRestaurant.Domain.Models.DbContexts.Configurations.Abouts
{
    public class TeamEntityTypeConfiguration : IEntityTypeConfiguration<Team>
    {
        public void Configure(EntityTypeBuilder<Team> builder)
        {
            builder.HasIndex(c => c.Id);

            builder.Property(c => c.Id)
                .UseIdentityColumn(1, 1);
            builder.Property(c => c.Name)
                .IsRequired();
            builder.Property(c => c.Surname)
                .IsRequired();
            builder.Property(c => c.Text)
                .IsRequired();
            builder.Property(c => c.ImagePath)
                .IsRequired();
        }
    }
}
