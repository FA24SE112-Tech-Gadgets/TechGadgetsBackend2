using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WebApi.Data.Entities;

namespace WebApi.Data.Configurations;

public class VoucherConfiguration : IEntityTypeConfiguration<Voucher>
{
    public void Configure(EntityTypeBuilder<Voucher> builder)
    {
        builder.ToTable(nameof(Voucher));

        builder.Property(x => x.Id).ValueGeneratedOnAdd();
        builder.Property(x => x.Status).HasConversion<string>();
    }
}
