using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WebApi.Data.Entities;

namespace WebApi.Data.Configurations;

public class DiscountTypeConfiguration : IEntityTypeConfiguration<DiscountType>
{
    public void Configure(EntityTypeBuilder<DiscountType> builder)
    {
        builder.ToTable(nameof(DiscountType));

        builder.Property(x => x.Id).ValueGeneratedOnAdd();
    }
}
