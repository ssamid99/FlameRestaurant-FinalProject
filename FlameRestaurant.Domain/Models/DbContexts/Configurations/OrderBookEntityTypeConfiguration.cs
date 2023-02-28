using FlameRestaurant.Domain.Models.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FlameRestaurant.Domain.Models.DataContexts.Configurations
{
    public class OrderBookEntityTypeConfiguration : IEntityTypeConfiguration<OrderProduct>
    {
        public void Configure(EntityTypeBuilder<OrderProduct> builder)
        {
            builder.HasKey(entity => entity.Id);

            builder.Property(entity => entity.Id)
                .UseIdentityColumn();

            builder.Property(entity => entity.Quantity)
               .IsRequired();
        }
    }
}
