using Gatherly.Application.Abstractions.Messaging;
using Gatherly.Application.Abstractions;
using Gatherly.Domain.DomainEvents;
using Gatherly.Domain.Repositories;
using Gatherly.Domain.Entities;

namespace Gatherly.Application.Members.Events
{
    internal sealed class MemberRegisteredDomainEventHandler
         : IDomainEventHandler<MemberRegisteredDomainEvent>
    {
        private readonly IMemberRepository _memberRepository;
        private readonly IEmailService _emailService;

        public MemberRegisteredDomainEventHandler(
            IMemberRepository memberRepository,
            IEmailService emailService)
        {
            _memberRepository = memberRepository;
            _emailService = emailService;
        }

        public async Task Handle(
            MemberRegisteredDomainEvent notification,
            CancellationToken cancellationToken)
        {
            Member member = await _memberRepository.GetByIdAsync(
                notification.MemberId,
                cancellationToken);

            if (member is null)
            {
                return;
            }

            await _emailService.SendWelcomeEmailAsync(member, cancellationToken);
        }
    }
}
