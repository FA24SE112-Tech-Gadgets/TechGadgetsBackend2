using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WebApi.Data.Entities;

namespace WebApi.Data.Configurations;

public class CartConfiguration : IEntityTypeConfiguration<Cart>
{
    public void Configure(EntityTypeBuilder<Cart> builder)
    {
        builder.ToTable(nameof(Cart));

        builder.Property(x => x.Id).ValueGeneratedOnAdd();

        builder.HasIndex(x => x.UserId).IsUnique();
    }
}