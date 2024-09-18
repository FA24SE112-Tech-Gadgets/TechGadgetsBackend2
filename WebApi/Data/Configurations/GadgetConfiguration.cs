using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WebApi.Data.Entities;

namespace WebApi.Data.Configurations;

public class GadgetConfiguration : IEntityTypeConfiguration<Gadget>
{
    public void Configure(EntityTypeBuilder<Gadget> builder)
    {
        builder.ToTable(nameof(Gadget));

        builder.Property(x => x.Id).ValueGeneratedOnAdd();
        builder.Property(x => x.Status).HasConversion<string>();
    }
}