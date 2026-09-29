namespace Mellon.Domain.Errors;

public class NotFoundException : MellonBaseException
{
    public NotFoundException(string? message)
        : base(ErrorCode.NotFoundException, message) { }

    public NotFoundException(string? message, Exception? inner)
        : base(ErrorCode.NotFoundException, message, inner) { }
}
