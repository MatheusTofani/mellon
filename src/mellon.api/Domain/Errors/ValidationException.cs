namespace Mellon.Domain.Errors;

public class ValidationException : MellonBaseException
{
    public ValidationException(string? message)
        : base(ErrorCode.ValidationException, message) { }

    public ValidationException(string? message, Exception? inner)
        : base(ErrorCode.ValidationException, message, inner) { }
}
