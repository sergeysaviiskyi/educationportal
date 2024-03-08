using static Org.BouncyCastle.Crypto.Engines.SM2Engine;

namespace EducationPortal.Application.Common.Validation
{
    public class ErrorResult : Result, IErrorResult
    {
        public int StatusCode { get; }
        public IReadOnlyCollection<Error> Errors { get; }

        public ErrorResult(int code = 400) : this(Array.Empty<Error>(), code)
        {
        }

        public ErrorResult(IReadOnlyCollection<Error> errors, int code = 400)
        {
            Success = false;
            Errors = errors ?? Array.Empty<Error>();
            StatusCode = code;
        }
    }

    public class ErrorResult<T> : Result<T>, IErrorResult
    {
        public int StatusCode { get; }
        public IReadOnlyCollection<Error> Errors { get; }
        public ErrorResult() : this(Array.Empty<Error>(), default, 400)
        {
        }

        public ErrorResult(IReadOnlyCollection<Error> errors, T data = default, int code = 400) : base(data)
        {
            StatusCode = code;
            Success = false;
            Errors = errors ?? Array.Empty<Error>();
        }
    }
}
