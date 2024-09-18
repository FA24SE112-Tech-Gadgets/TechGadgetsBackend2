using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WebApi.Data.Entities;

namespace WebApi.Data.Configurations;

public class VoucherUserConfiguration : IEntityTypeConfiguration<VoucherUser>
{
    public void Configure(EntityTypeBuilder<VoucherUser> builder)
    {
        builder.ToTable(nameof(VoucherUser));

        builder.HasKey(x => new { x.VoucherId, x.UserId });
        builder.Property(x => x.Status).HasConversion<string>();
    }
}
