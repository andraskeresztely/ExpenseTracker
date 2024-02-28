namespace ExpenseTracker.Domain.Abstractions
{
    public class Result<TValue>
    {
        public TValue Value => IsSuccess ? _value! : throw new Exception("Value is inaccessible for failure.");
        public ErrorList Errors => IsFailure ? _errors : throw new Exception("Error is inaccessible for success.");

        public bool IsFailure { get; }
        public bool IsSuccess => !IsFailure;

        private readonly TValue? _value;
        private readonly ErrorList _errors;

        private Result(bool isFailure, TValue? value, ErrorList errors)
        {
            IsFailure = isFailure;
            _value = value;
            _errors = errors;
        }

        public static implicit operator Result<TValue>(TValue value) => new(false, value, new ErrorList([]));

        public static implicit operator Result<TValue>(ErrorList errors) => new(true, default, errors);
    }
}