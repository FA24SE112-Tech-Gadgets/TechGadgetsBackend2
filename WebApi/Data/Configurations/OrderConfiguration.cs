using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WebApi.Data.Entities;

namespace WebApi.Data.Configurations;

public class OrderConfiguration : IEntityTypeConfiguration<Order>
{
    public void Configure(EntityTypeBuilder<Order> builder)
    {
        builder.ToTable(nameof(Order));

        builder.Property(x => x.Id).ValueGeneratedOnAdd();
        builder.Property(x => x.PaymentStatus).HasConversion<string>();
        builder.Property(x => x.PaymentMethod).HasConversion<string>();
    }
}
