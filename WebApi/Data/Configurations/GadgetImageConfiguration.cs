using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WebApi.Data.Entities;

namespace WebApi.Data.Configurations;

public class GadgetImageConfiguration : IEntityTypeConfiguration<GadgetImage>
{
    public void Configure(EntityTypeBuilder<GadgetImage> builder)
    {
        builder.ToTable(nameof(GadgetImage));

        builder.Property(x => x.Id).ValueGeneratedOnAdd();
    }
}