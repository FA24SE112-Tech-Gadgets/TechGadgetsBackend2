using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WebApi.Data.Entities;

namespace WebApi.Data.Configurations;

public class BrandCategoryConfiguration : IEntityTypeConfiguration<BrandCategory>
{
    public void Configure(EntityTypeBuilder<BrandCategory> builder)
    {
        builder.ToTable(nameof(BrandCategory));

        builder.HasKey(x => new { x.BrandId, x.CategoryId });
    }
}