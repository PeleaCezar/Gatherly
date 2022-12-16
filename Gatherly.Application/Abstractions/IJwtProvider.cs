using Gatherly.Domain.Entities;

namespace Gatherly.Application.Abstractions
{
    public interface IJwtProvider
    {
        string Generate(Member member);
    }
}
