using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WebApi.Data.Entities;

namespace WebApi.Data.Configurations;

public class UserConfiguration : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder.ToTable(nameof(User));

        builder.Property(x => x.Id).ValueGeneratedOnAdd();
        builder.Property(x => x.Status).HasConversion<string>();
        builder.Property(x => x.Role).HasConversion<string>();
        builder.Property(x => x.LoginMethod).HasConversion<string>();
    }
}

