using Gatherly.Application.Abstractions;
using Gatherly.Application.Abstractions.Messaging;
using Gatherly.Domain.Entities;
using Gatherly.Domain.Errors;
using Gatherly.Domain.Repositories;
using Gatherly.Domain.Shared;
using Gatherly.Domain.ValueObjects;

namespace Gatherly.Application.Members.Commands.Login
{
    internal sealed class LoginCommandHandler
        : ICommandHandler<LoginCommand, string>
    {
        private readonly IMemberRepository _memberRepository;
        private readonly IJwtProvider _jwtProvider;

        public LoginCommandHandler(IMemberRepository memberRepository,
            IJwtProvider jwtProvider)
        {
            _memberRepository = memberRepository;
            _jwtProvider = jwtProvider;
        }
        public async  Task<Result<string>> Handle(LoginCommand request, CancellationToken cancellationToken)
        {
            Result<Email> email = Email.Create(request.Email);

            Member member = await _memberRepository.GetByEmailAsync(
                email.Value,
                cancellationToken);

            if(member is null)
            {
                return Result.Failure<string>(
                    DomainErrors.Member.InvalidCredentials);
            }

            string token = _jwtProvider.Generate(member);

            return token;
        }
    }
}
