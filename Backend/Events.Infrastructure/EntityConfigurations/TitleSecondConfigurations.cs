

using Events.Domain.Entities;
using Events.Infrastructure.Abstractions;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace Events.Infrastructure.EntityConfigurations
{
    public class TitleSecondConfigurations : IBaseEntityConfiguration, IEntityTypeConfiguration<TitleSecond>
    {
        public void Configure(EntityTypeBuilder<TitleSecond> builder)
        {
            builder.ToTable("TitleSecond", "Lookups");
            builder.HasKey(a => a.Id);
            builder.Property(a => a.Id).ValueGeneratedOnAdd();
        }
    }
}
