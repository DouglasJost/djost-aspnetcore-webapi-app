namespace AppServiceCore
{
    public class CommandResult<T>
    {
        public bool IsSuccess { get; private set; }
        public string ErrorMessage { get; private set; } = string.Empty;
        public T? Value { get; private set; }

        private CommandResult(bool isSuccess, T? value, string? errorMessage)
        {
            IsSuccess = isSuccess;
            Value = value;
            ErrorMessage = !string.IsNullOrEmpty(errorMessage) ? errorMessage : string.Empty;
        }

        // Factory methods for creating success and failure results
        public static CommandResult<T> Success(T value)
        {
            return new CommandResult<T>(true, value, null);
        }

        public static CommandResult<T> Failure(string errorMessage)
        {
            return new CommandResult<T>(false, default(T), errorMessage);
        }
    }
}
