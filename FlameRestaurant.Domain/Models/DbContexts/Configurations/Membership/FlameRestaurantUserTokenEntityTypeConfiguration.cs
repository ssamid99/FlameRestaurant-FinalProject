using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using FlameRestaurant.Domain.Models.Entities.Membership;

namespace FlameRestaurant.Domain.Models.DataContexts.Configurations.Membership
{
    public class FlameRestaurantUserTokenEntityTypeConfiguration : IEntityTypeConfiguration<FlameRestaurantUserToken>
    {
        public void Configure(EntityTypeBuilder<FlameRestaurantUserToken> builder)
        {
            builder.ToTable("UserTokens", "Membership");
        }
    }
}
