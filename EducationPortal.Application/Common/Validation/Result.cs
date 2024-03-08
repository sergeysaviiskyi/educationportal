namespace EducationPortal.Application.Common.Validation
{
    public abstract class Result
    {
        public bool Success { get; protected set; }
        public bool Failure => !Success;
    }

    public abstract class Result<T> : Result
    {
        private T _data;
        public T Data
        {
            get => _data;
            set => _data = value;
        }

        protected Result(T data)
        {
            Data = data;
        }
    }
}