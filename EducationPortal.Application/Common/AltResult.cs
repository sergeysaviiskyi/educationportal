namespace EducationPortal.Application.Common
{
    public readonly struct AltResult<TValue, TError>
    {
        private readonly TValue _value;
        private readonly TError _error;
        public bool IsError { get; }
        public bool IsSuccess => !IsError;
        public AltResult()
        {
            IsError = false;
            _value = default;
            _error = default;
        }
        public AltResult(TValue value)
        {
            IsError = false;
            _value = value;
            _error = default;
        }

        public AltResult(TError error)
        {
            IsError = true;
            _value = default;
            _error = error;
        }

        public static implicit operator AltResult<TValue, TError>(TValue value) => new (value);
        public static implicit operator AltResult<TValue, TError>(TError error) => new (error);

        public IActionResult Match<IActionResult>(
            Func<TValue, IActionResult> success,
            Func<TError, IActionResult> failure) =>
            !IsError ? success(_value) : failure(_error);
    }
}