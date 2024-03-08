namespace EducationPortal.Application.Common.Validation
{
    internal interface IErrorResult
    {
        int StatusCode { get; }
        IReadOnlyCollection<Error> Errors { get; }
    }
}
