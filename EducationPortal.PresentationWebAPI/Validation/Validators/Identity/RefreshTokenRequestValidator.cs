namespace EducationPortal.PresentationWebAPI.Validation.Validators.Identity
{
    public class RefreshTokenRequestValidator : AbstractValidator<RefreshTokenRequest>
    {
        public RefreshTokenRequestValidator(IUnitOfWork unitOfWork)
        {
            RuleLevelCascadeMode = CascadeMode.Stop;

            RuleFor(p => p.AccessToken)
                .NotEmpty()
                .WithMessage("{PropertyName} cannot be empty.");

            RuleFor(p => p.RefreshToken)
                .NotEmpty()
                .WithMessage("{PropertyName} cannot be empty.");
        }
    }
}
