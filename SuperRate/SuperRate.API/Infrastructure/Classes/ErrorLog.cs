using System.Net;
using Microsoft.AspNetCore.Mvc;
using SuperRate.API.Localizations;
using SuperRate.Application.Exceptions;

namespace SuperRate.API.Infrastructure.Classes;

public class ErrorLog : ProblemDetails
{
    private const string UnhandledErrorCode = "UnhandledError";
    public LogLevel LogLevel { get; set; }
    public string Message { get; set; } = default!;
    public string Code { get; set; } = default!;

    public ErrorLog(HttpContext httpContext, Exception exception)
    {
        Extensions["TraceId"] = httpContext.TraceIdentifier;
        Instance = httpContext.Request.Path;

        HandleException((dynamic)exception);
    }

    private void HandleException(Exception exception)
    {
        Code = UnhandledErrorCode;
        Status = (int)HttpStatusCode.InternalServerError;
        Type = "https://datatracker.ietf.org/doc/html/rfc7231#section-6.6.1";
        Title = exception.Message;
        LogLevel = LogLevel.Error;
        Message = ErrorMessages.UnhandledErrorOccurred;
    }

    private void HandleException(UserNotFoundException exception)
    {
        Code = UserNotFoundException.Code;
        Status = (int)HttpStatusCode.NotFound;
        Type = "https://datatracker.ietf.org/doc/html/rfc7231#section-6.5.4";
        Title = exception.Message;
        LogLevel = LogLevel.Error;
        Message = ErrorMessages.UserNotFound;
    }

    private void HandleException(IBanNotFoundException exception)
    {
        Code = IBanNotFoundException.Code;
        Status = (int)HttpStatusCode.NotFound;
        Type = "https://datatracker.ietf.org/doc/html/rfc7231#section-6.5.4";
        Title = exception.Message;
        LogLevel = LogLevel.Error;
        Message = ErrorMessages.IBanNotFound;
    }

    private void HandleException(OrderNotFoundException exception)
    {
        Code = OrderNotFoundException.Code;
        Status = (int)HttpStatusCode.NotFound;
        Type = "https://datatracker.ietf.org/doc/html/rfc7231#section-6.5.4";
        Title = exception.Message;
        LogLevel = LogLevel.Error;
        Message = ErrorMessages.OrderNotFound;
    }

    private void HandleException(InvalidPasswordException exception)
    {
        Code = InvalidPasswordException.Code;
        Status = (int)HttpStatusCode.Unauthorized;
        Type = "https://datatracker.ietf.org/doc/html/rfc7235#section-3.1";
        Title = exception.Message;
        LogLevel = LogLevel.Error;
        Message = ErrorMessages.InvalidPassword;
    }

    private void HandleException(EmailAlreadyExistsException exception)
    {
        Code = EmailAlreadyExistsException.Code;
        Status = (int)HttpStatusCode.Conflict;
        Type = "https://datatracker.ietf.org/doc/html/rfc7231#section-6.5.8";
        Title = exception.Message;
        LogLevel = LogLevel.Error;
        Message = ErrorMessages.EmailAlreadyExists;
    }

    private void HandleException(UsernameAlreadyExistsException exception)
    {
        Code = UsernameAlreadyExistsException.Code;
        Status = (int)HttpStatusCode.Conflict;
        Type = "https://datatracker.ietf.org/doc/html/rfc7231#section-6.5.8";
        Title = exception.Message;
        LogLevel = LogLevel.Error;
        Message = ErrorMessages.UsernameAlreadyExists;
    }

    private void HandleException(CouldNotRegisterException exception)
    {
        Code = CouldNotRegisterException.Code;
        Status = (int)HttpStatusCode.Conflict;
        Type = "https://datatracker.ietf.org/doc/html/rfc7231#section-6.5.8";
        Title = exception.Message;
        LogLevel = LogLevel.Error;
        Message = ErrorMessages.CouldNotRegister;
    }
}