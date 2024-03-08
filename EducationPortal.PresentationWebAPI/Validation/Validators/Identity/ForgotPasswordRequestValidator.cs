namespace EducationPortal.PresentationWebAPI.Validation.Validators.Identity
{
    public class ForgotPasswordRequestValidator : AbstractValidator<ForgotPasswordRequest>
    {
        public ForgotPasswordRequestValidator(IUnitOfWork unitOfWork)
        {
            RuleLevelCascadeMode = CascadeMode.Stop;

            RuleFor(p => p.UserEmail)
                .NotEmpty()
                .WithMessage("{PropertyName} cannot be empty.")
                .EmailAddress()
                .WithMessage("{PropertyName} is invalid.")
                .MustAsync(async (email, _) =>
                {
                    return await unitOfWork.Users.IsExistingEmail(email);
                })
                .WithMessage("User with such {PropertyName} doesn't exist.");
        }
    }
}
