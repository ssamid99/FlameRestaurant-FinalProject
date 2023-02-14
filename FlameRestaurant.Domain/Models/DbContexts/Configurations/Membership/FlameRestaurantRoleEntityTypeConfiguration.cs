using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using FlameRestaurant.Domain.Models.Entities.Membership;

namespace FlameRestaurant.Domain.Models.DataContexts.Configurations.Membership
{
    public class FlameRestaurantRoleEntityTypeConfiguration : IEntityTypeConfiguration<FlameRestaurantRole>
    {
        public void Configure(EntityTypeBuilder<FlameRestaurantRole> builder)
        {
            builder.ToTable("Roles", "Membership");
        }
    }
}
