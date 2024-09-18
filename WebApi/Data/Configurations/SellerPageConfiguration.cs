using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WebApi.Data.Entities;

namespace WebApi.Data.Configurations;

public class SellerPageConfiguration : IEntityTypeConfiguration<SellerPage>
{
    public void Configure(EntityTypeBuilder<SellerPage> builder)
    {
        builder.ToTable(nameof(SellerPage));

        builder.Property(x => x.Id).ValueGeneratedOnAdd();
    }
}
