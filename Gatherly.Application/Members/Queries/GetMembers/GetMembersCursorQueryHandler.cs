using Gatherly.Application.Abstractions.Messaging;
using Gatherly.Application.Members.Queries.GetMemberById;
using Gatherly.Domain.Errors;
using Gatherly.Domain.Repositories;
using Gatherly.Domain.Shared;

namespace Gatherly.Application.Members.Queries.GetMembers
{
    internal sealed class GetMembersCursorQueryHandler
        : IQueryHandler<GetMembersCursorQuery, CursorResponse<List<MemberResponse>>>
    {
        private readonly IMemberRepository _memberRepository;

        public GetMembersCursorQueryHandler(IMemberRepository memberRepository)
        {
            _memberRepository = memberRepository;
        }

        public async Task<Result<CursorResponse<List<MemberResponse>>>> Handle(GetMembersCursorQuery request, CancellationToken cancellationToken)
        {
            var members = await _memberRepository.GetMembersAsync(cancellationToken);

            if (members.Count == 0)
            {
                return Result.Failure<CursorResponse<List<MemberResponse>>>(
                          DomainErrors.Member.NotExist);
            }

            //var memberResp = members
            //     .Where(p => p.Id >= request.Cursor) // use Cursor instead Page when we want to improve time of response. (50 ms)
            //     .Take(request.PageSize + 1)        // also .. we should have int instead instead guid at PK in database..
            //     .OrderBy(m => m.Id)
            //     .Select(m => new MemberResponse(
            //         m.Id,
            //         m.Email.Value,
            //         m.FirstName.Value,
            //         m.LastName.Value))
            //     .ToList();

            // long cursor = memberResp[^1].Id;

            // List<MemberResponse> memberResponses = memberResp.Take(request.PageSize).ToList();

            // return new CursorResponse<List<MemberResponse>>(cursor, memberResponses);
            return null;
        }
    }
}

