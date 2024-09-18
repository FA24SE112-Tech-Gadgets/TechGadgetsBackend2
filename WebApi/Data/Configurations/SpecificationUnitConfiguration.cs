using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WebApi.Data.Entities;

namespace WebApi.Data.Configurations;

public class SpecificationUnitConfiguration : IEntityTypeConfiguration<SpecificationUnit>
{
    public void Configure(EntityTypeBuilder<SpecificationUnit> builder)
    {
        builder.ToTable(nameof(SpecificationUnit));

        builder.Property(x => x.Id).ValueGeneratedOnAdd();
    }
}
