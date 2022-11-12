using Gatherly.Application.Abstractions;
using Gatherly.Domain.Entities;

namespace Gatherly.Infrastructure.Services
{
    internal sealed class EmailService : IEmailService
    {
        public Task SendWelcomeEmailAsync(Member member, CancellationToken cancellationToken = default) =>
            Task.CompletedTask;

        public Task SendInvitationSentEmailAsync(Member member, Gathering gathering, CancellationToken cancellationToken = default) =>
            Task.CompletedTask;

        public Task SendInvitationAcceptedEmailAsync(Gathering gathering, CancellationToken cancellationToken = default) =>
            Task.CompletedTask;
    }
}
