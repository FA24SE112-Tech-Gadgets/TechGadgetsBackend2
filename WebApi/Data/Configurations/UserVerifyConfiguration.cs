using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WebApi.Data.Entities;

namespace WebApi.Data.Configurations;

public class UserVerifyConfiguration : IEntityTypeConfiguration<UserVerify>
{
    public void Configure(EntityTypeBuilder<UserVerify> builder)
    {
        builder.ToTable(nameof(UserVerify));

        builder.Property(x => x.Id).ValueGeneratedOnAdd();
        builder.Property(x => x.Status).HasConversion<string>();
    }
}
