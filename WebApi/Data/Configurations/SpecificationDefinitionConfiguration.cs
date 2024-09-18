using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WebApi.Data.Entities;

namespace WebApi.Data.Configurations;

public class SpecificationDefinitionConfiguration : IEntityTypeConfiguration<SpecificationDefinition>
{
    public void Configure(EntityTypeBuilder<SpecificationDefinition> builder)
    {
        builder.ToTable(nameof(SpecificationDefinition));

        builder.Property(x => x.Id).ValueGeneratedOnAdd();
    }
}
