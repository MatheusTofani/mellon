namespace Mellon.Domain.Errors;

public abstract class MellonBaseException : Exception
{
    public ErrorCode ErrorCode { get; }

    protected MellonBaseException(ErrorCode code, string? message)
        : base(message) => ErrorCode = code;

    protected MellonBaseException(ErrorCode code, string? message, Exception? inner)
        : base(message, inner) => ErrorCode = code;
}
