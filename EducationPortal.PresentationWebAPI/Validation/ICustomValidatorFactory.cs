namespace EducationPortal.PresentationWebAPI.Validation
{
    public interface ICustomValidatorFactory
    {
        IValidator GetValidatorFor(Type type, IServiceScope scope);
    }
}
