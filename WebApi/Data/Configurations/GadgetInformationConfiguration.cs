using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WebApi.Data.Entities;

namespace WebApi.Data.Configurations;

public class GadgetInformationConfiguration : IEntityTypeConfiguration<GadgetInformation>
{
    public void Configure(EntityTypeBuilder<GadgetInformation> builder)
    {
        builder.ToTable(nameof(GadgetInformation));

        builder.Property(x => x.Id).ValueGeneratedOnAdd();
    }
}
