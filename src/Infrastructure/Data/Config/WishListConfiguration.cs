using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.eShopWeb.ApplicationCore.Entities.WishListAggregate;

namespace Microsoft.eShopWeb.Infrastructure.Data.Config
{
    public class WishListConfiguration : IEntityTypeConfiguration<WishList>
    {
        public void Configure(EntityTypeBuilder<WishList> builder)
        {
            var navigation = builder.Metadata.FindNavigation(nameof(WishList.Items));
            navigation.SetPropertyAccessMode(PropertyAccessMode.Field);

            builder.Property(b => b.WisherId)
                .IsRequired()
                .HasMaxLength(40);
        }
    }
}
