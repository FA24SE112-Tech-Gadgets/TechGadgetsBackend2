using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WebApi.Data.Entities;

namespace WebApi.Data.Configurations;

public class BillingMailApplicationConfiguration : IEntityTypeConfiguration<BillingMailApplication>
{
    public void Configure(EntityTypeBuilder<BillingMailApplication> builder)
    {
        builder.ToTable(nameof(BillingMailApplication));

        builder.Property(x => x.Id).ValueGeneratedOnAdd();
    }
}
