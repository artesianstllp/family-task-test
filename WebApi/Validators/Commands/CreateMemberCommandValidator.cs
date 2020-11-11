using Domain.Commands;
using FluentValidation;

namespace WebApi.Validators.Commands
{
    public class CreateMemberCommandValidator: AbstractValidator<CreateMemberCommand>
    {
        public CreateMemberCommandValidator()
        {
            RuleFor(x => x.FirstName).NotNull().NotEmpty();
            RuleFor(x => x.LastName).NotNull().NotEmpty();
            RuleFor(x => x.Email).EmailAddress().NotNull().NotEmpty();
            RuleFor(x => x.FirstName).NotNull().NotEmpty();
        }
    }
}
