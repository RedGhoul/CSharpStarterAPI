using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.Configurations
{
    internal class EventConfiguration : IEntityTypeConfiguration<Event>
    {
        public void Configure(EntityTypeBuilder<Event> builder)
        {
            builder.HasIndex(e => e.CreatedDate);
            builder.HasIndex(e => e.UpdatedOnDate);
            builder.Property(e => e.Description)
                .IsRequired()
                .HasMaxLength(500);

            builder.HasOne(w => w.EventType)
                    .WithMany(w => w.Events)
                    .HasForeignKey(w => w.EventTypeId)
                    .OnDelete(DeleteBehavior.SetNull);

        }
    }
}
