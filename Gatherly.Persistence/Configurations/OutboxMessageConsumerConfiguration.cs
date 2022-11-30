using Gatherly.Persistence.Constants;
using Gatherly.Persistence.Outbox;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace Gatherly.Persistence.Configurations
{
    internal sealed class OutboxMessageConsumerConfiguration : IEntityTypeConfiguration<OutboxMessageConsumer>
    {
        public void Configure(EntityTypeBuilder<OutboxMessageConsumer> builder)
        {
            builder.ToTable(TableNames.OutboxMessageConsumers);

            builder.HasKey(outboxMessageConsumer => new
            {
                outboxMessageConsumer.Id,
                outboxMessageConsumer.Name
            });
        }
    }
}
