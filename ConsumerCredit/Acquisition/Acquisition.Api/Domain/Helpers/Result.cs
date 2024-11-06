namespace Acquisition.Api.Domain.Helpers;

public record Result<T>
{
    private Result()
    {
    }

    public bool IsSuccess { get; private init; }
    public bool IsFailure => !IsSuccess;
    public T? Value { get; private init; }
    public string? ErrorMessage { get; private init; }

    public static Result<T> Success(T value)
    {
        return new Result<T> { IsSuccess = true, Value = value };
    }

    public static Result<T> Failure(string errorMessage)
    {
        return new Result<T>
        {
            IsSuccess = false, ErrorMessage = errorMessage
        };
    }
}