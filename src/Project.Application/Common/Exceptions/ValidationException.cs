using System.Net;

namespace Project.Application.Common.Exceptions;

public class ValidationException : AppException
{
    private const string ErrorType = "https://httpstatuses.com/400/";
    private const string ErrorTitle = "Validation Error";
    private const string ErrorMessage = "One or more validation failures occurred.";
    private readonly IDictionary<string, string[]> _errors;

    public override string Type => ErrorType;
    public override HttpStatusCode StatusCode => HttpStatusCode.BadRequest;
    public override string Title => ErrorTitle;
    public override IDictionary<string, string[]> Errors => _errors;

    public ValidationException() : base(ErrorMessage) =>
        _errors = new Dictionary<string, string[]>();

    public ValidationException(Exception? inner) : base(ErrorMessage, inner) =>
        _errors = new Dictionary<string, string[]>();

    public ValidationException(IDictionary<string, string[]> errors) : base(ErrorMessage) =>
        _errors = errors ?? new Dictionary<string, string[]>();
}