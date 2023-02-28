using FlameRestaurant.Domain.Models.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FlameRestaurant.Domain.Models.DataContexts.Configurations
{
    public class ProductTagItemEntityTypeConfiguration : IEntityTypeConfiguration<ProductTagItem>
    {
        public void Configure(EntityTypeBuilder<ProductTagItem> builder)
        {
            builder.HasKey(entity => new { entity.ProductId, entity.TagId });

            builder.Property(entity => entity.Id)
                .UseIdentityColumn();

            builder.ToTable("ProductTagCloud");
        }
    }
}
