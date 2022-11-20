using FluentValidation;
using Gatherly.Domain.ValueObjects;

namespace Gatherly.Application.Members.Commands.UpdateMember
{
    internal class UpdateMemberCommandValidator : AbstractValidator<UpdateMemberCommand>
    {
        public UpdateMemberCommandValidator()
        {
            RuleFor(x => x.MemberId).NotEmpty();

            RuleFor(x => x.FirstName).NotEmpty().MaximumLength(FirstName.MaxLength);

            RuleFor(x => x.LastName).NotEmpty().MaximumLength(LastName.MaxLength);
        }
    }
}
