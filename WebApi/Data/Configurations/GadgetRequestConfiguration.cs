using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WebApi.Data.Entities;

namespace WebApi.Data.Configurations;

public class GadgetRequestConfiguration : IEntityTypeConfiguration<GadgetRequest>
{
    public void Configure(EntityTypeBuilder<GadgetRequest> builder)
    {
        builder.ToTable(nameof(GadgetRequest));

        builder.Property(x => x.Id).ValueGeneratedOnAdd();
        builder.Property(x => x.Status).HasConversion<string>();
        builder.Property(x => x.Type).HasConversion<string>();
    }
}