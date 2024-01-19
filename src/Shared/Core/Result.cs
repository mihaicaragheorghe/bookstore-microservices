namespace Shared.Core;

/// <summary>
/// Represents the outcome of an operation, containing the result value or an error.
/// </summary>
/// <typeparam name="T">The type of the value returned in the result.</typeparam>
/// <remarks>
/// This class is used to encapsulate the result of an operation, which can be either successful with a value of type <typeparamref name="T"/>
/// or unsuccessful with an <see cref="Error"/>. It allows for a more structured approach to error handling and result management.
/// </remarks>
public class Result<T>
{
    public T? Value { get; private set; }

    public Error? Error { get; private set; }

    public bool IsSuccessful { get; private set; }

    private Result(T value)
    {
        Value = value;
        IsSuccessful = true;
    }

    private Result(Error error)
    {
        Error = error;
        Value = default!;
        IsSuccessful = false;
    }

    /// <summary>
    /// Implicitly converts a value of type <typeparamref name="T"/> to a successful <see cref="Result{T}"/>.
    /// </summary>
    /// <param name="value">The result value to convert.</param>
    public static implicit operator Result<T>(T value) => new(value);

    /// <summary>
    /// Implicitly converts an <see cref="Error"/> to an unsuccessful <see cref="Result{T}"/>.
    /// </summary>
    /// <param name="error">The error to convert.</param>
    public static implicit operator Result<T>(Error error) => new(error);
}
