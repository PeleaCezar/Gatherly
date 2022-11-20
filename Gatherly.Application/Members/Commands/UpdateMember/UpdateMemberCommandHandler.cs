using Gatherly.Application.Abstractions.Messaging;
using Gatherly.Domain.Entities;
using Gatherly.Domain.Errors;
using Gatherly.Domain.Repositories;
using Gatherly.Domain.Shared;
using Gatherly.Domain.ValueObjects;

namespace Gatherly.Application.Members.Commands.UpdateMember
{
    internal sealed class UpdateMemberCommandHandler : ICommandHandler<UpdateMemberCommand>
    {
        private readonly IMemberRepository _memberRepository;
        private readonly IUnitOfWork _unitOfWork;

        public UpdateMemberCommandHandler(
            IMemberRepository memberRepository,
            IUnitOfWork unitOfWork)
        {
            _memberRepository = memberRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<Result> Handle(
            UpdateMemberCommand request,
            CancellationToken cancellationToken)
        {
            Member member = await _memberRepository.GetByIdAsync(
                request.MemberId,
                cancellationToken);

            if (member is null)
            {
                return Result.Failure(
                    DomainErrors.Member.NotFound(request.MemberId));
            }

            Result<FirstName> firstNameResult = FirstName.Create(request.FirstName);
            Result<LastName> lastNameResult = LastName.Create(request.LastName);

            member.ChangeName(
                firstNameResult.Value,
                lastNameResult.Value);

            _memberRepository.Update(member);

            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return Result.Success();
        }
    }
}
