using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WebApi.Data.Entities;

namespace WebApi.Data.Configurations;

public class GadgetRequestSpecificationConfiguration : IEntityTypeConfiguration<GadgetRequestSpecification>
{
    public void Configure(EntityTypeBuilder<GadgetRequestSpecification> builder)
    {
        builder.ToTable(nameof(GadgetRequestSpecification));

        builder.Property(x => x.Id).ValueGeneratedOnAdd();
    }
}
