namespace Project.Application.Common.Identities;

public interface IPasswordHasherService
{
    string Hash(string password, int workFactor = 12);

    bool Verify(string providedPassword, string hashedPassword);

    Task<string> HashAsync(string password, int workFactor = 12, CancellationToken cancellationToken = default);

    Task<bool> VerifyAsync(string providedPassword, string hashedPassword, CancellationToken cancellationToken = default);
}