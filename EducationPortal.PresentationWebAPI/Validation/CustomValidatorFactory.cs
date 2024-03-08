namespace EducationPortal.PresentationWebAPI.Validation
{
    public class CustomValidatorFactory : ICustomValidatorFactory
    {
        public IValidator GetValidatorFor(Type type, IServiceScope scope)
        {
            var genericValidatorType = typeof(IValidator<>);
            var specificValidatorType = genericValidatorType.MakeGenericType(type);
            var validatorInstance = (IValidator)scope.ServiceProvider.GetService(specificValidatorType);
            return validatorInstance;
        }
    }
}
