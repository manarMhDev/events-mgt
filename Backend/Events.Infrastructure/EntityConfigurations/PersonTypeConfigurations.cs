

using Events.Domain.Entities;
using Events.Infrastructure.Abstractions;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace Events.Infrastructure.EntityConfigurations
{
    public class PersonTypeConfigurations : IBaseEntityConfiguration, IEntityTypeConfiguration<PersonType>
    {
        public void Configure(EntityTypeBuilder<PersonType> builder)
        {
            builder.ToTable("PersonTypes", "Lookups");
            builder.HasKey(a => a.Id);
            builder.Property(a => a.Id).ValueGeneratedOnAdd();
        }
    }
}
