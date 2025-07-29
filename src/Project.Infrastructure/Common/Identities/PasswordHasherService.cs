using Project.Application.Common.Exceptions;
using Project.Application.Common.Identities;
using BC = BCrypt.Net.BCrypt;

namespace Project.Infrastructure.Common.Identities;

public class PasswordHasherService : IPasswordHasherService
{
    public string Hash(string password, int workFactor = 12)
    {
        ValidatePassword(password);
        return BC.HashPassword(password, workFactor);
    }

    public bool Verify(string providedPassword, string hashedPassword)
    {
        ValidatePassword(providedPassword);
        return BC.Verify(providedPassword, hashedPassword);
    }

    public Task<string> HashAsync(string password, int workFactor = 12, CancellationToken cancellationToken = default) =>
        Task.Run(() => BC.HashPassword(password, workFactor), cancellationToken);

    public Task<bool> VerifyAsync(string providedPassword, string hashedPassword, CancellationToken cancellationToken = default) =>
        Task.Run(() => BC.Verify(providedPassword, hashedPassword), cancellationToken);

    private static void ValidatePassword(string password)
    {
        if (string.IsNullOrWhiteSpace(password))
            throw new BadRequestException(nameof(password), "is required.");
    }
}