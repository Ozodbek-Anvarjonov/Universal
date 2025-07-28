using System.Globalization;
using System.Net;

namespace Project.Application.Common.Exceptions;

public class BadRequestException : AppException
{
    private const string ErrorType = "https://httpstatuses.com/400/";
    private const string ErrorTitle = "Bad Request";
    private const string ErrorMessage = "The request could not be understood or was missing required parameters.";
    private const string ErrorMessageFormat = "{0} is invalid. Reason: {1}.";

    public override string Type => ErrorType;
    public override HttpStatusCode StatusCode => HttpStatusCode.BadRequest;
    public override string Title => ErrorTitle;

    public BadRequestException() : base(ErrorMessage) { }

    public BadRequestException(string message)
        : this(message, inner: null) { }

    public BadRequestException(string message, Exception? inner)
        : base(GetSafeMessage(message), inner) { }

    public BadRequestException(string fieldName, string reason)
        : this(fieldName, reason, null) { }

    public BadRequestException(string fieldName, string reason, Exception? inner)
        : base(CreateDetailedMessage(fieldName, reason), inner) { }

    private static string GetSafeMessage(string message) =>
        string.IsNullOrWhiteSpace(message) ? ErrorMessage : message;


    private static string CreateDetailedMessage(string fieldName, string reason)
    {
        var safeField = fieldName?.Trim() ?? throw new ArgumentException(nameof(fieldName));
        var safeReason = reason?.Trim() ?? throw new ArgumentException(nameof(reason));

        return string.Format(
            CultureInfo.InvariantCulture,
            ErrorMessageFormat,
            safeField,
            safeReason
        );
    }
}