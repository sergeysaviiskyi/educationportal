using EducationPortal.Infrastructure.Services;

namespace EducationPortal.PresentationWebAPI.Validation.Validators.Identity
{
    public class AuthenticationRequestValidator : AbstractValidator<AuthenticationRequest>
    {
        public AuthenticationRequestValidator(IUnitOfWork unitOfWork)
        {
            RuleLevelCascadeMode = CascadeMode.Stop;

            RuleFor(p => p.Email)
                .NotEmpty()
                .WithMessage("{PropertyName} cannot be empty.")
                .EmailAddress()
                .WithMessage("{PropertyName} is invalid.")
                .MustAsync(async (email, _) =>
                {
                    return await unitOfWork.Users.IsExistingEmail(email);
                })
                .WithMessage("User with such {PropertyName} doesn't exist.");

            RuleFor(p => p.Password)
                .NotEmpty()
                .WithMessage("{PropertyName} cannot be empty.");
        }
    }
}
