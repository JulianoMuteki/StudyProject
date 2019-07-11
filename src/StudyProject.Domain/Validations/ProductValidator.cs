using FluentValidation;
using StudyProject.Domain.Entities;

namespace StudyProject.Domain.Validations
{
    public class ProductValidator : AbstractValidator<Product>
    {
        public ProductValidator()
        {
            RuleFor(x => x.Name).NotNull().Length(0, 200);
            RuleFor(x => x.Description).NotNull().Length(0, 250);
        }
    }
}
