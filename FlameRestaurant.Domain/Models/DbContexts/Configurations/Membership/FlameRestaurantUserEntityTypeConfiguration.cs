using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using FlameRestaurant.Domain.Models.Entities.Membership;

namespace FlameRestaurant.Domain.Models.DataContexts.Configurations.Membership
{
    public class FlameRestaurantUserEntityTypeConfiguration : IEntityTypeConfiguration<FlameRestaurantUser>
    {
        public void Configure(EntityTypeBuilder<FlameRestaurantUser> builder)
        {
            builder.ToTable("Users", "Membership");
        }
    }
}
