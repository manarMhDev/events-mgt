

using Events.Domain.Entities;
using Events.Infrastructure.Abstractions;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace Events.Infrastructure.EntityConfigurations
{
    public class PlaceSeatConfigurations : IBaseEntityConfiguration, IEntityTypeConfiguration<PlaceSeat>
    {
        public void Configure(EntityTypeBuilder<PlaceSeat> builder)
        {
            builder.ToTable("PlaceSeats", "Event");
            builder.HasKey(a => a.Id);
            builder.Property(a => a.Id).ValueGeneratedOnAdd();


            builder.HasOne(a => a.EventPlace)
                .WithMany(c => c.PlaceSeats)
                .HasForeignKey(a => a.EventPlaceId)
                .IsRequired()
                .OnDelete(DeleteBehavior.NoAction);


            builder.HasOne(a => a.SeatType)
                .WithMany(c => c.PlaceSeats)
                .HasForeignKey(a => a.SeatTypeId)
                .IsRequired()
                .OnDelete(DeleteBehavior.NoAction);
        }
    }
}
