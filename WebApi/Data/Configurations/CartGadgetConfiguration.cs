using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WebApi.Data.Entities;

namespace WebApi.Data.Configurations;

public class CartGadgetConfiguration : IEntityTypeConfiguration<CartGadget>
{
    public void Configure(EntityTypeBuilder<CartGadget> builder)
    {
        builder.ToTable(nameof(CartGadget));

        builder.HasKey(x => new { x.CartId, x.GadgetId });
    }
}