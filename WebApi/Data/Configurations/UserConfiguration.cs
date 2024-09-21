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

        builder.HasData(
            new User
            {
                Id = 1,
                FullName = "Admin 1",
                Role = Role.Admin,
                Email = "admin1@gmail.com",
                Status = UserStatus.Active,
                LoginMethod = LoginMethod.Default,
                Password = "5wJ0xMM/o1DPaTby8haqjIeEx0hqnJfyw4SmivYCGT17khWSPTXkR+56laWZr3/U",
                CreatedAt = DateTime.Parse("2023-09-14T05:37:42.345Z", null, System.Globalization.DateTimeStyles.RoundtripKind),
                UpdatedAt = DateTime.Parse("2023-09-14T05:37:42.345Z", null, System.Globalization.DateTimeStyles.RoundtripKind)
            },

            new User
            {
                Id = 2,
                FullName = "Admin 2",
                Role = Role.Admin,
                Email = "admin2@gmail.com",
                Status = UserStatus.Active,
                LoginMethod = LoginMethod.Default,
                Password = "5wJ0xMM/o1DPaTby8haqjIeEx0hqnJfyw4SmivYCGT17khWSPTXkR+56laWZr3/U",
                CreatedAt = DateTime.Parse("2023-09-14T05:37:42.345Z", null, System.Globalization.DateTimeStyles.RoundtripKind),
                UpdatedAt = DateTime.Parse("2023-09-14T05:37:42.345Z", null, System.Globalization.DateTimeStyles.RoundtripKind)
            },

            new User
            {
                Id = 3,
                FullName = "Admin 3",
                Role = Role.Admin,
                Email = "admin3@gmail.com",
                Status = UserStatus.Active,
                LoginMethod = LoginMethod.Default,
                Password = "5wJ0xMM/o1DPaTby8haqjIeEx0hqnJfyw4SmivYCGT17khWSPTXkR+56laWZr3/U",
                CreatedAt = DateTime.Parse("2023-09-14T05:37:42.345Z", null, System.Globalization.DateTimeStyles.RoundtripKind),
                UpdatedAt = DateTime.Parse("2023-09-14T05:37:42.345Z", null, System.Globalization.DateTimeStyles.RoundtripKind)
            }
            );
    }
}

