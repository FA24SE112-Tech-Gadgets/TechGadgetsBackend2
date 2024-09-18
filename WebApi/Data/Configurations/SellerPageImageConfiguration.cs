using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WebApi.Data.Entities;

namespace WebApi.Data.Configurations;

public class SellerPageImageConfiguration : IEntityTypeConfiguration<SellerPageImage>
{
    public void Configure(EntityTypeBuilder<SellerPageImage> builder)
    {
        builder.ToTable(nameof(SellerPageImage));

        builder.Property(x => x.Id).ValueGeneratedOnAdd();
    }
}
