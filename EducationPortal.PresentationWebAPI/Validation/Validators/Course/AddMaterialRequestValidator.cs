namespace EducationPortal.PresentationWebAPI.Validation.Validators.Material
{
    public class AddMaterialRequestValidator : AbstractValidator<AddMaterialRequest>
    {
        public AddMaterialRequestValidator(IUnitOfWork unitOfWork)
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

            RuleFor(p => p.MaterialId)
                .NotEmpty()
                .WithMessage("{PropertyName} cannot be empty.")
                .MustAsync(async (id, _) =>
                {
                    return await unitOfWork.Materials.IsExisting(id);
                })
                .WithMessage("Such material does not exist.");
        }
    }
}
