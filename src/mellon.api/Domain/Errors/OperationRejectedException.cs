namespace Mellon.Domain.Errors;

public class OperationRejectedException : MellonBaseException
{
    public OperationRejectedException(string? message)
        : base(ErrorCode.OperationRejectedException, message) { }

    public OperationRejectedException(string? message, Exception? inner)
        : base(ErrorCode.OperationRejectedException, message, inner) { }
}
