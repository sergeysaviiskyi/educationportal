namespace EducationPortal.PresentationWebAPI.Validation
{
    public static class ValidationResultExtensions
    {
        public static ProblemDetails ValidationFailuresToProblemDetails(this IEnumerable<ValidationFailure> validationFailures)
        {
            var errors = validationFailures.ToDictionary(x => x.PropertyName, x => x.ErrorMessage);
            var problemDetails = new ProblemDetails
            {
                Type = "ValidationError",
                Detail = "Invalid request, please check the error list for more details",
                Status = (int)(HttpStatusCode.BadRequest),
                Title = "Invalid request"
            };
            problemDetails.Extensions.Add("errors", errors);
            return problemDetails;
        }

        public static ProblemDetails ErrorsToProblemDetails(this IEnumerable<Error> errorsList)
        {
            var errors = errorsList.ToDictionary(x => x.PropertyName, x => x.Message);
            var problemDetails = new ProblemDetails
            {
                Type = "Input data error",
                Detail = "Invalid request, please check the error list for more details",
                Status = (int)(HttpStatusCode.BadRequest),
                Title = "Invalid request"
            };
            problemDetails.Extensions.Add("errors", errors);
            return problemDetails;
        }
    }
}