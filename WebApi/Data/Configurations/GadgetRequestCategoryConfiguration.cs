using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WebApi.Data.Entities;

namespace WebApi.Data.Configurations;

public class GadgetRequestCategoryConfiguration : IEntityTypeConfiguration<GadgetRequestCategory>
{
    public void Configure(EntityTypeBuilder<GadgetRequestCategory> builder)
    {
        builder.ToTable(nameof(GadgetRequestCategory));

        builder.Property(x => x.Id).ValueGeneratedOnAdd();
    }
}
