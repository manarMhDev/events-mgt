

using Events.Domain.Entities;
using Events.Infrastructure.Abstractions;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace Events.Infrastructure.EntityConfigurations
{
    public class EventConfigurations : IBaseEntityConfiguration, IEntityTypeConfiguration<Event>
    {
        public void Configure(EntityTypeBuilder<Event> builder)
        {
            builder.ToTable("Events", "Event");
            builder.HasKey(a => a.Id);
            builder.Property(a => a.Id).ValueGeneratedOnAdd();
        }
    }
}
