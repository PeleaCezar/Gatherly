using Gatherly.Domain.Entities;
using Gatherly.Persistence.Constants;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Gatherly.Persistence.Configurations
{
    internal sealed class GatheringConfiguration : IEntityTypeConfiguration<Gathering>
    {
        public void Configure(EntityTypeBuilder<Gathering> builder)
        {
            builder.ToTable(TableNames.Gatherings);

            builder.HasKey(x => x.Id);

            builder.HasQueryFilter(x => !x.Cancelled);

            builder
                .HasOne(x => x.Creator)
                .WithMany();

            builder
                .HasMany(x => x.Invitations)
                .WithOne()
                .HasForeignKey(x => x.GatheringId);

            builder
                .HasMany(x => x.Attendees)
                .WithOne()
                .HasForeignKey(x => x.GatheringId);
        }
    }
}
