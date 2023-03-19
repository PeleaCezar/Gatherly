using Gatherly.Application.Members.Commands.CreateMember;
using Gatherly.Application.Members.Commands.Login;
using Gatherly.Application.Members.Commands.UpdateMember;
using Gatherly.Application.Members.Queries.GetMemberById;
using Gatherly.Application.Members.Queries.GetMembers;
using Gatherly.Domain.Enums;
using Gatherly.Domain.Shared;
using Gatherly.Infrastructure.Authentication;
using Gatherly.Presentation.Abstractions;
using Gatherly.Presentation.Authentication;
using Gatherly.Presentation.Contracts.Members;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Gatherly.Presentation.Controllers
{
    [ApiKey]
    [Route("api/members")]
    public sealed class MembersController : ApiController
    {
        public MembersController(ISender sender)
            : base(sender)
        {
        }

        [HasPermission(Permission.ReadMember)]
        [HttpGet("members")]
        public async Task<IActionResult> GetMembers(int page, int pageSize, CancellationToken cancellationToken)
        {
            var query = new GetMembersQuery(page, pageSize);

            Result<List<MemberResponse>> response = await Sender.Send(query, cancellationToken);

            return response.IsSuccess ? Ok(response.Value) : NotFound(response.Error);
        }

        [HasPermission(Permission.ReadMember)]
        [HttpGet("{id:guid}")]
        public async Task<IActionResult> GetMemberById(Guid id, CancellationToken cancellationToken)
        {
            var query = new GetMemberByIdQuery(id);

            Result<MemberResponse> response = await Sender.Send(query, cancellationToken);

            return response.IsSuccess ? Ok(response.Value) : NotFound(response.Error);
        }

        [HttpPost("login")]
        public async Task<IActionResult> LoginMember(
            [FromBody] LoginRequest request,
            CancellationToken cancellationToken)
        {
            var command = new LoginCommand(request.Email);

            Result<string> tokenResult = await Sender.Send(
                command,
                cancellationToken);

            if (tokenResult.IsFailure)
            {
                return HandleFailure(tokenResult);
            }

            return Ok(tokenResult.Value);

        }

        [HttpPost]
        public async Task<IActionResult> RegisterMember(
            [FromBody] RegisterMemberRequest request, 
            CancellationToken cancellationToken)
        {
            var command = new CreateMemberCommand(
                request.Email,
                request.FirstName,
                request.LastName);

            Result<Guid> result = await Sender.Send(command, cancellationToken);

            if(result.IsFailure)
            {
                return HandleFailure(result);
            }

            return CreatedAtAction(
               nameof(GetMemberById),
               new { id = result.Value },
               result.Value);
        }

        [HttpPut("{id:guid}")]
        [HasPermission(Permission.UpdateMember)]
        public async Task<IActionResult> UpdateMember(
            Guid id,
            [FromBody] UpdateMemberRequest request,
            CancellationToken cancellationToken)
        {
            var command = new UpdateMemberCommand(
                id,
                request.FirstName,
                request.LastName);

            Result result = await Sender.Send(
                command,
                cancellationToken);

            if (result.IsFailure)
            {
                return HandleFailure(result);
            }

            return NoContent();
        }
    }
}
