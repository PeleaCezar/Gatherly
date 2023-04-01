using Gatherly.Application.Abstractions.Messaging;
using Gatherly.Application.Caching;
using Gatherly.Domain.Errors;
using Gatherly.Domain.Repositories;
using Gatherly.Domain.Shared;

namespace Gatherly.Application.Members.Queries.GetMemberById
{
    internal sealed class GetMemberByIdQueryHandlerWithDapper
      : IQueryHandler<GetMemberByIdQuery, MemberResponse>
    {
        private readonly IMemberRepository _memberRepository;
        private readonly ICacheService _cacheService;

        public GetMemberByIdQueryHandlerWithDapper(IMemberRepository memberRepository, ICacheService cacheService)
        {
            _cacheService = cacheService;
            _memberRepository = memberRepository;
        }

        public async Task<Result<MemberResponse>> Handle(
            GetMemberByIdQuery request,
            CancellationToken cancellationToken)
        {
            //normally , we want to cache multiple values, but GetMembers is used with multiple pagination techniques and
            // we use GetMember only for concept presentation.

            //initially, we find member in cache
            var cachedMember = await _cacheService
                .GetAsync<MemberResponse>("member", cancellationToken);

            if (cachedMember is not null)
            {
                return cachedMember;
            }

            var dbMember = await _memberRepository.GetByIdWithDapperAsync(
                 request.MemberId,
                 cancellationToken);

            if (dbMember is null)
            {
                return Result.Failure<MemberResponse>(
                          DomainErrors.Member.NotFound(request.MemberId));
            }

            var response = new MemberResponse(dbMember.Id, dbMember.Email.Value, dbMember.FirstName.Value, dbMember.LastName.Value);

            //after we get member in db, we convert it into MemberResponse object and save it in cache.

            await _cacheService.SetAsync("member", response, cancellationToken);

            return response;


            //or we can use GetAndSetAsync method defined in repo
            //but for one member we should check null response from db 
            //if we use the cache for GetMembers we would not encounter this problem.

            //return await _cacheService.GetAndSetAsync(
            //    "member",
            //    async () =>
            //    {
            //        var member = await _memberRepository.GetByIdWithDapperAsync(
            //             request.MemberId,
            //             cancellationToken);

            //        var response = new MemberResponse(member.Id, member.Email.Value, member.FirstName.Value, member.LastName.Value);

            //        return response;
            //    },
            //    cancellationToken);
        }
    }
}
