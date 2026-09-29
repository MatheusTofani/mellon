namespace Mellon.Domain.Errors;

public enum ErrorCode
{
    UnexpectedException          = 1000,
    PermissionDeniedException    = 1001,
    UnprocessableEntityException = 1002,
    AlreadyExistsException       = 1003,
    NotFoundException            = 1004,
    NotModifiedException         = 1005,
    OperationRejectedException   = 1006,
    ValidationException          = 1007,
    ForbiddenException           = 1008,
    PartialSuccess               = 1009,
    InvalidExecutionException    = 1010,
    OperationInProgressException = 1011,
}
