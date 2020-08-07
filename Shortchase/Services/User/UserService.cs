using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Localization;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using Shortchase.Dtos;
using Shortchase.Entities;
using Shortchase.Helpers;
using Shortchase.Authorization;
using System.Security.Principal;
using Shortchase.Helpers.Extensions;

namespace Shortchase.Services
{
    public class UserService : IUserService
    {
        private DataContext _context;
        private readonly SignInManager<User> signInManager;
        private readonly UserManager<User> userManager;
        private readonly RoleManager<Role> roleManager;
        private readonly AppSettings appSettings;
        private readonly IHttpContextAccessor httpContextAccessor;
        private readonly IErrorLogService errorLogService;
        private readonly IEmailSenderService emailSenderService;
        private readonly IPermissionService permissionService;
        private readonly IUserFollowService userFollowService;
        private readonly IUserRatingService userRatingService;
        private readonly IUserSubscriptionService userSubscriptionService;

        public UserService
        (
            DataContext context,
            SignInManager<User> signInManager,
            UserManager<User> userManager,
            RoleManager<Role> roleManager,
            IOptions<AppSettings> appSettings,
            IHttpContextAccessor httpContextAccessor,
            IErrorLogService logService,
            IEmailSenderService emailSenderService,
            IPermissionService permissionService,
            IUserFollowService userFollowService,
            IUserRatingService userRatingService,
            IUserSubscriptionService userSubscriptionService
        )
        {
            _context = context;
            this.signInManager = signInManager;
            this.userManager = userManager;
            this.roleManager = roleManager;
            this.appSettings = appSettings.Value;
            this.httpContextAccessor = httpContextAccessor;
            this.errorLogService = logService;
            this.emailSenderService = emailSenderService;
            this.permissionService = permissionService;
            this.userFollowService = userFollowService;
            this.userRatingService = userRatingService;
            this.userSubscriptionService = userSubscriptionService;
        }

        public async Task<bool> AuthenticateAsync(string id, string password, bool RememberMe)
        {
            try
            {
                if (string.IsNullOrEmpty(id) || string.IsNullOrEmpty(password))
                    return false;

                User CurrentUser = await userManager.FindByEmailAsync(id).ConfigureAwait(false);
                if (CurrentUser == null) CurrentUser = await userManager.FindByNameAsync(id).ConfigureAwait(false);
                if (CurrentUser == null) return false;

                var result = await signInManager.PasswordSignInAsync(CurrentUser.UserName, password, RememberMe, lockoutOnFailure: true).ConfigureAwait(false);
                return result.Succeeded;
            }
            catch (Exception e)
            {
                await errorLogService.InsertException(e).ConfigureAwait(false);
                throw;
            }
        }

        public async Task<ICollection<User>> GetAll()
        {
            try
            {
                return await _context.Users.Where(i => !i.IsDeleted).ToListAsync().ConfigureAwait(false);
            }
            catch (Exception e)
            {
                await errorLogService.InsertException(e).ConfigureAwait(false);
                throw;
            }
        }

        public async Task<IQueryable<User>> GetAllQ()
        {
            try
            {
                return _context.Users;
            }
            catch (Exception e)
            {
                await errorLogService.InsertException(e).ConfigureAwait(false);
                throw;
            }
        }

        public async Task<User> GetById(Guid id)
        {
            try
            {
                return await userManager.FindByIdAsync(id.ToString()).ConfigureAwait(false);
            }
            catch (Exception e)
            {
                await errorLogService.InsertException(e).ConfigureAwait(false);
                throw;
            }
        }


        public async Task<UserProfilePageDto> GetProfileById(Guid id, Guid? CurrentUserId)
        {
            try
            {
                User user = await _context.Users.Include(i => i.Permissions).Include(i => i.PhoneCountry).Include(i => i.BetListings).Where(i => i.Id == id).SingleOrDefaultAsync().ConfigureAwait(false);
                if (user == null) throw new Exception("No user found with given Id");
                UserProfilePageDto data = new UserProfilePageDto { 
                    Id = user.Id,
                    Intro = user.BioDescription,
                    From = user.PhoneCountry == null ? "" : user.PhoneCountry.Name,
                    JoinedSince = user.RowDate,
                    Name = user.FirstName + " " + user.LastName,
                    LastPosted = null,
                    HitRate = 0.00m,
                    IsFollowing = null,
                    Picture = user.ProfilePicture,
                    UserRating = null
                };
                data.Listings = await _context.BetListings.Include(i => i.Category).Include(i => i.SubCategory).Include(i => i.Pick).Include(i => i.Market).Include(i => i.Tip).Include(i => i.Bookmaker).Where(i => !i.Deleted && i.PostedbyId == user.Id).OrderByDescending(o => o.RowDate).ToListAsync().ConfigureAwait(false);

                if (CurrentUserId.HasValue && CurrentUserId != id) {
                    //Check if user is following (FALSE FOR NOW)
                    data.IsFollowing = await userFollowService.IsFollowing(CurrentUserId.Value, id).ConfigureAwait(false);
                    data.UserRating = await userRatingService.GetByFromTo(CurrentUserId.Value, id).ConfigureAwait(false);
                }

                data.IsAdmin = await permissionService.HasPermission(user.Id, Permission.Admin).ConfigureAwait(false);
                data.Rating = await userRatingService.GetAverageRatingCorrect(user.Id).ConfigureAwait(false);

                data.Picks = data.Listings.Count;
                data.Wins = data.Listings.Count(i => i.IsCorrect.HasValue && i.IsCorrect.Value);

                if (data.Listings.Count > 0) {
                    data.HitRate = (Convert.ToDecimal(data.Wins) / Convert.ToDecimal(data.Picks)) * 100;
                    data.LastPosted = data.Listings.First().RowDate;
                    data.Last10Picks = data.Listings.Take(10).Select(i => i.IsCorrect).ToList();
                }
                
                return data;
            }
            catch (Exception e)
            {
                await errorLogService.InsertException(e).ConfigureAwait(false);
                throw;
            }
        }

        public async Task<int> GetUserRating(Guid id)
        {
            try
            {
               
                return await userRatingService.GetAverageRating(id).ConfigureAwait(false);

            }
            catch (Exception e)
            {
                await errorLogService.InsertException(e).ConfigureAwait(false);
                throw;
            }
        }

        public async Task<ICollection<User>> GetAdminList()
        {
            try
            {
                Permissions adminPermission = await _context.Permissions.Where(p => p.Name == UserType.Admin).SingleOrDefaultAsync().ConfigureAwait(false);
                if (adminPermission == null) throw new Exception("Admin permission not found");
                ICollection<User> admins = await _context.Users.Include(u => u.PhoneCountry).Where(u => u.Permissions.Any(up => up.PermissionsId == adminPermission.Id) && !u.IsDeleted).ToListAsync().ConfigureAwait(false);
                return admins;
            }
            catch (Exception e)
            {
                await errorLogService.InsertException(e).ConfigureAwait(false);
                throw;
            }
        }

        public async Task<ICollection<User>> GetBoisterousUserList()
        {
            try
            {
                var permissions = await _context.Permissions.Where(p => p.Name == UserType.Bettor || p.Name == UserType.Capper).ToListAsync().ConfigureAwait(false);
                if (permissions == null || !permissions.Any()) throw new Exception("Permissions not found");
                ICollection<User> users = await _context.Users.Include(i => i.PhoneCountry).Where(u => u.Permissions.Any(up => permissions.Contains(up.Permissions)) && u.UserSubscriptions.Where(us => u.Id == us.UserId && SubscriptionPlanType.Boisterous == us.Type && !us.Deleted && us.SubscriptionEnd.Date >= DateTime.Now.Date && us.PaymentStatus != UserSubscriptionPaymentStatus.Cancelled && us.PaymentStatus != UserSubscriptionPaymentStatus.Rejected).SingleOrDefault() != null && !u.IsDeleted).ToListAsync().ConfigureAwait(false);
                return users;
            }
            catch (Exception e)
            {
                await errorLogService.InsertException(e).ConfigureAwait(false);
                throw;
            }
        }

        public async Task<int> GetBoisterousUserListCount()
        {
            try
            {
                var permissions = await _context.Permissions.Where(p => p.Name == UserType.Bettor || p.Name == UserType.Capper).ToListAsync().ConfigureAwait(false);
                if (permissions == null || !permissions.Any()) throw new Exception("Permissions not found");
                int users = await _context.Users.Include(i => i.PhoneCountry).CountAsync(u => u.Permissions.Any(up => permissions.Contains(up.Permissions)) && u.UserSubscriptions.Where(us => u.Id == us.UserId && SubscriptionPlanType.Boisterous == us.Type && !us.Deleted && us.SubscriptionEnd.Date >= DateTime.Now.Date && us.PaymentStatus != UserSubscriptionPaymentStatus.Cancelled && us.PaymentStatus != UserSubscriptionPaymentStatus.Rejected).SingleOrDefault() != null && !u.IsDeleted).ConfigureAwait(false);
                return users;
            }
            catch (Exception e)
            {
                await errorLogService.InsertException(e).ConfigureAwait(false);
                throw;
            }
        }
        public async Task<int> GetProUserListCount()
        {
            try
            {
                var permissions = await _context.Permissions.Where(p => p.Name == UserType.Bettor || p.Name == UserType.Capper).ToListAsync().ConfigureAwait(false);
                if (permissions == null || !permissions.Any()) throw new Exception("Permissions not found");
                int users = await _context.Users.Include(i => i.PhoneCountry).CountAsync(u => u.Permissions.Any(up => permissions.Contains(up.Permissions)) && u.UserSubscriptions.Where(us => u.Id == us.UserId && SubscriptionPlanType.ShortchasePro == us.Type && !us.Deleted && us.SubscriptionEnd.Date >= DateTime.Now.Date && us.PaymentStatus != UserSubscriptionPaymentStatus.Cancelled && us.PaymentStatus != UserSubscriptionPaymentStatus.Rejected).SingleOrDefault() != null && !u.IsDeleted).ConfigureAwait(false);
                return users;
            }
            catch (Exception e)
            {
                await errorLogService.InsertException(e).ConfigureAwait(false);
                throw;
            }
        }


        public async Task<ICollection<User>> GetProUserList()
        {
            try
            {
                var permissions = await _context.Permissions.Where(p => p.Name == UserType.Bettor || p.Name == UserType.Capper).ToListAsync().ConfigureAwait(false);
                if (permissions == null || !permissions.Any()) throw new Exception("Permissions not found");
                var users = await _context.Users.Include(i => i.PhoneCountry).Where(u => u.Permissions.Any(up => permissions.Contains(up.Permissions)) && u.UserSubscriptions.Where(us => u.Id == us.UserId && SubscriptionPlanType.ShortchasePro == us.Type && !us.Deleted && us.SubscriptionEnd.Date >= DateTime.Now.Date && us.PaymentStatus != UserSubscriptionPaymentStatus.Cancelled && us.PaymentStatus != UserSubscriptionPaymentStatus.Rejected).SingleOrDefault() != null && !u.IsDeleted).ToListAsync().ConfigureAwait(false);
                return users;
            }
            catch (Exception e)
            {
                await errorLogService.InsertException(e).ConfigureAwait(false);
                throw;
            }
        }

        public async Task<ICollection<User>> GetUserList()
        {
            try
            {
                var permissions = await _context.Permissions.Where(p => p.Name == UserType.Bettor || p.Name == UserType.Capper).ToListAsync().ConfigureAwait(false);
                if (permissions == null || !permissions.Any()) throw new Exception("Permissions not found");
                ICollection<User> users = await _context.Users.Include(i => i.Payouts).Include(i => i.PhoneCountry).Where(u => u.Permissions.Any(up => permissions.Contains(up.Permissions)) && !u.IsDeleted).ToListAsync().ConfigureAwait(false);
                return users;
            }
            catch (Exception e)
            {
                await errorLogService.InsertException(e).ConfigureAwait(false);
                throw;
            }
        }


        public async Task<int> GetUserListCount()
        {
            try
            {
                var permissions = await _context.Permissions.Where(p => p.Name == UserType.Bettor || p.Name == UserType.Capper).ToListAsync().ConfigureAwait(false);
                if (permissions == null || !permissions.Any()) throw new Exception("Permissions not found");
                int users = await _context.Users.Include(i => i.Payouts).Include(i => i.PhoneCountry).CountAsync(u => u.Permissions.Any(up => permissions.Contains(up.Permissions)) && !u.IsDeleted).ConfigureAwait(false);
                return users;
            }
            catch (Exception e)
            {
                await errorLogService.InsertException(e).ConfigureAwait(false);
                throw;
            }
        }


        public async Task<int> GetBettorCapperCount()
        {
            try
            {
                var permissions = await _context.Permissions.Where(p => p.Name == UserType.Bettor || p.Name == UserType.Capper).ToListAsync().ConfigureAwait(false);
                if (permissions == null || !permissions.Any()) throw new Exception("Permissions not found");
                ICollection<User> users = await _context.Users.Include(i => i.PhoneCountry).Where(u => u.Permissions.Any(up => permissions.Contains(up.Permissions)) && !u.IsDeleted).ToListAsync().ConfigureAwait(false);
                return users.Count;
            }
            catch (Exception e)
            {
                await errorLogService.InsertException(e).ConfigureAwait(false);
                throw;
            }
        }



        public async Task<int> GetBoisterousBettorCount()
        {
            try
            {
                var users = await _context.UserSubscriptions.Include(i => i.User).Where(u => SubscriptionPlanType.Boisterous == u.Type && !u.Deleted && u.SubscriptionEnd > DateTime.UtcNow && u.PaymentStatus != UserSubscriptionPaymentStatus.Cancelled && u.PaymentStatus != UserSubscriptionPaymentStatus.Rejected && !u.User.IsDeleted).ToListAsync().ConfigureAwait(false);
                return users.Count;
            }
            catch (Exception e)
            {
                await errorLogService.InsertException(e).ConfigureAwait(false);
                throw;
            }
        }

        public async Task<int> GetShortchaseProCapperCount()
        {
            try
            {
                var users = await _context.UserSubscriptions.Include(i => i.User).Where(u => SubscriptionPlanType.ShortchasePro == u.Type && !u.Deleted && u.SubscriptionEnd > DateTime.UtcNow && u.PaymentStatus != UserSubscriptionPaymentStatus.Cancelled && u.PaymentStatus != UserSubscriptionPaymentStatus.Rejected && !u.User.IsDeleted).ToListAsync().ConfigureAwait(false);
                return users.Count;
            }
            catch (Exception e)
            {
                await errorLogService.InsertException(e).ConfigureAwait(false);
                throw;
            }
        }

        public async Task<bool> CreateAsync(User user, string password)
        {
            try
            {
                // validation
                if (user == null)
                    throw new Exception("User information is empty");

                if (!string.IsNullOrWhiteSpace(user.UserName))
                {
                    if (user.UserName.Contains('@', StringComparison.InvariantCulture))
                        throw new Exception("Username cannot use the @ Symbol");
                    if (_context.Users.Any(x => x.UserName == user.UserName))

                        throw new Exception($"Username \"{user.UserName}\" is already taken");
                }
                else user.UserName = user.Email;

                if (string.IsNullOrWhiteSpace(password))
                    throw new Exception("Password is required");

                if (string.IsNullOrWhiteSpace(user.Email))
                    throw new Exception("E-mail is required");

                if (_context.Users.Any(x => x.Email == user.Email))
                    throw new Exception("E-mail \"" + user.Email + "\" is already taken");

                string ConfirmationCode = GenerateConfirmationCode(6);

                user.ConfirmationCode = HashCode(ConfirmationCode);
                user.RowDate = DateTime.Now.ToUniversalTime();

                var result = await userManager.CreateAsync(user, password).ConfigureAwait(false);

                return result.Succeeded;
            }
            catch (Exception e)
            {
                await errorLogService.InsertException(e).ConfigureAwait(false);
                throw;
            }
        }

        /// <summary>
        /// Updates the current user with new information. Should not be used to update other users
        /// </summary>
        /// <param name="user">User information to be updated</param>
        /// <returns></returns>
        [Authorize]
        public async Task<bool> UpdateAsync(EditUserDto user)
        {
            Guid? UserId = null;
            try
            {
                if (user == null) throw new Exception("New user information cannot be null");
                var CurrentUser = await userManager.FindByIdAsync(httpContextAccessor.HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier)).ConfigureAwait(false);

                if (CurrentUser == null) throw new Exception("User not found");

                UserId = CurrentUser.Id;
                // update user properties
                if (!string.IsNullOrWhiteSpace(user.FirstName)) CurrentUser.FirstName = user.FirstName;
                if (!string.IsNullOrWhiteSpace(user.LastName)) CurrentUser.LastName = user.LastName;
                if (!string.IsNullOrWhiteSpace(user.PhoneNumber)) CurrentUser.PhoneNumber = user.PhoneNumber;

                bool result1 = false;
                bool result2 = false;

                if (!string.IsNullOrWhiteSpace(user.OldPassword) && !string.IsNullOrWhiteSpace(user.NewPassword))
                {
                    var r1 = await userManager.ChangePasswordAsync(CurrentUser, user.OldPassword, user.NewPassword).ConfigureAwait(false);
                    result1 = r1.Succeeded;
                }
                else result1 = true;

                var r2 = await userManager.UpdateAsync(CurrentUser).ConfigureAwait(false);
                result2 = r2.Succeeded;

                return result1 && result2;
            }
            catch (Exception e)
            {
                await errorLogService.InsertException(e, UserId).ConfigureAwait(false);
                throw;
            }
        }

        public async Task Delete(Guid id)
        {
            try
            {
                User user = _context.Users.Find(id);
                if (user != null)
                {
                    _context.Users.Remove(user);
                    await _context.SaveChangesAsync().ConfigureAwait(false);
                }
            }
            catch (Exception e)
            {
                await errorLogService.InsertException(e).ConfigureAwait(false);
                throw;
            }
        }

        public async Task<bool> CreateRoleAsync(Role role)
        {
            try
            {
                var result = await roleManager.CreateAsync(role).ConfigureAwait(false);
                if (!result.Succeeded)
                {
                    throw new Exception(result.Errors.FirstOrDefault().Description);
                }
                return true;
            }
            catch (Exception e)
            {
                if (!(await errorLogService.InsertException(e).ConfigureAwait(false))) throw;
                return false;
            }
        }

        public async Task<bool> AddToRoleAsync(User user, IEnumerable<string> roles)
        {
            Guid? UserId = null;
            try
            {
                if (user == null) throw new Exception("User not found");
                UserId = user.Id;
                var result = await userManager.AddToRolesAsync(user, roles).ConfigureAwait(false);
                if (!result.Succeeded)
                {
                    throw new Exception(result.Errors.FirstOrDefault().Description);
                }
                return true;
            }
            catch (Exception e)
            {
                if (!(await errorLogService.InsertException(e, UserId).ConfigureAwait(false))) throw;
                return false;
            }
        }

        public async Task<bool> ConfirmEmailCodeAsync(string ConfirmationCode, string Email = "")
        {
            Guid? UserId = null;
            try
            {
                if (string.IsNullOrWhiteSpace(ConfirmationCode)) throw new Exception("No Confirmation Code");
                var User = await userManager.FindByIdAsync(httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value).ConfigureAwait(false);
                if (User == null && !string.IsNullOrWhiteSpace(Email)) User = await userManager.FindByEmailAsync(Email).ConfigureAwait(false);
                if (User == null) throw new NullReferenceException("User not Found");
                UserId = User.Id;
                if (User.ConfirmationCode == HashCode(ConfirmationCode))
                {
                    User.EmailConfirmed = true;
                    await userManager.AddToRoleAsync(User, Roles.User).ConfigureAwait(false);
                    await userManager.UpdateAsync(User).ConfigureAwait(false);
                    await AccessClear(User).ConfigureAwait(false);
                    return true;
                }
                else
                {
                    await AccessFailed(User).ConfigureAwait(false);
                    return false;
                }
            }
            catch (Exception e)
            {
                if (!(await errorLogService.InsertException(e, UserId).ConfigureAwait(false))) throw;
                else return false;
            }
        }

        public async Task<bool> IsEmailCodeAsync(string ConfirmationCode, string Email = "")
        {
            Guid? UserId = null;
            try
            {
                if (string.IsNullOrWhiteSpace(ConfirmationCode)) throw new Exception("No Confirmation Code");
                var User = await userManager.FindByEmailAsync(Email).ConfigureAwait(false);
                if (User == null) throw new NullReferenceException("User not Found");
                UserId = User.Id;
                return User.ConfirmationCode == HashCode(ConfirmationCode);
            }
            catch (Exception e)
            {
                if (!(await errorLogService.InsertException(e, UserId).ConfigureAwait(false))) throw;
                else return false;
            }
        }

        [Authorize]
        public async Task<bool> ResendEmailConfirmationAsync(Guid Id)
        {
            Guid UserId = Id;
            try
            {
                User CurrentUser = await userManager.FindByIdAsync(Id.ToString()).ConfigureAwait(false);
                string OldCode = CurrentUser.ConfirmationCode;
                string ConfirmationCode = GenerateConfirmationCode(6);
                CurrentUser.ConfirmationCode = HashCode(ConfirmationCode);
                var result = await userManager.UpdateAsync(CurrentUser).ConfigureAwait(false);
                if (result.Succeeded)
                {
                    bool sendSuccess = await emailSenderService.SendConfirmCode(CurrentUser.Email, ConfirmationCode).ConfigureAwait(false);
                    if (sendSuccess) return true;

                    //Could't send the email, revert to old code
                    CurrentUser.ConfirmationCode = OldCode;
                    result = await userManager.UpdateAsync(CurrentUser).ConfigureAwait(false);
                    if (result.Succeeded) return false;
                    else return false; //Can't send the email, can't revert the user to old code. Server or Network error during process, user will have to try again later.
                }
                else return false;
            }
            catch (Exception e)
            {
                await errorLogService.InsertException(e, UserId).ConfigureAwait(false);
                return false;
            }
        }

        public async Task<bool> ResetPasswordAsync(string Email, string Password)
        {
            Guid? UserId = null;
            try
            {
                User user = await userManager.FindByEmailAsync(Email).ConfigureAwait(false);
                if (user == null) return false;

                UserId = user.Id;

                string PasswordHash = user.PasswordHash;

                var result = await userManager.RemovePasswordAsync(user).ConfigureAwait(false);
                if (result.Succeeded)
                {
                    result = await userManager.AddPasswordAsync(user, Password).ConfigureAwait(false);
                    if (!result.Succeeded)
                    {
                        //Revert if failed
                        user.PasswordHash = PasswordHash;
                        await userManager.UpdateAsync(user).ConfigureAwait(false);
                    }
                    return result.Succeeded;
                }
                else return result.Succeeded;
            }
            catch (Exception e)
            {
                if (!(await errorLogService.InsertException(e, UserId).ConfigureAwait(false))) throw;
                else return false;
            }
        }

        public async Task<bool> SendForgotPwdEmailAsync(string email)
        {
            Guid? UserId = null;
            try
            {
                var CurrentUser = await userManager.FindByEmailAsync(email).ConfigureAwait(false);
                if (CurrentUser == null) return true; // Don't reveal that the user does not exist
                string OldCode = CurrentUser.ConfirmationCode;
                string ConfirmationCode = GenerateConfirmationCode(6);
                CurrentUser.ConfirmationCode = HashCode(ConfirmationCode);
                var result = await userManager.UpdateAsync(CurrentUser).ConfigureAwait(false);
                if (result.Succeeded)
                {
                    bool sendSuccess = await emailSenderService.SendForgotPasswordShortchase(CurrentUser.Email, CurrentUser.FirstName, Security.Encrypt(ConfirmationCode)).ConfigureAwait(false);//SendConfirmCode(CurrentUser.Email, ConfirmationCode).ConfigureAwait(false);
                    if (sendSuccess) return true;

                    //Could't send the email, revert to old code
                    CurrentUser.ConfirmationCode = OldCode;
                    result = await userManager.UpdateAsync(CurrentUser).ConfigureAwait(false);
                    if (result.Succeeded) return true;
                    else return false; //Can't send the email, can't revert the user to old code. Server or Network error during process, user will have to try again later.
                }
                else return false;
            }
            catch (Exception e)
            {
                await errorLogService.InsertException(e, UserId).ConfigureAwait(false);
                return false;
            }
        }




        [Authorize]
        public async Task<bool?> IsLockedAsync()
        {
            Guid? UserId = null;
            try
            {
                User User = await userManager.FindByIdAsync(httpContextAccessor.HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier)).ConfigureAwait(false);
                if (User == null) throw new NullReferenceException("No User With Specified ID");
                return await userManager.IsLockedOutAsync(User).ConfigureAwait(false);
            }
            catch (Exception e)
            {
                if (!(await errorLogService.InsertException(e, UserId).ConfigureAwait(false))) throw;
                else return null;
            }
        }

        [Authorize]
        public async Task<bool?> IsLockedAsync(User user)
        {
            Guid? UserId = null;
            try
            {
                if (user == null) throw new NullReferenceException("No User With Specified ID");
                UserId = user.Id;
                return await userManager.IsLockedOutAsync(user).ConfigureAwait(false);
            }
            catch (Exception e)
            {
                if (!(await errorLogService.InsertException(e, UserId).ConfigureAwait(false))) throw;
                else return null;
            }
        }

        // private helper methods
        private async Task AccessFailed(User user)
        {
            try
            {
                _ = await userManager.AccessFailedAsync(user).ConfigureAwait(false);
                if (user.AccessFailedCount > IdentitySettings.LockoutTries) await userManager.SetLockoutEndDateAsync(user, DateTime.Now.Add(new TimeSpan(0, IdentitySettings.LockoutTime, 0))).ConfigureAwait(false);
            }
            catch (Exception e)
            {
                await errorLogService.InsertException(e).ConfigureAwait(false);
                throw;
            }
        }

        private async Task UnlockUser(User user)
        {
            try
            {
                await userManager.SetLockoutEndDateAsync(user, DateTime.Now.Subtract(new TimeSpan(1, 0, 0))).ConfigureAwait(false);
                await AccessClear(user).ConfigureAwait(false);
            }
            catch (Exception e)
            {
                await errorLogService.InsertException(e).ConfigureAwait(false);
                throw;
            }
        }

        private async Task AccessClear(User user)
        {
            try
            {
                await userManager.ResetAccessFailedCountAsync(user).ConfigureAwait(false);
            }
            catch (Exception e)
            {
                await errorLogService.InsertException(e).ConfigureAwait(false);
                throw;
            }
        }

        private string GenerateConfirmationCode(int length)
        {
            const string valid = "ABCDEFGHIJKLMNOPQRSTUVWXYZ1234567890";
            StringBuilder res = new StringBuilder();
            using (RNGCryptoServiceProvider rng = new RNGCryptoServiceProvider())
            {
                byte[] uintBuffer = new byte[sizeof(uint)];

                while (length-- > 0)
                {
                    rng.GetBytes(uintBuffer);
                    uint num = BitConverter.ToUInt32(uintBuffer, 0);
                    res.Append(valid[(int)(num % (uint)valid.Length)]);
                }
            }

            return res.ToString();
        }

        private string HashCode(string Code)
        {
            if (string.IsNullOrEmpty(Code))
                return string.Empty;

            using (var sha = new SHA256Managed())
            {
                byte[] textData = Encoding.UTF8.GetBytes(Code);
                byte[] hash = sha.ComputeHash(textData);
                return BitConverter.ToString(hash).Replace("-", string.Empty, StringComparison.InvariantCulture);
            }
        }

        private static void CreatePasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt)
        {
            if (password == null) throw new ArgumentNullException(nameof(password));
            if (string.IsNullOrWhiteSpace(password)) throw new ArgumentException("Value cannot be empty or whitespace only string.", nameof(password));

            using (var hmac = new System.Security.Cryptography.HMACSHA512())
            {
                passwordSalt = hmac.Key;
                passwordHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
            }
        }

        private static bool VerifyPasswordHash(string password, byte[] storedHash, byte[] storedSalt)
        {
            if (password == null) throw new ArgumentNullException(nameof(password));
            if (string.IsNullOrWhiteSpace(password)) throw new ArgumentException("Value cannot be empty or whitespace only string.", nameof(password));
            if (storedHash.Length != 64) throw new ArgumentException("Invalid length of password hash (64 bytes expected).", "passwordHash");
            if (storedSalt.Length != 128) throw new ArgumentException("Invalid length of password salt (128 bytes expected).", "passwordHash");

            using (var hmac = new System.Security.Cryptography.HMACSHA512(storedSalt))
            {
                var computedHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
                for (int i = 0; i < computedHash.Length; i++)
                {
                    if (computedHash[i] != storedHash[i]) return false;
                }
            }

            return true;
        }

        public async Task SignOutAsync()
        {
            await Microsoft.AspNetCore.Authentication.AuthenticationHttpContextExtensions.SignOutAsync(httpContextAccessor.HttpContext, IdentityConstants.ApplicationScheme);
            await Microsoft.AspNetCore.Authentication.AuthenticationHttpContextExtensions.SignOutAsync(httpContextAccessor.HttpContext, IdentityConstants.ExternalScheme);
            await Microsoft.AspNetCore.Authentication.AuthenticationHttpContextExtensions.SignOutAsync(httpContextAccessor.HttpContext, IdentityConstants.TwoFactorRememberMeScheme);
            await Microsoft.AspNetCore.Authentication.AuthenticationHttpContextExtensions.SignOutAsync(httpContextAccessor.HttpContext, IdentityConstants.TwoFactorUserIdScheme);
        }

        public async Task<bool> HasAcceptedCookies(Guid Id)
        {
            try
            {
                User user = await _context.Users.Where(i => i.Id == Id).SingleOrDefaultAsync().ConfigureAwait(false);
                if (user != null)
                {
                    return user.AcceptedCookies;
                }
                else return false;
            }
            catch (Exception e)
            {
                await errorLogService.InsertException(e).ConfigureAwait(false);
                throw;
            }
        }

        public async Task<bool> AcceptCookies(Guid Id)
        {
            try
            {
                User user = await _context.Users.Where(i => i.Id == Id).SingleOrDefaultAsync().ConfigureAwait(false);
                if (user != null)
                {
                    user.AcceptedCookies = true;

                    _context.Entry(user).State = EntityState.Modified;
                    await _context.SaveChangesAsync().ConfigureAwait(false);
                    return true;
                }
                else return false;
            }
            catch (Exception e)
            {
                await errorLogService.InsertException(e).ConfigureAwait(false);
                throw;
            }
        }
        #region Shortchase methods

        public async Task<User> GetConfirmationAccount(string ConfirmationCode, bool CodeIsHashed = true)
        {
            try
            {
                if (!CodeIsHashed) {
                    ConfirmationCode = HashCode(ConfirmationCode);
                }
                return await _context.Users.Where(u => u.EmailConfirmed == false && u.ConfirmationCode == ConfirmationCode && !u.IsDeleted).SingleOrDefaultAsync().ConfigureAwait(false);
            }
            catch (Exception e)
            {
                await errorLogService.InsertException(e).ConfigureAwait(false);
                throw;
            }
        }
        public async Task<User> GetAccountFromConfirmationCode(string ConfirmationCode, bool CodeIsHashed = true)
        {
            try
            {
                if (!CodeIsHashed)
                {
                    ConfirmationCode = HashCode(ConfirmationCode);
                }
                return await _context.Users.Where(u => u.ConfirmationCode == ConfirmationCode && !u.IsDeleted).SingleOrDefaultAsync().ConfigureAwait(false);
            }
            catch (Exception e)
            {
                await errorLogService.InsertException(e).ConfigureAwait(false);
                throw;
            }
        }

        public async Task<bool> ConfirmAccountAsync(User user)
        {
            try
            {
                if (user == null) throw new Exception("No user found.");

                user.EmailConfirmed = true;
                var result = await userManager.UpdateAsync(user).ConfigureAwait(false);

                return result.Succeeded;
            }
            catch (Exception e)
            {
                await errorLogService.InsertException(e).ConfigureAwait(false);
                throw;
            }
        }


        public async Task<bool> AuthenticateWithouPasswordAsync(User user, bool RememberMe)
        {
            try
            {
                if (user == null) return false;

                await signInManager.SignInAsync(user, RememberMe).ConfigureAwait(false);
                return true;
            }
            catch (Exception e)
            {
                await errorLogService.InsertException(e).ConfigureAwait(false);
                return false;
            }
        }


        public async Task<bool> CheckIfUserConfirmedEmail(string email)
        {
            try
            {
                User user = await _context.Users.Where(u => u.Email == email && u.EmailConfirmed && !u.IsDeleted).SingleOrDefaultAsync().ConfigureAwait(false);
                if (user != null)
                {
                    return true;
                }
                else return false;
            }
            catch (Exception e)
            {
                await errorLogService.InsertException(e).ConfigureAwait(false);
                return false;
            }
        }

        private async Task<bool> IsReferralCodeUsed(string code)
        {
            try
            {
                ICollection<User> users = await _context.Users.Where(u => u.ReferralCode == code && !u.IsDeleted).ToListAsync().ConfigureAwait(false);
                if (users.Any())
                {
                    return true;
                }
                else
                {
                    return false;
                }

            }
            catch (Exception e)
            {
                await errorLogService.InsertException(e).ConfigureAwait(false);
                return true;
            }
        }

        private async Task<bool> IsCouponCodeUsed(string code)
        {
            try
            {
                ICollection<RewardsClaimedMapping> coupons = await _context.RewardsClaimedMappings.Include(i => i.User).Where(u => u.CouponCode == code && !u.User.IsDeleted).ToListAsync().ConfigureAwait(false);
                if (coupons.Any())
                {
                    return true;
                }
                else
                {
                    return false;
                }

            }
            catch (Exception e)
            {
                await errorLogService.InsertException(e).ConfigureAwait(false);
                return true;
            }
        }

        public async Task<User> GetByEmail(string email)
        {
            try
            {
                User user = await _context.Users.Include(u => u.PhoneCountry).Where(u => u.Email == email && !u.IsDeleted).SingleOrDefaultAsync().ConfigureAwait(false);
                return user;
            }
            catch (Exception e)
            {
                await errorLogService.InsertException(e).ConfigureAwait(false);
                return null;
            }
        }


        public async Task<ICollection<MassPaypalPayoutRetrieveUserListItemDto>> GetAllMassPayout()
        {
            try
            {
                ICollection<MassPaypalPayoutRetrieveUserListItemDto> users = await _context.Users.Where(u => !u.IsDeleted).Select(i => new MassPaypalPayoutRetrieveUserListItemDto { Id = i.Id, Email = i.Email, Name = i.FirstName + " " + i.LastName, PaypalEmail = i.PaypalAccountEmail }).ToListAsync().ConfigureAwait(false);
                return users;
            }
            catch (Exception e)
            {
                await errorLogService.InsertException(e).ConfigureAwait(false);
                return null;
            }
        }

        public async Task<UserRegistrationCompleteDto> CreateShortchaseUserAsync(User user, string password)
        {
            UserRegistrationCompleteDto res = new UserRegistrationCompleteDto();
            try
            {
                // validation
                if (user == null)
                    throw new Exception("User information is empty");

                if (!string.IsNullOrWhiteSpace(user.UserName))
                {
                    if (user.UserName.Contains('@', StringComparison.InvariantCulture))
                        throw new Exception("Username cannot use the @ Symbol");
                    if (_context.Users.Any(x => x.UserName == user.UserName))

                        throw new Exception($"Username \"{user.UserName}\" is already taken");
                }
                else user.UserName = user.Email;

                if (string.IsNullOrWhiteSpace(password))
                    throw new Exception("Password is required");

                if (string.IsNullOrWhiteSpace(user.Email))
                    throw new Exception("E-mail is required");

                if (_context.Users.Any(x => x.Email == user.Email))
                    throw new Exception("E-mail \"" + user.Email + "\" is already taken");

                string ConfirmationCode = GenerateConfirmationCode(6);
                res.Code = ConfirmationCode;
                int countTimesTried = 1;
                int codeCharacters = 6;
                string ReferralCode = GenerateConfirmationCode(codeCharacters);
                while (IsReferralCodeUsed(ReferralCode).Result)
                {
                    countTimesTried++;
                    if (countTimesTried == 5)
                    {
                        codeCharacters++;
                    }
                    else if (countTimesTried == 7)
                    {
                        codeCharacters++;
                    }
                    else if (countTimesTried == 9)
                    {
                        codeCharacters++;
                    }
                    ReferralCode = GenerateConfirmationCode(codeCharacters);
                }
                user.ReferralCode = ReferralCode;
                user.ConfirmationCode = HashCode(ConfirmationCode);
                user.RowDate = DateTime.Now.ToUniversalTime();
                user.IsActive = true;

                var result = await userManager.CreateAsync(user, password).ConfigureAwait(false);
                if (result.Succeeded)
                {
                    var bettorPermissionResult = await permissionService.AddToUser(user, Permission.Bettor).ConfigureAwait(false);

                    if (bettorPermissionResult)
                    {
                        res.Result = true;
                        return res;
                    }
                    else
                    {
                        throw new Exception("Error adding user to bettor role.");
                    }
                }
                else
                {
                    string errMsg = "";
                    foreach (var err in result.Errors) {
                        errMsg += err + ";";
                    }
                    throw new Exception(errMsg);
                }
            }
            catch (Exception e)
            {
                await errorLogService.InsertException(e).ConfigureAwait(false);

                res.Code = null;
                res.Result = false;
                return res;
            }
        }


        public async Task<bool> CreateShortchaseAdministratorUserAsync(User user, string password)
        {
            try
            {
                // validation
                if (user == null)
                    throw new Exception("User information is empty");

                if (!string.IsNullOrWhiteSpace(user.UserName))
                {
                    if (user.UserName.Contains('@', StringComparison.InvariantCulture))
                        throw new Exception("Username cannot use the @ Symbol");
                    if (_context.Users.Any(x => x.UserName == user.UserName))

                        throw new Exception($"Username \"{user.UserName}\" is already taken");
                }
                else user.UserName = user.Email;

                if (string.IsNullOrWhiteSpace(password))
                    throw new Exception("Password is required");

                if (string.IsNullOrWhiteSpace(user.Email))
                    throw new Exception("E-mail is required");

                if (_context.Users.Any(x => x.Email == user.Email))
                    throw new Exception("E-mail \"" + user.Email + "\" is already taken");

                user.EmailConfirmed = true;
                user.PhoneNumberConfirmed = true;
                user.IsActive = true;
                user.ReferralCode = null;
                user.ConfirmationCode = null;
                user.RowDate = DateTime.Now.ToUniversalTime();

                var result = await userManager.CreateAsync(user, password).ConfigureAwait(false);
                if (result.Succeeded)
                {
                    var adminPermissionResult = await permissionService.AddToUser(user, Permission.Admin).ConfigureAwait(false);

                    if (adminPermissionResult)
                    {
                        return true;
                    }
                    else
                    {
                        throw new Exception("Error adding user to bettor role.");
                    }
                }
                else
                {
                    throw new Exception("Error creating user");
                }
            }
            catch (Exception e)
            {
                await errorLogService.InsertException(e).ConfigureAwait(false);
                return false;
            }
        }



        public async Task<bool> CreateShortchaseStandardUserAsync(User user, string password)
        {
            try
            {
                // validation
                if (user == null)
                    throw new Exception("User information is empty");

                if (!string.IsNullOrWhiteSpace(user.UserName))
                {
                    if (user.UserName.Contains('@', StringComparison.InvariantCulture))
                        throw new Exception("Username cannot use the @ Symbol");
                    if (_context.Users.Any(x => x.UserName == user.UserName))

                        throw new Exception($"Username \"{user.UserName}\" is already taken");
                }
                else user.UserName = user.Email;

                if (string.IsNullOrWhiteSpace(password))
                    throw new Exception("Password is required");

                if (string.IsNullOrWhiteSpace(user.Email))
                    throw new Exception("E-mail is required");

                if (_context.Users.Any(x => x.Email == user.Email))
                    throw new Exception("E-mail \"" + user.Email + "\" is already taken");

                user.EmailConfirmed = false;
                user.PhoneNumberConfirmed = false;
                user.IsActive = true;
                user.ReferralCode = null;
                user.ConfirmationCode = null;
                user.RowDate = DateTime.Now.ToUniversalTime();

                var result = await userManager.CreateAsync(user, password).ConfigureAwait(false);
                if (result.Succeeded)
                {
                    var permissionResult = await permissionService.AddToUser(user, Permission.Bettor).ConfigureAwait(false);

                    if (permissionResult)
                    {
                        return true;
                    }
                    else
                    {
                        throw new Exception("Error adding user to bettor role.");
                    }
                }
                else
                {
                    throw new Exception("Error creating user");
                }
            }
            catch (Exception e)
            {
                await errorLogService.InsertException(e).ConfigureAwait(false);
                return false;
            }
        }

        public async Task<UserRegistrationCompleteDto> ResetVerificationCode(User user)
        {
            UserRegistrationCompleteDto res = new UserRegistrationCompleteDto();
            try
            {
                string ConfirmationCode = GenerateConfirmationCode(6);
                user.ConfirmationCode = HashCode(ConfirmationCode);
                
                var result = await userManager.UpdateAsync(user).ConfigureAwait(false);
                if (result.Succeeded)
                {
                    res.Code = ConfirmationCode;
                    res.Result = true;
                }
                else {
                    res.Code = null;
                    res.Result = false;
                }
                return res;
            }
            catch (Exception e)
            {
                await errorLogService.InsertException(e).ConfigureAwait(false);
                throw;
            }
        }

        public async Task<bool> ValidateSMSCode(User user, string Code)
        {
            try
            {
                string ConfirmationCode = HashCode(Code);
                bool result = false;
                if (user.ConfirmationCode == ConfirmationCode) {
                    user.PhoneNumberConfirmed = true;
                    result = (await userManager.UpdateAsync(user).ConfigureAwait(false)).Succeeded;
                }
                return result;
            }
            catch (Exception e)
            {
                await errorLogService.InsertException(e).ConfigureAwait(false);
                throw;
            }
        }

        public async Task<bool> ChangePasswordAsync(string Email, string Password)
        {
            Guid? UserId = null;
            try
            {
                User user = await userManager.FindByEmailAsync(Email).ConfigureAwait(false);
                if (user == null) return false;

                UserId = user.Id;

                string PasswordHash = user.PasswordHash;

                var result = await userManager.RemovePasswordAsync(user).ConfigureAwait(false);
                if (result.Succeeded)
                {
                    result = await userManager.AddPasswordAsync(user, Password).ConfigureAwait(false);
                    if (!result.Succeeded)
                    {
                        //Revert if failed
                        user.PasswordHash = PasswordHash;
                        await userManager.UpdateAsync(user).ConfigureAwait(false);
                    }
                    return result.Succeeded;
                }
                else return result.Succeeded;
            }
            catch (Exception e)
            {
                if (!(await errorLogService.InsertException(e, UserId).ConfigureAwait(false))) throw;
                else return false;
            }
        }

        public async Task<bool> UpdateAsync(User user)
        {
            try
            {

                var result = await userManager.UpdateAsync(user).ConfigureAwait(false);

                return result.Succeeded;
            }
            catch (Exception e)
            {
                await errorLogService.InsertException(e).ConfigureAwait(false);
                throw;
            }
        }


        public async Task<string> AccountManagerClaimReward(CreateClaimRewardDto data, User user, string IP)
        {
            using (var transaction = await _context.Database.BeginTransactionAsync())
            {
                try
                {
                    RewardsClaimedMapping newClaim = new RewardsClaimedMapping
                    {
                        EquivalentAmount = Convert.ToDecimal(data.EquivalentAmount),
                        PointsClaimed = data.PointsClaimed,
                        UserId = data.UserId,
                        IsUsed = false,
                        UsedDate = null,
                        UsedIPAddress = null,
                        ClaimedIPAddress = IP,
                        Type = data.DiscountType
                    };
                    newClaim.RowDate = DateTime.Now.ToUniversalTime();

                    user.TotalPointsAvailable -= data.PointsClaimed;
                    user.TotalPointsClaimed += data.PointsClaimed;
                    user.WalletBalance += data.EquivalentAmount;
                    int codeCharacters = 6;
                    int countTimesTried = 1;
                    string CouponCode = GenerateConfirmationCode(codeCharacters);
                    while (IsCouponCodeUsed(CouponCode).Result)
                    {
                        countTimesTried++;
                        if (countTimesTried == 5)
                        {
                            codeCharacters++;
                        }
                        else if (countTimesTried == 7)
                        {
                            codeCharacters++;
                        }
                        else if (countTimesTried == 9)
                        {
                            codeCharacters++;
                        }
                        CouponCode = GenerateConfirmationCode(codeCharacters);
                    }
                    newClaim.CouponCode = CouponCode;

                    _context.RewardsClaimedMappings.Add(newClaim);
                    _context.Entry(newClaim).State = EntityState.Added;
                    _context.Entry(user).State = EntityState.Modified;

                    await _context.SaveChangesAsync().ConfigureAwait(false);
                    await transaction.CommitAsync().ConfigureAwait(false);
                    return CouponCode;
                }
                catch (Exception e)
                {
                    await transaction.RollbackAsync().ConfigureAwait(false);
                    return null;
                }
            }
        }

        public async Task<bool> UpdateClaims(IIdentity user)
        {
            User CurrentUser = await userManager.FindByIdAsync(user.Id()).ConfigureAwait(true);
            if (CurrentUser == null) return false;
            if (user == null) return false;
            if (!(user is ClaimsIdentity identity)) return false;
            await signInManager.RefreshSignInAsync(CurrentUser).ConfigureAwait(true);
            return true;
        }



        public async Task<User> GetUserByReferralCode(string EncryptedCode)
        {
            try
            {
                string code = Security.Decrypt(EncryptedCode);
                return await _context.Users.Where(i => i.ReferralCode == code && !i.IsDeleted).SingleOrDefaultAsync().ConfigureAwait(false);
            }
            catch (Exception e)
            {
                await errorLogService.InsertException(e).ConfigureAwait(false);
                throw;
            }
        }



        public async Task<bool> DeleteBatch(Guid[] Ids)
        {
            try
            {
                var items = await _context.Users.Where(c => Ids.Contains(c.Id)).ToListAsync().ConfigureAwait(false);
                if (items != null && items.Count > 0)
                {
                    foreach (var item in items)
                    {
                        item.IsDeleted = true;
                        item.IsActive = false;

                        _context.Entry(item).State = EntityState.Modified;
                    }
                    await _context.SaveChangesAsync().ConfigureAwait(false);

                }
                return true;
            }
            catch (Exception e)
            {
                await errorLogService.InsertException(e).ConfigureAwait(false);
                return false;
            }
        }


        public async Task<bool> SwitchStatusBatch(Guid[] Ids, bool Status)
        {
            try
            {
                var items = await _context.Users.Where(c => Ids.Contains(c.Id)).ToListAsync().ConfigureAwait(false);
                if (items != null && items.Count > 0)
                {
                    foreach (var item in items)
                    {
                        item.IsActive = Status;

                        _context.Entry(item).State = EntityState.Modified;
                    }
                    await _context.SaveChangesAsync().ConfigureAwait(false);

                }
                return true;
            }
            catch (Exception e)
            {
                await errorLogService.InsertException(e).ConfigureAwait(false);
                return false;
            }
        }




        public async Task<bool> SwitchSubscriptionStatusBatch(Guid[] Ids, bool Status)
        {
            try
            {
                var items = await _context.UserSubscriptions.Where(c => Ids.Contains(c.Id)).ToListAsync().ConfigureAwait(false);
                if (items != null && items.Count > 0)
                {
                    foreach (var item in items)
                    {
                        item.AutoRenew = Status;

                        _context.Entry(item).State = EntityState.Modified;
                    }
                    await _context.SaveChangesAsync().ConfigureAwait(false);

                }
                return true;
            }
            catch (Exception e)
            {
                await errorLogService.InsertException(e).ConfigureAwait(false);
                return false;
            }
        }

        #endregion
    }
}