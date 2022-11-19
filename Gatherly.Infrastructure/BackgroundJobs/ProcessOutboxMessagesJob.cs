using Gatherly.Domain.Primitives;
using Gatherly.Persistence;
using Gatherly.Persistence.Outbox;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using Polly;
using Polly.Retry;
using Quartz;

namespace Gatherly.Infrastructure.BackgroundJobs
{
    [DisallowConcurrentExecution]
    public class ProcessOutboxMessagesJob : IJob
    {
        private readonly ApplicationDbContext _dbContext;
        private readonly IPublisher _publisher;

        public ProcessOutboxMessagesJob(ApplicationDbContext dbContext, IPublisher publisher)
        {
            _dbContext = dbContext;
            _publisher = publisher;
        }
        public async Task Execute(IJobExecutionContext context)
        {
            var messages =  await _dbContext
                .Set<OutboxMessage>()
                .Where(m => m.ProcessedOnUtc == null)
                .Take(20)
                .ToListAsync(context.CancellationToken);

            foreach (OutboxMessage outboxMessage in messages)
            {
                var domainEvent = JsonConvert
                    .DeserializeObject<IDomainEvent>(outboxMessage.Content);

                if(domainEvent is null)
                {
                    continue;
                }

                AsyncRetryPolicy policy = Policy
                    .Handle<Exception>()
                    .WaitAndRetryAsync(3, attempt => TimeSpan.FromMilliseconds(50 * attempt));

                PolicyResult result = await policy.ExecuteAndCaptureAsync(() =>

                 _publisher.Publish(
                     domainEvent,
                     context.CancellationToken));

                outboxMessage.Error = result.FinalException?.ToString();
                outboxMessage.ProcessedOnUtc = DateTime.UtcNow;
            }

            await _dbContext.SaveChangesAsync();
        }
    }
}
