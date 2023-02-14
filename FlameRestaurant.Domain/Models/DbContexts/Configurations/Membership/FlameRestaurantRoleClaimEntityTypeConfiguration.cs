using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using FlameRestaurant.Domain.Models.Entities.Membership;

namespace FlameRestaurant.Domain.Models.DataContexts.Configurations.Membership
{
    public class FlameRestaurantRoleClaimEntityTypeConfiguration : IEntityTypeConfiguration<FlameRestaurantRoleClaim>
    {
        public void Configure(EntityTypeBuilder<FlameRestaurantRoleClaim> builder)
        {
            builder.ToTable("RoleClaims", "Membership");
        }
    }
}
