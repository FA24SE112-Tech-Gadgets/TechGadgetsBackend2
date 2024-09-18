using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WebApi.Data.Entities;

namespace WebApi.Data.Configurations;

public class VoucherTypeConfiguration : IEntityTypeConfiguration<VoucherType>
{
    public void Configure(EntityTypeBuilder<VoucherType> builder)
    {
        builder.ToTable(nameof(VoucherType));

        builder.Property(x => x.Id).ValueGeneratedOnAdd();
    }
}
