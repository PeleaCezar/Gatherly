using Gatherly.Application.Abstractions.Messaging;
using Gatherly.Application.Members.Queries.GetMemberById;
using Gatherly.Domain.Errors;
using Gatherly.Domain.Repositories;
using Gatherly.Domain.Shared;

namespace Gatherly.Application.Members.Queries.GetMembers
{
    internal sealed class GetMembersQueryHandler
        : IQueryHandler<GetMembersQuery, List<MemberResponse>>
    {
        private readonly IMemberRepository _memberRepository;

        public GetMembersQueryHandler(IMemberRepository memberRepository)
        {
            _memberRepository = memberRepository;
        }

        public async Task<Result<List<MemberResponse>>> Handle(GetMembersQuery request, CancellationToken cancellationToken)
        {
            var members = await _memberRepository.GetMembersAsync(cancellationToken);

            if (members.Count == 0)
            {
                return Result.Failure<List<MemberResponse>>(
                          DomainErrors.Member.NotExist);
            }

            var memberResponses = members.
                Select(p => new MemberResponse(
                    p.Id,
                    p.Email.Value,
                    p.FirstName.Value,
                    p.LastName.Value))
                .OrderBy(p => p.Id)
                .Skip((request.Page - 1) * request.PageSize) //100ms, but works with guid and int
                .ToList();


            //var memberResponses =  members
            //     .Select(m => new MemberResponse(
            //         m.Id,
            //         m.Email.Value,
            //         m.FirstName.Value,
            //         m.LastName.Value))
            //     .OrderBy(m => m.Id)
            //     .Where(p => p.Id >= request.Cursor) //use Cursor instead Page when we want to improve time of response. (50 ms)
            //     .Take(request.PageSize)            // also .. we should have int instead instead guid at PK in database..
            //     .ToList();

            return memberResponses;
        }
    }
}

