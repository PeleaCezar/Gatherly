using Gatherly.Domain.Entities;

namespace Gatherly.Application.Abstractions
{
    public interface IJwtProvider
    {
        Task<string> GenerateAsync(Member member);
    }
}
