

using Events.Domain.Entities;
using Events.Infrastructure.Abstractions;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace Events.Infrastructure.EntityConfigurations
{
    public class EventPlaceConfigurations : IBaseEntityConfiguration, IEntityTypeConfiguration<EventPlace>
    {
        public void Configure(EntityTypeBuilder<EventPlace> builder)
        {
            builder.ToTable("EventPlaces", "Event");
            builder.HasKey(a => a.Id);
            builder.Property(a => a.Id).ValueGeneratedOnAdd();


        }
    }
}
