namespace BaitaHora.Domain.Commons
{

    public class Result
    {
        public bool IsSuccess { get; }
        public string? Error { get; }

        public bool IsFailure => !IsSuccess;

        protected Result(bool isSuccess, string? error)
        {
            IsSuccess = isSuccess;
            Error = error;
        }

        public static Result Success() => new Result(true, null);
        public static Result Failure(string error) => new Result(false, error);
    }

    public class Result<T> : Result
    {
        public T? Value { get; }

        private Result(T? value, bool isSuccess, string? error)
            : base(isSuccess, error)
        {
            Value = value;
        }

        public static Result<T> Success(T value) => new Result<T>(value, true, null);
        public static new Result<T> Failure(string error) => new Result<T>(default, false, error);
    }
}