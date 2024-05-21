

using Events.Domain.Entities;
using Events.Infrastructure.Abstractions;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace Events.Infrastructure.EntityConfigurations
{
    public class TitleFirstConfigurations : IBaseEntityConfiguration, IEntityTypeConfiguration<TitleFirst>
    {
        public void Configure(EntityTypeBuilder<TitleFirst> builder)
        {
            builder.ToTable("TitleFirst", "Lookups");
            builder.HasKey(a => a.Id);
            builder.Property(a => a.Id).ValueGeneratedOnAdd();
        }
    }
}
