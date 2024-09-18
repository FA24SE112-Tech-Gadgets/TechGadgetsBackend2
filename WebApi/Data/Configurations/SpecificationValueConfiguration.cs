using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WebApi.Data.Entities;

namespace WebApi.Data.Configurations;

public class SpecificationValueConfiguration : IEntityTypeConfiguration<SpecificationValue>
{
    public void Configure(EntityTypeBuilder<SpecificationValue> builder)
    {
        builder.ToTable(nameof(SpecificationValue));

        builder.Property(x => x.Id).ValueGeneratedOnAdd();
    }
}
