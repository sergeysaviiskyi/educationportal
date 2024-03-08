using System.Text.RegularExpressions;
using EducationPortal.PresentationWebAPI.Validation.Validators.CustomValidatiors;

namespace EducationPortal.PresentationWebAPI.Validation.Validators
{
    public class CreateEbookRequestValidator : AbstractValidator<CreateEbookRequest>
    {
        public CreateEbookRequestValidator(IUnitOfWork unitOfWork)
        {
            RuleLevelCascadeMode = CascadeMode.Stop;

            RuleFor(p => p.Name)
                .IsValidMaterialName(unitOfWork);

            RuleFor(p => p.Author)
                .IsValidUserName();
        }
    }
}