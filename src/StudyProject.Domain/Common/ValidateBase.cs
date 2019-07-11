using FluentValidation;
using FluentValidation.Results;
using StudyProject.Domain.Interfaces.Base;

namespace StudyProject.Domain.Common
{
  public  class ValidateBase: IComponentValidate
    {
        public bool IsValid { get; set; }
        public ValidationResult ValidationResult { get; set; }

        public bool Validate<TModel>(TModel model, AbstractValidator<TModel> validator)
        {
            ValidationResult = validator.Validate(model);
            this.IsValid = ValidationResult.IsValid;
            return this.IsValid;
        }
    }
}
