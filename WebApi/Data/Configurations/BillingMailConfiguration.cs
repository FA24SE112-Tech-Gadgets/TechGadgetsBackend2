using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WebApi.Data.Entities;

namespace WebApi.Data.Configurations;

public class BillingMailConfiguration : IEntityTypeConfiguration<BillingMail>
{
    public void Configure(EntityTypeBuilder<BillingMail> builder)
    {
        builder.ToTable(nameof(BillingMail));

        builder.Property(x => x.Id).ValueGeneratedOnAdd();
    }
}
