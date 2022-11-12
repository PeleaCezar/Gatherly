using Gatherly.Application.Abstractions;
using Gatherly.Application.Abstractions.Messaging;
using Gatherly.Domain.DomainEvents;
using Gatherly.Domain.Repositories;

namespace Gatherly.Application.Members.Events
{
    public class MemberRegisteredDomainEventHandler
         : IDomainEventHandler<MemberRegisteredDomainEvent>
    {
        private readonly IEmailService _emailService;
        private readonly IMemberRepository _memberRepository;

        public MemberRegisteredDomainEventHandler
            (IEmailService emailService,
            IMemberRepository memberRepository)
        {
            _emailService = emailService;
            _memberRepository = memberRepository;
        }
        public async Task Handle(MemberRegisteredDomainEvent notification,
            CancellationToken cancellationToken)
        {
            var member = await _memberRepository
                .GetByIdAsync(notification.MemberId, cancellationToken);

            if (member is null) return;

            await _emailService.SendWelcomeEmailAsync(member, cancellationToken);
        }
    }
}
