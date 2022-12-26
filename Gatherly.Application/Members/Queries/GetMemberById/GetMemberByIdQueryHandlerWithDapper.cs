using Gatherly.Application.Abstractions.Messaging;
using Gatherly.Domain.Errors;
using Gatherly.Domain.Repositories;
using Gatherly.Domain.Shared;

namespace Gatherly.Application.Members.Queries.GetMemberById
{
    internal sealed class GetMemberByIdQueryHandlerWithDapper
      : IQueryHandler<GetMemberByIdQuery, MemberResponse>
    {
        private readonly IMemberRepository _memberRepository;

        public GetMemberByIdQueryHandlerWithDapper(IMemberRepository memberRepository)
        {
            _memberRepository = memberRepository;
        }

        public async Task<Result<MemberResponse>> Handle(
            GetMemberByIdQuery request,
            CancellationToken cancellationToken)
        {

            var member = await _memberRepository.GetByIdWithDapperAsync(
                 request.MemberId,
                 cancellationToken);

            if (member is null)
            {
                return Result.Failure<MemberResponse>(
                          DomainErrors.Member.NotFound(request.MemberId));
            }

            var response = new MemberResponse(member.Id, member.Email.Value, member.FirstName.Value, member.LastName.Value);

            return response;
        }
    }
}
