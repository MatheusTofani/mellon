namespace Mellon.Domain.Errors;

public class NotModifiedException : MellonBaseException
{
    public NotModifiedException(string? message)
        : base(ErrorCode.NotModifiedException, message) { }

    public NotModifiedException(string? message, Exception? inner)
        : base(ErrorCode.NotModifiedException, message, inner) { }
}
