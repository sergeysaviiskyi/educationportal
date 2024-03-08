namespace EducationPortal.PresentationWebAPI.Validation.Validators
{
    public class AddSkillRequestValidator : AbstractValidator<AddSkillRequest>
    {
        public AddSkillRequestValidator(IUnitOfWork unitOfWork)
        {
            RuleLevelCascadeMode = CascadeMode.Stop;

            RuleFor(p => p.CourseId)
                .NotEmpty()
                .WithMessage("{PropertyName} cannot be empty.")
                .MustAsync(async (id, _) =>
                {
                    return await unitOfWork.Courses.IsExisting(id);
                })
                .WithMessage("Such course does not exist.");

            RuleFor(p => p.SkillId)
                .NotEmpty()
                .WithMessage("{PropertyName} cannot be empty.")
                .MustAsync(async (id, _) =>
                {
                    return await unitOfWork.Skills.IsExisting(id);
                })
                .WithMessage("Such skill does not exist.");
        }
    }
}
