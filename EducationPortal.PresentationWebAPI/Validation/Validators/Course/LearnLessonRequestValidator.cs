namespace EducationPortal.PresentationWebAPI.Validation.Validators
{
    public class LearnLessonRequestValidator : AbstractValidator<LearnLessonRequest>
    {
        public LearnLessonRequestValidator(IUnitOfWork unitOfWork)
        {
            RuleLevelCascadeMode = CascadeMode.Stop;

            RuleFor(p => p.UserId)
                .NotEmpty()
                .WithMessage("{PropertyName} cannot be empty.")
                .MustAsync(async (id, _) =>
                {
                    return await unitOfWork.Users.IsExisting(id);
                })
                .WithMessage("Such user does not exist.");

            RuleFor(p => p.CourseId)
                .NotEmpty()
                .WithMessage("{PropertyName} cannot be empty.")
                .MustAsync(async (id, _) =>
                {
                    return await unitOfWork.Courses.IsExisting(id);
                })
                .WithMessage("Such course does not exist.");
        }
    }
}
