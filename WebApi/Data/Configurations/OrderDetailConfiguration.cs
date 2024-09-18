using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WebApi.Data.Entities;

namespace WebApi.Data.Configurations;

public class OrderDetailConfiguration : IEntityTypeConfiguration<OrderDetail>
{
    public void Configure(EntityTypeBuilder<OrderDetail> builder)
    {
        builder.ToTable(nameof(OrderDetail));

        builder.Property(x => x.Id).ValueGeneratedOnAdd();
        builder.Property(x => x.Status).HasConversion<string>();
    }
}
