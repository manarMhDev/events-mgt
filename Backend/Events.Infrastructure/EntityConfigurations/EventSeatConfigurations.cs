

using Events.Domain.Entities;
using Events.Infrastructure.Abstractions;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace Events.Infrastructure.EntityConfigurations
{
    public class EventSeatConfigurations : IBaseEntityConfiguration, IEntityTypeConfiguration<EventSeat>
    {
        public void Configure(EntityTypeBuilder<EventSeat> builder)
        {
            builder.ToTable("EventSeats", "Event");
            builder.HasKey(a => a.Id);
            builder.Property(a => a.Id).ValueGeneratedOnAdd();

            builder.HasOne(a => a.Invitation)
             .WithOne(c => c.EventSeat)
             .HasForeignKey<EventSeat>(a => a.InvitationId)
             .IsRequired(false)
             .OnDelete(DeleteBehavior.NoAction);

            builder.HasOne(a => a.PlaceSeat)
            .WithMany(c => c.EventSeats)
            .HasForeignKey(a => a.PlaceSeatId)
            .IsRequired()
            .OnDelete(DeleteBehavior.NoAction);
        }
    }
}
