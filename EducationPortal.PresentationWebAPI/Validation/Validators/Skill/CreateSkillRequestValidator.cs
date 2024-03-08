using EducationPortal.PresentationWebAPI.Validation.Validators.CustomValidatiors;

namespace EducationPortal.PresentationWebAPI.Validation.Validators
{
    public class CreateSkillRequestValidator : AbstractValidator<CreateSkillRequest>
    {
        public CreateSkillRequestValidator(IUnitOfWork unitOfWork)
        {
            RuleLevelCascadeMode = CascadeMode.Stop;

            RuleFor(p => p.Name)
                .IsValidSkillName(unitOfWork);
        }
    }
}