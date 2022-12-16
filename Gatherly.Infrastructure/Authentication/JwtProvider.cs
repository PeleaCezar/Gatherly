using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Gatherly.Application.Abstractions;
using Gatherly.Domain.Entities;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace Gatherly.Infrastructure.Authentication
{
    internal sealed class JwtProvider : IJwtProvider
    {
        private readonly JwtOptions _options;

        public JwtProvider(IOptions<JwtOptions> options)
        {  
            _options = options.Value;
        }

        public string Generate(Member member)
        {
            var claims = new Claim[]
            {
                new(JwtRegisteredClaimNames.Sub, member.Id.ToString()),
                new(JwtRegisteredClaimNames.Email, member.Email.Value)
            };

            var signingCredentials = new SigningCredentials(
                 new SymmetricSecurityKey(
                     Encoding.UTF8.GetBytes(_options.SecretKey)),
                 SecurityAlgorithms.HmacSha256);


            var token = new JwtSecurityToken(
                _options.Issuer,
                _options.Audience,
                claims,
                null,
                DateTime.UtcNow.AddHours(1),
                signingCredentials);

            string tokenValue = new JwtSecurityTokenHandler()
                 .WriteToken(token);

            return tokenValue;
        }
    }
}
