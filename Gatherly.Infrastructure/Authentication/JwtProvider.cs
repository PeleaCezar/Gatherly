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
        private readonly IPermissionService _permissionService;

        public JwtProvider(
            IOptions<JwtOptions> options,
            IPermissionService permissionService)
        {  
            _options = options.Value;
            _permissionService = permissionService;

        }

        public async Task<string> GenerateAsync(Member member)
        {
            var claims = new List<Claim>
            {
                new(JwtRegisteredClaimNames.Sub, member.Id.ToString()),
                new(JwtRegisteredClaimNames.Email, member.Email.Value)
            };

            HashSet<string> permissions = await _permissionService
                .GetPermissionsAsync(member.Id);

            foreach(string permission in permissions)
            {
                claims.Add(new(CustomClaims.Permissions, permission));
            }

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
