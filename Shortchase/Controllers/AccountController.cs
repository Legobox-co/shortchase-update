using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Shortchase.Authorization;
using Shortchase.Dtos;
using Shortchase.Entities;
using Shortchase.Entities.Extensions;
using Shortchase.Helpers;
using Shortchase.Helpers.Extensions;
using Shortchase.Services;

namespace Shortchase.Controllers
{
    [Authorize]
    public class AccountController : Controller
    {
        private readonly IErrorLogService errorLogService;
        private readonly IUserService userService;
        private readonly IEmailSenderService emailSenderService;
        private readonly IPermissionService permissionService;
        private readonly IWebHostEnvironment hostingEnvironment;
        private readonly IRewardsClaimedMappingService rewardsClaimedMappingService;
        private readonly ISMSSenderService smsSenderService;
        private readonly IAccessLogService accessLogService;

        public AccountController
        (
            IErrorLogService errorLogService,
            IUserService userService,
            IEmailSenderService emailSenderService,
            IPermissionService permissionService,
            IWebHostEnvironment hostingEnvironment,
            IRewardsClaimedMappingService rewardsClaimedMappingService,
            ISMSSenderService smsSenderService,
            IAccessLogService accessLogService
        )
        {
            this.errorLogService = errorLogService;
            this.userService = userService;
            this.emailSenderService = emailSenderService;
            this.permissionService = permissionService;
            this.hostingEnvironment = hostingEnvironment;
            this.rewardsClaimedMappingService = rewardsClaimedMappingService;
            this.smsSenderService = smsSenderService;
            this.accessLogService = accessLogService;
        }
        #region Generic Methods
        [AllowAnonymous]
        public IActionResult Login()
        {
            return RedirectToAction("Index", "Home");
        }

        //
        // POST: /Account/Login
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(AuthDto model)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return View(model);
                }

                // To disable/enable password failures to trigger account lockout, change boolean value to false/true
                var result = await userService.AuthenticateAsync(model.Email, model.Password, model.RememberMe).ConfigureAwait(false);
                if (result)
                {
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    ModelState.AddModelError("", "Invalid login attempt.");
                    return View(model);
                }
            }
            catch (Exception e)
            {
                await errorLogService.InsertException(e).ConfigureAwait(false);
                return RedirectToAction("Login");
            }
        }

        [AllowAnonymous]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(UserDto model)
        {
            if (ModelState.IsValid)
            {
                // map dto to entity
                var user = model.ToUser();

                try
                {
                    // save
                    var result = await userService.CreateAsync(user, model.Password);
                    if (result) return RedirectToAction("Login", "Account");
                    else throw new Exception("An unexpected error occured. Please try again later.");
                }
                catch (Exception e)
                {
                    ModelState.AddModelError(string.Empty, "Something went wrong, please try again later");
                    await errorLogService.InsertException(e);
                    return View(model);
                }
            }
            return View(model);
        }

        public IActionResult ConfirmEmail()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> ConfirmEmail(string ConfirmationCode)
        {
            Guid? UserId = null;
            try
            {
                UserId = new Guid(User.Identity.Id());
                if (string.IsNullOrEmpty(ConfirmationCode))
                {
                    return View();
                }
                else
                {
                    var result = await userService.ConfirmEmailCodeAsync(ConfirmationCode);
                    if (result)
                    {
                        return RedirectToAction("Index", "Home");
                    }
                    else
                    {
                        return View();
                    }
                }
            }
            catch (Exception e)
            {
                await errorLogService.InsertException(e, UserId);
                return View();
            }
        }

        [AllowAnonymous]
        public IActionResult Locked()
        {
            return View();
        }

        public async Task<IActionResult> ResendEmailConfirmation()
        {
            Guid? UserId = null;
            try
            {
                UserId = new Guid(User.Identity.Id());
                var result = await userService.ResendEmailConfirmationAsync(UserId.Value);
                return Json(result);
            }
            catch (Exception e)
            {
                await errorLogService.InsertException(e, UserId).ConfigureAwait(false);
                return Json(false);
            }
        }

        [AllowAnonymous]
        public IActionResult ForgotPassword()
        {
            if ((User != null) && User.Identity.IsAuthenticated) return RedirectToAction("Index", "Home"); //Someone logged in shouldn't access this page
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> ForgotPasswordEmail(ForgotPwdDto model)
        {
            try
            {
                if (ModelState.IsValid && model != null)
                {
                    var result = await userService.SendForgotPwdEmailAsync(model.Email).ConfigureAwait(false);
                    return Json(result);
                }
                return Json(false);
            }
            catch (Exception e)
            {
                await errorLogService.InsertException(e).ConfigureAwait(false);
                return Json(false);
            }
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ForgotPassword(ForgotPwdDto model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    if (!string.IsNullOrWhiteSpace(model.ConfirmationCode) && !string.IsNullOrWhiteSpace(model.NewPassword))
                    {
                        if (await userService.IsEmailCodeAsync(model.ConfirmationCode, model.Email))
                        {
                            var result = await userService.ResetPasswordAsync(model.Email, model.NewPassword);
                            if (result) return RedirectToAction("Login", "Account");
                        }
                    }
                }
                return View();
            }
            catch (Exception e)
            {
                await errorLogService.InsertException(e);
                return View();
            }
        }

        public async Task<IActionResult> EditAccount()
        {
            Guid? UserId = null;
            try
            {
                UserId = new Guid(User.Identity.Id());
                var CurrentUser = await userService.GetById(UserId.Value);
                return View(CurrentUser.ToEditUser());
            }
            catch (Exception e)
            {
                await errorLogService.InsertException(e, UserId);
                return RedirectToAction("Index", "Home");
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditAccount(EditUserDto user)
        {
            Guid? UserId = null;
            try
            {
                UserId = new Guid(User.Identity.Id());
                var result = await userService.UpdateAsync(user);
                if (!result)
                {
                    ModelState.AddModelError(string.Empty, "Could not alter your inforation. Please try again later");
                }
                return View(user);
            }
            catch (Exception e)
            {
                await errorLogService.InsertException(e, UserId);
                return RedirectToAction("Index", "Error");
            }
        }

        public IActionResult ChangePassword()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> LogOff()
        {
            Guid? UserId = null;
            try
            {
                UserId = new Guid(User.Identity.Id());
                await userService.SignOutAsync();
                return RedirectToAction("Index", "Home");
            }
            catch (Exception e)
            {
                await errorLogService.InsertException(e, UserId);
                return RedirectToAction("Index", "Error");
            }
        }

        #endregion

        #region ShortChase Specific Methods

        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> RegisterUser(UserRegisterDto model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    // map dto to entity
                    var user = model.ToUser();

                    var isLegalAge = user.BirthDate <= DateTime.Now.AddYears(-18);
                    if (isLegalAge)
                    {
                        // save
                        var result = await userService.CreateShortchaseUserAsync(user, model.Password).ConfigureAwait(true);
                        if (result.Result)
                        {
                            bool validationResult = false;
                            if (model.VerificationType == "phone")
                            {
                                string message = "Your Shortchase verification code is: " + result.Code;
                                validationResult = await smsSenderService.SendSMS(model.FullPhone, message).ConfigureAwait(true);
                            }
                            else
                            {
                                validationResult = await emailSenderService.SendRegistrationComplete(user.Email, user.FirstName, user.ConfirmationCode).ConfigureAwait(true);
                            }
                            if (validationResult)
                            {
                                return Json(new { status = true, messageTitle = "Success!", message = "Registration completed successfully." });
                            }
                            else
                            {
                                throw new Exception("There was an error sending confirmation email. Try again later.");
                            }

                        }
                        else throw new Exception("An unexpected error occured. Please try again later.");
                    }
                    else return Json(new { status = false, messageTitle = "No minors allowed", message = "You must be at least 18 years old in order to have a Shortchase account." });

                }
                else throw new Exception("Model state is invalid!");
            }
            catch (Exception e)
            {
                ModelState.AddModelError(string.Empty, "Something went wrong, please try again later");
                await errorLogService.InsertException(e).ConfigureAwait(true);
                return Json(new { status = false, messageTitle = "Error", message = e.Message });
            }
        }


        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> RegisterReferredUser(UserRegisterReferralDto model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    // map dto to entity
                    var user = model.ToUser();

                    var isLegalAge = user.BirthDate <= DateTime.Now.AddYears(-18);
                    if (isLegalAge)
                    {
                        // save
                        var result = await userService.CreateShortchaseUserAsync(user, model.Password).ConfigureAwait(true);
                        if (result.Result)
                        {
                            bool validationResult = false;
                            if (model.VerificationType == "phone")
                            {
                                string message = "Your Shortchase.com verification code is: " + result.Code;
                                validationResult = await smsSenderService.SendSMS(model.FullPhone, message).ConfigureAwait(true);
                            }
                            else
                            {
                                validationResult = await emailSenderService.SendRegistrationComplete(user.Email, user.FirstName, user.ConfirmationCode).ConfigureAwait(true);
                            }
                            if (validationResult)
                            {
                                return Json(new { status = true, messageTitle = "Success!", message = "Registration completed successfully." });
                            }
                            else
                            {
                                throw new Exception("There was an error sending confirmation email. Try again later.");
                            }

                        }
                        else throw new Exception("An unexpected error occured. Please try again later.");
                    }
                    else return Json(new { status = false, messageTitle = "No minor allowed", message = "You must be at least 18 years old in order to have a Shortchase account." });

                }
                else throw new Exception("Model state is invalid!");
            }
            catch (Exception e)
            {
                ModelState.AddModelError(string.Empty, "Something went wrong, please try again later");
                await errorLogService.InsertException(e).ConfigureAwait(true);
                return Json(new { status = false, messageTitle = "Error", message = e.Message });
            }
        }


        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> ResendSMSVerificationCode(string Email)
        {
            try
            {
                if (!string.IsNullOrWhiteSpace(Email))
                {
                    User user = await userService.GetByEmail(Email).ConfigureAwait(true);
                    if (user == null) throw new Exception("No user found.");
                    else
                    {
                        var result = await userService.ResetVerificationCode(user).ConfigureAwait(true);
                        if (result.Result)
                        {
                            string message = "Your Shortchase.com verification code is: " + result.Code;
                            string FullPhone = "+" + user.PhoneCountry.Code + user.PhoneNumber;
                            bool validation = await smsSenderService.SendSMS(FullPhone, message).ConfigureAwait(true);
                            if (validation) return Json(new { status = true, messageTitle = "Success", message = "Verification code sent!" });
                            else throw new Exception("Verification code could not be sent (2). Try again later.");
                        }
                        else throw new Exception("Verification code could not be sent. Try again later.");

                    }

                }
                else throw new Exception("No email found.");
            }
            catch (Exception e)
            {
                ModelState.AddModelError(string.Empty, "Something went wrong, please try again later");
                await errorLogService.InsertException(e).ConfigureAwait(true);
                return Json(new { status = false, messageTitle = "Error", message = e.Message });
            }
        }


        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> ValidateVerificationCode(string Email, string Code)
        {
            try
            {
                if (!string.IsNullOrWhiteSpace(Email) && !string.IsNullOrWhiteSpace(Code))
                {
                    User user = await userService.GetByEmail(Email).ConfigureAwait(true);
                    if (user == null) throw new Exception("No user found.");
                    else
                    {
                        var result = await userService.ValidateSMSCode(user, Code).ConfigureAwait(true);
                        if (result)
                        {
                            var authenticationResult = await userService.AuthenticateWithouPasswordAsync(user, true).ConfigureAwait(true);
                            if (authenticationResult)
                            {
                                return Json(new { status = true, messageTitle = "Success!", message = "Your code is successfully validated!" });
                            }
                            else throw new Exception("An error ocurred while signing in user. Try again later.");
                        }
                        else throw new Exception("Code invalid! Verify if you typed it correctly and try again.");

                    }

                }
                else throw new Exception("No email or code found.");
            }
            catch (Exception e)
            {
                ModelState.AddModelError(string.Empty, "Something went wrong, please try again later");
                await errorLogService.InsertException(e).ConfigureAwait(true);
                return Json(new { status = false, messageTitle = "Error", message = e.Message });
            }
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> LogOut()
        {
            Guid? UserId = null;
            try
            {
                UserId = new Guid(User.Identity.Id());
                var remoteIpAddress = Request.HttpContext.Connection.RemoteIpAddress;
                await accessLogService.Insert(UserId.Value, AccessLogType.SignOut, remoteIpAddress.ToString()).ConfigureAwait(true);
                await userService.SignOutAsync();
                return Json(new { status = true, messageTitle = "Success!", message = "Logged out successfully." });
            }
            catch (Exception e)
            {
                await errorLogService.InsertException(e, UserId);
                return Json(new { status = false, messageTitle = "Error!", message = "Could not be logged out." });
            }
        }


        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> SignIn(AuthDto model)
        {
            try
            {
                var isEmailConfirmed = await userService.CheckIfUserConfirmedEmail(model.Email).ConfigureAwait(true);
                User user = await userService.GetByEmail(model.Email).ConfigureAwait(true);
                if (isEmailConfirmed)
                {
                    if (user.IsActive)
                    {
                        var result = await userService.AuthenticateAsync(model.Email, model.Password, true).ConfigureAwait(true);
                        if (result)
                        {
                            var remoteIpAddress = Request.HttpContext.Connection.RemoteIpAddress;
                            await accessLogService.Insert(user.Id, AccessLogType.SignIn, remoteIpAddress.ToString()).ConfigureAwait(true);
                            return Json(new { status = true, messageTitle = "Success!", message = "Signed in successfully!" });
                        }
                        else
                        {
                            return Json(new { status = false, messageTitle = "Error!", message = "Invalid email or password!" });
                        }
                    }
                    else return Json(new { status = false, messageTitle = "Error!", message = "Your user is deactivated." });
                }
                else
                {

                    if (user.IsActive)
                    {
                        if (user.PhoneCountryId.HasValue && !string.IsNullOrWhiteSpace(user.PhoneNumber))
                        {

                            var result = await userService.AuthenticateAsync(model.Email, model.Password, true).ConfigureAwait(true);
                            if (result)
                            {
                                if (!user.PhoneNumberConfirmed) return Json(new { status = false, messageTitle = "ValidatePhone", message = "ValidatePhone" });
                                else return Json(new { status = true, messageTitle = "Success!", message = "Signed in successfully!" });
                            }
                            else
                            {
                                return Json(new { status = false, messageTitle = "Error!", message = "Invalid email or password!" });
                            }

                        }
                        else
                        {
                            var emailResult = await emailSenderService.SendRegistrationComplete(user.Email, user.FirstName, user.ConfirmationCode).ConfigureAwait(true);
                            if (emailResult)
                            {
                                return Json(new { status = false, messageTitle = "Error!", message = "An activation email has been sent to your email address." });
                            }
                            else
                            {
                                return Json(new { status = false, messageTitle = "Error!", message = "There was an error sending confirmation email. Try again later." });
                            }
                        }
                    }
                    else return Json(new { status = false, messageTitle = "Error!", message = "Your user is deactivated." });
                }
            }
            catch (Exception e)
            {
                await errorLogService.InsertException(e).ConfigureAwait(false);
                return Json(new { status = false, messageTitle = "Error!", message = "Error trying to sign in, try again later." });
            }
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> SwitchProfile(string Profile)
        {
            try
            {
                User user = await userService.GetById(new Guid(User.Identity.Id())).ConfigureAwait(true);
                Permission? permission = null;
                try
                {
                    permission = User.Identity.Permissions().Where(p => p == Permission.Bettor || p == Permission.Capper).SingleOrDefault();
                }
                catch (Exception e)
                {
                    await errorLogService.InsertException(e);
                    var result = await permissionService.RemoveFromUser(user, Permission.Capper).ConfigureAwait(true);
                    if (result)
                    {
                        var updateClaimsResult = await permissionService.UpdateClaims(User.Identity).ConfigureAwait(true);
                        if (updateClaimsResult)
                        {
                            return Json(new { status = true, messageTitle = "Success!", message = "Profile switched successfully!" });
                        }
                        else throw new Exception("Error updating claims.");
                    }
                    else throw new Exception("Error removing user from Capper profile.");
                }

                if (!permission.HasValue)
                {

                    var result = await permissionService.AddToUser(user, Permission.Bettor).ConfigureAwait(true);
                    if (result)
                    {
                        var updateClaimsResult = await permissionService.UpdateClaims(User.Identity).ConfigureAwait(true);
                        if (updateClaimsResult)
                        {
                            return Json(new { status = true, messageTitle = "Success!", message = "Profile switched successfully!" });
                        }
                        else throw new Exception("Error updating claims.");
                    }
                    else throw new Exception("Error adding user from Capper profile.");
                }


                var removeResult = await permissionService.RemoveFromUser(user, permission.Value).ConfigureAwait(true);
                if (removeResult)
                {
                    var PermissionToSwitch = Profile == Permission.Bettor.ToString() ? Permission.Bettor : Permission.Capper;
                    var addResult = await permissionService.AddToUser(user, PermissionToSwitch).ConfigureAwait(true);
                    if (addResult)
                    {
                        var updateClaimsResult = await permissionService.UpdateClaims(User.Identity).ConfigureAwait(true);
                        if (updateClaimsResult)
                        {
                            return Json(new { status = true, messageTitle = "Success!", message = "Profile switched successfully!" });
                        }
                        else throw new Exception("Error updating claims.");
                    }
                    else throw new Exception("Error adding user from profile.");
                }
                else throw new Exception("Error removing user from profile.");

            }
            catch (Exception e)
            {
                await errorLogService.InsertException(e);
                return Json(new { status = false, messageTitle = "Error!", message = "Could not be logged out." });
            }
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> AccountManagerUpdatePersonalInfo(UpdatePersonalInfoDto data)
        {
            try
            {
                var userId = new Guid(data.Id);
                User user = await userService.GetById(userId).ConfigureAwait(true);
                if (user == null) throw new Exception("No user found.");
                else
                {
                    user.BioDescription = data.BioDescription;
                    user.Company = data.Company;
                    user.LastName = data.LastName;
                    user.FirstName = data.FirstName;
                    user.PhoneNumber = data.Phone;
                    user.PhoneCountryId = data.PhoneCountry;

                    var dateSplit = data.DateOfBirth.Split("-");

                    user.BirthDate = new DateTime(Convert.ToInt32(dateSplit[0]), Convert.ToInt32(dateSplit[1]), Convert.ToInt32(dateSplit[2]));

                    var isLegalAge = user.BirthDate <= DateTime.UtcNow.AddYears(-18);
                    if (isLegalAge)
                    {
                        if (data.PictureChanged)
                        {
                            string extension = Path.GetExtension(data.PictureFile.FileName);
                            long size = data.PictureFile.Length;
                            if (FileHelper.FileIsFormSafe(extension, size))
                            {
                                string basePath = hostingEnvironment.ContentRootPath;
                                string mediaPath = FileHelper.PathCombine(basePath, "\\Media\\", "\\User");
                                Directory.CreateDirectory(mediaPath);

                                string path = FileHelper.PathCombine(mediaPath, Guid.NewGuid().ToString() + Path.GetExtension(data.PictureFile.FileName));
                                using (var fileStream = new FileStream(path, FileMode.Create))
                                {
                                    await data.PictureFile.CopyToAsync(fileStream).ConfigureAwait(true);
                                }
                                user.ProfilePicture = path;
                            }
                            else
                            {
                                return Json(new { status = false, messageTitle = "File type is not acceptable!", message = "Try to upload one of the following types: JPG or PNG" });
                            }
                        }

                        if (data.PasswordChanged)
                        {
                            if (!string.IsNullOrWhiteSpace(data.NewPassword) && !string.IsNullOrWhiteSpace(data.OldPassword))
                            {
                                var resultPassword = await userService.ChangePasswordAsync(user.Email, data.NewPassword).ConfigureAwait(true);
                                if (!resultPassword) throw new Exception("Password could not be changed");
                            }
                        }

                        var finalResult = await userService.UpdateAsync(user).ConfigureAwait(true);
                        if (finalResult)
                        {
                            return Json(new { status = true, messageTitle = "Success!", message = "Profile updated successfully." });
                        }
                        else
                        {
                            return Json(new { status = false, messageTitle = "Error!", message = "Could not update personal info. Try again later." });
                        }

                    }
                    else return Json(new { status = false, messageTitle = "No minors allowed", message = "You must be at least 18 years old in order to have a Shortchase account." });
                    
                }
            }
            catch (Exception e)
            {
                await errorLogService.InsertException(e);
                return Json(new { status = false, messageTitle = "Error!", message = "Could not update personal info." });
            }
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> AccountManagerClaimReward(CreateClaimRewardDto data)
        {
            try
            {
                if (data.UserId == Guid.Empty || data.PointsClaimed <= 0 || data.EquivalentAmount <= 0) throw new Exception("Insuficient data.");
                else
                {
                    User user = await userService.GetById(data.UserId).ConfigureAwait(true);
                    if (user == null) throw new Exception("No user found.");
                    else
                    {
                        if (user.TotalPointsAvailable >= data.PointsClaimed)
                        {
                            string IP = HttpContext.Connection.RemoteIpAddress.ToString();
                            data.DiscountType = DiscountType.Claimed;
                            var result = await userService.AccountManagerClaimReward(data, user, IP).ConfigureAwait(true);
                            if (!string.IsNullOrWhiteSpace(result))
                            {
                                var emailResult = await emailSenderService.SendToUserRewardClaimed(user.Email, user.FirstName, data.PointsClaimed, data.EquivalentAmount, result).ConfigureAwait(true);
                                if (emailResult)
                                {
                                    return Json(new { status = true, messageTitle = "Success!", message = "Rewards claimed successfully!" });
                                }
                                else throw new Exception("Could not send email after redeeming points and updating user.");
                            }
                            else throw new Exception("Could not insert new reward claim.");
                        }
                        else
                        {
                            int pointsDiff = data.PointsClaimed - user.TotalPointsAvailable;
                            return Json(new { status = false, messageTitle = "You need more points!", message = "You currently do not have enough points to redeem this amount. Get " + pointsDiff + " more points in order to be able to redeem this reward." });
                        }
                    }
                }
            }
            catch (Exception e)
            {
                await errorLogService.InsertException(e);
                return Json(new { status = false, messageTitle = "Error!", message = "Could not claim reward this time. Try again later." });
            }
        }

        #endregion
    }
}