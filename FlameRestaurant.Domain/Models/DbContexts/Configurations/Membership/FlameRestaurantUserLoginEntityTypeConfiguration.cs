using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using FlameRestaurant.Domain.Models.Entities.Membership;

namespace FlameRestaurant.Domain.Models.DataContexts.Configurations.Membership
{
    public class FlameRestaurantUserLoginEntityTypeConfiguration : IEntityTypeConfiguration<FlameRestaurantUserLogin>
    {
        public void Configure(EntityTypeBuilder<FlameRestaurantUserLogin> builder)
        {
            builder.ToTable("UserLogins", "Membership");
        }
    }
}
