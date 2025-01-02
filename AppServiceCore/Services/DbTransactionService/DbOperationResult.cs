namespace AppServiceCore.Services.DbTransactionService
{
    public class DbOperationResult<T>
    {
        public bool Success { get; set; }
        public string? ErrorMessage { get; set; } = string.Empty;
        public T? Data { get; set; }
    }
}
