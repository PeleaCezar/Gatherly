using Gatherly.Application.Abstractions;
using Gatherly.Domain.Entities;
using Gatherly.Domain.Repositories;
using Gatherly.Domain.Shared;
using MediatR;

namespace Gatherly.Application.Invitations.Commands.SendInvitation
{
    internal class SendInvitationCommandHandler
    {
        private readonly IMemberRepository _memberRepository;
        private readonly IGatheringRepository _gatheringRepository;
        private readonly IInvitationRepository _invitationRepository;
        private readonly IEmailService _emailService;
        private readonly IUnitOfWork _unitOfWork;
        public SendInvitationCommandHandler(
            IMemberRepository memberRepository,
            IGatheringRepository gatheringRepository,
            IInvitationRepository invitationRepository,
            IEmailService emailService,
            IUnitOfWork unitOfWork)
        {
            _memberRepository = memberRepository;
            _gatheringRepository = gatheringRepository;
            _invitationRepository = invitationRepository;
            _emailService = emailService;
            _unitOfWork = unitOfWork;
        }
        public async Task<Unit> Handle(SendInvitationCommand request, CancellationToken cancellationToken)
        {
            var member = await _memberRepository
                .GetByIdAsync(request.MemberId, cancellationToken);

            var gathering = await _gatheringRepository
                .GetByIdWithCreatorAsync(request.MemberId, cancellationToken);

            if (member is null || gathering is null)
            {
                return Unit.Value;
            }

            Result<Invitation> invitationResult = gathering.SendInvitation(member);

            if (invitationResult.IsFailure)
            {
                //Log error
                return Unit.Value;
            }

            _invitationRepository.Add(invitationResult.Value);

            await _unitOfWork.SaveChangesAsync(cancellationToken);

            await _emailService.SendInvitationSentEmailAsync(
                member,
                gathering,
                cancellationToken);

            return Unit.Value;
        }
    }
}
