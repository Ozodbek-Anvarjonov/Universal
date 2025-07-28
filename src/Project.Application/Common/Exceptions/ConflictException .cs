using System.Globalization;
using System.Net;

namespace Project.Application.Common.Exceptions;

public class ConflictException : AppException
{
    private const string ErrorType = "https://developer.mozilla.org/en-US/docs/Web/HTTP/Reference/Status/409";
    private const string ErrorTitle = "Resource Already Exists";
    private const string ErrorMessage = "The resource you are trying to create already exists.";
    private const string ErrorMessageFormat = "{0} with {1} '{2}' already exists.";

    public override string Type => ErrorType;
    public override HttpStatusCode StatusCode => HttpStatusCode.Conflict;
    public override string Title => ErrorTitle;

    public ConflictException() : base(ErrorMessage) { }

    public ConflictException(string message)
        : this(message, null) { }

    public ConflictException(string message, Exception? inner)
        : base(GetSafeMessage(message), inner) { }

    public ConflictException(string entity, string property, string key)
        : this(entity, property, key, null) { }

    public ConflictException(string entity, string property, string key, Exception? inner)
        : base(CreateEntityMessage(entity, property, key), inner) { }

    private static string GetSafeMessage(string message) =>
        string.IsNullOrWhiteSpace(message) ? ErrorMessage : message;

    private static string CreateEntityMessage(string entity, string property, string key)
    {
        var safeEntity = entity?.Trim() ?? throw new ArgumentException(nameof(entity));
        var safeProperty = property?.Trim() ?? throw new ArgumentException(nameof(property));
        var safeKey = key?.Trim() ?? throw new ArgumentException(nameof(key));

        return string.Format(
                CultureInfo.InvariantCulture,
                ErrorMessageFormat,
                safeEntity,
                safeProperty.ToUpperInvariant(),
                safeKey
            );
    }
}