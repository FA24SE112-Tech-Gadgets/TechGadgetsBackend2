using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WebApi.Data.Entities;

namespace WebApi.Data.Configurations;

public class KeywordHistoryConfiguration : IEntityTypeConfiguration<KeywordHistory>
{
    public void Configure(EntityTypeBuilder<KeywordHistory> builder)
    {
        builder.ToTable(nameof(KeywordHistory));

        builder.Property(x => x.Id).ValueGeneratedOnAdd();
    }
}
