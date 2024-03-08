using EducationPortal.PresentationWebAPI.Validation.Validators.CustomValidatiors;

namespace EducationPortal.PresentationWebAPI.Validation.Validators
{
    public class CreateArticleRequestValidator : AbstractValidator<CreateArticleRequest>
    {
        public CreateArticleRequestValidator(IUnitOfWork unitOfWork)
        {
            RuleLevelCascadeMode = CascadeMode.Stop;

            RuleFor(p => p.Name)
                .IsValidMaterialName(unitOfWork);
        }
    }
}
