using static System.Runtime.InteropServices.JavaScript.JSType;

namespace UserManagementApp.Domain.Models;

public class Result
{
    public Error? Error { get; protected set; }

    protected Result(Error? error) => Error = error;

    public bool IsSuccess => Error is null;

    public bool IsFailure => Error is not null;

    public static Result Success() => new(null);

    public static Result Failure(Error error) => new(error);

    public static implicit operator Result(Error error) => Failure(error);

    public TResult Map<TResult>(Func<TResult> onSuccess, Func<Error, TResult> onFailure) => IsSuccess ? onSuccess() : onFailure(Error!);
}

public sealed class Result<T> : Result
{
    public T? Value { get; }

    private Result(T? value) : base(null) => Value = value;

    private Result(Error? error) : base(error) => Value = default;

    public static Result<T> Success(T value) => new(value);

    public static new Result<T> Failure(Error error) => new(error);

    public static implicit operator Result<T>(T value) => Success(value);

    public static implicit operator Result<T>(Error error) => Failure(error);

    public TResult Map<TResult>(Func<T, TResult> onSuccess, Func<Error, TResult> onFailure) => IsSuccess ? onSuccess(Value!) : onFailure(Error!);
}
