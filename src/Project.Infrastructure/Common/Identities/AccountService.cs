using Project.Application.Common.Exceptions;
using Project.Application.Common.Identities;
using Project.Application.Dtos.Accounts;
using Project.Application.Services;
using Project.Domain.Entities;
using Project.Persistence.UnitOfWork.Interfaces;
using System.Net;

namespace Project.Infrastructure.Common.Identities;

public class AccountService(
    IUserContext userContext,
    IUserService userService,
    IPasswordHasherService passwordHasherService
    ) : IAccountService
{
    public async Task<User> GetAsync(bool asNoTracking = true, CancellationToken cancellationToken = default)
    {
        var currentUserId = userContext.UserId
            ?? throw new CustomException("Unauthorized.", HttpStatusCode.Unauthorized);
        var currentUser = await userService.GetByIdAsync(currentUserId, asNoTracking, cancellationToken);

        return currentUser;
    }

    public async Task<bool> ChangePasswordAsync(ChangePasswordRequest request, bool saveChanges = true, CancellationToken cancellationToken = default)
    {
        var currentUserId = userContext.UserId
            ?? throw new CustomException("Unauthorized.", HttpStatusCode.Unauthorized);

        var currentUser = await userService.GetByIdAsync(currentUserId, cancellationToken: cancellationToken);

        var validatePassword = await passwordHasherService.VerifyAsync(request.CurrentPassword, currentUser.Password);
        if (!validatePassword)
            throw new BadRequestException("Invalid email or password.");

        currentUser.Password = request.NewPassword;
        var updatedUser = await userService.UpdateAsync(currentUserId, currentUser, cancellationToken: cancellationToken);

        return updatedUser != null;
    }

    public Task<bool> ForgotPasswordByEmailAsync(ForgotPasswordByEmailRequest request, CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }

    public Task<bool> ForgotPasswordByPhoneAsync(ForgotPasswordByPhoneRequest request, CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }

    public Task<bool> ResetPasswordByEmailAsync(ResetPasswordByEmailRequest request, CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }

    public Task<bool> ResetPasswordByPhoneAsync(ResetPasswordByPhoneRequest request, CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }
}