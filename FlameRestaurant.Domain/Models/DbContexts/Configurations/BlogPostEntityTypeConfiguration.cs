using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FlameRestaurant.Domain.Models.DataContexts.Configurations.BlogPost
{
    public class BlogPostEntityTypeConfiguration : IEntityTypeConfiguration<Entities.BlogPost>
    {

        public void Configure(EntityTypeBuilder<Entities.BlogPost> builder)
        {
            builder.HasKey(p => p.Id);
            builder.Property(p => p.Title)
                .IsRequired();
            builder.Property(p => p.Body)
                .IsRequired();
            builder.Property(p => p.ImagePath)
                .IsRequired();

            builder.Property(p => p.Slug)
                .IsUnicode(false)
                .HasMaxLength(900)
                .IsRequired();

            builder.HasIndex(p => p.Slug)
                .IsUnique();
        }
    }
}
