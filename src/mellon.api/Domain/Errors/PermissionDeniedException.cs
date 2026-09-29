namespace Mellon.Domain.Errors;

public class PermissionDeniedException : MellonBaseException
{
    public PermissionDeniedException(string? message)
        : base(ErrorCode.PermissionDeniedException, message) { }

    public PermissionDeniedException(string? message, Exception? inner)
        : base(ErrorCode.PermissionDeniedException, message, inner) { }
}
