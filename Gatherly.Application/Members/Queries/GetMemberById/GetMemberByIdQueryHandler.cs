using Gatherly.Application.Abstractions.Messaging;
using Gatherly.Domain.Errors;
using Gatherly.Domain.Repositories;
using Gatherly.Domain.Shared;

namespace Gatherly.Application.Members.Queries.GetMemberById
{
    internal sealed class GetMemberByIdQueryHandler
        : IQueryHandler<GetMemberByIdQuery,MemberResponse>
    {
        private readonly IMemberRepository _memberRepository;

        public GetMemberByIdQueryHandler(IMemberRepository memberRepository)
        {
            _memberRepository = memberRepository;
        }

        public async Task<Result<MemberResponse>> Handle
            (GetMemberByIdQuery request,
            CancellationToken cancellationToken)
        {
            var member = await _memberRepository.GetByIdAsync(
                request.MemberId,
                cancellationToken);

            if(member is null)
            {
                return Result.Failure<MemberResponse>(
                          DomainErrors.Member.NotFound(request.MemberId));
            }

            var response = new MemberResponse(member.Id, member.Email.Value);

            return response;
        }
    }
}
