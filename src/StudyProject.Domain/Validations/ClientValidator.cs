using FluentValidation;
using StudyProject.Domain.Entities;

namespace StudyProject.Domain.Validations
{
    public class ClientValidator : AbstractValidator<Client>
    {
        public ClientValidator()
        {
            RuleFor(x => x.Name).NotNull().Length(0, 250);
            RuleFor(x => x.Email).NotNull().Length(0, 150);
        }
    }
}
