namespace EducationPortal.Application.Common.Validation
{
    public class Error
    {
        public string PropertyName { get; } = "Unknown name";
        public string Message { get; } = "Something went wrong";

        public Error() { }

        public Error(string message)
        {
            Message = message;
        }

        public Error(string propertyName, string message) : this(message)
        {
            PropertyName = propertyName;
        }
    }
}
