using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WebApi.Data.Entities;

namespace WebApi.Data.Configurations;

public class BusinessModelConfiguration : IEntityTypeConfiguration<BusinessModel>
{
    public void Configure(EntityTypeBuilder<BusinessModel> builder)
    {
        builder.ToTable(nameof(BusinessModel));

        builder.Property(x => x.Id).ValueGeneratedOnAdd();
    }
}
