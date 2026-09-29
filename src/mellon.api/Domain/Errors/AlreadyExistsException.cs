namespace Mellon.Domain.Errors;

public class AlreadyExistsException : MellonBaseException
{
    public AlreadyExistsException(string? message)
        : base(ErrorCode.AlreadyExistsException, message) { }

    public AlreadyExistsException(string? message, Exception? inner)
        : base(ErrorCode.AlreadyExistsException, message, inner) { }
}
