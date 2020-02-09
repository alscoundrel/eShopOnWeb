using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.eShopWeb.ApplicationCore.Entities.WishListAggregate;

namespace Microsoft.eShopWeb.Infrastructure.Data.Config
{
    public class WishItemConfiguration : IEntityTypeConfiguration<WishItem>
    {
        public void Configure(EntityTypeBuilder<WishItem> builder)
        {
            builder.Property(bi => bi.UnitPrice)
                .IsRequired(true)
                .HasColumnType("decimal(18,2)");
        }
    }
}
