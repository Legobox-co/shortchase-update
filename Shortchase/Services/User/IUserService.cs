using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Threading.Tasks;
using Shortchase.Dtos;
using Shortchase.Entities;

namespace Shortchase.Services
{
    public interface IUserService
    {
        Task<bool> AuthenticateAsync(string id, string password, bool RememberMe);

        Task SignOutAsync();

        Task<ICollection<User>> GetAll();

        Task<IQueryable<User>> GetAllQ();

        Task<User> GetById(Guid id);

        Task<bool> CreateAsync(User user, string password);

        Task<bool> UpdateAsync(EditUserDto user);

        Task Delete(Guid id);

        Task<bool> CreateRoleAsync(Role role);

        Task<bool> AddToRoleAsync(User user, IEnumerable<string> roles);

        Task<bool> ConfirmEmailCodeAsync(string ConfirmationCode, string Email = "");

        Task<bool> IsEmailCodeAsync(string ConfirmationCode, string Email = "");

        Task<bool> ResendEmailConfirmationAsync(Guid Id);

        Task<bool> SendForgotPwdEmailAsync(string email);

        Task<bool> ResetPasswordAsync(string Email, string Password);

        Task<bool?> IsLockedAsync(User user);

        Task<bool?> IsLockedAsync();

        #region Shortchase Methods
        Task<User> GetAccountFromConfirmationCode(string ConfirmationCode, bool CodeIsHashed = true);
        Task<User> GetByEmail(string email);
        Task<User> GetConfirmationAccount(string ConfirmationCode, bool CodeIsHashed = true);
        Task<bool> CheckIfUserConfirmedEmail(string email);
        Task<bool> ConfirmAccountAsync(User user);
        Task<bool> AuthenticateWithouPasswordAsync(User user, bool RememberMe);
        Task<UserRegistrationCompleteDto> CreateShortchaseUserAsync(User user, string password);
        Task<bool> ChangePasswordAsync(string Email, string Password);

        Task<bool> UpdateAsync(User user);
        Task<UserRegistrationCompleteDto> ResetVerificationCode(User user);
        Task<bool> ValidateSMSCode(User user, string Code);

        Task<string> AccountManagerClaimReward(CreateClaimRewardDto data, User user, string IP);

        Task<ICollection<User>> GetAdminList();
        Task<ICollection<User>> GetUserList();
        Task<int> GetBettorCapperCount();
        Task<int> GetBoisterousBettorCount();
        Task<int> GetShortchaseProCapperCount();
        Task<bool> CreateShortchaseAdministratorUserAsync(User user, string password);
        Task<bool> CreateShortchaseStandardUserAsync(User user, string password);

        Task<UserProfilePageDto> GetProfileById(Guid id, Guid? CurrentUserId);

        Task<bool> HasAcceptedCookies(Guid Id);
        Task<bool> AcceptCookies(Guid Id);
        Task<ICollection<User>> GetBoisterousUserList();
        Task<ICollection<User>> GetProUserList();

        Task<bool> UpdateClaims(IIdentity user);
        Task<int> GetUserRating(Guid id);

        Task<User> GetUserByReferralCode(string EncryptedCode);

        Task<ICollection<MassPaypalPayoutRetrieveUserListItemDto>> GetAllMassPayout();

        Task<int> GetUserListCount();
        Task<int> GetBoisterousUserListCount();
        Task<int> GetProUserListCount();

        Task<bool> SwitchStatusBatch(Guid[] Ids, bool Status);
        Task<bool> DeleteBatch(Guid[] Ids);

        Task<bool> SwitchSubscriptionStatusBatch(Guid[] Ids, bool Status);
        #endregion
    }
}