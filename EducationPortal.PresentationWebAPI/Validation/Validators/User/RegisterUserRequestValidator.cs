using EducationPortal.PresentationWebAPI.Validation.Validators.CustomValidatiors;

namespace EducationPortal.PresentationWebAPI.Validation.Validators
{
    public class RegisterUserRequestValidator : AbstractValidator<RegisterUserRequest>
    {
        public RegisterUserRequestValidator(IUnitOfWork unitOfWork)
        {
            RuleLevelCascadeMode = CascadeMode.Stop;

            RuleFor(p => p.Name)
                .IsValidUserName();

            RuleFor(p => p.Email)
                .NotEmpty()
                .WithMessage("{PropertyName} cannot be empty.")
                .EmailAddress()
                .WithMessage("{PropertyName} is invalid.")
                .MustAsync(async (email, _) =>
                {
                    return !await unitOfWork.Users.IsExistingEmail(email);
                })
                .WithMessage("{PropertyName} must be unique.");

            RuleFor(p => p.Password)
                .NotEmpty()
                .WithMessage("{PropertyName} cannot be empty.");
        }
    }
}