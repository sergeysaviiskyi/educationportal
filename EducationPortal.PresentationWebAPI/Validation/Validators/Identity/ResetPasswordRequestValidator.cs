﻿namespace EducationPortal.PresentationWebAPI.Validation.Validators.Identity
{
    public class ResetPasswordRequestValidator : AbstractValidator<ResetPasswordRequest>
    {
        public ResetPasswordRequestValidator(IUnitOfWork unitOfWork)
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

            RuleFor(p => p.VerificationCode)
                .NotEmpty()
                .WithMessage("{PropertyName} cannot be empty.");

            RuleFor(p => p.Password)
                .NotEmpty()
                .WithMessage("{PropertyName} cannot be empty.");

            RuleFor(p => p.ConfirmPassword)
                .NotEmpty()
                .WithMessage("{PropertyName} cannot be empty.");
        }
    }
}