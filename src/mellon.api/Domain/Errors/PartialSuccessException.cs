namespace Mellon.Domain.Errors;

public class PartialSuccessException : MellonBaseException
{
    public PartialSuccessException(string? message)
        : base(ErrorCode.PartialSuccess, message) { }

    public PartialSuccessException(string? message, Exception? inner)
        : base(ErrorCode.PartialSuccess, message, inner) { }
}

public class PartialSuccessException<T> : PartialSuccessException
{
    public T Result { get; }

    public PartialSuccessException(string? message, T result)
        : base(message) => Result = result;

    public PartialSuccessException(string? message, T result, Exception? inner)
        : base(message, inner) => Result = result;
}
