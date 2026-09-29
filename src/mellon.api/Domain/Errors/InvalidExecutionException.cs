namespace Mellon.Domain.Errors;

public class InvalidExecutionException : MellonBaseException
{
    public InvalidExecutionException(string? message)
        : base(ErrorCode.InvalidExecutionException, message) { }

    public InvalidExecutionException(string? message, Exception? inner)
        : base(ErrorCode.InvalidExecutionException, message, inner) { }
}
