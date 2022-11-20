using Gatherly.Application.Members.Commands.CreateMember;
using Gatherly.Application.Members.Commands.UpdateMember;
using Gatherly.Application.Members.Queries.GetMemberById;
using Gatherly.Domain.Shared;
using Gatherly.Presentation.Abstractions;
using Gatherly.Presentation.Contracts.Members;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Gatherly.Presentation.Controllers
{
    [Route("api/members")]
    public sealed class MembersController : ApiController
    {
        public MembersController(ISender sender)
            : base(sender)
        {
        }

        [HttpGet("{id:guid}")]
        public async Task<IActionResult> GetMemberById(Guid id, CancellationToken cancellationToken)
        {
            var query = new GetMemberByIdQuery(id);

            Result<MemberResponse> response = await Sender.Send(query, cancellationToken);

            return response.IsSuccess ? Ok(response.Value) : NotFound(response.Error);
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
