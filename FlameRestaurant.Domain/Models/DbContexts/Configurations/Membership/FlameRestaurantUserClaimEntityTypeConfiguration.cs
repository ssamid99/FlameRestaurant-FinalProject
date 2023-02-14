using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using FlameRestaurant.Domain.Models.Entities.Membership;

namespace FlameRestaurant.Domain.Models.DataContexts.Configurations.Membership
{
    public class FlameRestaurantUserClaimEntityTypeConfiguration : IEntityTypeConfiguration<FlameRestaurantUserClaim>
    {
        public void Configure(EntityTypeBuilder<FlameRestaurantUserClaim> builder)
        {
            builder.ToTable("UserClaims", "Membership");
        }
    }
}
