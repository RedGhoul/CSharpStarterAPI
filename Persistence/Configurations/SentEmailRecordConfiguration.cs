using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.Configurations
{
    public class SentEmailRecordConfiguration : IEntityTypeConfiguration<SentEmailRecord>
    {
        public void Configure(EntityTypeBuilder<SentEmailRecord> builder)
        {
            builder.HasIndex(x => x.CreatedAt);
            builder.HasIndex(x => x.Recipient);
        }
    }
}
