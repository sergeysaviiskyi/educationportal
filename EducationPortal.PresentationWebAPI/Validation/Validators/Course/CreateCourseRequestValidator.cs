using EducationPortal.PresentationWebAPI.Validation.Validators.CustomValidatiors;

namespace EducationPortal.PresentationWebAPI.Validation.Validators
{
    public class CreateCourseRequestValidator : AbstractValidator<CreateCourseRequest>
    {
        public CreateCourseRequestValidator(IUnitOfWork unitOfWork)
        {
            RuleLevelCascadeMode = CascadeMode.Stop;

            RuleFor(p => p.Name)
                .IsValidCourseName(unitOfWork);

            RuleFor(p => p.Description)
                .NotEmpty()
                .WithMessage("{PropertyName} cannot be empty.")
                .Length(2, 500)
                .WithMessage("Length ({TotalLength}) of {PropertyName} is invalid.");
        }
    }
}
