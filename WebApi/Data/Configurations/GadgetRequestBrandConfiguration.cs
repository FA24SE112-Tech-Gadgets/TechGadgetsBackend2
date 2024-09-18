using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WebApi.Data.Entities;

namespace WebApi.Data.Configurations;

public class GadgetRequestBrandConfiguration : IEntityTypeConfiguration<GadgetRequestBrand>
{
    public void Configure(EntityTypeBuilder<GadgetRequestBrand> builder)
    {
        builder.ToTable(nameof(GadgetRequestBrand));

        builder.Property(x => x.Id).ValueGeneratedOnAdd();
    }
}
