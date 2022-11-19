using Gatherly.Application.Abstractions.Messaging;
using Gatherly.Domain.Primitives;
using Gatherly.Persistence;
using Gatherly.Persistence.Outbox;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Gatherly.Infrastructure.Idempotence
{
    public class IdempotentDomainEventHandler<TDomainEvent> : IDomainEventHandler<TDomainEvent>
        where TDomainEvent : IDomainEvent
    {
        private readonly INotificationHandler<TDomainEvent> _decorated;
        private readonly ApplicationDbContext _dbContext;

        public IdempotentDomainEventHandler(INotificationHandler<TDomainEvent> decorated, 
            ApplicationDbContext dbContext)
        {
            _decorated = decorated;
            _dbContext = dbContext;
        }
           
    
        public async Task Handle(TDomainEvent notification, CancellationToken cancellationToken)
        {
            string consumer = _decorated.GetType().Name;

            // this query checks if is there a record in the database for this domain event Id 
            if  (await _dbContext.Set<OutboxMessageConsumer>()
                .AnyAsync(o => o.Id == notification.Id &&
                               o.Name == consumer))
            {
                return;
            }

            await _decorated.Handle(notification, cancellationToken);

            _dbContext.Set<OutboxMessageConsumer>()
                .Add(new OutboxMessageConsumer
                {
                    Id = notification.Id,
                    Name = consumer
                });

            await _dbContext.SaveChangesAsync(cancellationToken);
        }
    }
}
