using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using System;
using System.Threading.Tasks;
using Shortchase.Authorization;
using Shortchase.Entities;
using Shortchase.Helpers.Extensions;
using Shortchase.Services;
using Shortchase.Dtos;
using Shortchase.Helpers;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.IO;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using System.Net;
using System.Text;
using ServiceStack.Text;
using System.Net.Http;
using Newtonsoft.Json;
using CsvHelper;
using System.Drawing;
using Newtonsoft.Json.Linq;

namespace Shortchase.Controllers
{
    [Permitted(Permission.Admin, Permission.Owner, Permission.AccessAll)]
    public class BackendController : Controller
    {
        private readonly IErrorLogService errorLogService;
        private readonly IAddressService addressService;
        private readonly ICountryService countryService;
        private readonly ISystemFlagsService systemFlagsService;
        private readonly ISocialMediaService socialMediaService;
        private readonly ISemiStaticTextService semiStaticTextService;
        private readonly IBlogPostsService blogPostsService;
        private readonly INewsPostService newsPostService;
        private readonly IWebHostEnvironment hostingEnvironment;
        private readonly IFAQItemService fAQItemService;
        private readonly IPromotionPostService promotionPostService;
        private readonly IListingCategoryService listingCategoryService;
        private readonly IListingSubCategoryService listingSubCategoryService;
        private readonly IUserService userService;
        private readonly ISubscriptionPlanService subscriptionPlanService;
        private readonly IPermissionService permissionService;
        private readonly IUserSubscriptionService userSubscriptionService;
        private readonly IBetListingService betListingService;
        private readonly IBetListingReportService betListingReportService;
        private readonly IPOTDListingService potdListingService;
        private readonly IPOTDListingPredictionService potdListingPredictionService;
        private readonly IPOTDListingLiveReportService potdListingLiveReportService;
        private readonly IPOTDListingLiveReportingInteractionService potdListingLiveReportingInteractionService;
        private readonly ICurrencyService currencyService;
        private readonly IAccessLogService accessLogService;
        private readonly IMarketService marketService;
        private readonly ITipService tipService;
        private readonly IBookmakerService bookmakerService;
        private readonly IPickService pickService;
        private readonly IRewardsClaimedMappingService rewardsClaimedMappingService;
        private readonly IRewardsMappingService rewardsMappingService;
        private readonly IEmailSenderService emailSenderService;
        private readonly IPaypalSettingsService paypalSettingsService;
        private readonly ISystemConstantsService systemConstantsService;
        private readonly IOrderItemService orderItemService;
        private readonly IOrderService orderService;
        private readonly IMessageService messageService;
        private readonly IUserPayoutService userPayoutService;
        private readonly IAPIValidationService apiValidationService;
        private readonly ISecondaryEmailTemplateService secondaryEmailTemplateService;
        private readonly IEmailConfigService emailConfigService;
        private readonly IMediaFolderService mediaFolderService;
        private readonly IMediaFileService mediaFileService;

        public BackendController
        (
            IErrorLogService errorLogService,
            IAddressService addressService,
            ICountryService countryService,
            ISystemFlagsService systemFlagsService,
            ISocialMediaService socialMediaService,
            ISemiStaticTextService semiStaticTextService,
            IBlogPostsService blogPostsService,
            INewsPostService newsPostService,
            IWebHostEnvironment hostingEnvironment,
            IFAQItemService fAQItemService,
            IPromotionPostService promotionPostService,
            IListingCategoryService listingCategoryService,
            IListingSubCategoryService listingSubCategoryService,
            IUserService userService,
            ISubscriptionPlanService subscriptionPlanService,
            IUserSubscriptionService userSubscriptionService,
            IBetListingService betListingService,
            IBetListingReportService betListingReportService,
            IPOTDListingService potdListingService,
            IPOTDListingPredictionService potdListingPredictionService,
            IPOTDListingLiveReportService potdListingLiveReportService,
            IPOTDListingLiveReportingInteractionService potdListingLiveReportingInteractionService,
            ICurrencyService currencyService,
            IAccessLogService accessLogService,
            IMarketService marketService,
            ITipService tipService,
            IBookmakerService bookmakerService,
            IPickService pickService,
            IPermissionService permissionService,
            IRewardsClaimedMappingService rewardsClaimedMappingService,
            IRewardsMappingService rewardsMappingService,
            IEmailSenderService emailSenderService,
            IPaypalSettingsService paypalSettingsService,
            ISystemConstantsService systemConstantsService,
            IOrderItemService orderItemService,
            IOrderService orderService,
            IEmailConfigService emailConfigService,
            IMessageService messageService,
            IUserPayoutService userPayoutService,
            IAPIValidationService apiValidationService,
            ISecondaryEmailTemplateService secondaryEmailTemplateService,
            IMediaFolderService mediaFolderService,
            IMediaFileService mediaFileService
        )
        {
            this.errorLogService = errorLogService;
            this.addressService = addressService;
            this.countryService = countryService;
            this.systemFlagsService = systemFlagsService;
            this.socialMediaService = socialMediaService;
            this.semiStaticTextService = semiStaticTextService;
            this.blogPostsService = blogPostsService;
            this.newsPostService = newsPostService;
            this.hostingEnvironment = hostingEnvironment;
            this.fAQItemService = fAQItemService;
            this.promotionPostService = promotionPostService;
            this.listingCategoryService = listingCategoryService;
            this.listingSubCategoryService = listingSubCategoryService;
            this.userService = userService;
            this.subscriptionPlanService = subscriptionPlanService;
            this.userSubscriptionService = userSubscriptionService;
            this.betListingService = betListingService;
            this.betListingReportService = betListingReportService;
            this.potdListingService = potdListingService;
            this.potdListingPredictionService = potdListingPredictionService;
            this.potdListingLiveReportService = potdListingLiveReportService;
            this.potdListingLiveReportingInteractionService = potdListingLiveReportingInteractionService;
            this.currencyService = currencyService;
            this.accessLogService = accessLogService;
            this.marketService = marketService;
            this.tipService = tipService;
            this.bookmakerService = bookmakerService;
            this.pickService = pickService;
            this.rewardsClaimedMappingService = rewardsClaimedMappingService;
            this.rewardsMappingService = rewardsMappingService;
            this.emailSenderService = emailSenderService;
            this.paypalSettingsService = paypalSettingsService;
            this.systemConstantsService = systemConstantsService;
            this.orderItemService = orderItemService;
            this.orderService = orderService;
            this.messageService = messageService;
            this.userPayoutService = userPayoutService;
            this.apiValidationService = apiValidationService;
            this.secondaryEmailTemplateService = secondaryEmailTemplateService;
            this.mediaFolderService = mediaFolderService;
            this.mediaFileService = mediaFileService;
            this.emailConfigService = emailConfigService;
            this.permissionService = permissionService;
        }

        public async Task<IActionResult> Index(int TimeOffset = 0)
        {
            ViewData["TimezoneOffset"] = TimeOffset;
            var id = User.Identity.Id();
            Guid? UserId = null;
            if (!string.IsNullOrWhiteSpace(id))
            {
                UserId = new Guid(id);
            }
            DashboardMainDto model = new DashboardMainDto
            {
                TotalUsers = await userService.GetBettorCapperCount().ConfigureAwait(true),
                TotalBoisterous = await userService.GetBoisterousBettorCount().ConfigureAwait(true),
                TotalShortchasePro = await userService.GetShortchaseProCapperCount().ConfigureAwait(true),
                LatestAccessLog = await accessLogService.GetLatestByUserId(UserId.Value).ConfigureAwait(true)
            };
            return View(model);
        }

        #region AccessLogs

        public async Task<IActionResult> AccessLogs(int TimeOffset = 0)
        {
            ViewData["TimezoneOffset"] = TimeOffset;
            Guid? UserId = null;
            RequestFeedback request = new RequestFeedback();
            try
            {
                ICollection<AccessLog> model = await accessLogService.GetAll().ConfigureAwait(true);
                return View(model);
            }
            catch (Exception e)
            {
                await errorLogService.InsertException(e, UserId).ConfigureAwait(true);
                return RedirectToAction("Index", "Error", request);
            }
        }

        #endregion

        #region Address

        public async Task<IActionResult> Addresses(int TimeOffset = 0)
        {
            Guid? UserId = null;
            RequestFeedback request = new RequestFeedback();
            try
            {
                UserId = User.Id();
                var countries = (await countryService.GetAll().ConfigureAwait(true));
                AddressListDto model = new AddressListDto
                {
                    Addresses = await addressService.GetAll().ConfigureAwait(true),
                    HasDisabled = (await addressService.GetAll(false).ConfigureAwait(true)).Count > 0 ? true : false,
                    Countries = countries.Select(a => new SelectListItem { Value = a.Id.ToString(), Text = a.Name }).OrderBy(o => o.Text).ToList(),
                    DisplayAddresses = (await systemFlagsService.GetByName(SystemFlagsNames.DisplayAddresses).ConfigureAwait(true)).Value
                };
                model.DefaultCountryId = countries.Where(c => c.IsDefault).Select(c => c.Id).SingleOrDefault();
                return View(model);
            }
            catch (Exception e)
            {
                await errorLogService.InsertException(e, UserId).ConfigureAwait(true);
                return RedirectToAction("Index", "Error", request);
            }
        }

        [HttpPost]
        public async Task<IActionResult> AddAddress(CreateAddressDto newAddress)
        {
            try
            {
                if (newAddress == null) throw new Exception("No address to add");
                else
                {
                    if (string.IsNullOrWhiteSpace(newAddress.Location)) throw new Exception("Location Title is mandatory!");
                    if (string.IsNullOrWhiteSpace(newAddress.StreetAddress)) throw new Exception("Address is mandatory!");
                    if (string.IsNullOrWhiteSpace(newAddress.City)) throw new Exception("City is mandatory!");
                    if (string.IsNullOrWhiteSpace(newAddress.Province)) throw new Exception("Province is mandatory!");
                    if (string.IsNullOrWhiteSpace(newAddress.ZipCode)) throw new Exception("Zip Code is mandatory!");
                    if (newAddress.CountryId == 0) throw new Exception("Country is mandatory!");


                    Address address = new Address
                    {
                        City = newAddress.City,
                        Location = newAddress.Location,
                        StreetAddress = newAddress.StreetAddress,
                        Province = newAddress.Province,
                        ZipCode = newAddress.ZipCode,
                        CountryId = newAddress.CountryId,
                        IsEnabled = true,
                        IsPrimary = newAddress.IsPrimary
                    };

                    Address primaryAddressCheck = await addressService.GetPrimaryAddress().ConfigureAwait(true);
                    if (primaryAddressCheck != null && address.IsPrimary)
                    {
                        primaryAddressCheck.IsPrimary = false;
                        var updateResult = await addressService.Update(primaryAddressCheck).ConfigureAwait(true);
                        if (!updateResult) throw new Exception("Error updating old primary address. Try again later.");
                    }

                    var result = await addressService.Insert(address).ConfigureAwait(true);
                    if (result)
                    {

                        return Json(new { status = true, messageTitle = "Success", message = "New address saved successfully!" });
                    }
                    else throw new Exception("Error creating new address. Try again later.");
                }
            }
            catch (Exception e)
            {
                ModelState.AddModelError(string.Empty, "Something went wrong, please try again later");
                await errorLogService.InsertException(e).ConfigureAwait(true);
                return Json(new { status = false, messageTitle = "Error", message = e.Message });
            }
        }

        [HttpPost]
        public async Task<IActionResult> EditAddress(EditAddressDto editAddress)
        {
            try
            {
                Address addressToEdit = await addressService.GetById(editAddress.Id).ConfigureAwait(true);
                if (editAddress == null || addressToEdit == null) throw new Exception("No address to edit");
                else
                {
                    if (string.IsNullOrWhiteSpace(editAddress.Location)) throw new Exception("Location Title is mandatory!");
                    if (string.IsNullOrWhiteSpace(editAddress.StreetAddress)) throw new Exception("Address is mandatory!");
                    if (string.IsNullOrWhiteSpace(editAddress.City)) throw new Exception("City is mandatory!");
                    if (string.IsNullOrWhiteSpace(editAddress.Province)) throw new Exception("Province is mandatory!");
                    if (string.IsNullOrWhiteSpace(editAddress.ZipCode)) throw new Exception("Zip Code is mandatory!");
                    if (editAddress.CountryId == 0) throw new Exception("Country is mandatory!");

                    addressToEdit.City = editAddress.City;
                    addressToEdit.Location = editAddress.Location;
                    addressToEdit.StreetAddress = editAddress.StreetAddress;
                    addressToEdit.Province = editAddress.Province;
                    addressToEdit.ZipCode = editAddress.ZipCode;
                    addressToEdit.CountryId = editAddress.CountryId;
                    addressToEdit.IsPrimary = editAddress.IsPrimary;

                    Address primaryAddressCheck = await addressService.GetPrimaryAddress().ConfigureAwait(true);
                    if (primaryAddressCheck != null && addressToEdit.IsPrimary && addressToEdit.Id != primaryAddressCheck.Id)
                    {
                        primaryAddressCheck.IsPrimary = false;
                        var updateResult = await addressService.Update(primaryAddressCheck).ConfigureAwait(true);
                        if (!updateResult) throw new Exception("Error updating old primary address. Try again later.");
                    }

                    var result = await addressService.Update(addressToEdit).ConfigureAwait(true);
                    if (result)
                    {
                        return Json(new { status = true, messageTitle = "Success", message = "Address edited successfully!" });
                    }
                    else throw new Exception("Error creating new address. Try again later.");
                }
            }
            catch (Exception e)
            {
                ModelState.AddModelError(string.Empty, "Something went wrong, please try again later");
                await errorLogService.InsertException(e).ConfigureAwait(true);
                return Json(new { status = false, messageTitle = "Error", message = e.Message });
            }
        }

        [HttpPost]
        public async Task<IActionResult> SwitchStatusAddress(int? Id, bool NewStatus)
        {
            try
            {
                if (!Id.HasValue || Id == 0) throw new Exception("No address to activate");
                else
                {
                    var result = await addressService.SwitchStatus(Id.Value, NewStatus).ConfigureAwait(true);
                    if (result)
                    {
                        string message = NewStatus ? "Address activated successfully!" : "Address deactivated successfully!";
                        return Json(new { status = true, messageTitle = "Success", message = message });
                    }
                    else throw new Exception("Error activating the address. Try again later.");
                }
            }
            catch (Exception e)
            {
                ModelState.AddModelError(string.Empty, "Something went wrong, please try again later");
                await errorLogService.InsertException(e).ConfigureAwait(true);
                return Json(new { status = false, messageTitle = "Error", message = e.Message });
            }
        }


        [HttpPost]
        public async Task<IActionResult> SwitchPrimaryAddress(int? Id)
        {
            try
            {
                if (!Id.HasValue || Id == 0) throw new Exception("No address to switch");
                else
                {
                    var result = await addressService.SwitchPrimaryAddress(Id.Value).ConfigureAwait(true);
                    if (result) return Json(new { status = true, messageTitle = "Success", message = "Changed primary address successfully!" });
                    else throw new Exception("Error activating the address. Try again later.");
                }
            }
            catch (Exception e)
            {
                ModelState.AddModelError(string.Empty, "Something went wrong, please try again later");
                await errorLogService.InsertException(e).ConfigureAwait(true);
                return Json(new { status = false, messageTitle = "Error", message = e.Message });
            }
        }


        [HttpPost]
        public async Task<IActionResult> SwitchDisplayAddressFlag(bool Value)
        {
            try
            {
                SystemFlags displayAddress = await systemFlagsService.GetByName(SystemFlagsNames.DisplayAddresses).ConfigureAwait(true);
                if (displayAddress == null) throw new Exception("No settings found.");
                displayAddress.Value = Value;
                var result = await systemFlagsService.Update(displayAddress).ConfigureAwait(true);
                if (!result) throw new Exception("Error updating settings. Try again later.");
                return Json(new { status = true, messageTitle = "Success", message = "Settings changed successfully!" });
            }
            catch (Exception e)
            {
                ModelState.AddModelError(string.Empty, "Something went wrong, please try again later");
                await errorLogService.InsertException(e).ConfigureAwait(true);
                return Json(new { status = false, messageTitle = "Error", message = e.Message });
            }
        }

        #endregion

        #region Currencies

        public async Task<IActionResult> Currencies(int TimeOffset = 0)
        {
            Guid? UserId = null;
            RequestFeedback request = new RequestFeedback();
            try
            {
                UserId = User.Id();
                CurrencyListDto model = new CurrencyListDto
                {
                    Currencies = await currencyService.GetAll(false).ConfigureAwait(true),
                };
                return View(model);
            }
            catch (Exception e)
            {
                await errorLogService.InsertException(e, UserId).ConfigureAwait(true);
                return RedirectToAction("Index", "Error", request);
            }
        }

        [HttpPost]
        public async Task<IActionResult> AddCurrency(CreateCurrencyDto newCurrency)
        {
            try
            {
                if (newCurrency == null) throw new Exception("No currency to add");
                else
                {
                    if (string.IsNullOrWhiteSpace(newCurrency.Name)) throw new Exception("Name is mandatory!");
                    if (string.IsNullOrWhiteSpace(newCurrency.Acronym)) throw new Exception("Acronym is mandatory!");

                    Currency currency = new Currency
                    {
                        Acronym = newCurrency.Acronym,
                        Name = newCurrency.Name,
                        Deleted = false
                    };

                    var result = await currencyService.Insert(currency).ConfigureAwait(true);
                    if (result)
                    {

                        return Json(new { status = true, messageTitle = "Success", message = "New currency saved successfully!" });
                    }
                    else throw new Exception("Error creating new currency. Try again later.");
                }
            }
            catch (Exception e)
            {
                ModelState.AddModelError(string.Empty, "Something went wrong, please try again later");
                await errorLogService.InsertException(e).ConfigureAwait(true);
                return Json(new { status = false, messageTitle = "Error", message = e.Message });
            }
        }

        [HttpPost]
        public async Task<IActionResult> EditCurrency(EditCurrencyDto editCurrency)
        {
            try
            {
                Currency currency = await currencyService.GetById(editCurrency.Id).ConfigureAwait(true);
                if (editCurrency == null || currency == null) throw new Exception("No Currency to edit");
                else
                {
                    if (string.IsNullOrWhiteSpace(editCurrency.Name)) throw new Exception("Name is mandatory!");
                    if (string.IsNullOrWhiteSpace(editCurrency.Acronym)) throw new Exception("Acronym is mandatory!");

                    currency.Name = editCurrency.Name;
                    currency.Acronym = editCurrency.Acronym;

                    var result = await currencyService.Update(currency).ConfigureAwait(true);
                    if (result)
                    {
                        return Json(new { status = true, messageTitle = "Success", message = "Currency edited successfully!" });
                    }
                    else throw new Exception("Error creating new Currency. Try again later.");
                }
            }
            catch (Exception e)
            {
                ModelState.AddModelError(string.Empty, "Something went wrong, please try again later");
                await errorLogService.InsertException(e).ConfigureAwait(true);
                return Json(new { status = false, messageTitle = "Error", message = e.Message });
            }
        }

        [HttpPost]
        public async Task<IActionResult> DeleteCurrency(Guid Id)
        {
            try
            {
                if (Id == Guid.Empty) throw new Exception("No Currency to delete");
                else
                {
                    var result = await currencyService.Delete(Id).ConfigureAwait(true);
                    if (result)
                    {
                        return Json(new { status = true, messageTitle = "Success", message = "Currency deleted successfully!" });
                    }
                    else throw new Exception("Error deleting the Currency. Try again later.");
                }
            }
            catch (Exception e)
            {
                ModelState.AddModelError(string.Empty, "Something went wrong, please try again later");
                await errorLogService.InsertException(e).ConfigureAwait(true);
                return Json(new { status = false, messageTitle = "Error", message = e.Message });
            }
        }


        [HttpPost]
        public async Task<IActionResult> ActivateCurrency(Guid Id)
        {
            try
            {
                if (Id == Guid.Empty) throw new Exception("No Currency to activate");
                else
                {
                    var result = await currencyService.Activate(Id).ConfigureAwait(true);
                    if (result)
                    {
                        return Json(new { status = true, messageTitle = "Success", message = "Currency activated successfully!" });
                    }
                    else throw new Exception("Error activating the Currency. Try again later.");
                }
            }
            catch (Exception e)
            {
                ModelState.AddModelError(string.Empty, "Something went wrong, please try again later");
                await errorLogService.InsertException(e).ConfigureAwait(true);
                return Json(new { status = false, messageTitle = "Error", message = e.Message });
            }
        }


        #endregion

        #region SocialMedia
        public async Task<IActionResult> SocialMedia(int TimeOffset = 0)
        {
            Guid? UserId = null;
            RequestFeedback request = new RequestFeedback();
            try
            {
                UserId = User.Id();
                SocialMediaList model = new SocialMediaList
                {
                    Medias = (await socialMediaService.GetAll().ConfigureAwait(true)).OrderBy(o => o.Name).ToList()
                };
                return View(model);
            }
            catch (Exception e)
            {
                await errorLogService.InsertException(e, UserId).ConfigureAwait(true);
                return RedirectToAction("Index", "Error", request);
            }
        }

        [HttpPost]
        public async Task<IActionResult> AddSocialMedia(CreateSocialMediaDto newMedia)
        {
            try
            {
                if (newMedia == null) throw new Exception("No media to add");
                else
                {
                    if (string.IsNullOrWhiteSpace(newMedia.Name)) throw new Exception("Name is mandatory!");
                    if (string.IsNullOrWhiteSpace(newMedia.Icon)) throw new Exception("Icon is mandatory!");
                    if (string.IsNullOrWhiteSpace(newMedia.Link)) throw new Exception("Link is mandatory!");

                    SocialMedia media = new SocialMedia
                    {
                        Name = newMedia.Name,
                        Link = newMedia.Link,
                        Icon = newMedia.Icon,
                        IsEnabled = true
                    };

                    var result = await socialMediaService.Insert(media).ConfigureAwait(true);
                    if (result)
                    {

                        return Json(new { status = true, messageTitle = "Success", message = "New social media saved successfully!" });
                    }
                    else throw new Exception("Error creating new social media. Try again later.");
                }
            }
            catch (Exception e)
            {
                ModelState.AddModelError(string.Empty, "Something went wrong, please try again later");
                await errorLogService.InsertException(e).ConfigureAwait(true);
                return Json(new { status = false, messageTitle = "Error", message = e.Message });
            }
        }

        [HttpPost]
        public async Task<IActionResult> SwitchStatusSocialMedia(int? Id, bool NewStatus)
        {
            try
            {
                if (!Id.HasValue || Id == 0) throw new Exception("No media to activate");
                else
                {
                    var result = await socialMediaService.SwitchStatus(Id.Value, NewStatus).ConfigureAwait(true);
                    if (result)
                    {
                        string message = NewStatus ? "Social Media activated successfully!" : "Social Media deactivated successfully!";
                        return Json(new { status = true, messageTitle = "Success", message = message });
                    }
                    else throw new Exception("Error activating the address. Try again later.");
                }
            }
            catch (Exception e)
            {
                ModelState.AddModelError(string.Empty, "Something went wrong, please try again later");
                await errorLogService.InsertException(e).ConfigureAwait(true);
                return Json(new { status = false, messageTitle = "Error", message = e.Message });
            }
        }

        [HttpPost]
        public async Task<IActionResult> EditSocialMedia(EditSocialMediaDto editMedia)
        {
            try
            {
                SocialMedia mediaToEdit = await socialMediaService.GetById(editMedia.Id).ConfigureAwait(true);
                if (editMedia == null || mediaToEdit == null) throw new Exception("No media to edit");
                else
                {
                    if (string.IsNullOrWhiteSpace(editMedia.Name)) throw new Exception("Name is mandatory!");
                    if (string.IsNullOrWhiteSpace(editMedia.Icon)) throw new Exception("Icon is mandatory!");
                    if (string.IsNullOrWhiteSpace(editMedia.Link)) throw new Exception("Link is mandatory!");

                    mediaToEdit.Name = editMedia.Name;
                    mediaToEdit.Icon = editMedia.Icon;
                    mediaToEdit.Link = editMedia.Link;

                    var result = await socialMediaService.Update(mediaToEdit).ConfigureAwait(true);
                    if (result)
                    {
                        return Json(new { status = true, messageTitle = "Success", message = "Social Media edited successfully!" });
                    }
                    else throw new Exception("Error creating new address. Try again later.");
                }
            }
            catch (Exception e)
            {
                ModelState.AddModelError(string.Empty, "Something went wrong, please try again later");
                await errorLogService.InsertException(e).ConfigureAwait(true);
                return Json(new { status = false, messageTitle = "Error", message = e.Message });
            }
        }

        #endregion

        #region App Config
        public async Task<IActionResult> AppConfig(int TimeOffset = 0)
        {
            Guid? UserId = null;
            RequestFeedback request = new RequestFeedback();
            ViewData["root"] = hostingEnvironment.ContentRootPath;
            try
            {
                UserId = User.Id();
                AppConfigs model = new AppConfigs
                {
                    AppLogo = (await semiStaticTextService.GetByName(SemiStaticTextNames.AppLogo).ConfigureAwait(true)),
                    AppName = (await semiStaticTextService.GetByName(SemiStaticTextNames.AppName).ConfigureAwait(true)),
                    AppTagline = (await semiStaticTextService.GetByName(SemiStaticTextNames.AppTagline).ConfigureAwait(true))
                };
                return View(model);
            }
            catch (Exception e)
            {
                await errorLogService.InsertException(e, UserId).ConfigureAwait(true);
                return RedirectToAction("Index", "Error", request);
            }
        }

        [HttpPost]
        public async Task<IActionResult> UpdateAppConfig(AppConfigsUpdateDto configs)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(configs.AppName)) throw new Exception("App Name cannot be empty!");
                if (string.IsNullOrWhiteSpace(configs.AppTagline)) throw new Exception("App Tagline cannot be empty!");

                SemiStaticText AppName = (await semiStaticTextService.GetByName(SemiStaticTextNames.AppName).ConfigureAwait(true));
                SemiStaticText AppTagline = (await semiStaticTextService.GetByName(SemiStaticTextNames.AppTagline).ConfigureAwait(true));
                SemiStaticText AppLogo = (await semiStaticTextService.GetByName(SemiStaticTextNames.AppLogo).ConfigureAwait(true));

                AppName.Value = configs.AppName;
                AppTagline.Value = configs.AppTagline;
                if (configs.AppLogo.HasValue)
                {
                    MediaFile file = await mediaFileService.GetById(configs.AppLogo.Value).ConfigureAwait(true);
                    if (file == null) throw new Exception("No logo file provided when required.");
                    else
                    {
                        AppLogo.Value = file.PhysicalPath;
                    }
                }


                var result = await semiStaticTextService.UpdateAllConfigs(AppName, AppTagline, AppLogo).ConfigureAwait(true);
                if (result)
                {
                    return Json(new { status = true, messageTitle = "Success", message = "App configuration updated successfully!" });
                }
                else throw new Exception("Error updating App configuration. Try again later.");

            }
            catch (Exception e)
            {
                ModelState.AddModelError(string.Empty, "Something went wrong, please try again later");
                await errorLogService.InsertException(e).ConfigureAwait(true);
                return Json(new { status = false, messageTitle = "Error", message = e.Message });
            }
        }
        #endregion

        #region Cookie Consent
        public async Task<IActionResult> CookieConsent(int TimeOffset = 0)
        {
            Guid? UserId = null;
            RequestFeedback request = new RequestFeedback();
            try
            {
                UserId = User.Id();
                SemiStaticText model = (await semiStaticTextService.GetByName(SemiStaticTextNames.CookieConsent).ConfigureAwait(true));
                return View(model);
            }
            catch (Exception e)
            {
                await errorLogService.InsertException(e, UserId).ConfigureAwait(true);
                return RedirectToAction("Index", "Error", request);
            }
        }

        [HttpPost]
        public async Task<IActionResult> UpdateCookieConsent(string text)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(text)) throw new Exception("Cookie Consent cannot be empty!");

                SemiStaticText CookieConsent = (await semiStaticTextService.GetByName(SemiStaticTextNames.CookieConsent).ConfigureAwait(true));
                CookieConsent.Value = text;
                var result = await semiStaticTextService.Update(CookieConsent).ConfigureAwait(true);
                if (result)
                {
                    return Json(new { status = true, messageTitle = "Success", message = "Cookie Consent updated successfully!" });
                }
                else throw new Exception("Error updating Terms of service. Try again later.");

            }
            catch (Exception e)
            {
                ModelState.AddModelError(string.Empty, "Something went wrong, please try again later");
                await errorLogService.InsertException(e).ConfigureAwait(true);
                return Json(new { status = false, messageTitle = "Error", message = e.Message });
            }
        }
        #endregion

        #region Terms of Service
        public async Task<IActionResult> TermsOfService(int TimeOffset = 0)
        {
            Guid? UserId = null;
            RequestFeedback request = new RequestFeedback();
            try
            {
                UserId = User.Id();
                SemiStaticText model = (await semiStaticTextService.GetByName(SemiStaticTextNames.TermsOfService).ConfigureAwait(true));
                return View(model);
            }
            catch (Exception e)
            {
                await errorLogService.InsertException(e, UserId).ConfigureAwait(true);
                return RedirectToAction("Index", "Error", request);
            }
        }

        [HttpPost]
        public async Task<IActionResult> UpdateTermsOfService(string text)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(text)) throw new Exception("Terms of service cannot be empty!");

                SemiStaticText terms = (await semiStaticTextService.GetByName(SemiStaticTextNames.TermsOfService).ConfigureAwait(true));
                terms.Value = text;
                var result = await semiStaticTextService.Update(terms).ConfigureAwait(true);
                if (result)
                {
                    return Json(new { status = true, messageTitle = "Success", message = "Terms of service updated successfully!" });
                }
                else throw new Exception("Error updating Terms of service. Try again later.");

            }
            catch (Exception e)
            {
                ModelState.AddModelError(string.Empty, "Something went wrong, please try again later");
                await errorLogService.InsertException(e).ConfigureAwait(true);
                return Json(new { status = false, messageTitle = "Error", message = e.Message });
            }
        }
        #endregion

        #region Privacy Policy
        public async Task<IActionResult> PrivacyPolicy(int TimeOffset = 0)
        {
            Guid? UserId = null;
            RequestFeedback request = new RequestFeedback();
            try
            {
                UserId = User.Id();
                SemiStaticText model = (await semiStaticTextService.GetByName(SemiStaticTextNames.PrivacyPolicy).ConfigureAwait(true));
                return View(model);
            }
            catch (Exception e)
            {
                await errorLogService.InsertException(e, UserId).ConfigureAwait(true);
                return RedirectToAction("Index", "Error", request);
            }
        }

        [HttpPost]
        public async Task<IActionResult> UpdatePrivacyPolicy(string text)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(text)) throw new Exception("Privacy Policy cannot be empty!");

                SemiStaticText privacyPolicy = (await semiStaticTextService.GetByName(SemiStaticTextNames.PrivacyPolicy).ConfigureAwait(true));
                privacyPolicy.Value = text;
                var result = await semiStaticTextService.Update(privacyPolicy).ConfigureAwait(true);
                if (result)
                {
                    return Json(new { status = true, messageTitle = "Success", message = "Privacy Policy updated successfully!" });
                }
                else throw new Exception("Error updating Privacy Policy. Try again later.");

            }
            catch (Exception e)
            {
                ModelState.AddModelError(string.Empty, "Something went wrong, please try again later");
                await errorLogService.InsertException(e).ConfigureAwait(true);
                return Json(new { status = false, messageTitle = "Error", message = e.Message });
            }
        }
        #endregion

        #region Blog
        public async Task<IActionResult> Blog(int TimeOffset = 0)
        {
            Guid? UserId = null;
            ViewData["TimezoneOffset"] = TimeOffset;
            RequestFeedback request = new RequestFeedback();
            try
            {
                UserId = User.Id();
                var model = (await blogPostsService.GetAllForbackend().ConfigureAwait(true)).OrderByDescending(o => o.DatePublished).ThenByDescending(o => o.DateCreated).ToList();
                return View(model);
            }
            catch (Exception e)
            {
                await errorLogService.InsertException(e, UserId).ConfigureAwait(true);
                return RedirectToAction("Index", "Error", request);
            }
        }
        public async Task<IActionResult> BlogPreviewBlog(string Slug, int TimeOffset = 0)
        {
            ViewData["TimezoneOffset"] = TimeOffset;
            ViewData["root"] = hostingEnvironment.ContentRootPath;
            try
            {
                if (string.IsNullOrWhiteSpace(Slug)) return RedirectToAction("Blog");
                else
                {
                    BlogPost post = await blogPostsService.GetBySlug(Slug).ConfigureAwait(true);
                    if (post == null) return RedirectToAction("Blog");
                    else return View(post);
                }
            }
            catch (Exception e)
            {
                await errorLogService.InsertException(e).ConfigureAwait(true);
                return RedirectToAction("Blog");
            }
        }
        public async Task<IActionResult> BlogAddPost()
        {
            Guid? UserId = null;
            RequestFeedback request = new RequestFeedback();
            try
            {
                UserId = User.Id();
                return View();
            }
            catch (Exception e)
            {
                await errorLogService.InsertException(e, UserId).ConfigureAwait(true);
                return RedirectToAction("Index", "Error", request);
            }
        }

        public async Task<IActionResult> BlogEditPost(int Id = 0, int TimeOffset = 0)
        {

            ViewData["TimezoneOffset"] = TimeOffset;
            Guid? UserId = null;
            RequestFeedback request = new RequestFeedback();
            try
            {
                UserId = User.Id();
                ViewData["root"] = hostingEnvironment.ContentRootPath;
                if (Id == 0) return RedirectToAction("Blog");
                else
                {
                    BlogPost post = await blogPostsService.GetById(Id).ConfigureAwait(true);
                    if (post == null) return RedirectToAction("Blog");
                    else return View(post);
                }
            }
            catch (Exception e)
            {
                await errorLogService.InsertException(e, UserId).ConfigureAwait(true);

                return RedirectToAction("Index", "Error", request);
            }
        }

        [HttpPost]
        public async Task<IActionResult> CreateNewBlogPost(CreateBlogPostDto Post)
        {
            try
            {
                DateTime? DatePublished = null;
                if (!Post.IsPublished && string.IsNullOrWhiteSpace(Post.DatePublished)) throw new Exception("You need to provide a date for publishing the post.");
                if (string.IsNullOrWhiteSpace(Post.Content)) throw new Exception("You need to provide content for publishing the post.");
                if (string.IsNullOrWhiteSpace(Post.Title)) throw new Exception("You need to provide a title for publishing the post.");
                if (string.IsNullOrWhiteSpace(Post.Slug)) throw new Exception("You need to provide a slug for publishing the post.");
                if (string.IsNullOrWhiteSpace(Post.Excerpt)) throw new Exception("You need to provide a excerpt for publishing the post.");
                if (Post.IsPublished)
                {
                    DatePublished = DateTime.UtcNow;
                }
                else
                {
                    var datetime = Post.DatePublished.Split(' ');
                    var time = datetime[1].Split(':');
                    var date = datetime[0].Split('/');
                    DatePublished = new DateTime(Convert.ToInt32(date[2]), Convert.ToInt32(date[0]), Convert.ToInt32(date[1]), Convert.ToInt32(time[0]), Convert.ToInt32(time[1]), Convert.ToInt32(time[2]));
                    DatePublished = DatePublished.Value.AddMinutes(Post.TimezoneOffset);
                }

                BlogPost newPost = new BlogPost
                {
                    Content = Post.Content,
                    DatePublished = DatePublished.Value,
                    Slug = Post.Slug,
                    IsPublished = Post.IsPublished,
                    Title = Post.Title,
                    Excerpt = Post.Excerpt
                };

                if (!Post.File.HasValue)
                {
                    newPost.FeaturedImage = Url.Content("~/img/blog/default.jpg");
                }
                else
                {
                    MediaFile file = await mediaFileService.GetById(Post.File.Value).ConfigureAwait(true);
                    if (file == null) throw new Exception("No file provided when required.");
                    else
                    {
                        newPost.FeaturedImage = file.PhysicalPath;
                    }
                }

                var result = await blogPostsService.Insert(newPost).ConfigureAwait(true);
                if (result)
                {
                    return Json(new { status = true, messageTitle = "Success", message = "Blog post created successfully!" });
                }
                else throw new Exception("Error creating blog post. Try again later.");

            }
            catch (Exception e)
            {
                ModelState.AddModelError(string.Empty, "Something went wrong, please try again later");
                await errorLogService.InsertException(e).ConfigureAwait(true);
                return Json(new { status = false, messageTitle = "Error", message = e.Message });
            }
        }

        [HttpPost]
        public async Task<IActionResult> UpdateBlogPost(UpdateBlogPostDto Post)
        {
            try
            {
                DateTime? DatePublished = null;
                BlogPost postToUpdate = await blogPostsService.GetById(Post.Id).ConfigureAwait(true);
                if (postToUpdate == null) throw new Exception("Post to update not found with given id.");
                if (string.IsNullOrWhiteSpace(Post.DatePublished)) throw new Exception("You need to provide a date for publishing the post.");
                if (string.IsNullOrWhiteSpace(Post.Content)) throw new Exception("You need to provide content for publishing the post.");
                if (string.IsNullOrWhiteSpace(Post.Title)) throw new Exception("You need to provide a title for publishing the post.");
                if (string.IsNullOrWhiteSpace(Post.Slug)) throw new Exception("You need to provide a slug for publishing the post.");
                if (string.IsNullOrWhiteSpace(Post.Excerpt)) throw new Exception("You need to provide a excerpt for publishing the post.");

                var datetime = Post.DatePublished.Split(' ');
                var time = datetime[1].Split(':');
                var date = datetime[0].Split('/');
                DatePublished = new DateTime(Convert.ToInt32(date[2]), Convert.ToInt32(date[1]), Convert.ToInt32(date[0]), Convert.ToInt32(time[0]), Convert.ToInt32(time[1]), Convert.ToInt32(time[2]));
                DatePublished = DatePublished.Value.AddMinutes(Post.TimezoneOffset);

                postToUpdate.Content = Post.Content;
                postToUpdate.Title = Post.Title;
                postToUpdate.Slug = Post.Slug;
                postToUpdate.Excerpt = Post.Excerpt;

                if (postToUpdate.DatePublished.Value == DatePublished.Value)
                {
                    if (postToUpdate.DatePublished.Value <= DateTime.UtcNow)
                    {
                        postToUpdate.IsPublished = true;
                    }
                    else
                    {
                        postToUpdate.IsPublished = false;
                    }
                }
                else
                {
                    if (DatePublished.Value <= DateTime.UtcNow)
                    {
                        postToUpdate.IsPublished = true;
                    }
                    else
                    {
                        postToUpdate.IsPublished = false;
                    }
                    postToUpdate.DatePublished = DatePublished.Value;
                }

                if (Post.File.HasValue)
                {


                    MediaFile file = await mediaFileService.GetById(Post.File.Value).ConfigureAwait(true);
                    if (file == null) throw new Exception("No file provided when required.");
                    else
                    {
                        postToUpdate.FeaturedImage = file.PhysicalPath;
                    }


                }

                var result = await blogPostsService.Update(postToUpdate).ConfigureAwait(true);
                if (result)
                {
                    return Json(new { status = true, messageTitle = "Success", message = "Blog post updated successfully!" });
                }
                else throw new Exception("Error updating blog post. Try again later.");

            }
            catch (Exception e)
            {
                ModelState.AddModelError(string.Empty, "Something went wrong, please try again later");
                await errorLogService.InsertException(e).ConfigureAwait(true);
                return Json(new { status = false, messageTitle = "Error", message = e.Message });
            }
        }


        [HttpPost]
        public async Task<IActionResult> PublishBlogPostNow(int Id)
        {
            try
            {
                BlogPost postToUpdate = await blogPostsService.GetById(Id).ConfigureAwait(true);
                if (postToUpdate == null) throw new Exception("Post to update not found with given id.");

                postToUpdate.DatePublished = DateTime.UtcNow;
                postToUpdate.IsPublished = true;

                var result = await blogPostsService.Update(postToUpdate).ConfigureAwait(true);
                if (result)
                {
                    return Json(new { status = true, messageTitle = "Success", message = "Blog post updated successfully!" });
                }
                else throw new Exception("Error updating blog post. Try again later.");

            }
            catch (Exception e)
            {
                ModelState.AddModelError(string.Empty, "Something went wrong, please try again later");
                await errorLogService.InsertException(e).ConfigureAwait(true);
                return Json(new { status = false, messageTitle = "Error", message = e.Message });
            }
        }

        #endregion

        #region FAQ

        public async Task<IActionResult> FAQ(int TimeOffset = 0)
        {
            Guid? UserId = null;
            RequestFeedback request = new RequestFeedback();
            try
            {
                UserId = User.Id();
                FAQListDto model = new FAQListDto
                {
                    Items = (await fAQItemService.GetAll(true).ConfigureAwait(true)).OrderByDescending(o => o.IsActive).ThenBy(o => o.Question).ToList()
                };
                List<SelectListItem> faqTypes = new List<SelectListItem>();
                faqTypes.Add(new SelectListItem { Value = FAQType.General, Text = FAQType.General });
                faqTypes.Add(new SelectListItem { Value = FAQType.Bettor, Text = FAQType.Bettor });
                faqTypes.Add(new SelectListItem { Value = FAQType.Capper, Text = FAQType.Capper });
                model.FAQTypes = faqTypes;
                return View(model);
            }
            catch (Exception e)
            {
                await errorLogService.InsertException(e, UserId).ConfigureAwait(true);
                return RedirectToAction("Index", "Error", request);
            }
        }


        [HttpPost]
        public async Task<IActionResult> AddFAQItem(CreateFAQListDto newItem)
        {
            try
            {
                if (newItem == null) throw new Exception("No FAQ item to add");
                else
                {
                    if (string.IsNullOrWhiteSpace(newItem.Question)) throw new Exception("Question is mandatory!");
                    if (string.IsNullOrWhiteSpace(newItem.Answer)) throw new Exception("Answer is mandatory!");
                    if (string.IsNullOrWhiteSpace(newItem.Type)) throw new Exception("Type is mandatory!");

                    FAQItem item = new FAQItem
                    {
                        Question = newItem.Question,
                        Answer = newItem.Answer,
                        Type = newItem.Type,
                        IsActive = true
                    };

                    var result = await fAQItemService.Insert(item).ConfigureAwait(true);
                    if (result)
                    {

                        return Json(new { status = true, messageTitle = "Success", message = "New FAQ saved successfully!" });
                    }
                    else throw new Exception("Error creating new FAQ. Try again later.");
                }
            }
            catch (Exception e)
            {
                ModelState.AddModelError(string.Empty, "Something went wrong, please try again later");
                await errorLogService.InsertException(e).ConfigureAwait(true);
                return Json(new { status = false, messageTitle = "Error", message = e.Message });
            }
        }

        [HttpPost]
        public async Task<IActionResult> SwitchStatusFAQItem(int? Id, bool NewStatus)
        {
            try
            {
                if (!Id.HasValue || Id == 0) throw new Exception("No FAQ item to activate");
                else
                {
                    var result = await fAQItemService.SwitchStatus(Id.Value, NewStatus).ConfigureAwait(true);
                    if (result)
                    {
                        string message = NewStatus ? "FAQ item activated successfully!" : "FAQ item deactivated successfully!";
                        return Json(new { status = true, messageTitle = "Success", message = message });
                    }
                    else throw new Exception("Error switching status of the FAQ item. Try again later.");
                }
            }
            catch (Exception e)
            {
                ModelState.AddModelError(string.Empty, "Something went wrong, please try again later");
                await errorLogService.InsertException(e).ConfigureAwait(true);
                return Json(new { status = false, messageTitle = "Error", message = e.Message });
            }
        }


        [HttpPost]
        public async Task<IActionResult> UpdateFAQItem(UpdateFAQListDto item)
        {
            try
            {
                if (item == null) throw new Exception("No FAQ item to update");
                else
                {
                    if (item.Id == 0) throw new Exception("Id is mandatory!");
                    if (string.IsNullOrWhiteSpace(item.Question)) throw new Exception("Question is mandatory!");
                    if (string.IsNullOrWhiteSpace(item.Answer)) throw new Exception("Answer is mandatory!");
                    if (string.IsNullOrWhiteSpace(item.Type)) throw new Exception("Type is mandatory!");

                    FAQItem faqItem = await fAQItemService.GetById(item.Id).ConfigureAwait(true);

                    if (faqItem == null) throw new Exception("No FAQ item found with given id.");

                    faqItem.Question = item.Question;
                    faqItem.Answer = item.Answer;
                    faqItem.Type = item.Type;

                    var result = await fAQItemService.Update(faqItem).ConfigureAwait(true);
                    if (result)
                    {

                        return Json(new { status = true, messageTitle = "Success", message = "FAQ updated successfully!" });
                    }
                    else throw new Exception("Error updating FAQ. Try again later.");
                }
            }
            catch (Exception e)
            {
                ModelState.AddModelError(string.Empty, "Something went wrong, please try again later");
                await errorLogService.InsertException(e).ConfigureAwait(true);
                return Json(new { status = false, messageTitle = "Error", message = e.Message });
            }
        }

        #endregion

        #region News
        public async Task<IActionResult> News(int TimeOffset = 0)
        {
            Guid? UserId = null;
            RequestFeedback request = new RequestFeedback();
            ViewData["TimezoneOffset"] = TimeOffset;
            try
            {
                UserId = User.Id();
                var model = (await newsPostService.GetAllForbackend().ConfigureAwait(true)).OrderByDescending(o => o.DatePublished).ThenByDescending(o => o.DateCreated).ToList();
                return View(model);
            }
            catch (Exception e)
            {
                await errorLogService.InsertException(e, UserId).ConfigureAwait(true);
                return RedirectToAction("Index", "Error", request);
            }
        }
        public async Task<IActionResult> NewsPreviewNews(string Slug, int TimeOffset = 0)
        {
            try
            {
                ViewData["TimezoneOffset"] = TimeOffset;
                ViewData["root"] = hostingEnvironment.ContentRootPath;
                if (string.IsNullOrWhiteSpace(Slug)) return RedirectToAction("News");
                else
                {
                    NewsPost post = await newsPostService.GetBySlug(Slug).ConfigureAwait(true);
                    if (post == null) return RedirectToAction("News");
                    else return View(post);
                }
            }
            catch (Exception e)
            {
                await errorLogService.InsertException(e).ConfigureAwait(true);
                return RedirectToAction("News");
            }
        }
        public async Task<IActionResult> NewsAddPost()
        {
            Guid? UserId = null;
            RequestFeedback request = new RequestFeedback();
            try
            {
                UserId = User.Id();
                return View();
            }
            catch (Exception e)
            {
                await errorLogService.InsertException(e, UserId).ConfigureAwait(true);
                return RedirectToAction("Index", "Error", request);
            }
        }

        public async Task<IActionResult> NewsEditPost(int Id = 0, int TimeOffset = 0)
        {
            Guid? UserId = null;
            RequestFeedback request = new RequestFeedback();
            try
            {
                ViewData["TimezoneOffset"] = TimeOffset;
                UserId = User.Id();
                ViewData["root"] = hostingEnvironment.ContentRootPath;
                if (Id == 0) return RedirectToAction("News");
                else
                {
                    NewsPost post = await newsPostService.GetById(Id).ConfigureAwait(true);
                    if (post == null) return RedirectToAction("News");
                    else return View(post);
                }
            }
            catch (Exception e)
            {
                await errorLogService.InsertException(e, UserId).ConfigureAwait(true);
                return RedirectToAction("Index", "Error", request);
            }
        }

        [HttpPost]
        public async Task<IActionResult> CreateNewNewsPost(CreateNewsPostDto Post)
        {
            try
            {
                DateTime? DatePublished = null;
                if (!Post.IsPublished && string.IsNullOrWhiteSpace(Post.DatePublished)) throw new Exception("You need to provide a date for publishing the post.");
                if (string.IsNullOrWhiteSpace(Post.Content)) throw new Exception("You need to provide content for publishing the post.");
                if (string.IsNullOrWhiteSpace(Post.Title)) throw new Exception("You need to provide a title for publishing the post.");
                if (string.IsNullOrWhiteSpace(Post.Slug)) throw new Exception("You need to provide a slug for publishing the post.");
                if (string.IsNullOrWhiteSpace(Post.Excerpt)) throw new Exception("You need to provide a excerpt for publishing the post.");
                if (Post.IsPublished)
                {
                    DatePublished = DateTime.UtcNow;
                }
                else
                {
                    var datetime = Post.DatePublished.Split(' ');
                    var time = datetime[1].Split(':');
                    var date = datetime[0].Split('/');
                    DatePublished = new DateTime(Convert.ToInt32(date[2]), Convert.ToInt32(date[0]), Convert.ToInt32(date[1]), Convert.ToInt32(time[0]), Convert.ToInt32(time[1]), Convert.ToInt32(time[2]));
                    DatePublished = DatePublished.Value.AddMinutes(Post.TimezoneOffset);
                }

                NewsPost newPost = new NewsPost
                {
                    Content = Post.Content,
                    DatePublished = DatePublished.Value,
                    Slug = Post.Slug,
                    IsPublished = Post.IsPublished,
                    Title = Post.Title,
                    Excerpt = Post.Excerpt
                };

                if (!Post.File.HasValue)
                {
                    newPost.FeaturedImage = Url.Content("~/img/blog/default.jpg");
                }
                else
                {
                    MediaFile file = await mediaFileService.GetById(Post.File.Value).ConfigureAwait(true);
                    if (file == null) throw new Exception("No file provided when required.");
                    else
                    {
                        newPost.FeaturedImage = file.PhysicalPath;
                    }

                }

                var result = await newsPostService.Insert(newPost).ConfigureAwait(true);
                if (result)
                {
                    return Json(new { status = true, messageTitle = "Success", message = "News post created successfully!" });
                }
                else throw new Exception("Error creating blog post. Try again later.");

            }
            catch (Exception e)
            {
                ModelState.AddModelError(string.Empty, "Something went wrong, please try again later");
                await errorLogService.InsertException(e).ConfigureAwait(true);
                return Json(new { status = false, messageTitle = "Error", message = e.Message });
            }
        }

        [HttpPost]
        public async Task<IActionResult> UpdateNewsPost(UpdateNewsPostDto Post)
        {
            try
            {
                DateTime? DatePublished = null;
                NewsPost postToUpdate = await newsPostService.GetById(Post.Id).ConfigureAwait(true);
                if (postToUpdate == null) throw new Exception("Post to update not found with given id.");
                if (string.IsNullOrWhiteSpace(Post.DatePublished)) throw new Exception("You need to provide a date for publishing the post.");
                if (string.IsNullOrWhiteSpace(Post.Content)) throw new Exception("You need to provide content for publishing the post.");
                if (string.IsNullOrWhiteSpace(Post.Title)) throw new Exception("You need to provide a title for publishing the post.");
                if (string.IsNullOrWhiteSpace(Post.Slug)) throw new Exception("You need to provide a slug for publishing the post.");
                if (string.IsNullOrWhiteSpace(Post.Excerpt)) throw new Exception("You need to provide a excerpt for publishing the post.");

                var datetime = Post.DatePublished.Split(' ');
                var time = datetime[1].Split(':');
                var date = datetime[0].Split('/');
                DatePublished = new DateTime(Convert.ToInt32(date[2]), Convert.ToInt32(date[1]), Convert.ToInt32(date[0]), Convert.ToInt32(time[0]), Convert.ToInt32(time[1]), Convert.ToInt32(time[2]));
                DatePublished = DatePublished.Value.AddMinutes(Post.TimezoneOffset);

                postToUpdate.Content = Post.Content;
                postToUpdate.Title = Post.Title;
                postToUpdate.Slug = Post.Slug;
                postToUpdate.Excerpt = Post.Excerpt;

                if (postToUpdate.DatePublished.Value == DatePublished.Value)
                {
                    if (postToUpdate.DatePublished.Value <= DateTime.UtcNow)
                    {
                        postToUpdate.IsPublished = true;
                    }
                    else
                    {
                        postToUpdate.IsPublished = false;
                    }
                }
                else
                {
                    if (DatePublished.Value <= DateTime.UtcNow)
                    {
                        postToUpdate.IsPublished = true;
                    }
                    else
                    {
                        postToUpdate.IsPublished = false;
                    }
                    postToUpdate.DatePublished = DatePublished.Value;
                }

                if (Post.File.HasValue)
                {
                    MediaFile file = await mediaFileService.GetById(Post.File.Value).ConfigureAwait(true);
                    if (file == null) throw new Exception("No file provided when required.");
                    else
                    {
                        postToUpdate.FeaturedImage = file.PhysicalPath;
                    }
                }

                var result = await newsPostService.Update(postToUpdate).ConfigureAwait(true);
                if (result)
                {
                    return Json(new { status = true, messageTitle = "Success", message = "News post updated successfully!" });
                }
                else throw new Exception("Error updating blog post. Try again later.");

            }
            catch (Exception e)
            {
                ModelState.AddModelError(string.Empty, "Something went wrong, please try again later");
                await errorLogService.InsertException(e).ConfigureAwait(true);
                return Json(new { status = false, messageTitle = "Error", message = e.Message });
            }
        }


        [HttpPost]
        public async Task<IActionResult> PublishNewsPostNow(int Id)
        {
            try
            {
                NewsPost postToUpdate = await newsPostService.GetById(Id).ConfigureAwait(true);
                if (postToUpdate == null) throw new Exception("Post to update not found with given id.");

                postToUpdate.DatePublished = DateTime.UtcNow;
                postToUpdate.IsPublished = true;

                var result = await newsPostService.Update(postToUpdate).ConfigureAwait(true);
                if (result)
                {
                    return Json(new { status = true, messageTitle = "Success", message = "News post updated successfully!" });
                }
                else throw new Exception("Error updating blog post. Try again later.");

            }
            catch (Exception e)
            {
                ModelState.AddModelError(string.Empty, "Something went wrong, please try again later");
                await errorLogService.InsertException(e).ConfigureAwait(true);
                return Json(new { status = false, messageTitle = "Error", message = e.Message });
            }
        }

        #endregion

        #region Promotions
        public async Task<IActionResult> Promotion(int TimeOffset = 0)
        {
            Guid? UserId = null;
            RequestFeedback request = new RequestFeedback();
            try
            {
                ViewData["TimezoneOffset"] = TimeOffset;
                UserId = User.Id();
                var model = (await promotionPostService.GetAllForbackend().ConfigureAwait(true)).OrderByDescending(o => o.DatePublished).ThenByDescending(o => o.DateCreated).ToList();
                return View(model);
            }
            catch (Exception e)
            {
                await errorLogService.InsertException(e, UserId).ConfigureAwait(true);
                return RedirectToAction("Index", "Error", request);
            }
        }
        public async Task<IActionResult> PromotionPreviewPromotion(string Slug, int TimeOffset = 0)
        {
            try
            {
                ViewData["TimezoneOffset"] = TimeOffset;
                ViewData["root"] = hostingEnvironment.ContentRootPath;
                if (string.IsNullOrWhiteSpace(Slug)) return RedirectToAction("PromotionList");
                else
                {
                    PromotionPost post = await promotionPostService.GetBySlug(Slug).ConfigureAwait(true);
                    if (post == null) return RedirectToAction("PromotionList");
                    else return View(post);
                }
            }
            catch (Exception e)
            {
                await errorLogService.InsertException(e).ConfigureAwait(true);
                return RedirectToAction("Promotion");
            }
        }
        public async Task<IActionResult> PromotionAddPost()
        {
            Guid? UserId = null;
            RequestFeedback request = new RequestFeedback();
            try
            {
                UserId = User.Id();
                return View();
            }
            catch (Exception e)
            {
                await errorLogService.InsertException(e, UserId).ConfigureAwait(true);
                return RedirectToAction("Index", "Error", request);
            }
        }

        public async Task<IActionResult> PromotionEditPost(int Id = 0, int TimeOffset = 0)
        {
            Guid? UserId = null;
            RequestFeedback request = new RequestFeedback();
            try
            {
                ViewData["TimezoneOffset"] = TimeOffset;
                UserId = User.Id();
                ViewData["root"] = hostingEnvironment.ContentRootPath;
                if (Id == 0) return RedirectToAction("Promotion");
                else
                {
                    PromotionPost post = await promotionPostService.GetById(Id).ConfigureAwait(true);
                    if (post == null) return RedirectToAction("Promotion");
                    else return View(post);
                }
            }
            catch (Exception e)
            {
                await errorLogService.InsertException(e, UserId).ConfigureAwait(true);
                return RedirectToAction("Index", "Error", request);
            }
        }

        [HttpPost]
        public async Task<IActionResult> CreateNewPromotionPost(CreatePromotionPostDto Post)
        {
            try
            {
                DateTime? DatePublished = null;
                if (!Post.IsPublished && string.IsNullOrWhiteSpace(Post.DatePublished)) throw new Exception("You need to provide a date for publishing the post.");
                if (string.IsNullOrWhiteSpace(Post.Content)) throw new Exception("You need to provide content for publishing the post.");
                if (string.IsNullOrWhiteSpace(Post.Title)) throw new Exception("You need to provide a title for publishing the post.");
                if (string.IsNullOrWhiteSpace(Post.Slug)) throw new Exception("You need to provide a slug for publishing the post.");
                if (string.IsNullOrWhiteSpace(Post.Excerpt)) throw new Exception("You need to provide a excerpt for publishing the post.");
                if (Post.IsPublished)
                {
                    DatePublished = DateTime.UtcNow;
                }
                else
                {
                    var datetime = Post.DatePublished.Split(' ');
                    var time = datetime[1].Split(':');
                    var date = datetime[0].Split('/');
                    DatePublished = new DateTime(Convert.ToInt32(date[2]), Convert.ToInt32(date[0]), Convert.ToInt32(date[1]), Convert.ToInt32(time[0]), Convert.ToInt32(time[1]), Convert.ToInt32(time[2]));
                    DatePublished = DatePublished.Value.AddMinutes(Post.TimezoneOffset);
                }

                PromotionPost newPost = new PromotionPost
                {
                    Content = Post.Content,
                    DatePublished = DatePublished.Value,
                    Slug = Post.Slug,
                    IsPublished = Post.IsPublished,
                    Title = Post.Title,
                    Excerpt = Post.Excerpt
                };

                if (!Post.File.HasValue)
                {
                    newPost.FeaturedImage = Url.Content("~/img/blog/default.jpg");
                }
                else
                {
                    MediaFile file = await mediaFileService.GetById(Post.File.Value).ConfigureAwait(true);
                    if (file == null) throw new Exception("No file provided when required.");
                    else
                    {
                        newPost.FeaturedImage = file.PhysicalPath;
                    }
                }

                var result = await promotionPostService.Insert(newPost).ConfigureAwait(true);
                if (result)
                {
                    return Json(new { status = true, messageTitle = "Success", message = "Promotion post created successfully!" });
                }
                else throw new Exception("Error creating blog post. Try again later.");

            }
            catch (Exception e)
            {
                ModelState.AddModelError(string.Empty, "Something went wrong, please try again later");
                await errorLogService.InsertException(e).ConfigureAwait(true);
                return Json(new { status = false, messageTitle = "Error", message = e.Message });
            }
        }

        [HttpPost]
        public async Task<IActionResult> UpdatePromotionPost(UpdatePromotionPostDto Post)
        {
            try
            {
                DateTime? DatePublished = null;
                PromotionPost postToUpdate = await promotionPostService.GetById(Post.Id).ConfigureAwait(true);
                if (postToUpdate == null) throw new Exception("Post to update not found with given id.");
                if (string.IsNullOrWhiteSpace(Post.DatePublished)) throw new Exception("You need to provide a date for publishing the post.");
                if (string.IsNullOrWhiteSpace(Post.Content)) throw new Exception("You need to provide content for publishing the post.");
                if (string.IsNullOrWhiteSpace(Post.Title)) throw new Exception("You need to provide a title for publishing the post.");
                if (string.IsNullOrWhiteSpace(Post.Slug)) throw new Exception("You need to provide a slug for publishing the post.");
                if (string.IsNullOrWhiteSpace(Post.Excerpt)) throw new Exception("You need to provide a excerpt for publishing the post.");

                var datetime = Post.DatePublished.Split(' ');
                var time = datetime[1].Split(':');
                var date = datetime[0].Split('/');
                DatePublished = new DateTime(Convert.ToInt32(date[2]), Convert.ToInt32(date[1]), Convert.ToInt32(date[0]), Convert.ToInt32(time[0]), Convert.ToInt32(time[1]), Convert.ToInt32(time[2]));
                DatePublished = DatePublished.Value.AddMinutes(Post.TimezoneOffset);

                postToUpdate.Content = Post.Content;
                postToUpdate.Title = Post.Title;
                postToUpdate.Slug = Post.Slug;
                postToUpdate.Excerpt = Post.Excerpt;

                if (postToUpdate.DatePublished.Value == DatePublished.Value)
                {
                    if (postToUpdate.DatePublished.Value <= DateTime.UtcNow)
                    {
                        postToUpdate.IsPublished = true;
                    }
                    else
                    {
                        postToUpdate.IsPublished = false;
                    }
                }
                else
                {
                    if (DatePublished.Value <= DateTime.UtcNow)
                    {
                        postToUpdate.IsPublished = true;
                    }
                    else
                    {
                        postToUpdate.IsPublished = false;
                    }
                    postToUpdate.DatePublished = DatePublished.Value;
                }

                if (Post.File.HasValue)
                {
                    MediaFile file = await mediaFileService.GetById(Post.File.Value).ConfigureAwait(true);
                    if (file == null) throw new Exception("No file provided when required.");
                    else
                    {
                        postToUpdate.FeaturedImage = file.PhysicalPath;
                    }
                }

                var result = await promotionPostService.Update(postToUpdate).ConfigureAwait(true);
                if (result)
                {
                    return Json(new { status = true, messageTitle = "Success", message = "Promotion post updated successfully!" });
                }
                else throw new Exception("Error updating blog post. Try again later.");

            }
            catch (Exception e)
            {
                ModelState.AddModelError(string.Empty, "Something went wrong, please try again later");
                await errorLogService.InsertException(e).ConfigureAwait(true);
                return Json(new { status = false, messageTitle = "Error", message = e.Message });
            }
        }


        [HttpPost]
        public async Task<IActionResult> PublishPromotionPostNow(int Id)
        {
            try
            {
                PromotionPost postToUpdate = await promotionPostService.GetById(Id).ConfigureAwait(true);
                if (postToUpdate == null) throw new Exception("Post to update not found with given id.");

                postToUpdate.DatePublished = DateTime.UtcNow;
                postToUpdate.IsPublished = true;

                var result = await promotionPostService.Update(postToUpdate).ConfigureAwait(true);
                if (result)
                {
                    return Json(new { status = true, messageTitle = "Success", message = "Promotion post updated successfully!" });
                }
                else throw new Exception("Error updating blog post. Try again later.");

            }
            catch (Exception e)
            {
                ModelState.AddModelError(string.Empty, "Something went wrong, please try again later");
                await errorLogService.InsertException(e).ConfigureAwait(true);
                return Json(new { status = false, messageTitle = "Error", message = e.Message });
            }
        }

        #endregion

        #region Listing Category

        public async Task<IActionResult> ListingCategories(int TimeOffset = 0)
        {
            ViewData["root"] = hostingEnvironment.ContentRootPath;
            Guid? UserId = null;
            RequestFeedback request = new RequestFeedback();
            try
            {
                UserId = User.Id();
                ListingCategoryListDto model = new ListingCategoryListDto
                {
                    ListingCategories = await listingCategoryService.GetAll(true).ConfigureAwait(true),
                };
                return View(model);
            }
            catch (Exception e)
            {
                await errorLogService.InsertException(e, UserId).ConfigureAwait(true);
                return RedirectToAction("Index", "Error", request);
            }
        }


        [HttpPost]
        public async Task<IActionResult> AddListingCategory(IFormCollection formSubmission)
        {

            try
            {
                CreateListingCategoryDto category = new CreateListingCategoryDto
                {
                    Name = formSubmission["Post.Name"],
                    Description = formSubmission["Post.Name"],
                    DisplayImage = new Guid(formSubmission["Post.DisplayImage"].ToString()),
                    MarketplaceImage = new Guid(formSubmission["Post.MarketplaceImage"].ToString())
                };
                if (string.IsNullOrWhiteSpace(category.Name)) throw new Exception("You need to provide name for the new Category.");
                if (!category.DisplayImage.HasValue || (category.DisplayImage.HasValue && category.DisplayImage.Value == Guid.Empty)) throw new Exception("You need to provide a picture for the new Category.");
                if (!category.MarketplaceImage.HasValue || (category.MarketplaceImage.HasValue && category.MarketplaceImage.Value == Guid.Empty)) throw new Exception("You need to provide a picture for the new Category.");

                ListingCategory newCategory = new ListingCategory
                {
                    Name = category.Name,
                    Description = category.Description,
                    IsEnabled = true
                };

                if (category.DisplayImage.HasValue)
                {
                    MediaFile file = await mediaFileService.GetById(category.DisplayImage.Value).ConfigureAwait(true);
                    if (file == null) throw new Exception("No file provided when required.");
                    else
                    {
                        newCategory.DisplayImage = file.PhysicalPath;
                    }

                }
                if (category.MarketplaceImage.HasValue)
                {

                    MediaFile file = await mediaFileService.GetById(category.MarketplaceImage.Value).ConfigureAwait(true);
                    if (file == null) throw new Exception("No file provided when required.");
                    else
                    {
                        newCategory.MarketplaceImage = file.PhysicalPath;
                    }
                }

                var result = await listingCategoryService.Insert(newCategory).ConfigureAwait(true);
                if (result)
                {
                    return Json(new { status = true, messageTitle = "Success", message = "Category created successfully!" });
                }
                else throw new Exception("Error creating category. Try again later.");

            }
            catch (Exception e)
            {
                ModelState.AddModelError(string.Empty, "Something went wrong, please try again later");
                await errorLogService.InsertException(e).ConfigureAwait(true);
                return Json(new { status = false, messageTitle = "Error", message = e.Message });
            }
        }


        [HttpPost]
        public async Task<IActionResult> SwitchStatusListingCategory(int? Id, bool NewStatus)
        {
            try
            {
                if (!Id.HasValue || Id == 0) throw new Exception("No Category to activate");
                else
                {
                    var hasChildren = await listingCategoryService.HasChildrenSubcategories(Id.Value).ConfigureAwait(true);
                    if (NewStatus == false)
                    {
                        if (!hasChildren)
                        {
                            var result = await listingCategoryService.SwitchStatus(Id.Value, NewStatus).ConfigureAwait(true);
                            if (result)
                            {
                                string message = NewStatus ? "Category activated successfully!" : "Category deleted successfully!";
                                return Json(new { status = true, messageTitle = "Success", message = message });
                            }
                            else throw new Exception("Error switching the Category status. Try again later.");
                        }
                        else return Json(new { status = false, messageTitle = "Error: Category has active subcategories!", message = "Delete all active subcategories from this category first, and then try again." });
                    }
                    else
                    {
                        var result = await listingCategoryService.SwitchStatus(Id.Value, NewStatus).ConfigureAwait(true);
                        if (result)
                        {
                            string message = NewStatus ? "Category activated successfully!" : "Category deleted successfully!";
                            return Json(new { status = true, messageTitle = "Success", message = message });
                        }
                        else throw new Exception("Error switching the Category status. Try again later.");
                    }
                }

            }
            catch (Exception e)
            {
                ModelState.AddModelError(string.Empty, "Something went wrong, please try again later");
                await errorLogService.InsertException(e).ConfigureAwait(true);
                return Json(new { status = false, messageTitle = "Error", message = e.Message });
            }
        }


        [HttpPost]
        public async Task<IActionResult> EditListingCategory(EditListingCategoryDto formSubmission)
        {

            try
            {
                EditListingCategoryDto category = new EditListingCategoryDto
                {
                    Id = formSubmission.Id,
                    Name = formSubmission.Name,
                    Description = formSubmission.Name,
                    DisplayImage = formSubmission.DisplayImage,
                    MarketplaceImage = formSubmission.MarketplaceImage,
                };



                if (string.IsNullOrWhiteSpace(category.Name)) throw new Exception("You need to provide name to edit the Category.");

                ListingCategory updateCategory = await listingCategoryService.GetById(category.Id).ConfigureAwait(true);

                updateCategory.Name = category.Name;


                if (category.DisplayImage.HasValue)
                {
                    MediaFile file = await mediaFileService.GetById(category.DisplayImage.Value).ConfigureAwait(true);
                    if (file == null) throw new Exception("No file provided when required.");
                    else
                    {
                        updateCategory.DisplayImage = file.PhysicalPath;
                    }

                }
                if (category.MarketplaceImage.HasValue)
                {

                    MediaFile file = await mediaFileService.GetById(category.MarketplaceImage.Value).ConfigureAwait(true);
                    if (file == null) throw new Exception("No file provided when required.");
                    else
                    {
                        updateCategory.MarketplaceImage = file.PhysicalPath;
                    }
                }

                var result = await listingCategoryService.Update(updateCategory).ConfigureAwait(true);
                if (result)
                {
                    return Json(new { status = true, messageTitle = "Success", message = "Category updated successfully!" });
                }
                else throw new Exception("Error creating category. Try again later.");

            }
            catch (Exception e)
            {
                ModelState.AddModelError(string.Empty, "Something went wrong, please try again later");
                await errorLogService.InsertException(e).ConfigureAwait(true);
                return Json(new { status = false, messageTitle = "Error", message = e.Message });
            }
        }

        #endregion

        #region Listing SubCategory


        public async Task<IActionResult> ListingSubCategories(int TimeOffset = 0)
        {
            ViewData["root"] = hostingEnvironment.ContentRootPath;
            Guid? UserId = null;
            RequestFeedback request = new RequestFeedback();
            try
            {
                UserId = User.Id();
                ListingSubCategoryListDto model = new ListingSubCategoryListDto
                {
                    ListingSubCategories = await listingSubCategoryService.GetAll(true, true).ConfigureAwait(true),
                    CategoriesOptions = (await listingCategoryService.GetAll(true).ConfigureAwait(true)).Select(a => new SelectListItem { Value = a.Id.ToString(), Text = a.Name }).OrderBy(o => o.Text).ToList()
                };
                return View(model);
            }
            catch (Exception e)
            {
                await errorLogService.InsertException(e, UserId).ConfigureAwait(true);
                return RedirectToAction("Index", "Error", request);
            }
        }


        [HttpPost]
        public async Task<IActionResult> AddListingSubCategory(CreateListingSubCategoryDto subCategory)
        {

            try
            {
                if (string.IsNullOrWhiteSpace(subCategory.Name)) throw new Exception("You need to provide name for the new subcategory.");

                ListingSubCategory newSubCategory = new ListingSubCategory
                {
                    Name = subCategory.Name,
                    Description = subCategory.Description,
                    IsEnabled = true,
                    MarketplaceImage = null,
                    DisplayImage = null,
                    CategoryId = subCategory.CategoryId
                };

                var result = await listingSubCategoryService.Insert(newSubCategory).ConfigureAwait(true);
                if (result)
                {
                    return Json(new { status = true, messageTitle = "Success", message = "Subcategory created successfully!" });
                }
                else throw new Exception("Error creating Subcategory. Try again later.");

            }
            catch (Exception e)
            {
                ModelState.AddModelError(string.Empty, "Something went wrong, please try again later");
                await errorLogService.InsertException(e).ConfigureAwait(true);
                return Json(new { status = false, messageTitle = "Error", message = e.Message });
            }
        }


        [HttpPost]
        public async Task<IActionResult> SwitchStatusListingSubCategory(int? Id, bool NewStatus)
        {
            try
            {
                if (!Id.HasValue || Id == 0) throw new Exception("No subcategory to activate");
                else
                {
                    var result = await listingSubCategoryService.SwitchStatus(Id.Value, NewStatus).ConfigureAwait(true);
                    if (result)
                    {
                        string message = NewStatus ? "Subcategory activated successfully!" : "Subcategory deleted successfully!";
                        return Json(new { status = true, messageTitle = "Success", message = message });
                    }
                    else throw new Exception("Error switching the subcategory status. Try again later.");
                }
            }
            catch (Exception e)
            {
                ModelState.AddModelError(string.Empty, "Something went wrong, please try again later");
                await errorLogService.InsertException(e).ConfigureAwait(true);
                return Json(new { status = false, messageTitle = "Error", message = e.Message });
            }
        }


        [HttpPost]
        public async Task<IActionResult> EditListingSubCategory(EditListingSubCategoryDto subcategory)
        {

            try
            {

                if (string.IsNullOrWhiteSpace(subcategory.Name)) throw new Exception("You need to provide name to edit the subcategory.");

                ListingSubCategory updateSubCategory = await listingSubCategoryService.GetById(subcategory.Id).ConfigureAwait(true);

                updateSubCategory.Name = subcategory.Name;
                updateSubCategory.Description = subcategory.Description;
                updateSubCategory.CategoryId = subcategory.CategoryId;


                var result = await listingSubCategoryService.Update(updateSubCategory).ConfigureAwait(true);
                if (result)
                {
                    return Json(new { status = true, messageTitle = "Success", message = "Subcategory updated successfully!" });
                }
                else throw new Exception("Error creating subcategory. Try again later.");

            }
            catch (Exception e)
            {
                ModelState.AddModelError(string.Empty, "Something went wrong, please try again later");
                await errorLogService.InsertException(e).ConfigureAwait(true);
                return Json(new { status = false, messageTitle = "Error", message = e.Message });
            }
        }

        #endregion

        #region Subscription Plans




        [HttpPost]
        public async Task<IActionResult> CreatePaypalProductForSubscriptionPlan(int? Id = null, string PaypalRequestBody = null)
        {

            try
            {
                if (!Id.HasValue) throw new Exception("No subscription plan received");
                if (string.IsNullOrWhiteSpace(PaypalRequestBody)) throw new Exception("No product data received");
                SubscriptionPlan subscription = await subscriptionPlanService.GetById(Id.Value).ConfigureAwait(true);
                if (subscription == null) throw new Exception("No subscription plan received");

                PaypalSettings paypalSettings = await paypalSettingsService.GetDefault().ConfigureAwait(true);
                string ClientId = paypalSettings.ClientID;
                string Secret = paypalSettings.Secret;


                var requestPaypal = await RequestPayPalToken(ClientId, Secret).ConfigureAwait(true);
                if (requestPaypal == null) throw new Exception("No token obtained");
                string paypalToken = requestPaypal.access_token;



                bool PayPalCreateProductRequestSuccess = await RequestPayPalCreateProduct(paypalToken, PaypalRequestBody, subscription).ConfigureAwait(true);
                if (PayPalCreateProductRequestSuccess) return Json(new { status = true, messageTitle = "Success", message = "Paypal product created successfully!" });
                else throw new Exception("Error creating product on Paypal.");


            }
            catch (Exception e)
            {
                ModelState.AddModelError(string.Empty, "Something went wrong, please try again later");
                await errorLogService.InsertException(e).ConfigureAwait(true);
                return Json(new { status = false, messageTitle = "Error", message = e.Message });
            }
        }

        [HttpPost]
        public async Task<IActionResult> CreatePaypalSubscriptionPlanForSubscriptionPlan(int? Id = null, string PaypalRequestBody = null)
        {

            try
            {
                if (!Id.HasValue) throw new Exception("No subscription plan received");
                if (string.IsNullOrWhiteSpace(PaypalRequestBody)) throw new Exception("No subscription plan data received");
                SubscriptionPlan subscription = await subscriptionPlanService.GetById(Id.Value).ConfigureAwait(true);
                if (subscription == null) throw new Exception("No subscription plan received");

                PaypalSettings paypalSettings = await paypalSettingsService.GetDefault().ConfigureAwait(true);
                string ClientId = paypalSettings.ClientID;
                string Secret = paypalSettings.Secret;


                var requestPaypal = await RequestPayPalToken(ClientId, Secret).ConfigureAwait(true);
                if (requestPaypal == null) throw new Exception("No token obtained");
                string paypalToken = requestPaypal.access_token;



                bool PayPalCreateProductRequestSuccess = await RequestPayPalCreateSubscriptionPlan(paypalToken, PaypalRequestBody, subscription).ConfigureAwait(true);
                if (PayPalCreateProductRequestSuccess) return Json(new { status = true, messageTitle = "Success", message = "Paypal subscription plan created successfully!" });
                else throw new Exception("Error creating subscription plan on Paypal.");


            }
            catch (Exception e)
            {
                ModelState.AddModelError(string.Empty, "Something went wrong, please try again later");
                await errorLogService.InsertException(e).ConfigureAwait(true);
                return Json(new { status = false, messageTitle = "Error", message = e.Message });
            }
        }




        public async Task<IActionResult> SubscriptionPlans(int TimeOffset = 0)
        {
            Guid? UserId = null;
            RequestFeedback request = new RequestFeedback();
            try
            {
                UserId = User.Id();
                SubscriptionPlanListDto model = new SubscriptionPlanListDto
                {
                    SubscriptionPlans = await subscriptionPlanService.GetAll().ConfigureAwait(true),
                    SubscriptionPlanTypeOptions = new List<SelectListItem>()
                };
                SelectListItem bettorOption = new SelectListItem { Text = SubscriptionPlanType.Boisterous, Value = SubscriptionPlanType.Boisterous };
                SelectListItem capperOption = new SelectListItem { Text = SubscriptionPlanType.ShortchasePro, Value = SubscriptionPlanType.ShortchasePro };

                model.SubscriptionPlanTypeOptions.Add(bettorOption);
                model.SubscriptionPlanTypeOptions.Add(capperOption);

                return View(model);
            }
            catch (Exception e)
            {
                await errorLogService.InsertException(e, UserId).ConfigureAwait(true);
                return RedirectToAction("Index", "Error", request);
            }
        }


        [HttpPost]
        public async Task<IActionResult> AddSubscriptionPlan(CreateSubscriptionPlanDto plan)
        {

            try
            {
                if (string.IsNullOrWhiteSpace(plan.Name)) throw new Exception("You need to provide name for the new subscription plan.");
                if (string.IsNullOrWhiteSpace(plan.Type)) throw new Exception("You need to provide type for the new subscription plan.");
                if (plan.DurationInMonths <= 0) throw new Exception("You need to provide valid Duration (in months) for the new subscription plan.");
                if (plan.ValuePerMonth <= 0) throw new Exception("You need to provide valid Value Per Month for the new subscription plan.");

                SubscriptionPlan newPlan = new SubscriptionPlan
                {
                    Name = plan.Name,
                    Type = plan.Type,
                    IsActive = true,
                    DurationInMonths = plan.DurationInMonths,
                    ValuePerMonth = plan.ValuePerMonth,
                    TotalValuePerDuration = plan.DurationInMonths * plan.ValuePerMonth
                };

                var result = await subscriptionPlanService.Insert(newPlan).ConfigureAwait(true);
                if (result)
                {
                    return Json(new { status = true, messageTitle = "Success", message = "Subscription plan created successfully!" });
                }
                else throw new Exception("Error creating subscription plan. Try again later.");

            }
            catch (Exception e)
            {
                ModelState.AddModelError(string.Empty, "Something went wrong, please try again later");
                await errorLogService.InsertException(e).ConfigureAwait(true);
                return Json(new { status = false, messageTitle = "Error", message = e.Message });
            }
        }


        [HttpPost]
        public async Task<IActionResult> SwitchStatusSubscriptionPlan(int? Id, bool NewStatus)
        {
            try
            {
                if (!Id.HasValue || Id == 0) throw new Exception("No Subscription plan to activate");
                else
                {
                    var result = await subscriptionPlanService.SwitchStatus(Id.Value, NewStatus).ConfigureAwait(true);
                    if (result)
                    {
                        string message = NewStatus ? "Subscription plan activated successfully!" : "Subscription plan deactivated successfully!";
                        return Json(new { status = true, messageTitle = "Success", message = message });
                    }
                    else throw new Exception("Error switching the Subscription plan status. Try again later.");
                }
            }
            catch (Exception e)
            {
                ModelState.AddModelError(string.Empty, "Something went wrong, please try again later");
                await errorLogService.InsertException(e).ConfigureAwait(true);
                return Json(new { status = false, messageTitle = "Error", message = e.Message });
            }
        }



        [HttpPost]
        public async Task<IActionResult> EditSubscriptionPlan(EditSubscriptionPlanDto plan)
        {

            try
            {
                if (string.IsNullOrWhiteSpace(plan.Name)) throw new Exception("You need to provide name for the subscription plan.");
                if (string.IsNullOrWhiteSpace(plan.Type)) throw new Exception("You need to provide type for the subscription plan.");
                if (plan.DurationInMonths <= 0) throw new Exception("You need to provide valid Duration (in months) for the subscription plan.");
                if (plan.ValuePerMonth <= 0) throw new Exception("You need to provide valid Value Per Month for the subscription plan.");

                SubscriptionPlan updatePlan = await subscriptionPlanService.GetById(plan.Id).ConfigureAwait(true);

                updatePlan.Name = plan.Name;
                updatePlan.Type = plan.Type;
                updatePlan.ValuePerMonth = plan.ValuePerMonth;
                updatePlan.DurationInMonths = plan.DurationInMonths;
                updatePlan.TotalValuePerDuration = plan.DurationInMonths * plan.ValuePerMonth;

                var result = await subscriptionPlanService.Update(updatePlan).ConfigureAwait(true);
                if (result)
                {
                    return Json(new { status = true, messageTitle = "Success", message = "Subscription plan updated successfully!" });
                }
                else throw new Exception("Error updating Subscription plan. Try again later.");

            }
            catch (Exception e)
            {
                ModelState.AddModelError(string.Empty, "Something went wrong, please try again later");
                await errorLogService.InsertException(e).ConfigureAwait(true);
                return Json(new { status = false, messageTitle = "Error", message = e.Message });
            }
        }

        #endregion

        #region USERS/Administrators

        public async Task<IActionResult> ManageAdministrators(int TimeOffset = 0)
        {
            Guid? UserId = null;
            ViewData["TimezoneOffset"] = TimeOffset;
            RequestFeedback request = new RequestFeedback();
            try
            {
                UserId = User.Id();
                var countries = (await countryService.GetAll().ConfigureAwait(true));
                BackendUserListDto model = new BackendUserListDto
                {
                    Users = (await userService.GetAdminList().ConfigureAwait(true)).Select(i => new UserListItemDto { Email = i.Email, FirstName = i.FirstName, LastName = i.LastName, Id = i.Id, LastSeen = null, IsActive = i.IsActive, DateRegistered = i.RowDate, PhoneNumber = i.PhoneNumber, UserName = i.UserName, PhoneCountryId = i.PhoneCountryId }).ToList(),
                    CountriesOptions = countries.OrderBy(o => o.Name).ToList(),
                    RolesOptions = (await permissionService.GetAdminRoles().ConfigureAwait(true)).ToList()
                };


                if (model.Users.Count > 0)
                {
                    foreach (var user in model.Users)
                    {
                        var lastSeenDate = await accessLogService.GetLatestByUserId(user.Id).ConfigureAwait(true);
                        if (lastSeenDate != null) user.LastSeen = lastSeenDate.RowDate;
                    }
                }
                model.DefaultCountryId = countries.Where(c => c.IsDefault).Select(c => c.Id).SingleOrDefault();
                model.DefaultCountryPhoneCode = countries.Where(c => c.IsDefault).Select(c => c.Code).SingleOrDefault();
                return View(model);
            }
            catch (Exception e)
            {
                await errorLogService.InsertException(e, UserId).ConfigureAwait(true);               
                return RedirectToAction("Index", "Error", request);
            }
        }

        [HttpPost]
        public async Task<IActionResult> AddAdministrator(CreateUserDto model)
        {
            try
            {
                // map dto to entity
                var user = model.ToUser();

                // save
                var result = await userService.CreateShortchaseAdministratorUserAsync(user, model.Password, model.role).ConfigureAwait(true);
                if (result)
                {
                    return Json(new { status = true, messageTitle = "Success!", message = "Registration completed successfully." });
                }
                else throw new Exception("An unexpected error occured. Please try again later.");


            }
            catch (Exception e)
            {
                ModelState.AddModelError(string.Empty, "Something went wrong, please try again later");
                await errorLogService.InsertException(e).ConfigureAwait(true);
                return Json(new { status = false, messageTitle = "Error", message = e.Message
                });
            }
        }


        [HttpPost]
        public async Task<IActionResult> SwitchStatusAdministrator(Guid? Id, bool NewStatus)
        {
            try
            {
                if (!Id.HasValue || Id == Guid.Empty) throw new Exception("No administrator to activate");
                else
                {
                    var user = await userService.GetById(Id.Value).ConfigureAwait(true);
                    if (user == null) throw new Exception("No administrator to activate");

                    user.IsActive = NewStatus;

                    var result = await userService.UpdateAsync(user).ConfigureAwait(true);
                    if (result)
                    {
                        string message = NewStatus ? "Administrator activated successfully!" : "Administrator deactivated successfully!";
                        return Json(new { status = true, messageTitle = "Success", message = message });
                    }
                    else throw new Exception("Error switching the administrator status. Try again later.");
                }
            }
            catch (Exception e)
            {
                ModelState.AddModelError(string.Empty, "Something went wrong, please try again later");
                await errorLogService.InsertException(e).ConfigureAwait(true);
                return Json(new { status = false, messageTitle = "Error", message = e.Message });
            }
        }

        [HttpPost]
        public async Task<IActionResult> EditAdministrator(UpdateUserDto adminInfo)
        {
            try
            {
                if (adminInfo.Id == Guid.Empty) throw new Exception("No administrator to update");
                else
                {
                    var user = await userService.GetById(adminInfo.Id).ConfigureAwait(true);
                    if (user == null) throw new Exception("No administrator to activate");

                    user.FirstName = adminInfo.FirstName;
                    user.LastName = adminInfo.LastName;
                    user.PhoneNumber = adminInfo.PhoneNumber;
                    user.PhoneCountryId = adminInfo.Country;


                    if (adminInfo.ChangePassword)
                    {
                        var updatePasswordResult = await userService.ResetPasswordAsync(user.Email, adminInfo.Password);
                        if (!updatePasswordResult) throw new Exception("Error updating administrator password. Try again later.");
                    }

                    var result = await userService.UpdateAsync(user).ConfigureAwait(true);
                    if (result)
                    {
                        return Json(new { status = true, messageTitle = "Success", message = "Administrator updated successfully!" });
                    }
                    else throw new Exception("Error updating administrator. Try again later.");
                }
            }
            catch (Exception e)
            {
                ModelState.AddModelError(string.Empty, "Something went wrong, please try again later");
                await errorLogService.InsertException(e).ConfigureAwait(true);
                return Json(new { status = false, messageTitle = "Error", message = e.Message });
            }
        }

        #endregion

        #region USERS/Bettors and Cappers






        public async Task<IActionResult> ManageUsers(int TimeOffset = 0)
        {
            ViewData["TimezoneOffset"] = TimeOffset;
            Guid? UserId = null;
            RequestFeedback request = new RequestFeedback();
            try
            {
                UserId = User.Id();
                var countries = (await countryService.GetAll().ConfigureAwait(true));
                BackendUserListDto model = new BackendUserListDto
                {
                    Users = (await userService.GetUserList().ConfigureAwait(true)).Select(i => new UserListItemDto { Email = i.Email, FirstName = i.FirstName, LastName = i.LastName, Id = i.Id, LastSeen = null, IsActive = i.IsActive, DateRegistered = i.RowDate }).ToList(),
                    CountriesOptions = countries.OrderBy(o => o.Name).ToList(),
                };

                if (model.Users.Count > 0)
                {
                    foreach (var user in model.Users)
                    {
                        var lastSeenDate = await accessLogService.GetLatestByUserId(user.Id).ConfigureAwait(true);
                        if (lastSeenDate != null) user.LastSeen = lastSeenDate.RowDate;
                    }
                }

                model.DefaultCountryId = countries.Where(c => c.IsDefault).Select(c => c.Id).SingleOrDefault();
                model.DefaultCountryPhoneCode = countries.Where(c => c.IsDefault).Select(c => c.Code).SingleOrDefault();
                return View(model);
            }
            catch (Exception e)
            {
                await errorLogService.InsertException(e, UserId).ConfigureAwait(true);
                return RedirectToAction("Index", "Error", request);
            }
        }


        public async Task<IActionResult> ManageUserSubscriptions(Guid Id, int TimeOffset = 0)
        {
            ViewData["TimezoneOffset"] = TimeOffset;
            Guid? UserId = null;
            RequestFeedback request = new RequestFeedback();
            try
            {
                if (Id == Guid.Empty) throw new Exception("No user to manage subscriptions");
                else
                {
                    var user = await userService.GetById(Id).ConfigureAwait(true);
                    if (user == null) throw new Exception("No user to activate");
                    ManageUserSubscriptionDto model = new ManageUserSubscriptionDto
                    {
                        User = user,
                        Subscriptions = await userSubscriptionService.GetAllFromUser(user.Id).ConfigureAwait(true),
                        SubscriptionPlanOptions = await subscriptionPlanService.GetAll(true).ConfigureAwait(true),
                        ActiveSubscriptionBoisterous = await userSubscriptionService.GetActiveSubscriptionPlan(user.Id, SubscriptionPlanType.Boisterous).ConfigureAwait(true),
                        ActiveSubscriptionShortchasePro = await userSubscriptionService.GetActiveSubscriptionPlan(user.Id, SubscriptionPlanType.ShortchasePro).ConfigureAwait(true)
                    };
                    return View(model);
                }
            }
            catch (Exception e)
            {
                await errorLogService.InsertException(e, UserId).ConfigureAwait(true);
                return RedirectToAction("Index", "Error", request);
            }
        }


        public async Task<IActionResult> ManageBoisterousUsers(int TimeOffset = 0)
        {
            ViewData["TimezoneOffset"] = TimeOffset;
            Guid? UserId = null;
            RequestFeedback request = new RequestFeedback();
            try
            {
                UserId = User.Id();
                var countries = (await countryService.GetAll().ConfigureAwait(true));
                BackendPaidUserListDto model = new BackendPaidUserListDto
                {
                    Users = (await userService.GetBoisterousUserList().ConfigureAwait(true)).Select(i => new PaidUserListItemDto { Email = i.Email, FirstName = i.FirstName, LastName = i.LastName, Id = i.Id, LastSeen = null, IsActive = i.IsActive, DateRegistered = i.RowDate }).ToList(),
                    CountriesOptions = countries.OrderBy(o => o.Name).ToList(),
                };

                if (model.Users.Count > 0)
                {
                    foreach (var user in model.Users)
                    {
                        var activeSubscription = await userSubscriptionService.GetActiveSubscriptionPlan(user.Id, SubscriptionPlanType.Boisterous).ConfigureAwait(true);
                        if (activeSubscription != null)
                        {
                            user.ActiveSubscriptionId = activeSubscription.Id;
                            user.SubscriptionName = activeSubscription.Name;
                            user.SubscriptionStart = activeSubscription.SubscriptionStart;
                            user.SubscriptionEnd = activeSubscription.SubscriptionEnd;
                            user.SubscriptionStatus = activeSubscription.PaymentStatus;
                            user.AutoRenewal = activeSubscription.AutoRenew;
                        }
                    }
                }
                model.DefaultCountryId = countries.Where(c => c.IsDefault).Select(c => c.Id).SingleOrDefault();
                model.DefaultCountryPhoneCode = countries.Where(c => c.IsDefault).Select(c => c.Code).SingleOrDefault();
                return View(model);
            }
            catch (Exception e)
            {
                await errorLogService.InsertException(e, UserId).ConfigureAwait(true);
                return RedirectToAction("Index", "Error", request);
            }
        }

        public async Task<IActionResult> ManageBoisterousUserSubscriptions(Guid Id, int TimeOffset = 0)
        {
            ViewData["TimezoneOffset"] = TimeOffset;
            Guid? UserId = null;
            RequestFeedback request = new RequestFeedback();
            try
            {
                if (Id == Guid.Empty) throw new Exception("No user to manage subscriptions");
                else
                {
                    var user = await userService.GetById(Id).ConfigureAwait(true);
                    if (user == null) throw new Exception("No user to activate");
                    ManageUserSubscriptionDto model = new ManageUserSubscriptionDto
                    {
                        User = user,
                        Subscriptions = await userSubscriptionService.GetAllFromUser(user.Id).ConfigureAwait(true),
                        SubscriptionPlanOptions = await subscriptionPlanService.GetAll(true).ConfigureAwait(true),
                        ActiveSubscriptionBoisterous = await userSubscriptionService.GetActiveSubscriptionPlan(user.Id, SubscriptionPlanType.Boisterous).ConfigureAwait(true),
                        ActiveSubscriptionShortchasePro = await userSubscriptionService.GetActiveSubscriptionPlan(user.Id, SubscriptionPlanType.ShortchasePro).ConfigureAwait(true)
                    };
                    return View(model);
                }
            }
            catch (Exception e)
            {
                await errorLogService.InsertException(e, UserId).ConfigureAwait(true);
                return RedirectToAction("Index", "Error", request);
            }
        }



        public async Task<IActionResult> ManageProUsers(int TimeOffset = 0)
        {
            ViewData["TimezoneOffset"] = TimeOffset;
            Guid? UserId = null;
            RequestFeedback request = new RequestFeedback();
            try
            {
                UserId = User.Id();
                var countries = (await countryService.GetAll().ConfigureAwait(true));

                BackendPaidUserListDto model = new BackendPaidUserListDto
                {
                    Users = (await userService.GetProUserList().ConfigureAwait(true)).Select(i => new PaidUserListItemDto { Email = i.Email, FirstName = i.FirstName, LastName = i.LastName, Id = i.Id, LastSeen = null, IsActive = i.IsActive, DateRegistered = i.RowDate }).ToList(),
                    CountriesOptions = countries.OrderBy(o => o.Name).ToList(),
                };

                if (model.Users.Count > 0)
                {
                    foreach (var user in model.Users)
                    {
                        var activeSubscription = await userSubscriptionService.GetActiveSubscriptionPlan(user.Id, SubscriptionPlanType.ShortchasePro).ConfigureAwait(true);
                        if (activeSubscription != null)
                        {
                            user.ActiveSubscriptionId = activeSubscription.Id;
                            user.SubscriptionName = activeSubscription.Name;
                            user.SubscriptionStart = activeSubscription.SubscriptionStart;
                            user.SubscriptionEnd = activeSubscription.SubscriptionEnd;
                            user.SubscriptionStatus = activeSubscription.PaymentStatus;
                            user.AutoRenewal = activeSubscription.AutoRenew;
                        }
                    }
                }
                model.DefaultCountryId = countries.Where(c => c.IsDefault).Select(c => c.Id).SingleOrDefault();
                model.DefaultCountryPhoneCode = countries.Where(c => c.IsDefault).Select(c => c.Code).SingleOrDefault();
                return View(model);
            }
            catch (Exception e)
            {
                await errorLogService.InsertException(e, UserId).ConfigureAwait(true);
                return RedirectToAction("Index", "Error", request);
            }
        }

        public async Task<IActionResult> ManageProUserSubscriptions(Guid Id, int TimeOffset = 0)
        {
            ViewData["TimezoneOffset"] = TimeOffset;
            Guid? UserId = null;
            RequestFeedback request = new RequestFeedback();
            try
            {
                if (Id == Guid.Empty) throw new Exception("No user to manage subscriptions");
                else
                {
                    var user = await userService.GetById(Id).ConfigureAwait(true);
                    if (user == null) throw new Exception("No user to activate");
                    ManageUserSubscriptionDto model = new ManageUserSubscriptionDto
                    {
                        User = user,
                        Subscriptions = await userSubscriptionService.GetAllFromUser(user.Id).ConfigureAwait(true),
                        SubscriptionPlanOptions = await subscriptionPlanService.GetAll(true).ConfigureAwait(true),
                        ActiveSubscriptionBoisterous = await userSubscriptionService.GetActiveSubscriptionPlan(user.Id, SubscriptionPlanType.Boisterous).ConfigureAwait(true),
                        ActiveSubscriptionShortchasePro = await userSubscriptionService.GetActiveSubscriptionPlan(user.Id, SubscriptionPlanType.ShortchasePro).ConfigureAwait(true)
                    };
                    return View(model);
                }
            }
            catch (Exception e)
            {
                await errorLogService.InsertException(e, UserId).ConfigureAwait(true);
                return RedirectToAction("Index", "Error", request);
            }
        }

        public async Task<ActionResult> DownloadSubscriptionReceipt(Guid SubscriptionId)
        {
            try
            {
                string root = hostingEnvironment.ContentRootPath;
                if (SubscriptionId == Guid.Empty) throw new Exception("Submission Id cannot be 0");
                var subscription = await userSubscriptionService.GetById(SubscriptionId).ConfigureAwait(true);
                if (subscription == null) throw new Exception("Subscription Id: " + SubscriptionId + " not found");

                if (System.IO.File.Exists(root + subscription.ReceiptPDF))
                {
                    byte[] fileBytes = System.IO.File.ReadAllBytes(root + @subscription.ReceiptPDF);
                    string extension = Path.GetExtension(subscription.ReceiptPDF);
                    string fileName = subscription.Name + " Subscription Receipt" + extension;
                    return File(fileBytes, System.Net.Mime.MediaTypeNames.Application.Octet, fileName);
                }
                else
                {
                    throw new Exception("Receipt not found.");

                }
            }
            catch (Exception e)
            {
                await errorLogService.InsertException(e).ConfigureAwait(true);
                return RedirectToAction("Index", "Error", "Error downloading PDF");
            }
        }


        [HttpPost]
        public async Task<IActionResult> CancelUserRenewSubscription(Guid Id)
        {
            try
            {
                if (Id == Guid.Empty) throw new Exception("You need to provide a valid subscription to cancel.");

                UserSubscription subscription = await userSubscriptionService.GetById(Id).ConfigureAwait(true);

                if (subscription == null) throw new Exception("You need to provide a valid subscription to cancel.");
                subscription.AutoRenew = false;
                var result = await userSubscriptionService.Update(subscription).ConfigureAwait(true);
                if (result)
                {
                    return Json(new { status = true, messageTitle = "Success!", message = "Subscription auto-renewal cancelled successfully." });
                }
                else
                {
                    throw new Exception("Could not save new subscription");
                }



            }
            catch (Exception e)
            {
                ModelState.AddModelError(string.Empty, "Something went wrong, please try again later");
                await errorLogService.InsertException(e).ConfigureAwait(true);
                return Json(new { status = false, messageTitle = "Error", message = e.Message });
            }
        }



        [HttpPost]
        public async Task<IActionResult> UnCancelUserRenewSubscription(Guid Id)
        {
            try
            {
                if (Id == Guid.Empty) throw new Exception("You need to provide a valid subscription to activated auto-renewal.");

                UserSubscription subscription = await userSubscriptionService.GetById(Id).ConfigureAwait(true);

                if (subscription == null) throw new Exception("You need to provide a valid subscription to activated auto-renewal.");
                subscription.AutoRenew = true;
                var result = await userSubscriptionService.Update(subscription).ConfigureAwait(true);
                if (result)
                {
                    return Json(new { status = true, messageTitle = "Success!", message = "Subscription auto-renewal activated successfully." });
                }
                else
                {
                    throw new Exception("Could not activate auto-renewal on subscription");
                }



            }
            catch (Exception e)
            {
                ModelState.AddModelError(string.Empty, "Something went wrong, please try again later");
                await errorLogService.InsertException(e).ConfigureAwait(true);
                return Json(new { status = false, messageTitle = "Error", message = e.Message });
            }
        }

        [HttpPost]
        public async Task<IActionResult> AddUserSubscriptionBackend(CreateUserSubscriptionBackendDto data)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(data.StartDate)) throw new Exception("You need to provide a Start Date for the new subscription.");
                if (data.SubscriptionId == 0) throw new Exception("You need to provide a Start Date for the new subscription.");
                if (data.GiftById == Guid.Empty) throw new Exception("You need to provide a valid admin for the new subscription.");
                if (data.UserId == Guid.Empty) throw new Exception("You need to provide a valid User for the new subscription.");

                var parseDate = data.StartDate.Split('-');
                DateTime startDate = new DateTime(Convert.ToInt32(parseDate[0]), Convert.ToInt32(parseDate[1]), Convert.ToInt32(parseDate[2]));

                SubscriptionPlan chosenPlan = await subscriptionPlanService.GetById(data.SubscriptionId).ConfigureAwait(true);
                User user = await userService.GetById(data.UserId).ConfigureAwait(true);

                DateTime endDate = startDate.AddMonths(chosenPlan.DurationInMonths);

                UserSubscription newSubscription = new UserSubscription
                {
                    UserId = data.UserId,
                    GiftById = data.GiftById,
                    SubscriptionId = data.SubscriptionId,
                    Deleted = false,
                    Name = chosenPlan.Name,
                    PaidValue = 0.00M,
                    PaymentStatus = UserSubscriptionPaymentStatus.Gift,
                    Type = chosenPlan.Type,
                    SubscriptionPrice = chosenPlan.TotalValuePerDuration,
                    SubscriptionStart = startDate.Date,
                    SubscriptionEnd = endDate.Date,
                    WalletBalanceAfterPurchase = user.WalletBalance,
                    WalletBalanceBeforePurchase = user.WalletBalance
                };

                var UserHasActiveSubscriptionPlan = await userSubscriptionService.UserHasActiveSubscriptionPlan(newSubscription).ConfigureAwait(true);

                if (UserHasActiveSubscriptionPlan)
                {
                    throw new Exception("User has active plan of the same type!");
                }
                else
                {
                    var result = await userSubscriptionService.Insert(newSubscription).ConfigureAwait(true);
                    if (result)
                    {
                        return Json(new { status = true, messageTitle = "Success!", message = "Subscription added successfully." });
                    }
                    else
                    {
                        throw new Exception("Could not save new subscription");
                    }
                }


            }
            catch (Exception e)
            {
                ModelState.AddModelError(string.Empty, "Something went wrong, please try again later");
                await errorLogService.InsertException(e).ConfigureAwait(true);
                return Json(new { status = false, messageTitle = "Error", message = e.Message });
            }
        }


        [HttpPost]
        public async Task<IActionResult> CancelUserSubscriptionBackend(Guid Id)
        {
            try
            {
                if (Id == Guid.Empty) throw new Exception("You need to provide a valid subscription to cancel.");

                UserSubscription subscription = await userSubscriptionService.GetById(Id).ConfigureAwait(true);

                if (subscription == null) throw new Exception("You need to provide a valid subscription to cancel.");
                var result = await userSubscriptionService.Cancel(subscription).ConfigureAwait(true);
                if (result)
                {
                    return Json(new { status = true, messageTitle = "Success!", message = "Subscription cancelled successfully." });
                }
                else
                {
                    throw new Exception("Could not save new subscription");
                }



            }
            catch (Exception e)
            {
                ModelState.AddModelError(string.Empty, "Something went wrong, please try again later");
                await errorLogService.InsertException(e).ConfigureAwait(true);
                return Json(new { status = false, messageTitle = "Error", message = e.Message });
            }
        }
        [HttpPost]
        public async Task<IActionResult> DeleteUserSubscriptionBackend(Guid Id)
        {
            try
            {
                if (Id == Guid.Empty) throw new Exception("You need to provide a valid subscription to cancel.");


                var result = await userSubscriptionService.Delete(Id).ConfigureAwait(true);
                if (result)
                {
                    return Json(new { status = true, messageTitle = "Success!", message = "Subscription deleted successfully." });
                }
                else
                {
                    throw new Exception("Could not save new subscription");
                }



            }
            catch (Exception e)
            {
                ModelState.AddModelError(string.Empty, "Something went wrong, please try again later");
                await errorLogService.InsertException(e).ConfigureAwait(true);
                return Json(new { status = false, messageTitle = "Error", message = e.Message });
            }
        }

        [HttpPost]
        public async Task<IActionResult> AddUser(CreateUserDto model)
        {
            try
            {
                // map dto to entity
                var user = model.ToUser();

                // save
                var result = await userService.CreateShortchaseStandardUserAsync(user, model.Password).ConfigureAwait(true);
                if (result)
                {
                    return Json(new { status = true, messageTitle = "Success!", message = "Registration completed successfully." });
                }
                else throw new Exception("An unexpected error occured. Please try again later.");
            }
            catch (Exception e)
            {
                ModelState.AddModelError(string.Empty, "Something went wrong, please try again later");
                await errorLogService.InsertException(e).ConfigureAwait(true);
                return Json(new { status = false, messageTitle = "Error", message = e.Message });
            }
        }


        [HttpPost]
        public async Task<IActionResult> SwitchStatusUsers(Guid? Id, bool NewStatus)
        {
            try
            {
                if (!Id.HasValue || Id == Guid.Empty) throw new Exception("No user to activate");
                else
                {
                    var user = await userService.GetById(Id.Value).ConfigureAwait(true);
                    if (user == null) throw new Exception("No user to activate");

                    user.IsActive = NewStatus;

                    var result = await userService.UpdateAsync(user).ConfigureAwait(true);
                    if (result)
                    {
                        string message = NewStatus ? "User activated successfully!" : "User deactivated successfully!";
                        return Json(new { status = true, messageTitle = "Success", message = message });
                    }
                    else throw new Exception("Error switching the user status. Try again later.");
                }
            }
            catch (Exception e)
            {
                ModelState.AddModelError(string.Empty, "Something went wrong, please try again later");
                await errorLogService.InsertException(e).ConfigureAwait(true);
                return Json(new { status = false, messageTitle = "Error", message = e.Message });
            }
        }



        [HttpPost]
        public async Task<IActionResult> SoftDeleteUsers(Guid? Id)
        {
            try
            {
                if (!Id.HasValue || Id == Guid.Empty) throw new Exception("No user to delete");
                else
                {
                    var user = await userService.GetById(Id.Value).ConfigureAwait(true);
                    if (user == null) throw new Exception("No user to delete");

                    user.IsDeleted = true;
                    user.IsActive = false;

                    var result = await userService.UpdateAsync(user).ConfigureAwait(true);
                    if (result)
                    {
                        string message = "User deleted successfully!";
                        return Json(new { status = true, messageTitle = "Success", message = message });
                    }
                    else throw new Exception("Error deleting the user. Try again later.");
                }
            }
            catch (Exception e)
            {
                ModelState.AddModelError(string.Empty, "Something went wrong, please try again later");
                await errorLogService.InsertException(e).ConfigureAwait(true);
                return Json(new { status = false, messageTitle = "Error", message = e.Message });
            }
        }

        [HttpPost]
        public async Task<IActionResult> EditUser(UpdateUserDto userInfo)
        {
            try
            {
                if (userInfo.Id == Guid.Empty) throw new Exception("No user to update");
                else
                {
                    var user = await userService.GetById(userInfo.Id).ConfigureAwait(true);
                    if (user == null) throw new Exception("No user to update");

                    user.FirstName = userInfo.FirstName;
                    user.LastName = userInfo.LastName;
                    user.PhoneNumber = userInfo.PhoneNumber;
                    user.PhoneCountryId = userInfo.Country;


                    if (userInfo.ChangePassword)
                    {
                        var updatePasswordResult = await userService.ResetPasswordAsync(user.Email, userInfo.Password);
                        if (!updatePasswordResult) throw new Exception("Error updating user password. Try again later.");
                    }

                    var result = await userService.UpdateAsync(user).ConfigureAwait(true);
                    if (result)
                    {
                        return Json(new { status = true, messageTitle = "Success", message = "User updated successfully!" });
                    }
                    else throw new Exception("Error updating user. Try again later.");
                }
            }
            catch (Exception e)
            {
                ModelState.AddModelError(string.Empty, "Something went wrong, please try again later");
                await errorLogService.InsertException(e).ConfigureAwait(true);
                return Json(new { status = false, messageTitle = "Error", message = e.Message });
            }
        }


        [HttpPost]
        public async Task<IActionResult> MakeIndividualPaypalPayout(string PaypalEmailToSend, decimal AmountToBeSend, string stringifiedFullObj, Guid UserId)
        {
            RequestFeedback request = new RequestFeedback();
            try
            {

                if (string.IsNullOrWhiteSpace(PaypalEmailToSend)) throw new Exception("No email to send!");
                if (AmountToBeSend <= 0.00m) throw new Exception("No valid amount to send!");
                string BatchStamp = DateTime.UtcNow.Year.ToString()
                    + DateTime.UtcNow.Month.ToString()
                    + DateTime.UtcNow.Day.ToString()
                    + DateTime.UtcNow.Hour.ToString()
                    + DateTime.UtcNow.Minute.ToString()
                    + DateTime.UtcNow.Second.ToString()
                    + DateTime.UtcNow.Millisecond.ToString();

                PaypalSettings paypalSettings = await paypalSettingsService.GetDefault().ConfigureAwait(true);
                string ClientId = paypalSettings.ClientID;
                string Secret = paypalSettings.Secret;


                var requestPaypal = await RequestPayPalToken(ClientId, Secret).ConfigureAwait(true);
                if (requestPaypal == null) throw new Exception("No token obtained");
                string paypalToken = requestPaypal.access_token;

                bool payoutRequestSuccess = await RequestPayPalPayout(paypalToken, stringifiedFullObj, UserId, AmountToBeSend).ConfigureAwait(true);

                if (payoutRequestSuccess) return Json(new { status = true, messageTitle = "Success", message = "Individual payout finished successfully!" });
                else throw new Exception("Insuficient funds!");
            }
            catch (Exception e)
            {
                ModelState.AddModelError(string.Empty, "Something went wrong, please try again later");
                await errorLogService.InsertException(e).ConfigureAwait(true);
                return Json(new { status = false, messageTitle = "Error", message = e.Message });
            }
        }


        [HttpPost]
        public async Task<IActionResult> FinishMassPayout(string BatchStamp, string stringifiedFullObj, ICollection<PaypalMassBatchPayoutDto> Payouts)
        {
            RequestFeedback request = new RequestFeedback();
            try
            {


                PaypalSettings paypalSettings = await paypalSettingsService.GetDefault().ConfigureAwait(true);
                string ClientId = paypalSettings.ClientID;
                string Secret = paypalSettings.Secret;


                var requestPaypal = await RequestPayPalToken(ClientId, Secret).ConfigureAwait(true);
                if (requestPaypal == null) throw new Exception("No token obtained");
                string paypalToken = requestPaypal.access_token;

                bool payoutRequestSuccess = await RequestPayPalBatchMassPayout(paypalToken, stringifiedFullObj, Payouts).ConfigureAwait(true);

                if (payoutRequestSuccess) return Json(new { status = true, messageTitle = "Success", message = "Mass payout finished successfully!" });
                else throw new Exception("Insuficient funds!");
            }
            catch (Exception e)
            {
                ModelState.AddModelError(string.Empty, "Something went wrong, please try again later");
                await errorLogService.InsertException(e).ConfigureAwait(true);
                return Json(new { status = false, messageTitle = "Error", message = e.Message });
            }
        }

        [HttpPost]
        public async Task<IActionResult> CheckIndividualPaypalPayout(string url, Guid PayoutId)
        {
            RequestFeedback request = new RequestFeedback();
            try
            {
                UserPayout payout = await userPayoutService.GetById(PayoutId).ConfigureAwait(true);
                if (payout == null) throw new Exception("No payout information to check!");
                PaypalSettings paypalSettings = await paypalSettingsService.GetDefault().ConfigureAwait(true);
                string ClientId = paypalSettings.ClientID;
                string Secret = paypalSettings.Secret;


                var requestPaypal = await RequestPayPalToken(ClientId, Secret).ConfigureAwait(true);
                if (requestPaypal == null) throw new Exception("No token obtained");
                string paypalToken = requestPaypal.access_token;

                bool payoutRequestSuccess = await CheckPayPalPayout(paypalToken, url, payout).ConfigureAwait(true);

                if (payoutRequestSuccess) return Json(new { status = true, messageTitle = "Success", message = "Payout checked successfully!" });
                else throw new Exception("Error resolving payout request to paypal.");
            }
            catch (Exception e)
            {
                ModelState.AddModelError(string.Empty, "Something went wrong, please try again later");
                await errorLogService.InsertException(e).ConfigureAwait(true);
                return Json(new { status = false, messageTitle = "Error", message = e.Message });
            }
        }
        private async Task<PayPalClient.PayPalTokenModel> RequestPayPalToken(string APIClientId, string APISecret)
        {
            // Discussion about SSL secure channel
            // http://stackoverflow.com/questions/32994464/could-not-create-ssl-tls-secure-channel-despite-setting-servercertificatevalida
            //ServicePointManager.ServerCertificateValidationCallback += (sender, cert, chain, sslPolicyErrors) => true;
            //ServicePointManager.SecurityProtocol = SecurityProtocolType.Ssl3 | SecurityProtocolType.Tls | SecurityProtocolType.Tls11 | SecurityProtocolType.Tls12;

            try
            {

                using (var client = new System.Net.Http.HttpClient())
                {
                    var byteArray = Encoding.UTF8.GetBytes(APIClientId + ":" + APISecret);
                    client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Basic", Convert.ToBase64String(byteArray));

                    var url = new Uri(APILinks.PayPal.GetToken, UriKind.Absolute);

                    client.DefaultRequestHeaders.IfModifiedSince = DateTime.UtcNow;

                    var requestParams = new List<KeyValuePair<string, string>>
                            {
                                new KeyValuePair<string, string>("grant_type", "client_credentials")
                            };

                    var content = new FormUrlEncodedContent(requestParams);
                    var webresponse = await client.PostAsync(url, content);
                    var jsonString = await webresponse.Content.ReadAsStringAsync();

                    // response will deserialized using Jsonconver
                    PayPalClient.PayPalTokenModel payPalTokenModel = JsonConvert.DeserializeObject<PayPalClient.PayPalTokenModel>(jsonString);
                    return payPalTokenModel;
                }
            }
            catch (System.Exception ex)
            {
                //TODO: Log connection error
                throw;
            }
        }

        private async Task<bool> RequestPayPalPayout(string token, string stringifiedFullObj, Guid UserId, decimal AmountToBeSend)
        {
            // Discussion about SSL secure channel
            // http://stackoverflow.com/questions/32994464/could-not-create-ssl-tls-secure-channel-despite-setting-servercertificatevalida
            //ServicePointManager.ServerCertificateValidationCallback += (sender, cert, chain, sslPolicyErrors) => true;
            //ServicePointManager.SecurityProtocol = SecurityProtocolType.Ssl3 | SecurityProtocolType.Tls | SecurityProtocolType.Tls11 | SecurityProtocolType.Tls12;

            try
            {

                using (var client = new System.Net.Http.HttpClient())
                {
                    client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);
                    var url = new Uri(APILinks.PayPal.Payouts, UriKind.Absolute);
                    client.DefaultRequestHeaders.IfModifiedSince = DateTime.UtcNow;

                    /*var requestParams = new List<KeyValuePair<string, string>>();

                    var sender_batch_header = new KeyValuePair<string, string>("sender_batch_header", sender_batch_headerStr);
                    var items = new KeyValuePair<string, string>("items", itemsStr);
                    requestParams.Add(sender_batch_header);
                    requestParams.Add(items);*/

                    var content = new StringContent(stringifiedFullObj, Encoding.UTF8, "application/json");//new FormUrlEncodedContent(requestParams);
                    var webresponse = await client.PostAsync(url, content);
                    var jsonString = await webresponse.Content.ReadAsStringAsync();

                    if (jsonString.ToUpper().Contains("INSUFFICIENT_FUNDS"))
                    {
                        return false;
                    }
                    else
                    {
                        // response will deserialized using Jsonconver
                        PayoutResponseJSON response = JsonConvert.DeserializeObject<PayoutResponseJSON>(jsonString);



                        UserPayout payoutInfo = new UserPayout
                        {
                            UserId = UserId,
                            PayoutBatchCancelledDate = null,
                            PayoutBatchCompletedDate = null,
                            PayoutBatchCreatedDate = DateTime.UtcNow,
                            PayoutBatchId = response.batch_header.payout_batch_id,
                            PayoutBatchStatus = response.batch_header.batch_status,
                            PayoutSenderBatchId = response.batch_header.sender_batch_header.sender_batch_id,
                            PayoutBatchCheckLink = response.links[0].href,
                            Value = AmountToBeSend,
                            Fees = 0.00m
                        };

                        var result = await userPayoutService.Insert(payoutInfo).ConfigureAwait(true);

                        if (!result) throw new Exception("Could not save new payout");

                        return true;
                    }

                }
            }
            catch (System.Exception ex)
            {
                //TODO: Log connection error
                throw;
            }
        }



        private async Task<bool> RequestPayPalCreateProduct(string token, string stringifiedFullObj, SubscriptionPlan plan)
        {
            try
            {

                using (var client = new System.Net.Http.HttpClient())
                {
                    client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);
                    var url = new Uri(APILinks.PayPal.Products, UriKind.Absolute);
                    client.DefaultRequestHeaders.IfModifiedSince = DateTime.UtcNow;


                    var content = new StringContent(stringifiedFullObj, Encoding.UTF8, "application/json");
                    var webresponse = await client.PostAsync(url, content);
                    var jsonString = await webresponse.Content.ReadAsStringAsync();

                    dynamic response = JsonConvert.DeserializeObject(jsonString);

                    plan.PaypalProductId = response.id;
                    plan.PaypalProductName = response.name;

                    bool result = await subscriptionPlanService.UpdatePaypalProductCreated(plan).ConfigureAwait(true);

                    if (result) return true;
                    else return false;


                }
            }
            catch (System.Exception ex)
            {
                //TODO: Log connection error
                throw;
            }
        }


        private async Task<bool> RequestPayPalCreateSubscriptionPlan(string token, string stringifiedFullObj, SubscriptionPlan plan)
        {
            try
            {

                using (var client = new System.Net.Http.HttpClient())
                {
                    client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);
                    var url = new Uri(APILinks.PayPal.SubscriptionPlans, UriKind.Absolute);
                    client.DefaultRequestHeaders.IfModifiedSince = DateTime.UtcNow;


                    var content = new StringContent(stringifiedFullObj, Encoding.UTF8, "application/json");
                    var webresponse = await client.PostAsync(url, content);
                    var jsonString = await webresponse.Content.ReadAsStringAsync();

                    dynamic response = JsonConvert.DeserializeObject(jsonString);

                    plan.PaypalSubscriptionPlanId = response.id;
                    plan.PaypalSubscriptionPlanName = response.name;
                    plan.PaypalSubscriptionPlanDescription = response.description;

                    bool result = await subscriptionPlanService.Update(plan).ConfigureAwait(true);

                    if (result) return true;
                    else return false;


                }
            }
            catch (System.Exception ex)
            {
                //TODO: Log connection error
                throw;
            }
        }


        private async Task<Guid?> RequestPayPalPayoutWithGuid(string token, string stringifiedFullObj, Guid UserId, decimal AmountToBeSend)
        {
            // Discussion about SSL secure channel
            // http://stackoverflow.com/questions/32994464/could-not-create-ssl-tls-secure-channel-despite-setting-servercertificatevalida
            //ServicePointManager.ServerCertificateValidationCallback += (sender, cert, chain, sslPolicyErrors) => true;
            //ServicePointManager.SecurityProtocol = SecurityProtocolType.Ssl3 | SecurityProtocolType.Tls | SecurityProtocolType.Tls11 | SecurityProtocolType.Tls12;

            try
            {

                using (var client = new System.Net.Http.HttpClient())
                {
                    client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);
                    var url = new Uri(APILinks.PayPal.Payouts, UriKind.Absolute);
                    client.DefaultRequestHeaders.IfModifiedSince = DateTime.UtcNow;

                    /*var requestParams = new List<KeyValuePair<string, string>>();

                    var sender_batch_header = new KeyValuePair<string, string>("sender_batch_header", sender_batch_headerStr);
                    var items = new KeyValuePair<string, string>("items", itemsStr);
                    requestParams.Add(sender_batch_header);
                    requestParams.Add(items);*/

                    var content = new StringContent(stringifiedFullObj, Encoding.UTF8, "application/json");//new FormUrlEncodedContent(requestParams);
                    var webresponse = await client.PostAsync(url, content);
                    var jsonString = await webresponse.Content.ReadAsStringAsync();

                    if (jsonString.ToUpper().Contains("INSUFFICIENT_FUNDS"))
                    {
                        return null;
                    }
                    else
                    {
                        // response will deserialized using Jsonconver
                        PayoutResponseJSON response = JsonConvert.DeserializeObject<PayoutResponseJSON>(jsonString);



                        UserPayout payoutInfo = new UserPayout
                        {
                            Id = Guid.NewGuid(),
                            UserId = UserId,
                            PayoutBatchCancelledDate = null,
                            PayoutBatchCompletedDate = null,
                            PayoutBatchCreatedDate = DateTime.UtcNow,
                            PayoutBatchId = response.batch_header.payout_batch_id,
                            PayoutBatchStatus = response.batch_header.batch_status,
                            PayoutSenderBatchId = response.batch_header.sender_batch_header.sender_batch_id,
                            PayoutBatchCheckLink = response.links[0].href,
                            Value = AmountToBeSend,
                            Fees = 0.00m
                        };

                        var result = await userPayoutService.Insert(payoutInfo).ConfigureAwait(true);

                        if (!result) throw new Exception("Could not save new payout");

                        return payoutInfo.Id;
                    }

                }
            }
            catch (System.Exception ex)
            {
                //TODO: Log connection error
                throw;
            }
        }



        private async Task<bool> RequestPayPalBatchPayout(string token, string stringifiedFullObj, Guid OrderId)
        {
            try
            {
                OrderDetailsDto data = await orderService.GetOrderDetails(OrderId).ConfigureAwait(true);
                OrderPayoutDetailsDto model = new OrderPayoutDetailsDto();
                model = model.FromOrderDetailsDto(data);

                if (model.OrderItems.Count > 0)
                {

                    foreach (var item in model.OrderItems)
                    {
                        OrderItemPayoutDto itemToPay = new OrderItemPayoutDto
                        {
                            UserToPay = item.SoldBy,
                            UserToPayId = item.BetListing.PostedbyId,
                            UserEmailToPay = string.IsNullOrWhiteSpace(item.BetListing.Postedby.PaypalAccountEmail) ? item.BetListing.Postedby.Email : item.BetListing.Postedby.PaypalAccountEmail,
                            ValueToPay = item.Price - ((model.Order.ServiceFeePercent * item.Price) / 100),
                            PaypalConnected = !string.IsNullOrWhiteSpace(item.BetListing.Postedby.PaypalAccountEmail)
                        };
                        if (!item.PayoutId.HasValue)
                        {
                            model.Payouts.Add(itemToPay);
                        }
                    }
                    model.Payouts = model.Payouts.GroupBy(g => g.UserToPayId).Select(x => new OrderItemPayoutDto { UserToPayId = x.Key, UserToPay = x.First().UserToPay, ValueToPay = x.Sum(y => y.ValueToPay), PaypalConnected = x.First().PaypalConnected, UserEmailToPay = x.First().UserEmailToPay }).OrderBy(o => o.UserToPayId).ToList();

                }
                if (model.Payouts.Count <= 0) throw new Exception("No payout to process!");

                using (var client = new System.Net.Http.HttpClient())
                {
                    client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);
                    var url = new Uri(APILinks.PayPal.Payouts, UriKind.Absolute);
                    client.DefaultRequestHeaders.IfModifiedSince = DateTime.UtcNow;

                    var content = new StringContent(stringifiedFullObj, Encoding.UTF8, "application/json");
                    var webresponse = await client.PostAsync(url, content);
                    var jsonString = await webresponse.Content.ReadAsStringAsync();

                    if (jsonString.ToUpper().Contains("INSUFFICIENT_FUNDS"))
                    {
                        return false;
                    }
                    else
                    {
                        // response will deserialized using Jsonconver
                        PayoutResponseJSON response = JsonConvert.DeserializeObject<PayoutResponseJSON>(jsonString);

                        ICollection<UserPayout> newPayouts = new List<UserPayout>();
                        foreach (var payout in model.Payouts)
                        {
                            UserPayout payoutInfo = new UserPayout
                            {
                                Id = Guid.NewGuid(),
                                UserId = payout.UserToPayId,
                                PayoutBatchCancelledDate = null,
                                PayoutBatchCompletedDate = null,
                                PayoutBatchCreatedDate = DateTime.UtcNow,
                                PayoutBatchId = response.batch_header.payout_batch_id,
                                PayoutBatchStatus = response.batch_header.batch_status,
                                PayoutSenderBatchId = response.batch_header.sender_batch_header.sender_batch_id,
                                PayoutBatchCheckLink = response.links[0].href,
                                Value = payout.ValueToPay,
                                Fees = 0.00m,
                                RowDate = DateTime.UtcNow
                            };

                            newPayouts.Add(payoutInfo);
                        }


                        bool result = await orderService.UpdateOrderItemPayouts(OrderId, newPayouts).ConfigureAwait(true);
                        if (!result) throw new Exception("Error updating payouts!");
                        return true;
                    }

                }
            }
            catch (System.Exception ex)
            {
                //TODO: Log connection error
                throw;
            }
        }




        private async Task<bool> RequestPayPalBatchMassPayout(string token, string stringifiedFullObj, ICollection<PaypalMassBatchPayoutDto> Payouts)
        {
            try
            {


                using (var client = new System.Net.Http.HttpClient())
                {
                    client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);
                    var url = new Uri(APILinks.PayPal.Payouts, UriKind.Absolute);
                    client.DefaultRequestHeaders.IfModifiedSince = DateTime.UtcNow;

                    var content = new StringContent(stringifiedFullObj, Encoding.UTF8, "application/json");
                    var webresponse = await client.PostAsync(url, content);
                    var jsonString = await webresponse.Content.ReadAsStringAsync();

                    if (jsonString.ToUpper().Contains("INSUFFICIENT_FUNDS"))
                    {
                        return false;
                    }
                    else
                    {
                        // response will deserialized using Jsonconver
                        PayoutResponseJSON response = JsonConvert.DeserializeObject<PayoutResponseJSON>(jsonString);

                        ICollection<UserPayout> newPayouts = new List<UserPayout>();
                        foreach (var payout in Payouts)
                        {
                            UserPayout payoutInfo = new UserPayout
                            {
                                Id = Guid.NewGuid(),
                                UserId = payout.UserId,
                                PayoutBatchCancelledDate = null,
                                PayoutBatchCompletedDate = null,
                                PayoutBatchCreatedDate = DateTime.UtcNow,
                                PayoutBatchId = response.batch_header.payout_batch_id,
                                PayoutBatchStatus = response.batch_header.batch_status,
                                PayoutSenderBatchId = response.batch_header.sender_batch_header.sender_batch_id,
                                PayoutBatchCheckLink = response.links[0].href,
                                Value = payout.Value,
                                Fees = 0.00m,
                                RowDate = DateTime.UtcNow
                            };

                            newPayouts.Add(payoutInfo);
                        }


                        bool result = await userPayoutService.Insert(newPayouts).ConfigureAwait(true);
                        if (!result) throw new Exception("Error updating payouts!");
                        return true;
                    }

                }
            }
            catch (System.Exception ex)
            {
                //TODO: Log connection error
                throw;
            }
        }

        private async Task<bool> CheckPayPalPayout(string token, string urlLink, UserPayout payout)
        {
            // Discussion about SSL secure channel
            // http://stackoverflow.com/questions/32994464/could-not-create-ssl-tls-secure-channel-despite-setting-servercertificatevalida
            //ServicePointManager.ServerCertificateValidationCallback += (sender, cert, chain, sslPolicyErrors) => true;
            //ServicePointManager.SecurityProtocol = SecurityProtocolType.Ssl3 | SecurityProtocolType.Tls | SecurityProtocolType.Tls11 | SecurityProtocolType.Tls12;

            try
            {

                using (var client = new System.Net.Http.HttpClient())
                {
                    client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);
                    var url = new Uri(urlLink + "?fields=batch_header", UriKind.Absolute);
                    client.DefaultRequestHeaders.IfModifiedSince = DateTime.UtcNow;


                    var webresponse = await client.GetAsync(url);
                    var jsonString = await webresponse.Content.ReadAsStringAsync();

                    // response will deserialized using Jsonconver
                    PayoutResponseCheckJSON response = JsonConvert.DeserializeObject<PayoutResponseCheckJSON>(jsonString);

                    if (response.batch_header.batch_status == PaypalPayoutStatus.SUCCESS)
                    {
                        payout.PayoutBatchCompletedDate = DateTime.UtcNow;
                        payout.PayoutBatchStatus = PaypalPayoutStatus.SUCCESS;
                    }
                    else if (response.batch_header.batch_status == PaypalPayoutStatus.CANCELED)
                    {
                        payout.PayoutBatchCancelledDate = DateTime.UtcNow;
                        payout.PayoutBatchStatus = PaypalPayoutStatus.CANCELED;
                    }
                    else
                    {
                        payout.PayoutBatchStatus = response.batch_header.batch_status;
                    }
                    payout.Fees = Convert.ToDecimal(response.batch_header.fees.value);

                    var result = await userPayoutService.Update(payout).ConfigureAwait(true);

                    if (!result) throw new Exception("Could not update new payout");

                    return true;
                }
            }
            catch (System.Exception ex)
            {
                //TODO: Log connection error
                throw;
            }
        }
        #endregion

        #region Bet Listing


        public async Task<IActionResult> BetListings(int TimeOffset = 0)
        {
            Guid? UserId = null;
            RequestFeedback request = new RequestFeedback();
            try
            {
                ViewData["TimezoneOffset"] = TimeOffset;
                UserId = User.Id();
                BetListingListDto model = new BetListingListDto
                {
                    BetListings = await betListingService.GetAll(false).ConfigureAwait(true),
                    BetListingsTypesOptions = new List<SelectListItem>(),
                    BetListingsOddFormatOptions = new List<SelectListItem>(),
                    CategoriesOptions = (await listingCategoryService.GetAll(true).ConfigureAwait(true)).Select(i => new SelectListItem { Value = i.Id.ToString(), Text = i.Name }).OrderBy(o => o.Text).ToList(),
                    BookmakerOptions = (await bookmakerService.GetAll(true).ConfigureAwait(true)).Select(i => new SelectListItem { Value = i.Id.ToString(), Text = i.Description }).OrderBy(o => o.Text).ToList()
                };

                model.BetListingsTypesOptions.Add(new SelectListItem { Value = BetListingType.Premium, Text = BetListingType.Premium });
                model.BetListingsTypesOptions.Add(new SelectListItem { Value = BetListingType.Live, Text = BetListingType.Live });
                model.BetListingsTypesOptions.Add(new SelectListItem { Value = BetListingType.Free, Text = BetListingType.Free });

                model.BetListingsOddFormatOptions.Add(new SelectListItem { Value = BetListingOddsFormat.Decimal, Text = BetListingOddsFormat.Decimal });
                model.BetListingsOddFormatOptions.Add(new SelectListItem { Value = BetListingOddsFormat.Fractional, Text = BetListingOddsFormat.Fractional, Disabled = true });
                model.BetListingsOddFormatOptions.Add(new SelectListItem { Value = BetListingOddsFormat.Moneyline, Text = BetListingOddsFormat.Moneyline, Disabled = true });

                return View(model);
            }
            catch (Exception e)
            {
                await errorLogService.InsertException(e, UserId).ConfigureAwait(true);
                return RedirectToAction("Index", "Error", request);
            }
        }


        public async Task<IActionResult> BetListingReports(Guid Id, int TimeOffset = 0)
        {
            Guid? UserId = null;
            RequestFeedback request = new RequestFeedback();
            try
            {
                ViewData["TimezoneOffset"] = TimeOffset;
                UserId = User.Id();
                if (Id == Guid.Empty) throw new Exception("No listing found with given Id");
                BetListingReportListDto model = new BetListingReportListDto
                {
                    BetListing = await betListingService.GetById(Id).ConfigureAwait(true),
                    Reports = await betListingReportService.GetAllByListingId(Id).ConfigureAwait(true)
                };

                return View(model);
            }
            catch (Exception e)
            {
                await errorLogService.InsertException(e, UserId).ConfigureAwait(true);
                return RedirectToAction("Index", "Error", request);
            }
        }

        [HttpPost]
        public async Task<IActionResult> AddBetListing(CreateBetListing newListingInfo)
        {
            try
            {
                if (newListingInfo == null) throw new Exception("No listing to add");
                else
                {
                    if (string.IsNullOrWhiteSpace(newListingInfo.Analysis)) throw new Exception("Analysis is mandatory!");
                    if (string.IsNullOrWhiteSpace(newListingInfo.OddsFormat)) throw new Exception("Odds Format is mandatory!");
                    if (newListingInfo.Pick == 0) throw new Exception("Pick is mandatory!");
                    if (newListingInfo.Bookmaker == 0) throw new Exception("Where to Play is mandatory!");
                    if (newListingInfo.Tip == 0) throw new Exception("Tip is mandatory!");
                    if (newListingInfo.Market == 0) throw new Exception("Market is mandatory!");
                    if (string.IsNullOrWhiteSpace(newListingInfo.PickType)) throw new Exception("Pick Type is mandatory!");
                    if (string.IsNullOrWhiteSpace(newListingInfo.Title)) throw new Exception("Title is mandatory!");
                    if (string.IsNullOrWhiteSpace(newListingInfo.StartTime)) throw new Exception("Start Time is mandatory!");
                    if (string.IsNullOrWhiteSpace(newListingInfo.FinishTime)) throw new Exception("Finish Time is mandatory!");
                    if (newListingInfo.HasSubcategories == 1 && (!newListingInfo.SubCategoryId.HasValue || newListingInfo.SubCategoryId == 0)) throw new Exception("SubCategory is mandatory!");
                    if (newListingInfo.CategoryId == 0) throw new Exception("Category is mandatory!");
                    if (Convert.ToDecimal(newListingInfo.Odds) <= 0) throw new Exception("Odds is mandatory!");
                    if (newListingInfo.PickType != BetListingType.Free && newListingInfo.Price <= 0) throw new Exception("Price is mandatory!");

                    /*DateTime dateStart = DateHelper.FromISO(newListingInfo.StartTime);
                    dateStart = dateStart.AddMinutes(newListingInfo.TimezoneOffset);
                    DateTime dateEnd = DateHelper.FromISO(newListingInfo.FinishTime);
                    dateEnd = dateEnd.AddMinutes(newListingInfo.TimezoneOffset);

                    if (dateEnd < dateStart) throw new Exception("Finish time cannot be before Start Time");
                    if (dateStart > DateTime.UtcNow) throw new Exception("You cannot post a pick after the match starts!");*/

                    BetListing newBetListing = new BetListing
                    {
                        Analysis = newListingInfo.Analysis,
                        CategoryId = newListingInfo.CategoryId,
                        DateVerifiedByApi = null,
                        Deleted = false,
                        IsReported = false,
                        Description = newListingInfo.Description,
                        IsCorrect = null,
                        Odds = newListingInfo.Odds,
                        OddsFormat = newListingInfo.OddsFormat,
                        PickId = newListingInfo.Pick,
                        PickType = newListingInfo.PickType,
                        PostedbyId = newListingInfo.PostedbyId,
                        BookmakerId = newListingInfo.Bookmaker,
                        Price = newListingInfo.Price,
                        ResultVerificationByApi = null,
                        Profit = newListingInfo.Profit,
                        Stake = newListingInfo.Stake,
                        Title = newListingInfo.Title,
                        MarketId = newListingInfo.Market,
                        TipId = newListingInfo.Tip,
                        SubCategoryId = newListingInfo.SubCategoryId.HasValue ? newListingInfo.SubCategoryId : null,
                        Views = 0,
                        TimezoneOffset = newListingInfo.TimezoneOffset
                    };

                    var activeProPlan = await userSubscriptionService.GetActiveSubscriptionPlan(newListingInfo.PostedbyId, SubscriptionPlanType.ShortchasePro).ConfigureAwait(true);
                    if (activeProPlan == null) newBetListing.IsProCapperListing = false;
                    else newBetListing.IsProCapperListing = true;

                    newBetListing.IsBoisterousListing = await betListingService.IsBoisterouListing(newBetListing).ConfigureAwait(true);

                    var result = await betListingService.Insert(newBetListing).ConfigureAwait(true);
                    if (result)
                    {
                        return Json(new { status = true, messageTitle = "Success", message = "New listing saved successfully!" });
                    }
                    else throw new Exception("Error creating new listing. Try again later.");
                }
            }
            catch (Exception e)
            {
                ModelState.AddModelError(string.Empty, "Something went wrong, please try again later");
                await errorLogService.InsertException(e).ConfigureAwait(true);
                return Json(new { status = false, messageTitle = "Error", message = e.Message });
            }
        }

        [HttpPost]
        public async Task<IActionResult> DeleteBetListingBatch(Guid[] Ids)
        {
            try
            {
                if (Ids == null || Ids.Count() <= 0) return Json(new { status = true, messageTitle = "No data to delete.", message = "" });
                else
                {
                    var result = await betListingService.DeleteBatch(Ids).ConfigureAwait(true);
                    if (result)
                    {
                        string message = "Bet listings deleted successfully!";
                        return Json(new { status = true, messageTitle = "Success", message = message });
                    }
                    else throw new Exception("Error activating the address. Try again later.");
                }
            }
            catch (Exception e)
            {
                ModelState.AddModelError(string.Empty, "Something went wrong, please try again later");
                await errorLogService.InsertException(e).ConfigureAwait(true);
                return Json(new { status = false, messageTitle = "Error", message = e.Message });
            }
        }

        [HttpPost]
        public async Task<IActionResult> DeleteBetListing(Guid? Id)
        {
            try
            {
                if (!Id.HasValue || Id == Guid.Empty) throw new Exception("No bet listing to delete");
                else
                {
                    var result = await betListingService.Delete(Id.Value).ConfigureAwait(true);
                    if (result)
                    {
                        string message = "Bet listing deleted successfully!";
                        return Json(new { status = true, messageTitle = "Success", message = message });
                    }
                    else throw new Exception("Error activating the address. Try again later.");
                }
            }
            catch (Exception e)
            {
                ModelState.AddModelError(string.Empty, "Something went wrong, please try again later");
                await errorLogService.InsertException(e).ConfigureAwait(true);
                return Json(new { status = false, messageTitle = "Error", message = e.Message });
            }
        }

        [HttpPost]
        public async Task<IActionResult> EditBetListing(UpdateBetListing listingInfo)
        {
            try
            {
                if (listingInfo == null) throw new Exception("No listing to edit");
                else
                {
                    if (listingInfo.Id == Guid.Empty) throw new Exception("No listing found with given Id!");
                    if (string.IsNullOrWhiteSpace(listingInfo.Analysis)) throw new Exception("Analysis is mandatory!");
                    if (string.IsNullOrWhiteSpace(listingInfo.OddsFormat)) throw new Exception("Odds Format is mandatory!");
                    if (listingInfo.Pick == 0) throw new Exception("Pick is mandatory!");
                    if (listingInfo.Bookmaker == 0) throw new Exception("Where to Play is mandatory!");
                    if (listingInfo.Tip == 0) throw new Exception("Tip is mandatory!");
                    if (listingInfo.Market == 0) throw new Exception("Market is mandatory!");
                    if (string.IsNullOrWhiteSpace(listingInfo.PickType)) throw new Exception("Pick Type is mandatory!");
                    if (string.IsNullOrWhiteSpace(listingInfo.Title)) throw new Exception("Title is mandatory!");
                    if (string.IsNullOrWhiteSpace(listingInfo.StartTime)) throw new Exception("Start Time is mandatory!");
                    if (string.IsNullOrWhiteSpace(listingInfo.FinishTime)) throw new Exception("Finish Time is mandatory!");
                    if (listingInfo.HasSubcategories == 1 && (!listingInfo.SubCategoryId.HasValue || listingInfo.SubCategoryId == 0)) throw new Exception("SubCategory is mandatory!");
                    if (listingInfo.CategoryId == 0) throw new Exception("Category is mandatory!");
                    if (Convert.ToDecimal(listingInfo.Odds) <= 0) throw new Exception("Odds is mandatory!");
                    if (listingInfo.PickType != BetListingType.Free && listingInfo.Price <= 0) throw new Exception("Price is mandatory!");
                    /*
                    DateTime dateStart = DateHelper.FromISO(listingInfo.StartTime);
                    dateStart = dateStart.AddMinutes(listingInfo.TimezoneOffset);
                    DateTime dateEnd = DateHelper.FromISO(listingInfo.FinishTime);
                    dateEnd = dateEnd.AddMinutes(listingInfo.TimezoneOffset);*/

                    BetListing betListing = await betListingService.GetById(listingInfo.Id).ConfigureAwait(true);


                    betListing.Analysis = listingInfo.Analysis;
                    betListing.CategoryId = listingInfo.CategoryId;
                    betListing.Description = listingInfo.Description;
                    betListing.Odds = listingInfo.Odds;
                    betListing.OddsFormat = listingInfo.OddsFormat;
                    betListing.PickId = listingInfo.Pick;
                    betListing.PickType = listingInfo.PickType;
                    betListing.MarketId = listingInfo.Market;
                    betListing.Price = listingInfo.Price;
                    betListing.Profit = listingInfo.Profit;
                    betListing.Stake = listingInfo.Stake;
                    betListing.Title = listingInfo.Title;
                    betListing.BookmakerId = listingInfo.Bookmaker;
                    betListing.TipId = listingInfo.Tip;
                    betListing.SubCategoryId = listingInfo.SubCategoryId.HasValue ? listingInfo.SubCategoryId : null;

                    var activeProPlan = await userSubscriptionService.GetActiveSubscriptionPlan(listingInfo.PostedbyId, SubscriptionPlanType.ShortchasePro).ConfigureAwait(true);
                    if (activeProPlan == null) betListing.IsProCapperListing = false;
                    else betListing.IsProCapperListing = true;

                    betListing.IsBoisterousListing = await betListingService.IsBoisterouListing(betListing).ConfigureAwait(true);

                    var result = await betListingService.Update(betListing).ConfigureAwait(true);
                    if (result)
                    {
                        return Json(new { status = true, messageTitle = "Success", message = "Bet listing updated successfully!" });
                    }
                    else throw new Exception("Error updating bet listing. Try again later.");
                }
            }
            catch (Exception e)
            {
                ModelState.AddModelError(string.Empty, "Something went wrong, please try again later");
                await errorLogService.InsertException(e).ConfigureAwait(true);
                return Json(new { status = false, messageTitle = "Error", message = e.Message });
            }
        }


        [HttpPost]
        public async Task<IActionResult> AddBetListingReport(CreateBetListingReportDto newReportInfo)
        {
            try
            {
                if (newReportInfo == null) throw new Exception("No listing to add");
                else
                {
                    if (string.IsNullOrWhiteSpace(newReportInfo.ReportContent)) throw new Exception("Report Content is mandatory!");
                    if (string.IsNullOrWhiteSpace(newReportInfo.ReportedById.ToString()) || newReportInfo.ReportedById == Guid.Empty) throw new Exception("Reported By Id is mandatory!");
                    if (string.IsNullOrWhiteSpace(newReportInfo.ReportedListingId.ToString()) || newReportInfo.ReportedListingId == Guid.Empty) throw new Exception("Reported Listing Id is mandatory!");




                    BetListingReport newReport = new BetListingReport
                    {
                        DateReported = DateTime.UtcNow,
                        IsCorrect = false,
                        ReportContent = newReportInfo.ReportContent,
                        ReportedById = newReportInfo.ReportedById,
                        ReportedListingId = newReportInfo.ReportedListingId,
                    };

                    var result = await betListingReportService.Insert(newReport).ConfigureAwait(true);
                    if (result)
                    {
                        return Json(new { status = true, messageTitle = "Success", message = "New listing report saved successfully!" });
                    }
                    else throw new Exception("Error creating new listing report. Try again later.");
                }
            }
            catch (Exception e)
            {
                ModelState.AddModelError(string.Empty, "Something went wrong, please try again later");
                await errorLogService.InsertException(e).ConfigureAwait(true);
                return Json(new { status = false, messageTitle = "Error", message = e.Message });
            }
        }




        [HttpPost]
        public async Task<IActionResult> SwitchStatusBetListingReport(Guid? Id, bool NewStatus)
        {
            try
            {
                if (!Id.HasValue || Id.Value == Guid.Empty) throw new Exception("No Bet Listing Report to validate");
                else
                {
                    var result = await betListingReportService.SwitchStatus(Id.Value, NewStatus).ConfigureAwait(true);
                    if (result)
                    {
                        string message = NewStatus ? "Bet Listing Report validated successfully!" : "Bet Listing Report invalidated successfully!";
                        return Json(new { status = true, messageTitle = "Success", message = message });
                    }
                    else throw new Exception("Error validating the Bet Listing Report. Try again later.");
                }
            }
            catch (Exception e)
            {
                ModelState.AddModelError(string.Empty, "Something went wrong, please try again later");
                await errorLogService.InsertException(e).ConfigureAwait(true);
                return Json(new { status = false, messageTitle = "Error", message = e.Message });
            }
        }


        [HttpPost]
        public async Task<IActionResult> DeleteBetListingReport(Guid? Id)
        {
            try
            {
                if (!Id.HasValue || Id.Value == Guid.Empty) throw new Exception("No Bet Listing Report to delete");
                else
                {
                    var result = await betListingReportService.Delete(Id.Value).ConfigureAwait(true);
                    if (result)
                    {
                        return Json(new { status = true, messageTitle = "Success", message = "Bet listing report deleted successfully!" });
                    }
                    else throw new Exception("Error deleting the Bet Listing Report. Try again later.");
                }
            }
            catch (Exception e)
            {
                ModelState.AddModelError(string.Empty, "Something went wrong, please try again later");
                await errorLogService.InsertException(e).ConfigureAwait(true);
                return Json(new { status = false, messageTitle = "Error", message = e.Message });
            }
        }

        #endregion

        #region POTD Listings


        public async Task<IActionResult> POTD(int TimeOffset = 0)
        {
            Guid? UserId = null;
            RequestFeedback request = new RequestFeedback();
            try
            {
                ViewData["root"] = hostingEnvironment.ContentRootPath;
                ViewData[""] = hostingEnvironment.WebRootPath;
                ViewData["TimezoneOffset"] = TimeOffset;
                UserId = User.Id();
                POTDListingListDto model = new POTDListingListDto
                {
                    POTDListings = await potdListingService.GetAll(false).ConfigureAwait(true),
                    CategoriesOptions = (await listingCategoryService.GetAll(true).ConfigureAwait(true)).Select(i => new SelectListItem { Value = i.Id.ToString(), Text = i.Name }).OrderBy(o => o.Text).ToList()
                };

                return View(model);
            }
            catch (Exception e)
            {
                await errorLogService.InsertException(e, UserId).ConfigureAwait(true);
                return RedirectToAction("Index", "Error", request);
            }
        }

        public async Task<IActionResult> POTDLiveReporting(Guid Id, int TimeOffset = 0)
        {
            Guid? UserId = null;
            RequestFeedback request = new RequestFeedback();
            try
            {
                ViewData["root"] = hostingEnvironment.ContentRootPath;
                ViewData["TimezoneOffset"] = TimeOffset;
                if (Id == Guid.Empty) throw new Exception("No POTD found with given id!");
                UserId = User.Id();
                POTDLiveReportingListDto model = new POTDLiveReportingListDto
                {
                    POTD = await potdListingService.GetById(Id).ConfigureAwait(true),
                    LiveReportings = await potdListingLiveReportService.GetByPOTDId(Id).ConfigureAwait(true)
                };

                if (model.POTD == null) throw new Exception("No POTD found with given id!");
                else return View(model);
            }
            catch (Exception e)
            {
                await errorLogService.InsertException(e, UserId).ConfigureAwait(true);
                return RedirectToAction("Index", "Error", request);
            }
        }

        public async Task<IActionResult> POTDViewAllPredictions(Guid Id, int TimeOffset = 0)
        {
            Guid? UserId = null;
            RequestFeedback request = new RequestFeedback();
            try
            {
                ViewData["TimezoneOffset"] = TimeOffset;
                POTDViewPredictionsListDto model = new POTDViewPredictionsListDto
                {
                    Predictions = await potdListingPredictionService.GetByPOTDId(Id).ConfigureAwait(true),
                    POTD = await potdListingService.GetById(Id).ConfigureAwait(true)
                };

                return View(model);
            }
            catch (Exception e)
            {
                await errorLogService.InsertException(e, UserId).ConfigureAwait(true);
                return RedirectToAction("Index", "Error", request);
            }
        }


        [HttpPost]
        public async Task<IActionResult> POTDReportingLoadInteractionData(Guid ReportId, string Type, int TimeOffset = 0)
        {
            Guid? UserId = null;
            RequestFeedback request = new RequestFeedback();
            try
            {
                ViewData["TimezoneOffset"] = TimeOffset;

                var interactions = await potdListingLiveReportingInteractionService.GetByPOTDLiveReportingAndType(ReportId, Type).ConfigureAwait(true);
                return PartialView(interactions);
            }
            catch (Exception e)
            {
                await errorLogService.InsertException(e, UserId).ConfigureAwait(true);
                return null;
            }
        }


        [HttpPost]
        public async Task<IActionResult> AddPOTDListingLiveReportItem(CreatePOTDLiveReportingDto newReporting)
        {
            try
            {
                if (newReporting == null) throw new Exception("No listing to add");
                else
                {
                    if (string.IsNullOrWhiteSpace(newReporting.DateTimeReported)) throw new Exception("Date/Time is mandatory!");
                    if (string.IsNullOrWhiteSpace(newReporting.Report)) throw new Exception("Report is mandatory!");
                    if (newReporting.POTDId == Guid.Empty) throw new Exception("Venue is mandatory!");
                    if (newReporting.ReportedById == Guid.Empty) throw new Exception("Venue is mandatory!");

                    DateTime date = DateHelper.FromISO(newReporting.DateTimeReported);
                    date = date.AddMinutes(newReporting.TimezoneOffset);

                    POTDListingLiveReport report = new POTDListingLiveReport
                    {
                        DateTimeReported = date,
                        Deleted = false,
                        Report = newReporting.Report,
                        POTDId = newReporting.POTDId,
                        ReportedById = newReporting.ReportedById,
                    };

                    var result = await potdListingLiveReportService.Insert(report).ConfigureAwait(true);
                    if (result)
                    {
                        return Json(new { status = true, messageTitle = "Success", message = "New POTD reporting saved successfully!" });
                    }
                    else throw new Exception("Error creating new listing. Try again later.");
                }
            }
            catch (Exception e)
            {
                ModelState.AddModelError(string.Empty, "Something went wrong, please try again later");
                await errorLogService.InsertException(e).ConfigureAwait(true);
                return Json(new { status = false, messageTitle = "Error", message = e.Message });
            }
        }

        [HttpPost]
        public async Task<IActionResult> POTDPredictionValidation(Guid? Id, bool Type)
        {
            try
            {
                if (!Id.HasValue || Id.Value == Guid.Empty) throw new Exception("No prediction to validate");
                else
                {
                    var result = await potdListingPredictionService.Validate(Id.Value, Type).ConfigureAwait(true);
                    if (result)
                    {
                        string message = "";
                        if (Type) message = "POTD validated successfully!";
                        else message = "POTD invalidated successfully!";

                        return Json(new { status = true, messageTitle = "Success", message = message });
                    }
                    else throw new Exception("Error validating the POTD prediction. Try again later.");
                }
            }
            catch (Exception e)
            {
                ModelState.AddModelError(string.Empty, "Something went wrong, please try again later");
                await errorLogService.InsertException(e).ConfigureAwait(true);
                return Json(new { status = false, messageTitle = "Error", message = e.Message });
            }
        }


        [HttpPost]
        public async Task<IActionResult> InformResultPOTDListing(Guid Id, string Result)
        {
            try
            {
                if (Id == Guid.Empty) throw new Exception("No POTD to save result");
                else
                {
                    var result = await potdListingService.SaveResult(Id, Result).ConfigureAwait(true);
                    if (result)
                    {
                        string message = "POTD result informed successfully!";
                        return Json(new { status = true, messageTitle = "Success", message = message });
                    }
                    else throw new Exception("Error saving the POTD result. Try again later.");
                }
            }
            catch (Exception e)
            {
                ModelState.AddModelError(string.Empty, "Something went wrong, please try again later");
                await errorLogService.InsertException(e).ConfigureAwait(true);
                return Json(new { status = false, messageTitle = "Error", message = e.Message });
            }
        }

        [HttpPost]
        public async Task<IActionResult> DeletePOTDLiveReporting(Guid? Id)
        {
            try
            {
                if (!Id.HasValue || Id == Guid.Empty) throw new Exception("No live reporting to delete");
                else
                {
                    var result = await potdListingLiveReportService.Delete(Id.Value).ConfigureAwait(true);
                    if (result)
                    {
                        string message = "POTD live reporting deleted successfully!";
                        return Json(new { status = true, messageTitle = "Success", message = message });
                    }
                    else throw new Exception("Error deleting the POTD Live Reporting. Try again later.");
                }
            }
            catch (Exception e)
            {
                ModelState.AddModelError(string.Empty, "Something went wrong, please try again later");
                await errorLogService.InsertException(e).ConfigureAwait(true);
                return Json(new { status = false, messageTitle = "Error", message = e.Message });
            }
        }

        [HttpPost]
        public async Task<IActionResult> POTDViewPredictions(Guid Id, int TimeOffset = 0)
        {
            Guid? UserId = null;
            RequestFeedback request = new RequestFeedback();
            try
            {
                ViewData["TimezoneOffset"] = TimeOffset;

                var predictions = await potdListingPredictionService.GetByPOTDId(Id).ConfigureAwait(true);
                return PartialView(predictions);
            }
            catch (Exception e)
            {
                await errorLogService.InsertException(e, UserId).ConfigureAwait(true);
                return null;
            }
        }

        [HttpPost]
        public async Task<IActionResult> AddPOTDListing(CreatePOTDListingItemDto newListingInfo)
        {
            try
            {
                if (newListingInfo == null) throw new Exception("No listing to add");
                else
                {
                    if (!newListingInfo.BackgroundImage.HasValue) throw new Exception("Background Image is mandatory!");
                    if (string.IsNullOrWhiteSpace(newListingInfo.Title)) throw new Exception("Title is mandatory!");
                    if (newListingInfo.Pick == 0) throw new Exception("Pick is mandatory!");
                    if (newListingInfo.Market == 0) throw new Exception("Market is mandatory!");
                    if (newListingInfo.Tip == 0) throw new Exception("Tip is mandatory!");
                    if (string.IsNullOrWhiteSpace(newListingInfo.Venue)) throw new Exception("Venue is mandatory!");
                    if (newListingInfo.HasSubcategories == 1 && (!newListingInfo.SubCategoryId.HasValue || newListingInfo.SubCategoryId == 0)) throw new Exception("SubCategory is mandatory!");
                    if (newListingInfo.CategoryId == 0) throw new Exception("Category is mandatory!");


                    POTDListing newListing = new POTDListing
                    {
                        CategoryId = newListingInfo.CategoryId,
                        Deleted = false,
                        Result = null,
                        DateResultInformed = null,
                        Description = null,
                        PickId = newListingInfo.Pick,
                        MarketId = newListingInfo.Market,
                        TipId = newListingInfo.Tip,
                        PostedbyId = newListingInfo.PostedbyId,
                        Title = newListingInfo.Title,
                        Venue = newListingInfo.Venue,
                        SubCategoryId = newListingInfo.SubCategoryId.HasValue ? newListingInfo.SubCategoryId : null,

                    };

                    if (newListingInfo.BackgroundImage.HasValue)
                    {
                        MediaFile file = await mediaFileService.GetById(newListingInfo.BackgroundImage.Value).ConfigureAwait(true);
                        if (file == null) throw new Exception("No file provided when required.");
                        else
                        {
                            newListing.BackgroundImage = file.PhysicalPath;
                        }
                    }

                    var result = await potdListingService.Insert(newListing).ConfigureAwait(true);
                    if (result)
                    {
                        return Json(new { status = true, messageTitle = "Success", message = "New listing saved successfully!" });
                    }
                    else throw new Exception("Error creating new listing. Try again later.");
                }
            }
            catch (Exception e)
            {
                ModelState.AddModelError(string.Empty, "Something went wrong, please try again later");
                await errorLogService.InsertException(e).ConfigureAwait(true);
                return Json(new { status = false, messageTitle = "Error", message = e.Message });
            }
        }


        [HttpPost]
        public async Task<IActionResult> DeletePOTDListing(Guid? Id)
        {
            try
            {
                if (!Id.HasValue || Id == Guid.Empty) throw new Exception("No listing to delete");
                else
                {
                    var result = await potdListingService.Delete(Id.Value).ConfigureAwait(true);
                    if (result)
                    {
                        string message = "POTD listing deleted successfully!";
                        return Json(new { status = true, messageTitle = "Success", message = message });
                    }
                    else throw new Exception("Error deleting the POTD. Try again later.");
                }
            }
            catch (Exception e)
            {
                ModelState.AddModelError(string.Empty, "Something went wrong, please try again later");
                await errorLogService.InsertException(e).ConfigureAwait(true);
                return Json(new { status = false, messageTitle = "Error", message = e.Message });
            }
        }

        [HttpPost]
        public async Task<IActionResult> DeletePOTDListingBatch(Guid[] Ids)
        {
            try
            {
                if (Ids == null || Ids.Count() <= 0) throw new Exception("No listings to delete");
                else
                {
                    var result = await potdListingService.DeleteBatch(Ids).ConfigureAwait(true);
                    if (result)
                    {
                        string message = "POTD listings deleted successfully!";
                        return Json(new { status = true, messageTitle = "Success", message = message });
                    }
                    else throw new Exception("Error deleting the POTD. Try again later.");
                }
            }
            catch (Exception e)
            {
                ModelState.AddModelError(string.Empty, "Something went wrong, please try again later");
                await errorLogService.InsertException(e).ConfigureAwait(true);
                return Json(new { status = false, messageTitle = "Error", message = e.Message });
            }
        }


        [HttpPost]
        public async Task<IActionResult> EditPOTDListing(UpdatePOTDListingItemDto newListingInfo)
        {
            try
            {
                if (newListingInfo == null) throw new Exception("No listing to edit");
                else
                {
                    if (newListingInfo.Id == Guid.Empty) throw new Exception("Id is mandatory!");
                    if (newListingInfo.Pick == 0) throw new Exception("Pick is mandatory!");
                    if (newListingInfo.Market == 0) throw new Exception("Market is mandatory!");
                    if (newListingInfo.Tip == 0) throw new Exception("Tip is mandatory!");
                    if (newListingInfo.HasSubcategories == 1 && (!newListingInfo.SubCategoryId.HasValue || newListingInfo.SubCategoryId == 0)) throw new Exception("SubCategory is mandatory!");
                    if (newListingInfo.CategoryId == 0) throw new Exception("Category is mandatory!");


                    POTDListing listing = await potdListingService.GetById(newListingInfo.Id).ConfigureAwait(true);

                    listing.CategoryId = newListingInfo.CategoryId;
                    listing.PickId = newListingInfo.Pick;
                    listing.MarketId = newListingInfo.Market;
                    listing.TipId = newListingInfo.Tip;
                    listing.Title = newListingInfo.Title;
                    listing.Venue = newListingInfo.Venue;
                    listing.SubCategoryId = newListingInfo.SubCategoryId.HasValue ? newListingInfo.SubCategoryId : null;

                    if (newListingInfo.BackgroundImage.HasValue)
                    {
                        MediaFile file = await mediaFileService.GetById(newListingInfo.BackgroundImage.Value).ConfigureAwait(true);
                        if (file == null) throw new Exception("No file provided when required.");
                        else
                        {
                            listing.BackgroundImage = file.PhysicalPath;
                        }

                    }

                    var result = await potdListingService.Update(listing).ConfigureAwait(true);
                    if (result)
                    {
                        return Json(new { status = true, messageTitle = "Success", message = "POTD listing updated successfully!" });
                    }
                    else throw new Exception("Error updating listing. Try again later.");
                }
            }
            catch (Exception e)
            {
                ModelState.AddModelError(string.Empty, "Something went wrong, please try again later");
                await errorLogService.InsertException(e).ConfigureAwait(true);
                return Json(new { status = false, messageTitle = "Error", message = e.Message });
            }
        }



        [HttpPost]
        public async Task<IActionResult> POTDValidateAllPredictions(Guid Id, int TimeOffset = 0)
        {
            Guid? UserId = null;
            RequestFeedback request = new RequestFeedback();
            try
            {

                ICollection<POTDListingPrediction> predictions = await potdListingPredictionService.GetByPOTDId(Id).ConfigureAwait(true);
                if (predictions != null && predictions.Count > 0)
                {
                    var result = await potdListingPredictionService.Validate(predictions).ConfigureAwait(true);
                    if (!result) throw new Exception("Error during validation process!");
                }
                return Json(new { status = true, messageTitle = "Success!", message = "Validation finished successfully!" });
            }
            catch (Exception e)
            {
                await errorLogService.InsertException(e, UserId).ConfigureAwait(true);
                return Json(new { status = false, messageTitle = "Error", message = e.Message });
            }
        }

        #endregion

        #region Markets 

        public async Task<IActionResult> Markets(int TimeOffset = 0)
        {
            ViewData["TimezoneOffset"] = TimeOffset;
            Guid? UserId = null;
            RequestFeedback request = new RequestFeedback();
            try
            {
                MarketListDto model = new MarketListDto
                {
                    Markets = await marketService.GetAll(true).ConfigureAwait(true),
                    CategoriesOptions = (await listingCategoryService.GetAll(true).ConfigureAwait(true)).Select(i => new SelectListItem { Value = i.Id.ToString(), Text = i.Name }).ToList()
                };
                return View(model);
            }
            catch (Exception e)
            {
                await errorLogService.InsertException(e, UserId).ConfigureAwait(true);
                return RedirectToAction("Index", "Error", request);
            }
        }


        [HttpPost]
        public async Task<IActionResult> AddMarket(CreateMarketDto newItem)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(newItem.Name)) throw new Exception("Name is mandatory!");
                if (newItem.CategoryId == 0) throw new Exception("Category is mandatory!");

                Market item = new Market
                {
                    Name = newItem.Name,
                    CategoryId = newItem.CategoryId,
                    IsEnabled = true,
                };

                var result = await marketService.Insert(item).ConfigureAwait(true);
                if (result)
                {

                    return Json(new { status = true, messageTitle = "Success", message = "New market saved successfully!" });
                }
                else throw new Exception("Error creating new market. Try again later.");

            }
            catch (Exception e)
            {
                ModelState.AddModelError(string.Empty, "Something went wrong, please try again later");
                await errorLogService.InsertException(e).ConfigureAwait(true);
                return Json(new { status = false, messageTitle = "Error", message = e.Message });
            }
        }

        [HttpPost]
        public async Task<IActionResult> EditMarket(EditMarketDto editItem)
        {
            try
            {
                Market itemToEdit = await marketService.GetById(editItem.Id).ConfigureAwait(true);
                if (itemToEdit == null) throw new Exception("No market to edit");
                else
                {
                    if (string.IsNullOrWhiteSpace(editItem.Name)) throw new Exception("Name is mandatory!");
                    if (editItem.CategoryId == 0) throw new Exception("Category is mandatory!");

                    itemToEdit.Name = editItem.Name;
                    itemToEdit.CategoryId = editItem.CategoryId;

                    var result = await marketService.Update(itemToEdit).ConfigureAwait(true);
                    if (result)
                    {
                        return Json(new { status = true, messageTitle = "Success", message = "Market edited successfully!" });
                    }
                    else throw new Exception("Error creating new market. Try again later.");
                }
            }
            catch (Exception e)
            {
                ModelState.AddModelError(string.Empty, "Something went wrong, please try again later");
                await errorLogService.InsertException(e).ConfigureAwait(true);
                return Json(new { status = false, messageTitle = "Error", message = e.Message });
            }
        }

        [HttpPost]
        public async Task<IActionResult> SwitchStatusMarket(int? Id, bool NewStatus)
        {
            try
            {
                if (!Id.HasValue || Id == 0) throw new Exception("No market to activate");
                else
                {
                    var result = await marketService.SwitchStatus(Id.Value, NewStatus).ConfigureAwait(true);
                    if (result)
                    {
                        string message = NewStatus ? "Market activated successfully!" : "Market deleted successfully!";
                        return Json(new { status = true, messageTitle = "Success", message = message });
                    }
                    else throw new Exception("Error activating the market. Try again later.");
                }
            }
            catch (Exception e)
            {
                ModelState.AddModelError(string.Empty, "Something went wrong, please try again later");
                await errorLogService.InsertException(e).ConfigureAwait(true);
                return Json(new { status = false, messageTitle = "Error", message = e.Message });
            }
        }


        #endregion

        #region Tips 

        public async Task<IActionResult> Tips(int TimeOffset = 0)
        {
            ViewData["TimezoneOffset"] = TimeOffset;
            Guid? UserId = null;
            RequestFeedback request = new RequestFeedback();
            try
            {
                TipListDto model = new TipListDto
                {
                    Tips = await tipService.GetAll(true).ConfigureAwait(true),
                    Markets = (await marketService.GetAll(true).ConfigureAwait(true)).Select(i => new SelectListItem { Value = i.Id.ToString(), Text = i.Name + " (" + i.Category.Name + ")" }).OrderBy(o => o.Text).ToList()
                };
                return View(model);
            }
            catch (Exception e)
            {
                await errorLogService.InsertException(e, UserId).ConfigureAwait(true);
                return RedirectToAction("Index", "Error", request);
            }
        }


        [HttpPost]
        public async Task<IActionResult> AddTip(CreateTipDto newItem)
        {
            try
            {
                //if (string.IsNullOrWhiteSpace(newItem.Name)) throw new Exception("Name is mandatory!");
                if (newItem.MarketId == 0) throw new Exception("Market is mandatory!");

                Tip item = new Tip
                {
                    Name = newItem.Name,
                    MarketId = newItem.MarketId,
                    Description = newItem.Description,
                    IsEnabled = true,
                };

                var result = await tipService.Insert(item).ConfigureAwait(true);
                if (result)
                {

                    return Json(new { status = true, messageTitle = "Success", message = "New Tip saved successfully!" });
                }
                else throw new Exception("Error creating new Tip. Try again later.");

            }
            catch (Exception e)
            {
                ModelState.AddModelError(string.Empty, "Something went wrong, please try again later");
                await errorLogService.InsertException(e).ConfigureAwait(true);
                return Json(new { status = false, messageTitle = "Error", message = e.Message });
            }
        }

        [HttpPost]
        public async Task<IActionResult> EditTip(EditTipDto item)
        {
            try
            {
                Tip itemToEdit = await tipService.GetById(item.Id).ConfigureAwait(true);
                if (itemToEdit == null) throw new Exception("No Tip to edit");
                else
                {
                    //if (string.IsNullOrWhiteSpace(item.Name)) throw new Exception("Name is mandatory!");
                    if (item.MarketId == 0) throw new Exception("Market is mandatory!");

                    itemToEdit.Name = item.Name;
                    itemToEdit.Description = item.Description;
                    itemToEdit.MarketId = item.MarketId;

                    var result = await tipService.Update(itemToEdit).ConfigureAwait(true);
                    if (result)
                    {
                        return Json(new { status = true, messageTitle = "Success", message = "Tip edited successfully!" });
                    }
                    else throw new Exception("Error creating new Tip. Try again later.");
                }
            }
            catch (Exception e)
            {
                ModelState.AddModelError(string.Empty, "Something went wrong, please try again later");
                await errorLogService.InsertException(e).ConfigureAwait(true);
                return Json(new { status = false, messageTitle = "Error", message = e.Message });
            }
        }

        [HttpPost]
        public async Task<IActionResult> SwitchStatusTip(int? Id, bool NewStatus)
        {
            try
            {
                if (!Id.HasValue || Id == 0) throw new Exception("No tip to activate");
                else
                {
                    var result = await tipService.SwitchStatus(Id.Value, NewStatus).ConfigureAwait(true);
                    if (result)
                    {
                        string message = NewStatus ? "Tip activated successfully!" : "Tip deleted successfully!";
                        return Json(new { status = true, messageTitle = "Success", message = message });
                    }
                    else throw new Exception("Error activating the tip. Try again later.");
                }
            }
            catch (Exception e)
            {
                ModelState.AddModelError(string.Empty, "Something went wrong, please try again later");
                await errorLogService.InsertException(e).ConfigureAwait(true);
                return Json(new { status = false, messageTitle = "Error", message = e.Message });
            }
        }


        #endregion

        #region Bookmakers


        public async Task<IActionResult> Bookmakers(int TimeOffset = 0)
        {
            ViewData["TimezoneOffset"] = TimeOffset;
            Guid? UserId = null;
            RequestFeedback request = new RequestFeedback();
            try
            {
                BookmakerListDto model = new BookmakerListDto
                {
                    Bookmakers = await bookmakerService.GetAll(true).ConfigureAwait(true),
                };
                return View(model);
            }
            catch (Exception e)
            {
                await errorLogService.InsertException(e, UserId).ConfigureAwait(true);
                return RedirectToAction("Index", "Error", request);
            }
        }


        [HttpPost]
        public async Task<IActionResult> AddBookmaker(CreateBookmakerDto newItem)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(newItem.Description)) throw new Exception("Description is mandatory!");

                Bookmaker item = new Bookmaker
                {
                    Description = newItem.Description,
                    IsEnabled = true,
                };

                var result = await bookmakerService.Insert(item).ConfigureAwait(true);
                if (result)
                {

                    return Json(new { status = true, messageTitle = "Success", message = "New Bookmaker saved successfully!" });
                }
                else throw new Exception("Error creating new Bookmaker. Try again later.");

            }
            catch (Exception e)
            {
                ModelState.AddModelError(string.Empty, "Something went wrong, please try again later");
                await errorLogService.InsertException(e).ConfigureAwait(true);
                return Json(new { status = false, messageTitle = "Error", message = e.Message });
            }
        }

        [HttpPost]
        public async Task<IActionResult> EditBookmaker(EditBookmakerDto editItem)
        {
            try
            {
                Bookmaker itemToEdit = await bookmakerService.GetById(editItem.Id).ConfigureAwait(true);
                if (itemToEdit == null) throw new Exception("No Bookmaker to edit");
                else
                {
                    if (string.IsNullOrWhiteSpace(editItem.Description)) throw new Exception("Name is mandatory!");

                    itemToEdit.Description = editItem.Description;

                    var result = await bookmakerService.Update(itemToEdit).ConfigureAwait(true);
                    if (result)
                    {
                        return Json(new { status = true, messageTitle = "Success", message = "Bookmaker edited successfully!" });
                    }
                    else throw new Exception("Error editing Bookmaker. Try again later.");
                }
            }
            catch (Exception e)
            {
                ModelState.AddModelError(string.Empty, "Something went wrong, please try again later");
                await errorLogService.InsertException(e).ConfigureAwait(true);
                return Json(new { status = false, messageTitle = "Error", message = e.Message });
            }
        }

        [HttpPost]
        public async Task<IActionResult> SwitchStatusBookmaker(int? Id, bool NewStatus)
        {
            try
            {
                if (!Id.HasValue || Id == 0) throw new Exception("No Bookmaker to activate");
                else
                {
                    var result = await bookmakerService.SwitchStatus(Id.Value, NewStatus).ConfigureAwait(true);
                    if (result)
                    {
                        string message = NewStatus ? "Bookmaker activated successfully!" : "Bookmaker deleted successfully!";
                        return Json(new { status = true, messageTitle = "Success", message = message });
                    }
                    else throw new Exception("Error activating the Bookmaker. Try again later.");
                }
            }
            catch (Exception e)
            {
                ModelState.AddModelError(string.Empty, "Something went wrong, please try again later");
                await errorLogService.InsertException(e).ConfigureAwait(true);
                return Json(new { status = false, messageTitle = "Error", message = e.Message });
            }
        }



        #endregion

        #region Picks 

        public async Task<IActionResult> Picks(int TimeOffset = 0)
        {
            ViewData["TimezoneOffset"] = TimeOffset;
            Guid? UserId = null;
            RequestFeedback request = new RequestFeedback();
            try
            {
                ViewData["root"] = hostingEnvironment.ContentRootPath;
                PickListDto model = new PickListDto
                {
                    Picks = await pickService.GetAll(true).ConfigureAwait(true),
                    CategoriesOptions = (await listingCategoryService.GetAll(true).ConfigureAwait(true)).OrderBy(o => o.Name).Select(i => new SelectListItem { Value = i.Id.ToString(), Text = i.Name }).ToList()
                };
                
                return View(model);
            }
            catch (Exception e)
            {
                await errorLogService.InsertException(e).ConfigureAwait(true);
                //return RedirectToAction("Index", "Error", request);
                throw;
            }
        }

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> AddPick(CreatePickDto data)
        {
           // return Content("Hello WOrld");
            string team1UniqueNameAndPath = null;
            string team2UniqueNameAndPath = null;
            try
            {
                if (string.IsNullOrWhiteSpace(data.Team1)) throw new Exception("Team 1 is mandatory!");
                if (string.IsNullOrWhiteSpace(data.Team2)) throw new Exception("Team 2 is mandatory!");
                if (string.IsNullOrWhiteSpace(data.StartTime)) throw new Exception("Start Time is mandatory!");
                if (string.IsNullOrWhiteSpace(data.FinishTime)) throw new Exception("Finish Time is mandatory!");
                if (data.Team1PhotoFile == null) throw new Exception("You must seelect Photo for Team 1 !");
                if (data.Team1PhotoFile == null) throw new Exception("You must seelect Photo for Team 2 !");
                if (data.CategoryId == 0) throw new Exception("Category is mandatory!");

                DateTime dateStart = DateHelper.FromISO(data.StartTime);
                dateStart = dateStart.AddMinutes(data.TimezoneOffset);
                DateTime dateEnd = DateHelper.FromISO(data.FinishTime);
                dateEnd = dateEnd.AddMinutes(data.TimezoneOffset);

               
                string team1uniquename = null;
                string team2uniquename = null;
                if (data.Team1PhotoFile != null && data.Team2PhotoFile != null)
                {
                     string extension = Path.GetExtension(data.Team1PhotoFile.FileName);
                        long size = data.Team1PhotoFile.Length;
                        if (FileHelper.FileIsFormSafe(extension, size))
                        {

                        string basePath = hostingEnvironment.ContentRootPath;
                    //C:\inetpub\wwwroot\Media\User\
                        //string mediaPath = Path.Combine(hostingEnvironment.WebRootPath, "images\\Pick");
                        string mediaPath = FileHelper.PathCombine(basePath, "\\Media\\", "\\Pick");
                        team1uniquename = Guid.NewGuid().ToString() + Path.GetExtension(data.Team1PhotoFile.FileName);
                            team1UniqueNameAndPath = FileHelper.PathCombine(mediaPath, team1uniquename);
                            using (var fileStream = new FileStream(team1UniqueNameAndPath, FileMode.Create))
                            {
                                await data.Team1PhotoFile.CopyToAsync(fileStream).ConfigureAwait(true);
                            }
                           // user.ProfilePicture = path;
                        }
                        else
                        {
                            return Json(new { status = false, messageTitle = "Home Team File type is not acceptable!", message = "Try to upload one of the following types: JPG or PNG" });
                        }



                    string extension2 = Path.GetExtension(data.Team2PhotoFile.FileName);
                    long size2 = data.Team2PhotoFile.Length;
                    if (FileHelper.FileIsFormSafe(extension2, size2))
                    {
                        string basePath = hostingEnvironment.ContentRootPath;
                       //string mediaPath = Path.Combine(hostingEnvironment.WebRootPath, "images\\Pick");
                        string mediaPath = FileHelper.PathCombine(basePath, "\\Media\\", "\\Pick");
                        Directory.CreateDirectory(mediaPath);
                        team2uniquename = Guid.NewGuid().ToString() + Path.GetExtension(data.Team2PhotoFile.FileName);
                        team2UniqueNameAndPath = FileHelper.PathCombine(mediaPath, team2uniquename);
                        using (var fileStream = new FileStream(team2UniqueNameAndPath, FileMode.Create))
                        {
                            await data.Team2PhotoFile.CopyToAsync(fileStream).ConfigureAwait(true);
                        }
                        // user.ProfilePicture = path;
                    }
                    else
                    {
                        return Json(new { status = false, messageTitle = "Away Team File type is not acceptable!", message = "Try to upload one of the following types: JPG or PNG" });
                    }

                    /* var UploadFolder = Path.Combine(hostingEnvironment.WebRootPath, "images");
                     team1UniqueName =  Guid.NewGuid().ToString() + "_" + data.Team1PhotoFile.FileName;
                     team2UniqueName = Guid.NewGuid().ToString() + "_" + data.Team2PhotoFile.FileName;
                     team1FilePath = Path.Combine(UploadFolder, team1UniqueName);
                     team2FilePath = Path.Combine(UploadFolder, team2UniqueName);
                     await data.Team1PhotoFile.CopyToAsync( new FileStream(team1FilePath, FileMode.Create )).ConfigureAwait(true);
                     await data.Team2PhotoFile.CopyToAsync(new FileStream(team2UniqueName, FileMode.Create)).ConfigureAwait(true);*/
                }
                if (dateEnd <= dateStart) throw new Exception("Start Time must be greater than Finish Time!");

                Pick item = new Pick
                {
                    Team1 = data.Team1,
                    Team2 = data.Team2,
                    StartTime = dateStart,
                    FinishTime = dateEnd,
                    CategoryId = data.CategoryId,
                   Team1PhotoPath = Path.Combine("Media\\Pick", team1uniquename),
                   Team2PhotoPath = Path.Combine("Media\\Pick", team2uniquename),
                    IsEnabled = true,
                };

                var result = await pickService.Insert(item).ConfigureAwait(true);
                if (result)
                {

                    return Json(new { status = true, messageTitle = "Success", message = "New pick saved successfully!" });
                }
                else throw new Exception("Error creating new pick. Try again later.");

            }
            catch (Exception e)
            {
                ModelState.AddModelError(string.Empty, "Something went wrong, please try again later");
                await errorLogService.InsertException(e).ConfigureAwait(true);
                return Json(new { status = false, messageTitle = "Error", message = e.Message });
            }
        }

        [HttpPost]
        public async Task<IActionResult> EditPick(EditPickDto editItem)
        {
            try
            {
                Pick itemToEdit = await pickService.GetById(editItem.Id).ConfigureAwait(true);
                if (itemToEdit == null) throw new Exception("No pick to edit");
                else
                {

                    if (string.IsNullOrWhiteSpace(editItem.Team1)) throw new Exception("Team 1 is mandatory!");
                    if (string.IsNullOrWhiteSpace(editItem.Team2)) throw new Exception("Team 2 is mandatory!");
                    if (string.IsNullOrWhiteSpace(editItem.StartTime)) throw new Exception("Start Time is mandatory!");
                    if (string.IsNullOrWhiteSpace(editItem.FinishTime)) throw new Exception("Finish Time is mandatory!");
                    
                    if (editItem.CategoryId == 0) throw new Exception("Category is mandatory!");

                    DateTime dateStart = DateHelper.FromISO(editItem.StartTime);
                    dateStart = dateStart.AddMinutes(editItem.TimezoneOffset);
                    DateTime dateEnd = DateHelper.FromISO(editItem.FinishTime);
                    dateEnd = dateEnd.AddMinutes(editItem.TimezoneOffset);

                    itemToEdit.Team1 = editItem.Team1;
                    itemToEdit.Team2 = editItem.Team2;
                    itemToEdit.StartTime = dateStart;
                    itemToEdit.FinishTime = dateEnd;
                    itemToEdit.CategoryId = editItem.CategoryId;
                    
                    var result = await pickService.Update(itemToEdit).ConfigureAwait(true);
                    if (result)
                    {
                        return Json(new { status = true, messageTitle = "Success", message = "Pick edited successfully!" });
                    }
                    else throw new Exception("Error editing the pick. Try again later.");
                }
            }
            catch (Exception e)
            {
                ModelState.AddModelError(string.Empty, "Something went wrong, please try again later");
                await errorLogService.InsertException(e).ConfigureAwait(true);
                return Json(new { status = false, messageTitle = "Error", message = e.Message });
            }
        }

        [HttpPost]
        public async Task<IActionResult> SwitchStatusPick(int? Id, bool NewStatus)
        {
            try
            {
                if (!Id.HasValue || Id == 0) throw new Exception("No pick to activate");
                else
                {
                    var result = await pickService.SwitchStatus(Id.Value, NewStatus).ConfigureAwait(true);
                    if (result)
                    {
                        string message = NewStatus ? "Pick activated successfully!" : "Pick deleted successfully!";
                        return Json(new { status = true, messageTitle = "Success", message = message });
                    }
                    else throw new Exception("Error activating the pick. Try again later.");
                }
            }
            catch (Exception e)
            {
                ModelState.AddModelError(string.Empty, "Something went wrong, please try again later");
                await errorLogService.InsertException(e).ConfigureAwait(true);
                return Json(new { status = false, messageTitle = "Error", message = e.Message });
            }
        }


        #endregion

        #region Orders

        public async Task<IActionResult> Discounts(int TimeOffset = 0)
        {
            ViewData["TimezoneOffset"] = TimeOffset;
            Guid? UserId = null;
            RequestFeedback request = new RequestFeedback();
            try
            {
                DiscountsListDto model = new DiscountsListDto
                {
                    Discounts = await rewardsClaimedMappingService.GetHistoryFromAllUsers().ConfigureAwait(true),
                    RewardsOptions = (await rewardsMappingService.GetAll(true).ConfigureAwait(true)).OrderBy(o => o.EquivalentAmount).Select(i => new SelectListItem { Value = i.EquivalentAmount.ToString(), Text = "$ " + i.EquivalentAmount.ToString() }).ToList(),
                    UsersOptions = (await userService.GetUserList().ConfigureAwait(true)).Where(i => i.IsActive).Select(i => new SelectListItem { Value = i.Id.ToString(), Text = i.FirstName + " " + i.LastName }).OrderBy(o => o.Text).ToList()
                };
                return View(model);
            }
            catch (Exception e)
            {
                await errorLogService.InsertException(e, UserId).ConfigureAwait(true);
                return RedirectToAction("Index", "Error", request);
            }
        }
        public async Task<IActionResult> SalesOverview(int TimeOffset = 0)
        {
            ViewData["TimezoneOffset"] = TimeOffset;
            Guid? UserId = null;
            RequestFeedback request = new RequestFeedback();
            try
            {
                UserId = User.Id();
                if (!UserId.HasValue || UserId.Value == Guid.Empty) throw new Exception("No user found!");
                var model = await orderService.GetAllOrdersForBackend().ConfigureAwait(true);
                return View(model);
            }
            catch (Exception e)
            {
                await errorLogService.InsertException(e, UserId).ConfigureAwait(true);
                return RedirectToAction("Index", "Error", request);
            }
        }
        public async Task<IActionResult> SalesOverviewDetails(Guid Id, int TimeOffset = 0)
        {
            ViewData["TimezoneOffset"] = TimeOffset;
            Guid? UserId = null;
            RequestFeedback request = new RequestFeedback();
            try
            {
                UserId = User.Id();
                if (!UserId.HasValue || UserId.Value == Guid.Empty) throw new Exception("No user found!");
                OrderDetailsDto model = await orderService.GetOrderDetails(Id).ConfigureAwait(true);
                return View(model);
            }
            catch (Exception e)
            {
                await errorLogService.InsertException(e, UserId).ConfigureAwait(true);
                return RedirectToAction("Index", "Error", request);
            }
        }

        public async Task<ActionResult> DownloadReceipt(Guid OrderId)
        {
            try
            {
                string root = hostingEnvironment.ContentRootPath;
                if (OrderId == Guid.Empty) throw new Exception("Submission Id cannot be 0");
                var order = await orderService.GetById(OrderId).ConfigureAwait(true);
                if (order == null) throw new Exception("Submission Id: " + OrderId + " not found");

                if (System.IO.File.Exists(root + order.ReceiptPDF))
                {
                    byte[] fileBytes = System.IO.File.ReadAllBytes(root + @order.ReceiptPDF);
                    string extension = Path.GetExtension(order.ReceiptPDF);
                    string fileName = "Order #" + order.OrderNumber + " Receipt" + extension;
                    return File(fileBytes, System.Net.Mime.MediaTypeNames.Application.Octet, fileName);
                }
                else
                {
                    throw new Exception("Receipt not found.");

                }
            }
            catch (Exception e)
            {
                await errorLogService.InsertException(e).ConfigureAwait(true);
                return RedirectToAction("Index", "Error", "Error downloading PDF");
            }
        }

        [HttpPost]
        public async Task<IActionResult> AddDiscount(CreateDiscountRewardDto data)
        {
            Guid? UserId = null;
            RequestFeedback request = new RequestFeedback();
            try
            {
                if (data.EquivalentAmount <= 0.0m) throw new Exception("Amount is mandatory");
                if (data.UserId == Guid.Empty) throw new Exception("User is mandatory");

                string IP = HttpContext.Connection.RemoteIpAddress.ToString();
                User user = await userService.GetById(data.UserId).ConfigureAwait(true);
                CreateClaimRewardDto discountData = new CreateClaimRewardDto
                {
                    DiscountType = DiscountType.Gift,
                    EquivalentAmount = data.EquivalentAmount,
                    UserId = data.UserId,
                    PointsClaimed = 0
                };
                var result = await userService.AccountManagerClaimReward(discountData, user, IP).ConfigureAwait(true);
                if (!string.IsNullOrWhiteSpace(result))
                {
                    var emailResult = await emailSenderService.SendToUserDiscount(user.Email, user.FirstName, discountData.EquivalentAmount, result).ConfigureAwait(true);
                    if (emailResult)
                    {
                        return Json(new { status = true, messageTitle = "Success!", message = "Discount sent successfully!" });
                    }
                    else throw new Exception("Could not send email after creating discount.");
                }
                else throw new Exception("Could not save new discount.");
            }
            catch (Exception e)
            {
                await errorLogService.InsertException(e, UserId).ConfigureAwait(true);
                return Json(new { status = false, messageTitle = "Error", message = e.Message });
            }
        }


        public async Task<IActionResult> PaypalSettings(int TimeOffset = 0)
        {
            ViewData["TimezoneOffset"] = TimeOffset;
            Guid? UserId = null;
            RequestFeedback request = new RequestFeedback();
            try
            {
                PaypalSettingsListDto model = new PaypalSettingsListDto
                {
                    Settings = await paypalSettingsService.GetAll().ConfigureAwait(true),
                };
                return View(model);
            }
            catch (Exception e)
            {
                await errorLogService.InsertException(e, UserId).ConfigureAwait(true);
                return RedirectToAction("Index", "Error", request);
            }
        }

        [HttpPost]
        public async Task<IActionResult> AddPaypalSettings(CreatePaypalSettingsDto data)
        {
            Guid? UserId = null;
            RequestFeedback request = new RequestFeedback();
            try
            {
                if (string.IsNullOrWhiteSpace(data.ClientID)) throw new Exception("Client ID is mandatory");
                if (string.IsNullOrWhiteSpace(data.Secret)) throw new Exception("Secret is mandatory");

                PaypalSettings settings = new PaypalSettings
                {
                    IsActive = true,
                    ClientID = data.ClientID,
                    Secret = data.Secret,
                    IsDefault = data.IsDefault,
                    Name = data.Name,
                };

                var result = await paypalSettingsService.Insert(settings).ConfigureAwait(true);
                if (result)
                {
                    return Json(new { status = true, messageTitle = "Success!", message = "Settings saved successfully!" });
                }
                else throw new Exception("Could not save new settings.");
            }
            catch (Exception e)
            {
                await errorLogService.InsertException(e, UserId).ConfigureAwait(true);
                return Json(new { status = false, messageTitle = "Error", message = e.Message });
            }
        }

        [HttpPost]
        public async Task<IActionResult> EditPaypalSettings(EditPaypalSettingsDto data)
        {
            Guid? UserId = null;
            RequestFeedback request = new RequestFeedback();
            try
            {
                if (data.Id == 0) throw new Exception("No settings found with given id!");
                if (string.IsNullOrWhiteSpace(data.ClientID)) throw new Exception("Client ID is mandatory");
                if (string.IsNullOrWhiteSpace(data.Secret)) throw new Exception("Secret is mandatory");

                PaypalSettings settings = await paypalSettingsService.GetById(data.Id).ConfigureAwait(true);

                if (settings == null) throw new Exception("No settings found with given id!");

                settings.IsDefault = data.IsDefault;
                settings.ClientID = data.ClientID;
                settings.Secret = data.Secret;
                settings.Name = data.Name;

                var result = await paypalSettingsService.Update(settings).ConfigureAwait(true);
                if (result)
                {
                    return Json(new { status = true, messageTitle = "Success!", message = "Settings updated successfully!" });
                }
                else throw new Exception("Could not updated settings.");
            }
            catch (Exception e)
            {
                await errorLogService.InsertException(e, UserId).ConfigureAwait(true);
                return Json(new { status = false, messageTitle = "Error", message = e.Message });
            }
        }

        [HttpPost]
        public async Task<IActionResult> SwitchStatusPaypalSettings(int? Id, bool NewStatus)
        {
            try
            {
                if (!Id.HasValue || Id == 0) throw new Exception("No settings to activate");
                else
                {
                    var result = await paypalSettingsService.SwitchStatus(Id.Value, NewStatus).ConfigureAwait(true);
                    if (result)
                    {
                        string message = NewStatus ? "Settings activated successfully!" : "Settings deactivated successfully!";
                        return Json(new { status = true, messageTitle = "Success", message = message });
                    }
                    else throw new Exception("Error activating the settings. Try again later.");
                }
            }
            catch (Exception e)
            {
                ModelState.AddModelError(string.Empty, "Something went wrong, please try again later");
                await errorLogService.InsertException(e).ConfigureAwait(true);
                return Json(new { status = false, messageTitle = "Error", message = e.Message });
            }
        }

        #endregion

        #region Service Fee and Taxes

        public async Task<IActionResult> ServiceFeeAndTaxes(int TimeOffset = 0)
        {
            Guid? UserId = null;
            RequestFeedback request = new RequestFeedback();
            try
            {
                ViewData["TimezoneOffset"] = TimeOffset;
                UserId = User.Id();

                SystemConstants regularFees = (await systemConstantsService.GetByName(SystemConstantName.RegularFees).ConfigureAwait(true));
                SystemConstants boiterousFees = (await systemConstantsService.GetByName(SystemConstantName.BoisterousFees).ConfigureAwait(true));
                SystemConstants taxes = (await systemConstantsService.GetByName(SystemConstantName.Taxes).ConfigureAwait(true));

                if (regularFees == null || taxes == null || boiterousFees == null)
                {
                    if (regularFees == null)
                    {
                        regularFees = new SystemConstants
                        {
                            Name = SystemConstantName.RegularFees,
                            Value = "0.00",
                            RowDate = DateTime.UtcNow,
                            Type = SystemConstantType.Decimal
                        };
                        var resultFees = await systemConstantsService.Insert(regularFees).ConfigureAwait(true);
                        if (!resultFees) throw new Exception("Service Fee could not be saved");
                    }
                    if (boiterousFees == null)
                    {
                        boiterousFees = new SystemConstants
                        {
                            Name = SystemConstantName.BoisterousFees,
                            Value = "0.00",
                            RowDate = DateTime.UtcNow,
                            Type = SystemConstantType.Decimal
                        };
                        var resultBoisterousFees = await systemConstantsService.Insert(boiterousFees).ConfigureAwait(true);
                        if (!resultBoisterousFees) throw new Exception("Service Fee could not be saved");
                    }
                    if (taxes == null)
                    {
                        taxes = new SystemConstants
                        {
                            Name = SystemConstantName.Taxes,
                            Value = "0.00",
                            RowDate = DateTime.UtcNow,
                            Type = SystemConstantType.Decimal
                        };
                        var resultTaxes = await systemConstantsService.Insert(taxes).ConfigureAwait(true);
                        if (!resultTaxes) throw new Exception("Estimated Taxes could not be saved");
                    }
                }

                ServiceFeeAndTaxesDto model = new ServiceFeeAndTaxesDto
                {
                    Taxes = Convert.ToDecimal(taxes.Value),
                    Fee = Convert.ToDecimal(regularFees.Value),
                    BoisterousFee = Convert.ToDecimal(boiterousFees.Value),
                };


                return View(model);
            }
            catch (Exception e)
            {
                await errorLogService.InsertException(e, UserId).ConfigureAwait(true);
                return RedirectToAction("Index", "Error", request);
            }
        }

        [HttpPost]
        public async Task<IActionResult> UpdateServiceFeeAndTaxes(ServiceFeeAndTaxesDto data)
        {
            try
            {
                if (data == null) throw new Exception("No data");
                else
                {
                    if (data.Fee <= 0) throw new Exception("Regular Service Fee needs to have a value greater than 0!");
                    if (data.BoisterousFee <= 0) throw new Exception("Boisterous Service Fee needs to have a value greater than 0!");
                    if (data.Taxes <= 0) throw new Exception("Estimated Taxes needs to have a value greater than 0!");

                    var result = await systemConstantsService.UpdateFeeAndTaxes(data.Fee, data.Taxes, data.BoisterousFee).ConfigureAwait(true);
                    if (result)
                    {
                        return Json(new { status = true, messageTitle = "Success", message = "Settings updated successfully!" });
                    }
                    else throw new Exception("Error updating settings. Try again later.");
                }
            }
            catch (Exception e)
            {
                ModelState.AddModelError(string.Empty, "Something went wrong, please try again later");
                await errorLogService.InsertException(e).ConfigureAwait(true);
                return Json(new { status = false, messageTitle = "Error", message = e.Message });
            }
        }
        #endregion

        #region Messages

        public async Task<IActionResult> Messages(int TimeOffset = 0)
        {
            ViewData["TimezoneOffset"] = TimeOffset;
            Guid? UserId = null;
            RequestFeedback request = new RequestFeedback();
            try
            {
                ICollection<MessagesListDto> model = await messageService.GetAllUsersThatHaveMessages().ConfigureAwait(true);
                return View(model);
            }
            catch (Exception e)
            {
                await errorLogService.InsertException(e, UserId).ConfigureAwait(true);
                return RedirectToAction("Index", "Error", request);
            }
        }
        public async Task<IActionResult> MessagesFromUser(Guid Id, int TimeOffset = 0)
        {
            ViewData["TimezoneOffset"] = TimeOffset;
            var root = hostingEnvironment.ContentRootPath;
            ViewData["root"] = root;
            Guid? UserId = null;
            RequestFeedback request = new RequestFeedback();
            try
            {
                UserId = User.Id();
                if (!UserId.HasValue) throw new Exception("No user to send message.");
                User user = await userService.GetById(Id).ConfigureAwait(true);
                var messages = await messageService.GetAllForUser(Id).ConfigureAwait(true);
                MessageRetrievedAdminDto model = new MessageRetrievedAdminDto
                {
                    UserId = user.Id,
                    UserName = $"{user.FirstName} {user.LastName}",
                    Messages = messages.Select
                        (
                            i =>
                                new MessageRetrievedDto
                                {
                                    Content = i.Content,
                                    DateRead = i.DateRead,
                                    SentById = i.FromId,
                                    SentByName = i.From.FirstName + " " + i.From.LastName,
                                    SentByProfilePicture = !string.IsNullOrWhiteSpace(i.From.ProfilePicture) ? ImageHelper.ConvertImageToBase64(root + i.From.ProfilePicture) : Url.Content("~/img/avatar.png"),
                                    SentToId = i.ToId,
                                    SentToName = i.To.FirstName + " " + i.To.LastName,
                                    SentToProfilePicture = i.To.ProfilePicture,
                                    DateSent = i.RowDate,
                                    ParsedDateRead = i.DateRead.HasValue ? DateHelper.DateFormat(i.DateRead.Value.FromUTCData(TimeOffset)) : null,
                                    ParsedDateSent = DateHelper.DateFormat(i.RowDate.FromUTCData(TimeOffset))
                                }
                        )
                    .ToList()
                };

                ICollection<Message> updateMessages = new List<Message>();
                if (messages.Count > 0)
                {
                    foreach (var msg in messages)
                    {
                        if (!msg.DateRead.HasValue && msg.FromId == user.Id)
                        {
                            msg.DateRead = DateTime.UtcNow;
                            updateMessages.Add(msg);
                        }
                    }

                    var result = await messageService.UpdateBatch(updateMessages).ConfigureAwait(true);
                    if (!result) throw new Exception("Error updating unread messages.");
                }



                return View(model);
            }
            catch (Exception e)
            {
                await errorLogService.InsertException(e, UserId).ConfigureAwait(true);
                return RedirectToAction("Index", "Error", request);
            }
        }



        [HttpPost]
        public async Task<IActionResult> SendMessage(string MessageContent, Guid Id, int TimezoneOffset = 0)
        {
            Guid? UserId = null;
            RequestFeedback request = new RequestFeedback();
            try
            {
                UserId = User.Id();
                if (!UserId.HasValue) throw new Exception("No user to send message.");
                if (string.IsNullOrWhiteSpace(MessageContent)) throw new Exception("No message content to send message.");

                Message newMessage = new Message
                {
                    FromId = UserId.Value,
                    ToId = Id,
                    DateRead = null,
                    Content = MessageContent
                };

                var result = await messageService.Insert(newMessage).ConfigureAwait(true);
                if (!result) throw new Exception("Message could not be sent!");

                return Json(new { status = true, messageTitle = "Success!", message = "Message sent successfully!" });
            }
            catch (Exception e)
            {
                await errorLogService.InsertException(e, UserId).ConfigureAwait(true);
                return Json(new { status = false, messageTitle = "Error", message = e.Message });
            }
        }

        #endregion

        #region BetListing and POTD Validations
        [HttpPost]
        public async Task<IActionResult> ValidateThroughAPI(Guid Id, int Timezoneoffset = 0)
        {
            Guid? UserId = null;
            RequestFeedback request = new RequestFeedback();
            try
            {
                BetListing betListing = await betListingService.GetById(Id).ConfigureAwait(true);
                string date = DateHelper.GetDateStringForAPI(betListing.Pick.StartTime.FromUTCData(Timezoneoffset));
                string url = Utility.GetAPICallUrl(betListing.Category, date);
                if (betListing.Category.Description == APIValidationCategories.Football)
                {
                    string dateFrom = date;
                    string dateTo = date;
                    url = Utility.GetLiveScoreAPICallUrl(betListing.Category, dateFrom, dateTo);
                }
                var result = await apiValidationService.Validate(url, betListing).ConfigureAwait(true);
                if (result.HasValue)
                {
                    betListing.DateVerifiedByApi = DateTime.UtcNow;
                    betListing.ResultVerificationByApi = result.Value;
                    betListing.IsCorrect = result.Value;

                    var resultUpdate = await betListingService.Update(betListing).ConfigureAwait(true);
                    if (!resultUpdate) throw new Exception("Error updating bet listing with API result!");
                    return Json(new { status = true, messageTitle = "Success!", message = "Validated by the API!" });
                }
                return Json(new { status = true, messageTitle = "Oops!", message = "This bet listing can't be validated through the API!" });

            }
            catch (Exception e)
            {
                await errorLogService.InsertException(e, UserId).ConfigureAwait(true);
                return Json(new { status = false, messageTitle = "Error", message = e.Message });
            }
        }


        [HttpPost]
        public async Task<IActionResult> ValidateBetListingManually(Guid Id, bool IsCorrect)
        {
            Guid? UserId = null;
            RequestFeedback request = new RequestFeedback();
            try
            {
                BetListing betListing = await betListingService.GetById(Id).ConfigureAwait(true);
                if (betListing == null) throw new Exception("No bet listing to validate!");

                betListing.DateVerifiedByApi = DateTime.UtcNow;
                betListing.ResultVerificationByApi = null;
                betListing.IsCorrect = IsCorrect;

                var resultUpdate = await betListingService.Update(betListing).ConfigureAwait(true);
                if (!resultUpdate) throw new Exception("Error updating bet listing with manual validation result!");

                return Json(new { status = true, messageTitle = "Success!", message = "Validated successfully!" });


            }
            catch (Exception e)
            {
                await errorLogService.InsertException(e, UserId).ConfigureAwait(true);
                return Json(new { status = false, messageTitle = "Error", message = e.Message });
            }
        }


        [HttpPost]
        public async Task<IActionResult> CancelBetListingValidation(Guid Id)
        {
            Guid? UserId = null;
            RequestFeedback request = new RequestFeedback();
            try
            {
                BetListing betListing = await betListingService.GetById(Id).ConfigureAwait(true);
                if (betListing == null) throw new Exception("No bet listing to cancel validation!");

                betListing.DateVerifiedByApi = null;
                betListing.ResultVerificationByApi = null;
                betListing.IsCorrect = null;

                var resultUpdate = await betListingService.Update(betListing).ConfigureAwait(true);
                if (!resultUpdate) throw new Exception("Error cancelling bet listing validation!");

                return Json(new { status = true, messageTitle = "Success!", message = "Validation cancelled successfully!" });


            }
            catch (Exception e)
            {
                await errorLogService.InsertException(e, UserId).ConfigureAwait(true);
                return Json(new { status = false, messageTitle = "Error", message = e.Message });
            }
        }

        #endregion

        #region Session Manager
        public async Task<IActionResult> SessionManager(int TimeOffset = 0)
        {
            Guid? UserId = null;
            RequestFeedback request = new RequestFeedback();
            try
            {
                ViewData["TimezoneOffset"] = TimeOffset;
                UserId = User.Id();

                SystemConstants SessionTimeout = (await systemConstantsService.GetByName(SystemConstantName.SessionTimeout).ConfigureAwait(true));

                if (SessionTimeout == null)
                {

                    SessionTimeout = new SystemConstants
                    {
                        Name = SystemConstantName.SessionTimeout,
                        Value = "20.00",
                        RowDate = DateTime.UtcNow,
                        Type = SystemConstantType.Int
                    };
                    var resultFees = await systemConstantsService.Insert(SessionTimeout).ConfigureAwait(true);
                    if (!resultFees) throw new Exception("Session Timeout could not be saved");


                }

                SessionManagerDto model = new SessionManagerDto
                {
                    SessionTimeout = Convert.ToInt32(Convert.ToDecimal(SessionTimeout.Value)),
                };


                return View(model);
            }
            catch (Exception e)
            {
                await errorLogService.InsertException(e, UserId).ConfigureAwait(true);
                return RedirectToAction("Index", "Error", request);
            }
        }


        [HttpPost]
        public async Task<IActionResult> UpdateSessionManager(SessionManagerDto data)
        {
            try
            {
                if (data == null) throw new Exception("No data");
                else
                {
                    if (data.SessionTimeout <= 0) throw new Exception("Session Timeout needs to have a value greater than 0!");

                    var result = await systemConstantsService.UpdateSessionTimeout(data.SessionTimeout).ConfigureAwait(true);
                    if (result)
                    {
                        return Json(new { status = true, messageTitle = "Success", message = "Settings updated successfully!" });
                    }
                    else throw new Exception("Error updating settings. Try again later.");
                }
            }
            catch (Exception e)
            {
                ModelState.AddModelError(string.Empty, "Something went wrong, please try again later");
                await errorLogService.InsertException(e).ConfigureAwait(true);
                return Json(new { status = false, messageTitle = "Error", message = e.Message });
            }
        }
        #endregion


        #region Email Configuration Editor
        public async Task<IActionResult> EmailConfigEditor(int TimeOffset = 0)
        {
            ViewData["TimezoneOffset"] = TimeOffset;
            Guid? UserId = null;
            RequestFeedback request = new RequestFeedback();
            try
            {
                EmailConfigAllDto model = new EmailConfigAllDto
                {
                    EmailConfigs = await emailConfigService.GetAll().ConfigureAwait(true)
                };
                return View(model);
            }
            catch (Exception e)
            {
                await errorLogService.InsertException(e, UserId).ConfigureAwait(true);
                return RedirectToAction("Index", "Error", request);
            }
        }
        public async Task<IActionResult> EmailConfigAdd(int TimeOffset = 0)
        {
            ViewData["TimezoneOffset"] = TimeOffset;
            Guid? UserId = null;
            RequestFeedback request = new RequestFeedback();
            try
            {
                return View();
            }
            catch (Exception e)
            {
                await errorLogService.InsertException(e, UserId).ConfigureAwait(true);
                return RedirectToAction("Index", "Error", request);
            }
        }
        public async Task<IActionResult> EmailConfigEdit(int Id = 0, int TimeOffset = 0)
        {
            ViewData["TimezoneOffset"] = TimeOffset;
            Guid? UserId = null;
            RequestFeedback request = new RequestFeedback();
            try
            {
                if (Id == 0) throw new Exception("You need to provide a email configuration detail to edit.");
                EmailConfig model = await emailConfigService.GetById(Id).ConfigureAwait(true);
                if (model == null) throw new Exception("You need to provide a email configuration template to edit.");
                return View(model);
            }
            catch (Exception e)
            {
                await errorLogService.InsertException(e, UserId).ConfigureAwait(true);
                return RedirectToAction("Index", "Error", request);
            }
        }

        [HttpPost]
        public async Task<IActionResult> CreateNewEmailConfig(EmailConfig template)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(template.Password)) throw new Exception("You need to provide a password for email sender.");
                if (string.IsNullOrWhiteSpace(template.Email)) throw new Exception("You need to provide a email");
                if (string.IsNullOrWhiteSpace(template.Display_name)) throw new Exception("You need to provide a display name for the new email template.");
                if (string.IsNullOrWhiteSpace(template.User_name)) throw new Exception("You need to provide a user name for the new email template.");
                if (string.IsNullOrWhiteSpace(template.Host)) throw new Exception("You need to provide a valid host for template.");

                EmailConfig newTemplate = new EmailConfig
                {
                    Port = template.Port,
                    Password = template.Password,
                    Email = template.Email,
                    Host = template.Host,
                    Display_name = template.Display_name,
                    Enable_ssl = template.Enable_ssl,
                    User_name= template.User_name,
                    Is_default_email_account = template.Is_default_email_account,
                    Active = template.Active
                };


                await emailConfigService.Insert(newTemplate).ConfigureAwait(true);

                return Json(new { status = true, messageTitle = "Success", message = "New Email config settings created successfully!" });

            }
            catch (Exception e)
            {
                ModelState.AddModelError(string.Empty, "Something went wrong, please try again later");
                await errorLogService.InsertException(e).ConfigureAwait(true);
                return Json(new { status = false, messageTitle = "Error", message = e.Message });
            }
        }


        [HttpPost]
        public async Task<IActionResult> UpdateNewEmailConfig(EmailConfig template)
        {
            try
            {
                if (template.Id == 0) throw new Exception("You need to provide email config id to update");

                EmailConfig updateTemplate = await emailConfigService.GetById(template.Id).ConfigureAwait(true);
                if (updateTemplate == null) throw new Exception("You need to provide a email config to update.");

                if (!String.IsNullOrEmpty(template.Display_name)) { updateTemplate.Display_name = template.Display_name; };
                if (!String.IsNullOrEmpty(template.User_name)) { updateTemplate.User_name = template.User_name; };
                if (!String.IsNullOrEmpty(template.Email)) { updateTemplate.Email = template.Email; };
                if (!String.IsNullOrEmpty(template.Password)) { updateTemplate.Password = template.Password; };
                if (!String.IsNullOrEmpty(template.Host)) { updateTemplate.Host = template.Host; };
                if(template.Port >0) { updateTemplate.Port = template.Port; };
                if(template.Is_default_email_account.HasValue) { updateTemplate.Is_default_email_account = template.Is_default_email_account; }
                if (template.Active.HasValue) { updateTemplate.Active = template.Active; }
                if (template.Enable_ssl.HasValue) { updateTemplate.Enable_ssl = template.Enable_ssl; }

                await emailConfigService.Update(updateTemplate).ConfigureAwait(true);

                return Json(new { status = true, messageTitle = "Success", message = "Email config details updated successfully!" });

            }
            catch (Exception e)
            {
                ModelState.AddModelError(string.Empty, "Something went wrong, please try again later");
                await errorLogService.InsertException(e).ConfigureAwait(true);
                return Json(new { status = false, messageTitle = "Error", message = e.Message });
            }
        }


        #endregion

        #region Email Template Editor

        public async Task<IActionResult> EmailTemplateEditor(int TimeOffset = 0)
        {
            ViewData["TimezoneOffset"] = TimeOffset;
            Guid? UserId = null;
            RequestFeedback request = new RequestFeedback();
            try
            {
                SecondaryEmailTemplateListDto model = new SecondaryEmailTemplateListDto
                {
                    Templates = await secondaryEmailTemplateService.GetAll().ConfigureAwait(true),
                    Users = (await userService.GetAll().ConfigureAwait(true)).OrderBy(o => o.FirstName).ThenBy(o => o.LastName).Select(i => new SecondaryEmailTemplateUserListItemDto { FirstName = i.FirstName, LastName = i.LastName, Email = i.Email, Id = i.Id }).ToList()
                };
                return View(model);
            }
            catch (Exception e)
            {
                await errorLogService.InsertException(e, UserId).ConfigureAwait(true);
                return RedirectToAction("Index", "Error", request);
            }
        }
        public async Task<IActionResult> EmailTemplateAdd(int TimeOffset = 0)
        {
            ViewData["TimezoneOffset"] = TimeOffset;
            Guid? UserId = null;
            RequestFeedback request = new RequestFeedback();
            try
            {
                return View();
            }
            catch (Exception e)
            {
                await errorLogService.InsertException(e, UserId).ConfigureAwait(true);
                return RedirectToAction("Index", "Error", request);
            }
        }
        public async Task<IActionResult> EmailTemplateEdit(int Id = 0, int TimeOffset = 0)
        {
            ViewData["TimezoneOffset"] = TimeOffset;
            Guid? UserId = null;
            RequestFeedback request = new RequestFeedback();
            try
            {
                if (Id == 0) throw new Exception("You need to provide a email template to edit.");
                SecondaryEmailTemplate model = await secondaryEmailTemplateService.GetById(Id).ConfigureAwait(true);
                if (model == null) throw new Exception("You need to provide a email template to edit.");
                return View(model);
            }
            catch (Exception e)
            {
                await errorLogService.InsertException(e, UserId).ConfigureAwait(true);
                return RedirectToAction("Index", "Error", request);
            }
        }

        [HttpPost]
        public async Task<IActionResult> CreateNewEmailTemplate(SecondaryEmailTemplateDto template)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(template.Title)) throw new Exception("You need to provide a Title for the new email template.");
                if (string.IsNullOrWhiteSpace(template.Content)) throw new Exception("You need to provide Content for the new email template.");
                if (string.IsNullOrWhiteSpace(template.Subject)) throw new Exception("You need to provide a Subject for the new email template.");

                SecondaryEmailTemplate newTemplate = new SecondaryEmailTemplate
                {
                    Content = template.Content,
                    Title = template.Title,
                    Subject = template.Subject,
                    Greeting = null
                };


                var result = await secondaryEmailTemplateService.Insert(newTemplate).ConfigureAwait(true);
                if (result)
                {
                    return Json(new { status = true, messageTitle = "Success", message = "Email template created successfully!" });
                }
                else throw new Exception("Error creating email template. Try again later.");

            }
            catch (Exception e)
            {
                ModelState.AddModelError(string.Empty, "Something went wrong, please try again later");
                await errorLogService.InsertException(e).ConfigureAwait(true);
                return Json(new { status = false, messageTitle = "Error", message = e.Message });
            }
        }


        [HttpPost]
        public async Task<IActionResult> SendEmailTemplateToUser(SecondaryEmailTemplateSendToUserDto info)
        {
            try
            {
                if (info.TemplateId == 0) throw new Exception("You need to provide a email template to send email.");
                if (string.IsNullOrWhiteSpace(info.Email)) throw new Exception("You need to provide an Email to send email.");
                if (string.IsNullOrWhiteSpace(info.Name)) throw new Exception("You need to provide an Name to send email.");

                SecondaryEmailTemplate sendTemplate = await secondaryEmailTemplateService.GetById(info.TemplateId).ConfigureAwait(true);
                if (sendTemplate == null) throw new Exception("You need to provide a email template to send email.");


                var result = await emailSenderService.SendEmailTemplateToUser(info.Email, info.Name, sendTemplate).ConfigureAwait(true);
                if (result)
                {
                    return Json(new { status = true, messageTitle = "Success", message = "Email sent successfully!" });
                }
                else throw new Exception("Error sending email. Try again later.");

            }
            catch (Exception e)
            {
                ModelState.AddModelError(string.Empty, "Something went wrong, please try again later");
                await errorLogService.InsertException(e).ConfigureAwait(true);
                return Json(new { status = false, messageTitle = "Error", message = e.Message });
            }
        }


        [HttpPost]
        public async Task<IActionResult> UpdateNewEmailTemplate(SecondaryEmailTemplateDto template)
        {
            try
            {
                if (template.Id == 0) throw new Exception("You need to provide a email template to edit.");
                if (string.IsNullOrWhiteSpace(template.Title)) throw new Exception("You need to provide a Title for the new email template.");
                if (string.IsNullOrWhiteSpace(template.Content)) throw new Exception("You need to provide Content for the new email template.");
                if (string.IsNullOrWhiteSpace(template.Subject)) throw new Exception("You need to provide a Subject for the new email template.");

                SecondaryEmailTemplate updateTemplate = await secondaryEmailTemplateService.GetById(template.Id).ConfigureAwait(true);
                if (updateTemplate == null) throw new Exception("You need to provide a email template to edit.");

                updateTemplate.Content = template.Content;
                updateTemplate.Title = template.Title;
                updateTemplate.Subject = template.Subject;

                var result = await secondaryEmailTemplateService.Update(updateTemplate).ConfigureAwait(true);
                if (result)
                {
                    return Json(new { status = true, messageTitle = "Success", message = "Email template updated successfully!" });
                }
                else throw new Exception("Error updating email template. Try again later.");

            }
            catch (Exception e)
            {
                ModelState.AddModelError(string.Empty, "Something went wrong, please try again later");
                await errorLogService.InsertException(e).ConfigureAwait(true);
                return Json(new { status = false, messageTitle = "Error", message = e.Message });
            }
        }
        #endregion

        #region Media Manager
        public async Task<IActionResult> MediaManager(Guid? Folder = null, int TimeOffset = 0)
        {
            ViewData["TimezoneOffset"] = TimeOffset;
            ViewData["root"] = hostingEnvironment.ContentRootPath;
            Guid? UserId = null;
            RequestFeedback request = new RequestFeedback();
            try
            {
                MediaFolder CurrentFolder = null;
                Guid? TopLevelId = null;
                if (Folder.HasValue)
                {
                    CurrentFolder = await mediaFolderService.GetById(Folder.Value).ConfigureAwait(true);

                    if (CurrentFolder.Deleted)
                    {
                        CurrentFolder = null;
                        TopLevelId = null;
                        Folder = null;
                    }
                    else
                    {
                        TopLevelId = CurrentFolder.ParentFolderId;
                    }

                }

                ICollection<MediaFolder> folderOptions = await mediaFolderService.GetAll(false).ConfigureAwait(true);
                ICollection<MediaFolder> Folders = await mediaFolderService.GetAllByParentId(Folder).ConfigureAwait(true);
                ICollection<MediaFile> FilesInFolder = await mediaFileService.GetAllFolderId(Folder).ConfigureAwait(true);

                MediaFolderListDto model = new MediaFolderListDto
                {
                    Folders = Folders.OrderBy(o => o.Name).ToList(),
                    TopLevelId = TopLevelId,
                    CurrentFolder = CurrentFolder,
                    FilesInFolder = FilesInFolder.OrderBy(o => o.Title).ToList(),
                    ParentFolderOptions = folderOptions.Select(i => new SelectListItem { Text = i.Name, Value = i.Id.ToString() }).OrderBy(o => o.Text).ToList()
                };
                return View(model);
            }
            catch (Exception e)
            {
                await errorLogService.InsertException(e, UserId).ConfigureAwait(true);
                return RedirectToAction("Index", "Error", request);
            }
        }

        public async Task<IActionResult> MediaManagerCropMedia(Guid Media, int TimezoneOffset = 0)
        {
            ViewData["TimezoneOffset"] = TimezoneOffset;
            ViewData["root"] = hostingEnvironment.ContentRootPath;
            Guid? UserId = null;
            RequestFeedback request = new RequestFeedback();
            try
            {
                MediaFile model = await mediaFileService.GetById(Media).ConfigureAwait(true);
                return View(model);
            }
            catch (Exception e)
            {
                await errorLogService.InsertException(e, UserId).ConfigureAwait(true);
                return RedirectToAction("Index", "Error", request);
            }
        }
        public async Task<IActionResult> GetMediaPickerData(int TimezoneOffset = 0, decimal? Width = null, decimal? Height = null)
        {
            Guid? UserId = null;
            RequestFeedback request = new RequestFeedback();
            string root = hostingEnvironment.ContentRootPath;
            try
            {
                ICollection<MediaFile> files = (await mediaFileService.GetAll(false).ConfigureAwait(true));
                if (Width.HasValue || Height.HasValue)
                {
                    if (Width.HasValue && Height.HasValue)
                    {
                        files = files.Where(i => i.Width == Width && i.Height == Height).ToList();
                    }
                    else
                    {
                        if (Width.HasValue)
                        {
                            files = files.Where(i => i.Width == Width).ToList();
                        }
                        if (Height.HasValue)
                        {
                            files = files.Where(i => i.Height == Height).ToList();
                        }
                    }

                }
                ICollection<MediaManagerPickerItemDto> model = files.Select(i => new MediaManagerPickerItemDto { Id = i.Id.ToString(), Title = i.Title, LastUpdated = DateHelper.DateSimpleFormat(i.LastUpdated.FromUTCData(TimezoneOffset)), Media = ImageHelper.ConvertImageToBase64(root + i.PhysicalPath) }).OrderBy(o => o.Title).ToList();
                return PartialView(model);
            }
            catch (Exception e)
            {
                await errorLogService.InsertException(e).ConfigureAwait(true);
                return PartialView(null);
            }
        }
        public async Task<IActionResult> GetMediaPickerData2(int TimezoneOffset = 0)
        {
            Guid? UserId = null;
            RequestFeedback request = new RequestFeedback();
            string root = hostingEnvironment.ContentRootPath;
            try
            {
                ICollection<MediaManagerPickerItemDto> model = (await mediaFileService.GetAll(false).ConfigureAwait(true)).Select(i => new MediaManagerPickerItemDto { Id = i.Id.ToString(), Title = i.Title, LastUpdated = DateHelper.DateSimpleFormat(i.LastUpdated.FromUTCData(TimezoneOffset)), Media = ImageHelper.ConvertImageToBase64(root + i.PhysicalPath) }).OrderBy(o => o.Title).ToList();
                return PartialView(model);
            }
            catch (Exception e)
            {
                await errorLogService.InsertException(e).ConfigureAwait(true);
                return PartialView(null);
            }
        }

        public async Task<IActionResult> DownloadMediaFile(Guid? FileIdDownload = null)
        {
            try
            {
                string basePath = hostingEnvironment.ContentRootPath;

                if (!FileIdDownload.HasValue || (FileIdDownload.HasValue && FileIdDownload.Value == Guid.Empty)) throw new Exception("No file found.");

                MediaFile file = await mediaFileService.GetById(FileIdDownload.Value).ConfigureAwait(true);
                if (file == null) throw new Exception("No file found.");

                string mediaPath = FileHelper.PathCombine(basePath, file.PhysicalPath);


                if (System.IO.File.Exists(mediaPath))
                {
                    byte[] fileBytes = System.IO.File.ReadAllBytes(@mediaPath);
                    string FileName = file.Title + System.IO.Path.GetExtension(mediaPath);
                    return File(fileBytes, System.Net.Mime.MediaTypeNames.Application.Octet, FileName);
                }
                else
                {
                    throw new FileNotFoundException();
                }
            }
            catch (Exception e)
            {
                await errorLogService.InsertException(e).ConfigureAwait(true);
                return RedirectToAction("Index", "Error", "");
            }
        }

        [HttpPost]
        public async Task<IActionResult> AddMediaFolder(MediaFolderItemDto item)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(item.Name)) throw new Exception("You need to provide a Name for the new folder.");

                MediaFolder newItem = new MediaFolder
                {
                    LastUpdated = DateTime.UtcNow,
                    Name = item.Name,
                    ParentFolderId = item.ParentFolderId,
                    Deleted = false
                };


                var result = await mediaFolderService.Insert(newItem).ConfigureAwait(true);
                if (result)
                {
                    return Json(new { status = true, messageTitle = "Success", message = "Folder created successfully!" });
                }
                else throw new Exception("Error creating folder. Try again later.");

            }
            catch (Exception e)
            {
                ModelState.AddModelError(string.Empty, "Something went wrong, please try again later");
                await errorLogService.InsertException(e).ConfigureAwait(true);
                return Json(new { status = false, messageTitle = "Error", message = e.Message });
            }
        }

        [HttpPost]
        public async Task<IActionResult> EditMediaFolder(MediaFolderItemDto item)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(item.Name)) throw new Exception("You need to provide a Name to edit the folder.");
                if (!item.Id.HasValue) throw new Exception("You need to provide a valid ID to edit the folder.");

                MediaFolder folder = await mediaFolderService.GetById(item.Id.Value).ConfigureAwait(true);

                folder.Name = item.Name;
                folder.LastUpdated = DateTime.UtcNow;

                var result = await mediaFolderService.Update(folder).ConfigureAwait(true);
                if (result)
                {
                    return Json(new { status = true, messageTitle = "Success", message = "Folder updated successfully!" });
                }
                else throw new Exception("Error editing folder. Try again later.");

            }
            catch (Exception e)
            {
                ModelState.AddModelError(string.Empty, "Something went wrong, please try again later");
                await errorLogService.InsertException(e).ConfigureAwait(true);
                return Json(new { status = false, messageTitle = "Error", message = e.Message });
            }
        }

        [HttpPost]
        public async Task<IActionResult> CropMedia(CroppedMediaFileItemDto item)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(item.CroppedMedia)) throw new Exception("You need to provide a cropped media to update.");

                MediaFile file = await mediaFileService.GetById(item.Id).ConfigureAwait(true);
                if (file == null) throw new Exception("No media to crop");
                var dataUri = item.CroppedMedia;
                var encodedImage = dataUri.Split(",")[1];
                var decodedImage = Convert.FromBase64String(encodedImage);

                string directory = "\\wwwroot\\MediaManager\\";
                string filename = Guid.NewGuid().ToString() + ".png";
                Directory.CreateDirectory(directory);
                string basePath = hostingEnvironment.ContentRootPath;
                string path = directory + filename;
                if (System.IO.File.Exists(basePath + path))
                {
                    System.IO.File.Delete(basePath + path);
                }
                System.IO.File.WriteAllBytes(basePath + path, decodedImage);

                if (!string.IsNullOrWhiteSpace(file.PhysicalPath))
                {
                    string oldFile = FileHelper.PathCombine(basePath, file.PhysicalPath);
                    if (oldFile != null && System.IO.File.Exists(oldFile))
                    {
                        System.IO.File.Delete(oldFile);
                        file.PhysicalPath = null;
                    }

                }
                file.LastUpdated = DateTime.UtcNow;
                file.PhysicalPath = path;
                file.URL = basePath + path;

                FileInfo fileInfo = new FileInfo(file.URL);
                var sizeInBytes = fileInfo.Length;

                Bitmap img = new Bitmap(file.URL);

                file.Height = Convert.ToDecimal(img.Height);
                file.Width = Convert.ToDecimal(img.Width);
                img.Dispose();

                var result = await mediaFileService.Update(file).ConfigureAwait(true);
                if (result)
                {
                    return Json(new { status = true, messageTitle = "Success", message = "Media cropped successfully!" });
                }
                else throw new Exception("Error cropping media. Try again later.");

            }
            catch (Exception e)
            {
                ModelState.AddModelError(string.Empty, "Something went wrong, please try again later");
                await errorLogService.InsertException(e).ConfigureAwait(true);
                return Json(new { status = false, messageTitle = "Error", message = e.Message });
            }
        }


        [HttpPost]
        public async Task<IActionResult> DeleteMediaFolder(Guid Id)
        {
            try
            {
                MediaFolder folder = await mediaFolderService.GetById(Id).ConfigureAwait(true);

                folder.Deleted = true;

                var result = await mediaFolderService.Update(folder).ConfigureAwait(true);
                if (result)
                {
                    return Json(new { status = true, messageTitle = "Success", message = "Folder deleted successfully!" });
                }
                else throw new Exception("Error deleting folder. Try again later.");

            }
            catch (Exception e)
            {
                ModelState.AddModelError(string.Empty, "Something went wrong, please try again later");
                await errorLogService.InsertException(e).ConfigureAwait(true);
                return Json(new { status = false, messageTitle = "Error", message = e.Message });
            }
        }



        [HttpPost]
        public async Task<IActionResult> AddMediaFile(CreateMediaFileItemDto Item)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(Item.Title)) throw new Exception("You need to provide a Title for the new media.");
                if (Item.File == null) throw new Exception("You need to provide file for the new media upload.");

                MediaFile file = new MediaFile
                {
                    FolderId = Item.FolderId,
                    Deleted = false,
                    LastUpdated = DateTime.UtcNow,
                    Title = Item.Title,
                };

                if (Item.File != null)
                {
                    string extension = Path.GetExtension(Item.File.FileName);
                    long size = Item.File.Length;
                    file.Size = Convert.ToDecimal(size);
                    if (FileHelper.FileIsFormSafe(extension, size))
                    {




                        string basePath = hostingEnvironment.ContentRootPath;
                        string mediaBlog = "\\wwwroot\\MediaManager";
                        Directory.CreateDirectory(FileHelper.PathCombine(basePath, mediaBlog));
                        string mediaPath = FileHelper.PathCombine(mediaBlog, Guid.NewGuid().ToString() + Path.GetExtension(Item.File.FileName));
                        string path = FileHelper.PathCombine(basePath, mediaPath);
                        using (var fileStream = new FileStream(path, FileMode.Create))
                        {
                            await Item.File.CopyToAsync(fileStream).ConfigureAwait(true);
                        }
                        file.PhysicalPath = mediaPath;
                        file.URL = basePath + mediaPath;

                        FileInfo fileInfo = new FileInfo(file.URL);
                        var sizeInBytes = fileInfo.Length;

                        Bitmap img = new Bitmap(file.URL);

                        file.Height = Convert.ToDecimal(img.Height);
                        file.Width = Convert.ToDecimal(img.Width);
                        img.Dispose();
                    }
                    else throw new Exception("Try to upload one of the following types: JPG or PNG");
                }

                var result = await mediaFileService.Insert(file).ConfigureAwait(true);
                if (result)
                {
                    return Json(new { status = true, messageTitle = "Success", message = "Media saved successfully!" });
                }
                else throw new Exception("Error upload media. Try again later.");

            }
            catch (Exception e)
            {
                ModelState.AddModelError(string.Empty, "Something went wrong, please try again later");
                await errorLogService.InsertException(e).ConfigureAwait(true);
                return Json(new { status = false, messageTitle = "Error", message = e.Message });
            }
        }




        [HttpPost]
        public async Task<IActionResult> EditMediaFile(MediaFileItemDto item)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(item.Title)) throw new Exception("You need to provide a Title to edit the folder.");
                if (item.Id == Guid.Empty) throw new Exception("You need to provide a valid ID to edit the folder.");

                MediaFile file = await mediaFileService.GetById(item.Id).ConfigureAwait(true);

                file.Title = item.Title;
                file.LastUpdated = DateTime.UtcNow;

                var result = await mediaFileService.Update(file).ConfigureAwait(true);
                if (result)
                {
                    return Json(new { status = true, messageTitle = "Success", message = "Media updated successfully!" });
                }
                else throw new Exception("Error editing Media. Try again later.");

            }
            catch (Exception e)
            {
                ModelState.AddModelError(string.Empty, "Something went wrong, please try again later");
                await errorLogService.InsertException(e).ConfigureAwait(true);
                return Json(new { status = false, messageTitle = "Error", message = e.Message });
            }
        }

        [HttpPost]
        public async Task<IActionResult> MoveMediaFile(MoveMediaFileDto item)
        {
            try
            {
                if (item.Id == Guid.Empty) throw new Exception("You need to provide a file to move it to a folder.");
                if (item.FolderToId.HasValue && item.FolderToId.Value == Guid.Empty) throw new Exception("You need to provide a valid folder to move the file.");

                MediaFile file = await mediaFileService.GetById(item.Id).ConfigureAwait(true);
                if (file == null) throw new Exception("You need to provide a file to move it to a folder.");

                if (item.FolderToId.HasValue)
                {
                    MediaFolder folder = await mediaFolderService.GetById(item.FolderToId.Value).ConfigureAwait(true);
                    if (folder == null) throw new Exception("You need to provide a valid folder to move the file.");
                }


                file.FolderId = item.FolderToId;
                file.LastUpdated = DateTime.UtcNow;

                var result = await mediaFileService.Update(file).ConfigureAwait(true);
                if (result)
                {
                    return Json(new { status = true, messageTitle = "Success", message = "Media moved successfully!" });
                }
                else throw new Exception("Error moving Media. Try again later.");

            }
            catch (Exception e)
            {
                ModelState.AddModelError(string.Empty, "Something went wrong, please try again later");
                await errorLogService.InsertException(e).ConfigureAwait(true);
                return Json(new { status = false, messageTitle = "Error", message = e.Message });
            }
        }


        [HttpPost]
        public async Task<IActionResult> DeleteMediaFile(Guid Id)
        {
            try
            {
                MediaFile file = await mediaFileService.GetById(Id).ConfigureAwait(true);

                file.Deleted = true;

                var result = await mediaFileService.Update(file).ConfigureAwait(true);
                if (result)
                {
                    return Json(new { status = true, messageTitle = "Success", message = "Media deleted successfully!" });
                }
                else throw new Exception("Error deleting media. Try again later.");

            }
            catch (Exception e)
            {
                ModelState.AddModelError(string.Empty, "Something went wrong, please try again later");
                await errorLogService.InsertException(e).ConfigureAwait(true);
                return Json(new { status = false, messageTitle = "Error", message = e.Message });
            }
        }


        #endregion

        #region Payments/Payouts
        public async Task<IActionResult> SendMoneyToUsers(int TimeOffset = 0)
        {
            ViewData["TimezoneOffset"] = TimeOffset;
            Guid? UserId = null;
            RequestFeedback request = new RequestFeedback();
            try
            {
                UserId = User.Id();
                UserListDto model = new UserListDto
                {
                    Users = await userService.GetUserList().ConfigureAwait(true),
                };
                return View(model);
            }
            catch (Exception e)
            {
                await errorLogService.InsertException(e, UserId).ConfigureAwait(true);
                return RedirectToAction("Index", "Error", request);
            }
        }
        public async Task<IActionResult> SendMoneyToUsersInMass(int TimeOffset = 0)
        {
            ViewData["TimezoneOffset"] = TimeOffset;
            Guid? UserId = null;
            RequestFeedback request = new RequestFeedback();
            try
            {
                UserId = User.Id();
                return View();
            }
            catch (Exception e)
            {
                await errorLogService.InsertException(e, UserId).ConfigureAwait(true);
                return RedirectToAction("Index", "Error", request);
            }
        }




        [HttpPost]
        public async Task<IActionResult> ParseMassPayoutTemplate(MassPaypalPayoutFormSubmissionDto template)
        {
            try
            {
                if (template.CsvFile == null) throw new Exception("You need to provide a valid file to submit a mass payout.");

                MassPaypalPayoutUserListDto model = new MassPaypalPayoutUserListDto
                {
                    Users = new List<MassPaypalPayoutUserListItemDto>(),
                    NonUsers = new List<MassPaypalPayoutListItemDto>()
                };
                ICollection<MassPaypalPayoutListItemDto> csvData = new List<MassPaypalPayoutListItemDto>();

                string extension = Path.GetExtension(template.CsvFile.FileName);
                long size = template.CsvFile.Length;
                if (FileHelper.FileIsMassUploaderSafe(extension, size))
                {
                    string basePath = hostingEnvironment.ContentRootPath;
                    string mediaPath = FileHelper.PathCombine(basePath, "\\Media\\", "\\TempCSV");
                    Directory.CreateDirectory(mediaPath);

                    string path = FileHelper.PathCombine(mediaPath, Guid.NewGuid().ToString() + Path.GetExtension(template.CsvFile.FileName));
                    using (var fileStream = new FileStream(path, FileMode.Create))
                    {
                        await template.CsvFile.CopyToAsync(fileStream);
                    }
                    using (var reader = new StreamReader(path))
                    {
                        using (var csv = new CsvHelper.CsvReader((IParser)reader))
                        {
                            csv.Read();
                            csv.ReadHeader();
                            while (csv.Read())
                            {
                                var record = new MassPaypalPayoutListItemDto
                                {
                                    Email = csv.GetField("Email"),
                                    Value = Convert.ToDecimal(csv.GetField("Value"))
                                };
                                csvData.Add(record);
                            }
                        }
                    }

                    if (System.IO.File.Exists(path)) System.IO.File.Delete(path);


                    if (csvData.Count > 0)
                    {
                        var users = await userService.GetAllMassPayout().ConfigureAwait(true);
                        var usersEmails = users.Select(i => i.Email.ToLower()).ToList();

                        foreach (var item in csvData)
                        {
                            if (usersEmails.Contains(item.Email.ToLower()))
                            {
                                string paypalAccount = users.Where(i => i.Email.ToLower() == item.Email.ToLower()).SingleOrDefault().PaypalEmail;

                                if (string.IsNullOrWhiteSpace(paypalAccount))
                                {

                                    MassPaypalPayoutUserListItemDto user = new MassPaypalPayoutUserListItemDto
                                    {
                                        Id = users.Where(i => i.Email.ToLower() == item.Email.ToLower()).SingleOrDefault().Id,
                                        Name = users.Where(i => i.Email.ToLower() == item.Email.ToLower()).SingleOrDefault().Name,
                                        Email = item.Email,
                                        Value = item.Value,
                                        PaypalConnected = !string.IsNullOrWhiteSpace(users.Where(i => i.Email.ToLower() == item.Email.ToLower()).SingleOrDefault().PaypalEmail),
                                        PaypalEmail = users.Where(i => i.Email.ToLower() == item.Email.ToLower()).SingleOrDefault().Email
                                    };
                                    model.Users.Add(user);
                                }
                                else
                                {
                                    MassPaypalPayoutUserListItemDto user = new MassPaypalPayoutUserListItemDto
                                    {
                                        Id = users.Where(i => i.Email.ToLower() == item.Email.ToLower()).SingleOrDefault().Id,
                                        Name = users.Where(i => i.Email.ToLower() == item.Email.ToLower()).SingleOrDefault().Name,
                                        Email = item.Email,
                                        Value = item.Value,
                                        PaypalConnected = !string.IsNullOrWhiteSpace(users.Where(i => i.Email.ToLower() == item.Email.ToLower()).SingleOrDefault().PaypalEmail),
                                        PaypalEmail = users.Where(i => i.Email.ToLower() == item.Email.ToLower()).SingleOrDefault().PaypalEmail
                                    };
                                    model.Users.Add(user);
                                }
                            }
                            else
                            {
                                MassPaypalPayoutListItemDto nonUser = new MassPaypalPayoutListItemDto
                                {
                                    Email = item.Email,
                                    Value = item.Value,
                                    Reason = "Not an user"
                                };
                                model.NonUsers.Add(nonUser);
                            }
                        }

                        model.Users = model.Users.OrderBy(o => o.Name).ToList();
                        model.NonUsers = model.NonUsers.OrderBy(o => o.Email).ToList();
                    }

                    if (model.Users.Count > 0)
                    {
                        model.PayoutJsonString = JsonConvert.SerializeObject(model.Users);
                    }

                    return PartialView("ParseMassPayoutTemplate", model);
                }
                else
                {
                    throw new Exception("File not safe.");
                }


            }
            catch (Exception e)
            {
                ModelState.AddModelError(string.Empty, "Something went wrong, please try again later");
                await errorLogService.InsertException(e).ConfigureAwait(true);
                return null;
            }
        }


        public async Task<IActionResult> SendMoneyToUsersPayoutHistory(Guid Id, int TimeOffset = 0)
        {
            ViewData["TimezoneOffset"] = TimeOffset;
            RequestFeedback request = new RequestFeedback();
            try
            {
                User user = await userService.GetById(Id).ConfigureAwait(true);
                UserPayoutList model = new UserPayoutList
                {
                    UserName = user.FirstName + " " + user.LastName,
                    UserId = user.Id,
                    Payouts = await userPayoutService.GetAllByUser(user.Id).ConfigureAwait(true)
                };
                return View(model);
            }
            catch (Exception e)
            {
                await errorLogService.InsertException(e).ConfigureAwait(true);
                return RedirectToAction("Index", "Error", request);
            }
        }


        public async Task<IActionResult> OrdersPayout(int TimeOffset = 0)
        {
            ViewData["TimezoneOffset"] = TimeOffset;
            Guid? UserId = null;
            RequestFeedback request = new RequestFeedback();
            try
            {
                UserId = User.Id();
                if (!UserId.HasValue || UserId.Value == Guid.Empty) throw new Exception("No user found!");
                OrdersPayoutDto model = new OrdersPayoutDto
                {
                    Orders = (await orderService.GetAllOrdersForBackend().ConfigureAwait(true)).Select(i => new OrdersPayoutItemDto { Order = i, HasPendingPayouts = i.Items.Any(x => !x.PayoutId.HasValue) }).ToList()
                };

                if (model.Orders.Count > 0)
                {

                    foreach (var order in model.Orders)
                    {
                        if (order.HasPendingPayouts)
                        {
                            ICollection<OrderItemPayoutDto> payouts = new List<OrderItemPayoutDto>();
                            foreach (var item in order.Order.Items)
                            {
                                OrderItemPayoutDto itemToPay = new OrderItemPayoutDto
                                {
                                    UserToPay = item.SoldBy,
                                    UserToPayId = item.BetListing.PostedbyId,
                                    UserEmailToPay = string.IsNullOrWhiteSpace(item.BetListing.Postedby.PaypalAccountEmail) ? item.BetListing.Postedby.Email : item.BetListing.Postedby.PaypalAccountEmail,
                                    ValueToPay = item.Price - ((order.Order.ServiceFeePercent * item.Price) / 100),
                                    PaypalConnected = !string.IsNullOrWhiteSpace(item.BetListing.Postedby.PaypalAccountEmail)
                                };
                                if (!item.PayoutId.HasValue)
                                {
                                    payouts.Add(itemToPay);
                                }
                            }
                            payouts = payouts.GroupBy(g => g.UserToPayId).Select(x => new OrderItemPayoutDto { UserToPayId = x.Key, UserToPay = x.First().UserToPay, ValueToPay = x.Sum(y => y.ValueToPay), PaypalConnected = x.First().PaypalConnected, UserEmailToPay = x.First().UserEmailToPay }).OrderBy(o => o.UserToPayId).ToList();
                            if (payouts.Count > 0)
                            {
                                order.PayoutJson = JsonConvert.SerializeObject(payouts);
                            }
                        }
                        else
                        {
                            order.PayoutJson = "";
                        }
                    }
                }

                return View(model);
            }
            catch (Exception e)
            {
                await errorLogService.InsertException(e, UserId).ConfigureAwait(true);
                return RedirectToAction("Index", "Error", request);
            }
        }

        public async Task<IActionResult> OrdersPayoutDetails(Guid Id, int TimeOffset = 0)
        {
            ViewData["TimezoneOffset"] = TimeOffset;
            Guid? UserId = null;
            RequestFeedback request = new RequestFeedback();
            try
            {
                UserId = User.Id();
                if (!UserId.HasValue || UserId.Value == Guid.Empty) throw new Exception("No user found!");
                OrderDetailsDto data = await orderService.GetOrderDetails(Id).ConfigureAwait(true);
                OrderPayoutDetailsDto model = new OrderPayoutDetailsDto();
                model = model.FromOrderDetailsDto(data);

                if (model.OrderItems.Count > 0)
                {

                    foreach (var item in model.OrderItems)
                    {
                        OrderItemPayoutDto itemToPay = new OrderItemPayoutDto
                        {
                            UserToPay = item.SoldBy,
                            UserToPayId = item.BetListing.PostedbyId,
                            UserEmailToPay = string.IsNullOrWhiteSpace(item.BetListing.Postedby.PaypalAccountEmail) ? item.BetListing.Postedby.Email : item.BetListing.Postedby.PaypalAccountEmail,
                            ValueToPay = item.Price - ((model.Order.ServiceFeePercent * item.Price) / 100),
                            PaypalConnected = !string.IsNullOrWhiteSpace(item.BetListing.Postedby.PaypalAccountEmail)
                        };
                        if (!item.PayoutId.HasValue)
                        {
                            model.Payouts.Add(itemToPay);
                        }
                    }
                    model.Payouts = model.Payouts.GroupBy(g => g.UserToPayId).Select(x => new OrderItemPayoutDto { UserToPayId = x.Key, UserToPay = x.First().UserToPay, ValueToPay = x.Sum(y => y.ValueToPay), PaypalConnected = x.First().PaypalConnected, UserEmailToPay = x.First().UserEmailToPay }).OrderBy(o => o.UserToPayId).ToList();
                    if (model.Payouts.Count > 0)
                    {
                        model.PayoutsJson = JsonConvert.SerializeObject(model.Payouts);
                    }
                }

                return View(model);
            }
            catch (Exception e)
            {
                await errorLogService.InsertException(e, UserId).ConfigureAwait(true);
                return RedirectToAction("Index", "Error", request);
            }
        }




        [HttpPost]
        public async Task<IActionResult> MakeOrderIndividualPaypalPayout(string PaypalEmailToSend, decimal AmountToBeSend, string stringifiedFullObj, Guid UserId, Guid OrderId)
        {
            RequestFeedback request = new RequestFeedback();
            try
            {

                if (string.IsNullOrWhiteSpace(PaypalEmailToSend)) throw new Exception("No email to send!");
                if (OrderId == Guid.Empty) throw new Exception("No order to update!");

                if (AmountToBeSend <= 0.00m) throw new Exception("No valid amount to send!");
                string BatchStamp = DateTime.UtcNow.Year.ToString()
                    + DateTime.UtcNow.Month.ToString()
                    + DateTime.UtcNow.Day.ToString()
                    + DateTime.UtcNow.Hour.ToString()
                    + DateTime.UtcNow.Minute.ToString()
                    + DateTime.UtcNow.Second.ToString()
                    + DateTime.UtcNow.Millisecond.ToString();

                PaypalSettings paypalSettings = await paypalSettingsService.GetDefault().ConfigureAwait(true);
                string ClientId = paypalSettings.ClientID;
                string Secret = paypalSettings.Secret;


                var requestPaypal = await RequestPayPalToken(ClientId, Secret);
                if (requestPaypal == null) throw new Exception("No token obtained");
                string paypalToken = requestPaypal.access_token;

                Guid? payoutRequestSuccess = await RequestPayPalPayoutWithGuid(paypalToken, stringifiedFullObj, UserId, AmountToBeSend);

                if (payoutRequestSuccess.HasValue && payoutRequestSuccess.Value != Guid.Empty)
                {
                    bool updateSuccess = await orderService.UpdateOrderItemPayouts(OrderId, UserId, payoutRequestSuccess.Value).ConfigureAwait(true);
                    if (!updateSuccess) throw new Exception("Error updating order after successfull payout.");
                    return Json(new { status = true, messageTitle = "Success", message = "Individual payout finished successfully!" });
                }
                else throw new Exception("Insuficient funds!");
            }
            catch (Exception e)
            {
                ModelState.AddModelError(string.Empty, "Something went wrong, please try again later");
                await errorLogService.InsertException(e).ConfigureAwait(true);
                return Json(new { status = false, messageTitle = "Error", message = e.Message });
            }
        }



        [HttpPost]
        public async Task<IActionResult> MakeOrderBatchPaypalPayout(string stringifiedFullObj, Guid OrderId)
        {
            RequestFeedback request = new RequestFeedback();
            try
            {

                if (OrderId == Guid.Empty) throw new Exception("No order to update!");

                string BatchStamp = DateTime.UtcNow.Year.ToString()
                    + DateTime.UtcNow.Month.ToString()
                    + DateTime.UtcNow.Day.ToString()
                    + DateTime.UtcNow.Hour.ToString()
                    + DateTime.UtcNow.Minute.ToString()
                    + DateTime.UtcNow.Second.ToString()
                    + DateTime.UtcNow.Millisecond.ToString();

                PaypalSettings paypalSettings = await paypalSettingsService.GetDefault().ConfigureAwait(true);
                string ClientId = paypalSettings.ClientID;
                string Secret = paypalSettings.Secret;


                var requestPaypal = await RequestPayPalToken(ClientId, Secret);
                if (requestPaypal == null) throw new Exception("No token obtained");
                string paypalToken = requestPaypal.access_token;

                bool payoutRequestSuccess = await RequestPayPalBatchPayout(paypalToken, stringifiedFullObj, OrderId);

                if (payoutRequestSuccess) return Json(new { status = true, messageTitle = "Success", message = "Batch payout finished successfully!" });

                else throw new Exception("Insuficient funds!");
            }
            catch (Exception e)
            {
                ModelState.AddModelError(string.Empty, "Something went wrong, please try again later");
                await errorLogService.InsertException(e).ConfigureAwait(true);
                return Json(new { status = false, messageTitle = "Error", message = e.Message });
            }
        }
        #endregion




        #region Batch Actions


        [HttpPost]
        public async Task<IActionResult> DeleteCategoriesBatch(int[] Ids)
        {
            try
            {
                if (Ids == null || Ids.Count() <= 0) throw new Exception("No items to delete");
                else
                {
                    var result = await listingCategoryService.DeleteBatch(Ids).ConfigureAwait(true);
                    if (result)
                    {
                        string message = "Items deleted successfully!";
                        return Json(new { status = true, messageTitle = "Success", message = message });
                    }
                    else throw new Exception("Error deleting the items. Try again later.");
                }
            }
            catch (Exception e)
            {
                ModelState.AddModelError(string.Empty, "Something went wrong, please try again later");
                await errorLogService.InsertException(e).ConfigureAwait(true);
                return Json(new { status = false, messageTitle = "Error", message = e.Message });
            }
        }
        

        [HttpPost]
        public async Task<IActionResult> DeleteSubcategoriesBatch(int[] Ids)
        {
            try
            {
                if (Ids == null || Ids.Count() <= 0) throw new Exception("No items to delete");
                else
                {
                    var result = await listingSubCategoryService.DeleteBatch(Ids).ConfigureAwait(true);
                    if (result)
                    {
                        string message = "Items deleted successfully!";
                        return Json(new { status = true, messageTitle = "Success", message = message });
                    }
                    else throw new Exception("Error deleting the items. Try again later.");
                }
            }
            catch (Exception e)
            {
                ModelState.AddModelError(string.Empty, "Something went wrong, please try again later");
                await errorLogService.InsertException(e).ConfigureAwait(true);
                return Json(new { status = false, messageTitle = "Error", message = e.Message });
            }
        }
        
        [HttpPost]
        public async Task<IActionResult> DeletePicksBatch(int[] Ids)
        {
            try
            {
                if (Ids == null || Ids.Count() <= 0) throw new Exception("No items to delete");
                else
                {
                    var result = await pickService.DeleteBatch(Ids).ConfigureAwait(true);
                    if (result)
                    {
                        string message = "Items deleted successfully!";
                        return Json(new { status = true, messageTitle = "Success", message = message });
                    }
                    else throw new Exception("Error deleting the items. Try again later.");
                }
            }
            catch (Exception e)
            {
                ModelState.AddModelError(string.Empty, "Something went wrong, please try again later");
                await errorLogService.InsertException(e).ConfigureAwait(true);
                return Json(new { status = false, messageTitle = "Error", message = e.Message });
            }
        }

        
        [HttpPost]
        public async Task<IActionResult> DeleteTipsBatch(int[] Ids)
        {
            try
            {
                if (Ids == null || Ids.Count() <= 0) throw new Exception("No items to delete");
                else
                {
                    var result = await tipService.DeleteBatch(Ids).ConfigureAwait(true);
                    if (result)
                    {
                        string message = "Items deleted successfully!";
                        return Json(new { status = true, messageTitle = "Success", message = message });
                    }
                    else throw new Exception("Error deleting the items. Try again later.");
                }
            }
            catch (Exception e)
            {
                ModelState.AddModelError(string.Empty, "Something went wrong, please try again later");
                await errorLogService.InsertException(e).ConfigureAwait(true);
                return Json(new { status = false, messageTitle = "Error", message = e.Message });
            }
        }
        
        [HttpPost]
        public async Task<IActionResult> DeleteMarketsBatch(int[] Ids)
        {
            try
            {
                if (Ids == null || Ids.Count() <= 0) throw new Exception("No items to delete");
                else
                {
                    var result = await marketService.DeleteBatch(Ids).ConfigureAwait(true);
                    if (result)
                    {
                        string message = "Items deleted successfully!";
                        return Json(new { status = true, messageTitle = "Success", message = message });
                    }
                    else throw new Exception("Error deleting the items. Try again later.");
                }
            }
            catch (Exception e)
            {
                ModelState.AddModelError(string.Empty, "Something went wrong, please try again later");
                await errorLogService.InsertException(e).ConfigureAwait(true);
                return Json(new { status = false, messageTitle = "Error", message = e.Message });
            }
        }


        
        [HttpPost]
        public async Task<IActionResult> DeleteBookmakersBatch(int[] Ids)
        {
            try
            {
                if (Ids == null || Ids.Count() <= 0) throw new Exception("No items to delete");
                else
                {
                    var result = await bookmakerService.DeleteBatch(Ids).ConfigureAwait(true);
                    if (result)
                    {
                        string message = "Items deleted successfully!";
                        return Json(new { status = true, messageTitle = "Success", message = message });
                    }
                    else throw new Exception("Error deleting the items. Try again later.");
                }
            }
            catch (Exception e)
            {
                ModelState.AddModelError(string.Empty, "Something went wrong, please try again later");
                await errorLogService.InsertException(e).ConfigureAwait(true);
                return Json(new { status = false, messageTitle = "Error", message = e.Message });
            }
        }
        
        [HttpPost]
        public async Task<IActionResult> DeleteCurrencyBatch(Guid[] Ids)
        {
            try
            {
                if (Ids == null || Ids.Count() <= 0) throw new Exception("No items to delete");
                else
                {
                    var result = await currencyService.DeleteBatch(Ids).ConfigureAwait(true);
                    if (result)
                    {
                        string message = "Items deleted successfully!";
                        return Json(new { status = true, messageTitle = "Success", message = message });
                    }
                    else throw new Exception("Error deleting the items. Try again later.");
                }
            }
            catch (Exception e)
            {
                ModelState.AddModelError(string.Empty, "Something went wrong, please try again later");
                await errorLogService.InsertException(e).ConfigureAwait(true);
                return Json(new { status = false, messageTitle = "Error", message = e.Message });
            }
        }
        
        
        [HttpPost]
        public async Task<IActionResult> DeleteUsersBatch(Guid[] Ids)
        {
            try
            {
                if (Ids == null || Ids.Count() <= 0) throw new Exception("No items to delete");
                else
                {
                    var result = await userService.DeleteBatch(Ids).ConfigureAwait(true);
                    if (result)
                    {
                        string message = "Items deleted successfully!";
                        return Json(new { status = true, messageTitle = "Success", message = message });
                    }
                    else throw new Exception("Error deleting the items. Try again later.");
                }
            }
            catch (Exception e)
            {
                ModelState.AddModelError(string.Empty, "Something went wrong, please try again later");
                await errorLogService.InsertException(e).ConfigureAwait(true);
                return Json(new { status = false, messageTitle = "Error", message = e.Message });
            }
        }
        
        [HttpPost]
        public async Task<IActionResult> SwitchUsersStatusBatch(Guid[] Ids, bool Status)
        {
            try
            {
                if (Ids == null || Ids.Count() <= 0) throw new Exception("No items to delete");
                else
                {
                    var result = await userService.SwitchStatusBatch(Ids, Status).ConfigureAwait(true);
                    if (result)
                    {
                        string message = "Items updated successfully!";
                        return Json(new { status = true, messageTitle = "Success", message = message });
                    }
                    else throw new Exception("Error updating the items. Try again later.");
                }
            }
            catch (Exception e)
            {
                ModelState.AddModelError(string.Empty, "Something went wrong, please try again later");
                await errorLogService.InsertException(e).ConfigureAwait(true);
                return Json(new { status = false, messageTitle = "Error", message = e.Message });
            }
        }



        [HttpPost]
        public async Task<IActionResult> UpdateUserRenewSubscriptionBatch(Guid[] Ids, bool Status)
        {
            try
            {

                if (Ids == null || Ids.Count() <= 0) throw new Exception("No items to delete");
                else
                {
                    var result = await userService.SwitchSubscriptionStatusBatch(Ids, Status).ConfigureAwait(true);
                    if (result)
                    {
                        string message = "Items updated successfully!";
                        return Json(new { status = true, messageTitle = "Success", message = message });
                    }
                    else throw new Exception("Error updating the items. Try again later.");
                }
            }
            catch (Exception e)
            {
                ModelState.AddModelError(string.Empty, "Something went wrong, please try again later");
                await errorLogService.InsertException(e).ConfigureAwait(true);
                return Json(new { status = false, messageTitle = "Error", message = e.Message });
            }
        }


        #endregion
    }
}