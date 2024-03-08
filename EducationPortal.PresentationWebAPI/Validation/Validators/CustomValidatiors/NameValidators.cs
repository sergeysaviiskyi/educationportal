namespace EducationPortal.PresentationWebAPI.Validation.Validators.CustomValidatiors
{
    public static class NameValidators
    {
        public static IRuleBuilderOptions<T, string> IsValidUserName<T>(this IRuleBuilder<T, string> rulebuilder)
        {
            return rulebuilder
                .NotEmpty()
                .WithMessage("{PropertyName} cannot be empty.")
                .Length(2, 50)
                .WithMessage("Length ({TotalLength}) of {PropertyName} is invalid.")
                .Must(str => Regex.IsMatch(str, "^[a-zA-Z\\s]+$"))
                .WithMessage("{PropertyName} must contain only letters.");
        }

        public static IRuleBuilderOptions<T, string> IsValidCourseName<T>(this IRuleBuilder<T, string> rulebuilder, IUnitOfWork unitOfWork)
        {
            return rulebuilder
                .NotEmpty()
                .WithMessage("{PropertyName} cannot be empty.")
                .Length(2, 100)
                .WithMessage("Length ({TotalLength}) of {PropertyName} is invalid.")
                .Must(str => Regex.IsMatch(str, "^[a-zA-Z0-9\\s]+$"))
                .WithMessage("{PropertyName} must contain only letters and numbers.")
                .MustAsync(async (name, _) =>
                {
                    return await unitOfWork.Courses.IsNameUniqueAsync(name);
                })
                .WithMessage("Course with such name already exists.");
        }

        public static IRuleBuilderOptions<T, string> IsValidSkillName<T>(this IRuleBuilder<T, string> rulebuilder, IUnitOfWork unitOfWork)
        {
            return rulebuilder
                .NotEmpty()
                .WithMessage("{PropertyName} cannot be empty.")
                .Length(2, 100)
                .WithMessage("Length ({TotalLength}) of {PropertyName} is invalid.")
                .Must(str => Regex.IsMatch(str, "^[a-zA-Z\\s]+$"))
                .WithMessage("{PropertyName} must contain only letters.")
                .MustAsync(async (name, _) =>
                {
                    return await unitOfWork.Skills.IsNameUniqueAsync(name);
                })
                .WithMessage("Skill with such name already exists.");
        }

        public static IRuleBuilderOptions<T, string> IsValidMaterialName<T>(this IRuleBuilder<T, string> rulebuilder, IUnitOfWork unitOfWork)
        {
            return rulebuilder
                .NotEmpty()
                .WithMessage("{PropertyName} cannot be empty.")
                .Length(2, 100)
                .WithMessage("Length ({TotalLength}) of {PropertyName} is invalid.")
                .Must(str => Regex.IsMatch(str, "^[a-zA-Z0-9\\s]+$"))
                .WithMessage("{PropertyName} must contain only letters and numbers.")
                .MustAsync(async (name, _) =>
                {
                    return await unitOfWork.Materials.IsNameUniqueAsync(name);
                })
                .WithMessage("Material with such name already exists.");
        }
    }
}
