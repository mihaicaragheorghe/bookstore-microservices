using System.Net;

namespace Shared.Core;

public readonly struct Error
{
    public string Code { get; }

    public string Description { get; }

    public ErrorType Type { get; }

    public HttpStatusCode HttpStatusCode { get; }

    private Error(string code, string description, ErrorType type, HttpStatusCode httpStatusCode)
    {
        Code = code;
        Description = description;
        Type = type;
        HttpStatusCode = httpStatusCode;
    }

    public static Error Validation(string code, string description)
    {
        return new(code, description, ErrorType.Validation, HttpStatusCode.BadRequest);
    }

    public static Error Failure(string code, string description)
    {
        return new(code, description, ErrorType.Failure, HttpStatusCode.InternalServerError);
    }

    public static Error NotFound(string code, string description)
    {
        return new(code, description, ErrorType.NotFound, HttpStatusCode.NotFound);
    }

    public static Error Conflict(string code, string description)
    {
        return new Error(code, description, ErrorType.Conflict, HttpStatusCode.Conflict);
    }

    public static Error Forbidden(string code, string description)
    {
        return new Error(code, description, ErrorType.Authorization, HttpStatusCode.Forbidden);
    }

    public static Error Internal()
    {
        return new("internal", "Internal server error", ErrorType.Failure, HttpStatusCode.InternalServerError);
    }
}
