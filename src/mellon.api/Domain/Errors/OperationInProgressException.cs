namespace Mellon.Domain.Errors;

public class OperationInProgressException : MellonBaseException
{
    public OperationInProgressException(string? message)
        : base(ErrorCode.OperationInProgressException, message) { }

    public OperationInProgressException(string? message, Exception? inner)
        : base(ErrorCode.OperationInProgressException, message, inner) { }
}
