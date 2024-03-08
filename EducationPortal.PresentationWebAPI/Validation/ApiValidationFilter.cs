namespace EducationPortal.PresentationWebAPI.Validation
{
    public class ApiValidationFilter : IAsyncActionFilter
    {
        private readonly ICustomValidatorFactory _validatorFactory;
        private readonly IServiceProvider _serviceProvider;

        public ApiValidationFilter(ICustomValidatorFactory validatorFactory, IServiceProvider serviceProvider)
        {
            _validatorFactory = validatorFactory;
            _serviceProvider = serviceProvider;
        }
        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            if (!context.ActionArguments.Any())
            {
                await next();
                return;
            }

            var validationFailures = new List<ValidationFailure>();

            foreach (var actionArgument in context.ActionArguments)
            {
                var validationErrors = await GetValidationErrorsAsync(actionArgument.Value);
                validationFailures.AddRange(validationErrors);
            }

            if (!validationFailures.Any())
            {
                await next();
                return;
            }
            context.Result = new BadRequestObjectResult(validationFailures.ValidationFailuresToProblemDetails());
        }

        private async Task<IEnumerable<ValidationFailure>> GetValidationErrorsAsync(object value)
        {
            if (value == null)
            {
                return new[] { new ValidationFailure("", "instance is null") };
            }
            using (var scope = _serviceProvider.CreateScope())
            {
                var validatorInstance = _validatorFactory.GetValidatorFor(value.GetType(), scope);

                if (validatorInstance == null)
                {
                    return new List<ValidationFailure>();
                }
                var validationResult = await validatorInstance.ValidateAsync(new ValidationContext<object>(value));
                return validationResult.Errors ?? new List<ValidationFailure>();
            }
        }
    }
}