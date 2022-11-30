using Gatherly.Persistence.Constants;
using Gatherly.Domain.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace Gatherly.Persistence.Configurations
{
    internal sealed class InvitationConfiguration : IEntityTypeConfiguration<Invitation>
    {
        public void Configure(EntityTypeBuilder<Invitation> builder)
        {
            builder.ToTable(TableNames.Invitations);

            builder.HasKey(x => x.Id);

            builder
                .HasOne<Member>()
                .WithMany()
                .HasForeignKey(x => x.MemberId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
