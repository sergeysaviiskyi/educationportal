using EducationPortal.PresentationWebAPI.Validation.Validators.CustomValidatiors;

namespace EducationPortal.PresentationWebAPI.Validation.Validators
{
    public class CreateVideoRequestValidator : AbstractValidator<CreateVideoRequest>
    {
        public CreateVideoRequestValidator(IUnitOfWork unitOfWork)
        {
            RuleLevelCascadeMode = CascadeMode.Stop;

            RuleFor(p => p.Name)
                .IsValidMaterialName(unitOfWork);
        }
    }
}
