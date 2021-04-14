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
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Net;
using System.IO;
using System.Text;
using ServiceStack.Text;
using Newtonsoft.Json;
using ServiceStack;
using FixerIoCore;
using Microsoft.AspNetCore.Http;
//1. Import the PayPal SDK client that was created in `Set up Server-Side SDK`.
//using BraintreeHttp;
using PayPalCheckoutSdk.Core;
using PayPalCheckoutSdk.Orders;
using Google.Contacts;
using Google.GData.Contacts;
using Google.GData.Client;
using Google.GData.Extensions;
using Rotativa;
using Rotativa.AspNetCore;
using Microsoft.Identity.Client;
using System.Net.Http;
using Newtonsoft.Json.Linq;
using System.Net.Http.Headers;

namespace Shortchase.Controllers
{
    public class HomeController : Controller
    {
        private readonly IErrorLogService errorLogService;
        private readonly IUserService userService;
        private readonly IPermissionService permissionService;
        private readonly IEmailSenderService emailSenderService;
        private readonly ICountryService countryService;
        private readonly IRewardsMappingService rewardsMappingService;
        private readonly IRewardsClaimedMappingService rewardsClaimedMappingService;
        private readonly ISMSSenderService smsSenderService;
        private readonly ISemiStaticTextService semiStaticTextService;
        private readonly IBlogPostsService blogPostsService;
        private readonly IFAQItemService FAQItemService;
        private readonly INewsPostService newsPostService;
        private readonly IPromotionPostService promotionPostService;
        private readonly IWebHostEnvironment hostingEnvironment;
        private readonly IPOTDListingService potdListingService;
        private readonly IPOTDListingPredictionService potdListingPredictionService;
        private readonly IPOTDListingLiveReportService potdListingLiveReportService;
        private readonly IPOTDListingLiveReportingInteractionService potdListingLiveReportingInteractionService;
        private readonly IBetListingService betListingService;
        private readonly IListingCategoryService listingCategoryService;
        private readonly IListingSubCategoryService listingSubCategoryService;
        private readonly IUserSubscriptionService userSubscriptionService;
        private readonly ICurrencyService currencyService;
        private readonly IBetListingReportService betListingReportService;
        private readonly IUserFollowService userFollowService;
        private readonly IUserRatingService userRatingService;
        private readonly IMarketService marketService;
        private readonly IPickService pickService;
        private readonly ITipService tipService;
        private readonly IBookmakerService bookmakerService;
        private readonly IShoppingCartService shoppingCartService;
        private readonly ISystemConstantsService systemConstantsService;
        private readonly IOrderItemService orderItemService;
        private readonly IOrderService orderService;
        private readonly INotificationService notificationService;
        private readonly IMessageService messageService;
        private readonly ISubscriptionPlanService subscriptionPlanService;
        private readonly IUserDiscountService userDiscountService;
        private readonly IPaypalSettingsService paypalSettingsService;
        private readonly IAPIValidationService apiValidationService;
        private readonly IAddressService addressService;
        private readonly IUserContactService userContactService;

        public HomeController
        (
            IErrorLogService errorLogService,
            IUserService userService,
            IPermissionService permissionService,
            IEmailSenderService emailSenderService,
            ICountryService countryService,
            IRewardsMappingService rewardsMappingService,
            IRewardsClaimedMappingService rewardsClaimedMappingService,
            ISMSSenderService smsSenderService,
            ISemiStaticTextService semiStaticTextService,
            IBlogPostsService blogPostsService,
            IFAQItemService FAQItemService,
            INewsPostService newsPostService,
            IPromotionPostService promotionPostService,
            IWebHostEnvironment hostingEnvironment,
            IPOTDListingService potdListingService,
            IPOTDListingPredictionService potdListingPredictionService,
            IPOTDListingLiveReportService potdListingLiveReportService,
            IPOTDListingLiveReportingInteractionService potdListingLiveReportingInteractionService,
            IBetListingService betListingService,
            IListingCategoryService listingCategoryService,
            IListingSubCategoryService listingSubCategoryService,
            IUserSubscriptionService userSubscriptionService,
            ICurrencyService currencyService,
            IBetListingReportService betListingReportService,
            IUserFollowService userFollowService,
            IUserRatingService userRatingService,
            IMarketService marketService,
            IPickService pickService,
            ITipService tipService,
            IBookmakerService bookmakerService,
            IShoppingCartService shoppingCartService,
            ISystemConstantsService systemConstantsService,
            IOrderItemService orderItemService,
            IOrderService orderService,
            INotificationService notificationService,
            IMessageService messageService,
            ISubscriptionPlanService subscriptionPlanService,
            IUserDiscountService userDiscountService,
            IPaypalSettingsService paypalSettingsService,
            IAPIValidationService apiValidationService,
            IUserContactService userContactService,
            IAddressService addressService
        )
        {
            this.errorLogService = errorLogService;
            this.userService = userService;
            this.permissionService = permissionService;
            this.emailSenderService = emailSenderService;
            this.countryService = countryService;
            this.rewardsMappingService = rewardsMappingService;
            this.rewardsClaimedMappingService = rewardsClaimedMappingService;
            this.smsSenderService = smsSenderService;
            this.semiStaticTextService = semiStaticTextService;
            this.blogPostsService = blogPostsService;
            this.FAQItemService = FAQItemService;
            this.newsPostService = newsPostService;
            this.promotionPostService = promotionPostService;
            this.hostingEnvironment = hostingEnvironment;
            this.potdListingService = potdListingService;
            this.potdListingPredictionService = potdListingPredictionService;
            this.potdListingLiveReportService = potdListingLiveReportService;
            this.potdListingLiveReportingInteractionService = potdListingLiveReportingInteractionService;
            this.betListingService = betListingService;
            this.listingCategoryService = listingCategoryService;
            this.listingSubCategoryService = listingSubCategoryService;
            this.userSubscriptionService = userSubscriptionService;
            this.currencyService = currencyService;
            this.betListingReportService = betListingReportService;
            this.userFollowService = userFollowService;
            this.userRatingService = userRatingService;
            this.marketService = marketService;
            this.pickService = pickService;
            this.tipService = tipService;
            this.bookmakerService = bookmakerService;
            this.shoppingCartService = shoppingCartService;
            this.systemConstantsService = systemConstantsService;
            this.orderItemService = orderItemService;
            this.orderService = orderService;
            this.notificationService = notificationService;
            this.messageService = messageService;
            this.subscriptionPlanService = subscriptionPlanService;
            this.userDiscountService = userDiscountService;
            this.paypalSettingsService = paypalSettingsService;
            this.apiValidationService = apiValidationService;
            this.userContactService = userContactService;
            this.addressService = addressService;
        }
        #region Website Pages



        #region Homepage
        [AllowAnonymous]
        public async Task<IActionResult> Index(int TimeOffset = 0)
        {

            ViewData["root"] = hostingEnvironment.ContentRootPath;
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
        [AllowAnonymous]
        public async Task<IActionResult> IndexPartTrending(int TimeOffset = 0)
        {

            ViewData["root"] = hostingEnvironment.ContentRootPath;
            ViewData["TimezoneOffset"] = TimeOffset;
            RequestFeedback request = new RequestFeedback();
            try
            {
                var posts = (await newsPostService.GetAllForWebsite().ConfigureAwait(true)).OrderByDescending(o => o.DatePublished).Take(5).ToList();
                return PartialView(posts);
            }
            catch (Exception e)
            {
                await errorLogService.InsertException(e).ConfigureAwait(true);
                return null;
            }
        }
        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> CommentOnPrediction(PredictionComment comment)
        {
            comment.CommenterId = User.Id();
            var result = await potdListingPredictionService.InsertComment(comment).ConfigureAwait(true);
            if (result)
            {
                return Json(new { isSuccess = result, redirectUrl = "/Home/Index/" });
            }
            return Json(new { isSuccess = result, redirectUrl = "/Home/Index/" });
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> IndexPartPOTD(int TimeOffset = 0)
        {

            ViewData["root"] = hostingEnvironment.ContentRootPath;
            ViewData["TimezoneOffset"] = TimeOffset;
            RequestFeedback request = new RequestFeedback();
            try
            {
                var data = new List<PotdPredictionVM>();
                var POTDs = await potdListingService.GetAllAvailable().ConfigureAwait(true);
                var POTDListing = await potdListingPredictionService.GetAll().ConfigureAwait(true);
                foreach (var item in POTDListing)
                {
                    var pick = new Pick();
                    var p = POTDs.FirstOrDefault(x => x.Id == item.POTDId);
                    if (p == null)
                    {
                        pick = new Pick();
                    }
                    else
                    {
                        pick = p.Pick;
                    }
                    var user =await potdListingService.GetUserById(item.PredictedById);
                    data.Add(new PotdPredictionVM {Id=item.Id, Name = user, DatePredicted = item.DatePredicted, Prediction =item.Prediction, Rowdate= item.RowDate, POTDID= item.POTDId, Picks= pick });
                }
                ViewData["Prediction"] = data.ToList(); 

                return PartialView(POTDs);
            }
            catch (Exception e)
            {
                await errorLogService.InsertException(e).ConfigureAwait(true);
                return null;
            }
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> SubmitPOTDforValidation(bool Type)
        {
            Guid? userId = User.Id();
            POTDListingPrediction request = new POTDListingPrediction
            {
                PredictedById = userId.Value,
                UserValidate = Type
            };
            try
            {
                if (!userId.HasValue || userId.Value == Guid.Empty) throw new Exception("No User Id prediction to validate");
                var POTDs = await potdListingPredictionService.Insert(request).ConfigureAwait(true);
                if (POTDs)
                {
                    return Json(new { status = true, messageTitle = "Success", message = "Prediction submitted successfully" });
                }

                else throw new Exception("Error submitting the POTD prediction. Try again later.");
            }
            catch (Exception e)
            {
                await errorLogService.InsertException(e).ConfigureAwait(true);
                return null;
            }
        }

        #endregion

        #region
        [AllowAnonymous]
        public IActionResult TopRankingCategories()
        {
            return View();
        }
        # endregion

        #region User Validation


        [AllowAnonymous]
        public async Task<IActionResult> ValidatePhone(string q)
        {
            Guid? UserId = null;
            RequestFeedback request = new RequestFeedback();
            try
            {
                UserId = User.Id();
                string Email = q;
                if (!string.IsNullOrWhiteSpace(Email))
                {
                    return View("ValidatePhone", Email);
                }
                else
                {
                    return RedirectToAction("Index", "Error");
                }

            }
            catch (Exception e)
            {
                await errorLogService.InsertException(e, UserId).ConfigureAwait(true);
                return RedirectToAction("Index", "Error", request);
            }
        }

        [AllowAnonymous]
        public async Task<IActionResult> ConfirmEmail(string q)
        {
            Guid? UserId = null;
            RequestFeedback request = new RequestFeedback();
            try
            {
                UserId = User.Id();
                if (string.IsNullOrWhiteSpace(q))
                {
                    return RedirectToAction("Index");
                }
                else
                {
                    var user = await userService.GetConfirmationAccount(q).ConfigureAwait(true);
                    if (user != null)
                    {
                        var result = await userService.ConfirmAccountAsync(user).ConfigureAwait(true);
                        if (result)
                        {
                            var authenticationResult = await userService.AuthenticateWithouPasswordAsync(user, true).ConfigureAwait(true);
                            if (authenticationResult)
                            {
                                var emailResult = await emailSenderService.SendEmailConfirmed(user.Email, user.FirstName).ConfigureAwait(true);
                                return View();
                            }
                            else
                            {
                                return RedirectToAction("Index");
                            }
                        }
                        else
                        {
                            return RedirectToAction("Index");
                        }
                    }
                    else
                    {
                        return RedirectToAction("Index");
                    }
                }
            }
            catch (Exception e)
            {
                await errorLogService.InsertException(e, UserId).ConfigureAwait(true);
                return RedirectToAction("Index", "Error", request);
            }
        }



        [AllowAnonymous]
        public async Task<IActionResult> ReferralRegistration(string q)
        {
            Guid? UserId = null;
            RequestFeedback request = new RequestFeedback();
            try
            {
                UserId = User.Id();
                if (string.IsNullOrWhiteSpace(q) || UserId.HasValue)
                {
                    return RedirectToAction("Index");
                }
                else
                {
                    var user = await userService.GetUserByReferralCode(q).ConfigureAwait(true);
                    if (user == null)
                    {
                        return RedirectToAction("Index");
                    }
                    else
                    {
                        ReferralRegistrationDto model = new ReferralRegistrationDto
                        {
                            Email = user.Email.ToLower()
                        };
                        model.CountryOptions = await countryService.GetAll().ConfigureAwait(true);
                        model.CountryOptions = model.CountryOptions.OrderBy(o => o.Name).ToList();
                        return View(model);
                    }
                }
            }
            catch (Exception e)
            {
                await errorLogService.InsertException(e, UserId).ConfigureAwait(true);
                return RedirectToAction("Index", "Error", request);
            }
        }
        #endregion

        #region About

        [AllowAnonymous]
        public async Task<IActionResult> About()
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

        #endregion

        #region Privacy Policy

        [AllowAnonymous]
        public async Task<IActionResult> PrivacyPolicy()
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
        #endregion

        #region Terms And Conditions

        [AllowAnonymous]
        public async Task<IActionResult> TermsAndConditions()
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
        #endregion

        #region Advertising
        [AllowAnonymous]
        public async Task<IActionResult> Advertising()
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
        #endregion

        #region News
        [AllowAnonymous]
        public async Task<IActionResult> NewsList(int TimeOffset = 0, int page = 1, int pageSize = 5)
        {
            Guid? UserId = null;
            RequestFeedback request = new RequestFeedback();
            ViewData["TimezoneOffset"] = TimeOffset;
            try
            {
                var posts = (await newsPostService.GetAllForWebsite().ConfigureAwait(true)).OrderByDescending(o => o.DatePublished).ToList();
                ViewData["Page"] = page;
                ViewData["PageSize"] = pageSize;
                ViewData["PageNumber"] = Utility.PageSize(posts.Count, pageSize);
                UserId = User.Id();
                ViewData["root"] = hostingEnvironment.ContentRootPath;

                posts = posts.Page(page, pageSize).ToList();
                return View(posts);
            }
            catch (Exception e)
            {
                await errorLogService.InsertException(e, UserId).ConfigureAwait(true);
                return RedirectToAction("Index", "Error", request);
            }
        }

        [AllowAnonymous]
        public async Task<IActionResult> News(string Slug, int TimeOffset = 0)
        {
            try
            {
                ViewData["TimezoneOffset"] = TimeOffset;
                ViewData["root"] = hostingEnvironment.ContentRootPath;
                if (string.IsNullOrWhiteSpace(Slug)) return RedirectToAction("NewsList");
                else
                {
                    NewsPost post = await newsPostService.GetBySlug(Slug).ConfigureAwait(true);
                    if (post == null) return RedirectToAction("NewsList");
                    else
                    {
                        if (post.IsPublished) return View(post);
                        else
                        {
                            if (post.DatePublished.Value.ToLocalTime() <= DateTime.UtcNow.ToLocalTime())
                            {
                                post.IsPublished = true;
                                var updateResult = await newsPostService.Update(post).ConfigureAwait(true);
                                if (updateResult) return View(post);
                                else return RedirectToAction("Index", "Error");
                            }
                            else return RedirectToAction("NewsList");
                        }
                    }
                }
            }
            catch (Exception e)
            {

                await errorLogService.InsertException(e).ConfigureAwait(true);
                return RedirectToAction("NewsList");
            }
        }
        #endregion

        #region Blog
        [AllowAnonymous]
        public async Task<IActionResult> BlogList(int TimeOffset = 0, int page = 1, int pageSize = 5)
        {
            Guid? UserId = null;
            RequestFeedback request = new RequestFeedback();
            try
            {
                var posts = (await blogPostsService.GetAllForWebsite().ConfigureAwait(true)).OrderByDescending(o => o.DatePublished).ToList();
                ViewData["Page"] = page;
                ViewData["PageSize"] = pageSize;
                ViewData["PageNumber"] = Utility.PageSize(posts.Count, pageSize);
                ViewData["TimezoneOffset"] = TimeOffset;
                UserId = User.Id();
                ViewData["root"] = hostingEnvironment.ContentRootPath;
                posts = posts.Page(page, pageSize).ToList();
                return View(posts);
            }
            catch (Exception e)
            {
                await errorLogService.InsertException(e, UserId).ConfigureAwait(true);
                return RedirectToAction("Index", "Error", request);
            }
        }

        [AllowAnonymous]
        public async Task<IActionResult> Blog(string Slug, int TimeOffset = 0)
        {
            try
            {
                ViewData["TimezoneOffset"] = TimeOffset;
                ViewData["root"] = hostingEnvironment.ContentRootPath;
                if (string.IsNullOrWhiteSpace(Slug)) return RedirectToAction("BlogList");
                else
                {
                    BlogPost post = await blogPostsService.GetBySlug(Slug).ConfigureAwait(true);
                    if (post == null) return RedirectToAction("BlogList");
                    else
                    {
                        if (post.IsPublished) return View(post);
                        else
                        {
                            if (post.DatePublished.Value.ToLocalTime() <= DateTime.UtcNow.ToLocalTime())
                            {
                                post.IsPublished = true;
                                var updateResult = await blogPostsService.Update(post).ConfigureAwait(true);
                                if (updateResult) return View(post);
                                else return RedirectToAction("Index", "Error");
                            }
                            else return RedirectToAction("BlogList");
                        }
                    }
                }
            }
            catch (Exception e)
            {

                await errorLogService.InsertException(e).ConfigureAwait(true);
                return RedirectToAction("BlogList");
            }
        }
        #endregion

        #region Promotions
        [AllowAnonymous]
        public async Task<IActionResult> PromotionsList(int TimeOffset = 0, int page = 1, int pageSize = 5)
        {
            Guid? UserId = null;
            RequestFeedback request = new RequestFeedback();
            try
            {
                var posts = (await promotionPostService.GetAllForWebsite().ConfigureAwait(true)).OrderByDescending(o => o.DatePublished).ToList();
                ViewData["Page"] = page;
                ViewData["PageSize"] = pageSize;
                ViewData["PageNumber"] = Utility.PageSize(posts.Count, pageSize);
                ViewData["TimezoneOffset"] = TimeOffset;
                UserId = User.Id();
                ViewData["root"] = hostingEnvironment.ContentRootPath;
                posts = posts.Page(page, pageSize).ToList();
                return View(posts);
            }
            catch (Exception e)
            {
                await errorLogService.InsertException(e, UserId).ConfigureAwait(true);
                return RedirectToAction("Index", "Error", request);
            }
        }

        [AllowAnonymous]
        public async Task<IActionResult> Promotions(string Slug, int TimeOffset = 0)
        {
            try
            {
                ViewData["TimezoneOffset"] = TimeOffset;
                ViewData["root"] = hostingEnvironment.ContentRootPath;
                if (string.IsNullOrWhiteSpace(Slug)) return RedirectToAction("PromotionsList");
                else
                {
                    PromotionPost post = await promotionPostService.GetBySlug(Slug).ConfigureAwait(true);
                    if (post == null) return RedirectToAction("PromotionsList");
                    else
                    {
                        if (post.IsPublished) return View(post);
                        else
                        {
                            if (post.DatePublished.Value.ToLocalTime() <= DateTime.UtcNow.ToLocalTime())
                            {
                                post.IsPublished = true;
                                var updateResult = await promotionPostService.Update(post).ConfigureAwait(true);
                                if (updateResult) return View(post);
                                else return RedirectToAction("Index", "Error");
                            }
                            else return RedirectToAction("PromotionsList");
                        }
                    }
                }
            }
            catch (Exception e)
            {
                await errorLogService.InsertException(e).ConfigureAwait(true);
                return RedirectToAction("PromotionsList");
            }
        }
        #endregion

        #region FAQ

        [AllowAnonymous]
        public async Task<IActionResult> FAQ()
        {
            Guid? UserId = null;
            RequestFeedback request = new RequestFeedback();
            try
            {
                UserId = User.Id();
                var allFaqItems = await FAQItemService.GetAll(true).ConfigureAwait(true);
                FAQWebsiteListDto model = new FAQWebsiteListDto
                {
                    BettorItems = allFaqItems.Where(i => i.Type == FAQType.Bettor).OrderBy(o => o.Question).ToList(),
                    CapperItems = allFaqItems.Where(i => i.Type == FAQType.Capper).OrderBy(o => o.Question).ToList(),
                    GeneralItems = allFaqItems.Where(i => i.Type == FAQType.General).OrderBy(o => o.Question).ToList(),
                };
                return View(model);
            }
            catch (Exception e)
            {
                await errorLogService.InsertException(e, UserId).ConfigureAwait(true);
                return RedirectToAction("Index", "Error", request);
            }
        }

        #endregion

        #region Marketplace/Listings
        [AllowAnonymous]
        public async Task<IActionResult> Marketplace(int TimeOffset = 0)
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

        [AllowAnonymous]
        public async Task<IActionResult> ListYourPick(int TimeOffset = 0)
        {
            ViewData["TimezoneOffset"] = TimeOffset;
            Guid? UserId = null;
            RequestFeedback request = new RequestFeedback();
            try
            {
                UserId = User.Id();
                WebsiteBetListingListYourPickDto model = new WebsiteBetListingListYourPickDto
                {
                    BetListingsTypesOptions = new List<SelectListItem>(),
                    BetListingsOddFormatOptions = new List<SelectListItem>(),
                    CategoriesOptions = (await listingCategoryService.GetAll(true).ConfigureAwait(true)).OrderBy(o => o.Name).ToList(),
                    BookmakerOptions = (await bookmakerService.GetAll(true).ConfigureAwait(true)).Select(i => new SelectListItem { Value = i.Id.ToString(), Text = i.Description }).OrderBy(o => o.Text).ToList()
                };

                UserSubscription activeProPlan = null;
                if (UserId.HasValue)
                {
                    activeProPlan = await userSubscriptionService.GetActiveSubscriptionPlan(UserId.Value, SubscriptionPlanType.ShortchasePro).ConfigureAwait(true); ;
                }


                model.BetListingsTypesOptions.Add(new SelectListItem { Value = BetListingType.Premium, Text = BetListingType.Premium });
                if (activeProPlan != null)
                {
                    model.BetListingsTypesOptions.Add(new SelectListItem { Value = BetListingType.Live, Text = BetListingType.Live });
                }
                else
                {
                    model.BetListingsTypesOptions.Add(new SelectListItem { Value = BetListingType.Live, Text = BetListingType.Live, Disabled = true });
                }
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

        [Permitted(Permission.Capper)]
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
                    dateEnd = dateEnd.AddMinutes(newListingInfo.TimezoneOffset);*/


                    var activeProPlan = await userSubscriptionService.GetActiveSubscriptionPlan(newListingInfo.PostedbyId, SubscriptionPlanType.ShortchasePro).ConfigureAwait(true);

                    Pick pick = await pickService.GetById(newListingInfo.Pick).ConfigureAwait(true);

                    if (pick == null) throw new Exception("To post a Live Pick you must select a pick.");

                    if (newListingInfo.PickType == BetListingType.Live)
                    {

                        if (activeProPlan == null) throw new Exception("To post a Live Pick you must be a ShortchasePro user.");



                        if (
                            pick.StartTime.FromUTCData(newListingInfo.TimezoneOffset) > DateTime.UtcNow.FromUTCData(newListingInfo.TimezoneOffset)
                            || pick.FinishTime.FromUTCData(newListingInfo.TimezoneOffset) < DateTime.UtcNow.FromUTCData(newListingInfo.TimezoneOffset)
                        )
                        {
                            throw new Exception("Live pick is permitted only between its Start Time and Finish Time");
                        }



                    }
                    else
                    {

                        if (
                            DateTime.UtcNow.FromUTCData(newListingInfo.TimezoneOffset) >= pick.StartTime.FromUTCData(newListingInfo.TimezoneOffset)
                        )
                        {
                            throw new Exception("Premium or Free picks are permitted only before it's Start Time");
                        }
                    }


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


                    if (activeProPlan == null) newBetListing.IsProCapperListing = false;
                    else newBetListing.IsProCapperListing = true;


                    newBetListing.IsBoisterousListing = await betListingService.IsBoisterouListing(newBetListing).ConfigureAwait(true);

                    bool isListingUnderLimits = newBetListing.IsProCapperListing ? true : await betListingService.IsListingUnderLimits(newBetListing).ConfigureAwait(true);
                    if (isListingUnderLimits)
                    {
                        var result = await betListingService.Insert(newBetListing).ConfigureAwait(true);
                        if (result)
                        {
                            var followers = await userFollowService.GetAllFollowers(newListingInfo.PostedbyId).ConfigureAwait(true);

                            User user = await userService.GetById(newListingInfo.PostedbyId).ConfigureAwait(true);
                            string message = "Capper " + user.FirstName + " just posted a new pick.";
                            if (followers != null && followers.Count > 0)
                            {
                                foreach (var follower in followers)
                                {

                                    var resultNotification = await notificationService.Insert(follower, message).ConfigureAwait(true);
                                    if (!resultNotification) throw new Exception("Could not save new notification");
                                }
                            }

                            return Json(new { status = true, messageTitle = "Success", message = "New listing saved successfully!" });
                        }
                        else throw new Exception("Error creating new listing. Try again later.");
                    }
                    else
                    {
                        string message = "You’ve reached your bet listing limit. Register as a Pro Capper to list unlimited bet listings.";
                        throw new Exception(message);
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



        [Permitted(Permission.Capper, Permission.Bettor)]
        public async Task<IActionResult> Listings(int TimeOffset = 0, int page = 1, int pageSize = 20)
        {
            ViewData["root"] = hostingEnvironment.ContentRootPath;
            ViewData["TimezoneOffset"] = TimeOffset;
            Guid? UserId = null;
            RequestFeedback request = new RequestFeedback();
            try
            {
                UserId = User.Id();

                var betListings = (await betListingService.GetAllListingsByUser(UserId.Value).ConfigureAwait(true)).OrderByDescending(o => o.RowDate).ToList();
                ViewData["Page"] = page;
                ViewData["PageSize"] = pageSize;
                ViewData["PageNumber"] = Utility.PageSize(betListings.Count, pageSize);
                betListings = betListings.Page(page, pageSize).ToList();
                return View(betListings);
            }
            catch (Exception e)
            {
                await errorLogService.InsertException(e, UserId).ConfigureAwait(true);
                return RedirectToAction("Index", "Error", request);
            }
        }

        [AllowAnonymous]
        public async Task<IActionResult> SearchResults(SearchFilters filters)
        {
            ViewData["TimezoneOffset"] = filters.TimeOffset;
            ViewData["root"] = hostingEnvironment.ContentRootPath;
            filters.page = filters.page == 0 ? 1 : filters.page;
            ViewData["Page"] = filters.page;
            filters.pageSize = filters.pageSize == 0 ? 20 : filters.pageSize;
            ViewData["PageSize"] = filters.pageSize;
            Guid? UserId = null;
            RequestFeedback request = new RequestFeedback();
            try
            {
                UserId = User.Id();
                SearchResults model = new SearchResults
                {
                    Listings = new List<BetListing>()
                };
                model.Filters = filters;
                model.CategoriesOptions = (await listingCategoryService.GetAll(true).ConfigureAwait(true)).Select(a => new SelectListItem { Value = a.Id.ToString(), Text = a.Name, Selected = filters.Category.HasValue ? a.Id == filters.Category.Value : false }).OrderBy(o => o.Text).ToList();
                model.CurrencyOptions = (await currencyService.GetAll(false).ConfigureAwait(true))
                    .Select(
                        a =>
                            new SelectListItem
                            {
                                Value = a.Acronym,
                                Text = a.Acronym,
                                Selected = !string.IsNullOrWhiteSpace(filters.SortByCurrency) ? a.Acronym == filters.SortByCurrency : false
                            }
                    )
                    .OrderBy(o => o.Text).ToList();
                if (model.Filters.Category.HasValue)
                {
                    model.SubCategoriesOptions = (await listingSubCategoryService.GetAllFromCategory(model.Filters.Category.Value).ConfigureAwait(true)).Select(a => new SelectListItem { Value = a.Id.ToString(), Text = a.Name, Selected = filters.SubCategory.HasValue ? a.Id == filters.SubCategory.Value : false }).OrderBy(o => o.Text).ToList();
                    if (model.SubCategoriesOptions != null && model.SubCategoriesOptions.Count > 0) model.HasSubCategoriesOptions = true;
                    else model.HasSubCategoriesOptions = false;
                }
                else
                {
                    model.SubCategoriesOptions = new List<SelectListItem>();
                }

                if (!string.IsNullOrWhiteSpace(filters.SortByPickType))
                {
                    model.Filters.SortByPickType = filters.SortByPickType;
                }

                model.PickTypeOptions = new List<SelectListItem>();
                model.PickTypeOptions.Add(new SelectListItem { Value = BetListingType.Premium, Text = BetListingType.Premium, Selected = !string.IsNullOrWhiteSpace(filters.SortByPickType) ? BetListingType.Premium == filters.SortByPickType : false });
                model.PickTypeOptions.Add(new SelectListItem { Value = BetListingType.Live, Text = BetListingType.Live, Selected = !string.IsNullOrWhiteSpace(filters.SortByPickType) ? BetListingType.Live == filters.SortByPickType : false });
                model.PickTypeOptions.Add(new SelectListItem { Value = BetListingType.Free, Text = BetListingType.Free, Selected = !string.IsNullOrWhiteSpace(filters.SortByPickType) ? BetListingType.Free == filters.SortByPickType : false });

                if (UserId.HasValue)
                {
                    model.IsAuth = true;
                    var activeProPlan = await userSubscriptionService.GetActiveSubscriptionPlan(UserId.Value, SubscriptionPlanType.Boisterous).ConfigureAwait(true);
                    if (activeProPlan == null) model.CanSortBy = false;
                    else model.CanSortBy = true;
                }
                else
                {
                    model.IsAuth = false;
                    model.CanSortBy = false;
                }


                /*ViewData["moneyvaltest"] = 1.52;
                if (!string.IsNullOrWhiteSpace(model.Filters.SortByCurrency)) {
                    var fixerIoClient = new FixerIoClient();
                    var symbol1 = CurrencyHelper.GetSymbolFromAcronym("CAD");
                    var symbol2 = CurrencyHelper.GetSymbolFromAcronym(model.Filters.SortByCurrency);
                    if (!symbol1.HasValue || !symbol2.HasValue) throw new Exception("Error converting currency."); 
                    ViewData["moneyvaltest2"] = fixerIoClient.Convert(symbol1.Value, symbol2.Value);
                }*/

                if
                (
                    model.Filters.Category.HasValue
                    || model.Filters.Odds.HasValue
                    || model.Filters.PriceMax.HasValue
                    || model.Filters.PriceMin.HasValue
                    || model.Filters.SubCategory.HasValue
                    || !string.IsNullOrWhiteSpace(model.Filters.Keyword)
                    || !string.IsNullOrWhiteSpace(model.Filters.SortBy)
                    || !string.IsNullOrWhiteSpace(model.Filters.SortByOdds)
                    || !string.IsNullOrWhiteSpace(model.Filters.SortByPickType)
                    || !string.IsNullOrWhiteSpace(model.Filters.SortByPrice)
                    || !string.IsNullOrWhiteSpace(model.Filters.SortByCurrency)
                )
                {
                    model.HasAnyFilter = true;
                }
                else model.HasAnyFilter = false;

                model.Listings = await betListingService.GetAllAvailableForMarketplace(model.Filters).ConfigureAwait(true);
                ViewData["TotalItems"] = model.Listings.Count;
                ViewData["PageNumber"] = Utility.PageSize(model.Listings.Count, filters.pageSize);
                model.Listings = model.Listings.Page(filters.page, filters.pageSize).ToList();
                ViewData["CurrentNumberOfItems"] = model.Listings.Count;

                if (model.Listings.Count > 0 && !string.IsNullOrWhiteSpace(model.Filters.SortByCurrency))
                {
                    decimal CADexchangeRate = GetCADExchangeRateFromAPI();
                    decimal OtherCurrencyexchangeRate = GetOtherCurrencyExchangeRateFromAPI(model.Filters.SortByCurrency);
                    foreach (var listing in model.Listings)
                    {
                        listing.Price = (listing.Price / CADexchangeRate) * OtherCurrencyexchangeRate;
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



        private decimal ExchangeRateFromAPI(string currencyToConvertTo)
        {

            try

            {

                WebClient web = new WebClient();

                string url = APILinks.CurrencyExchange.Rates;

                // Get response as string

                string response = new WebClient().DownloadString(url);

                // Convert string to number
                dynamic jsonData = JObject.Parse(response);
                JObject rates = jsonData.rates;


                decimal exchangeRate = Convert.ToDecimal(rates[currencyToConvertTo]);

                return exchangeRate;

            }

            catch (Exception ex)
            {

                return 0;

            }

        }

        private decimal GetCADExchangeRateFromAPI()
        {

            try

            {

                WebClient web = new WebClient();

                string url = APILinks.CurrencyExchange.LatestRates;

                // Get response as string

                string response = new WebClient().DownloadString(url);

                // Convert string to number
                dynamic jsonData = JObject.Parse(response);
                JObject rates = jsonData.rates;

                decimal CADrateForEUR = Convert.ToDecimal(rates["CAD"]);
                return CADrateForEUR;

            }

            catch (Exception ex)
            {

                return 0;

            }

        }
        private decimal GetOtherCurrencyExchangeRateFromAPI(string CurrencyCode)
        {

            try

            {

                WebClient web = new WebClient();

                string url = APILinks.CurrencyExchange.LatestRates;

                // Get response as string

                string response = new WebClient().DownloadString(url);

                // Convert string to number
                dynamic jsonData = JObject.Parse(response);
                JObject rates = jsonData.rates;

                decimal rateForEUR = Convert.ToDecimal(rates[CurrencyCode.ToUpper()]);
                return rateForEUR;

            }

            catch (Exception ex)
            {

                return 0;

            }

        }
        private decimal ExchangeRateFromAPI2(string currencyToConvertTo, decimal amount)
        {

            try

            {

                WebClient web = new WebClient();

                string url = APILinks.CurrencyExchange.LatestRates;

                // Get response as string

                string response = new WebClient().DownloadString(url);

                // Convert string to number
                dynamic jsonData = JObject.Parse(response);
                JObject rates = jsonData.rates;

                decimal CADrateForEUR = Convert.ToDecimal(rates["CAD"]);
                decimal currencyRateForEUR = Convert.ToDecimal(rates[currencyToConvertTo]);


                decimal convertedValue = (amount / CADrateForEUR) * currencyRateForEUR;

                return convertedValue;

            }

            catch (Exception ex)
            {

                return 0;

            }

        }

        [AllowAnonymous]
        [HttpPost]
        public async Task<IActionResult> HomeSearchBarDropdownResults(string Keyword)
        {
            Guid? UserId = null;
            RequestFeedback request = new RequestFeedback();
            DropdownResultsDto items = new DropdownResultsDto
            {
                Cappers = new List<User>(),
                Categories = new List<ListingCategory>(),
                SubCategories = new List<ListingSubCategory>(),
                CanUserSeeLivePicks = false,
                HasFreePicks = false,
                HasLivePicks = false,
                HasPremiumPicks = false
            };
            try
            {
                ICollection<BetListing> results = await betListingService.GetAllAvailableForDropdownSearch(Keyword).ConfigureAwait(true);

                if (results.Count > 0)
                {

                    items.Categories = results.Select(i => i.Category).Distinct().ToList();
                    items.SubCategories = results.Where(i => i.SubCategoryId.HasValue).Select(i => i.SubCategory).Distinct().ToList();
                    items.Cappers = results.Select(i => i.Postedby).Distinct().ToList();
                    items.HasPremiumPicks = results.Any(i => i.PickType == BetListingType.Premium);
                    items.HasFreePicks = results.Any(i => i.PickType == BetListingType.Free);
                    items.HasLivePicks = results.Any(i => i.PickType == BetListingType.Live);

                }
                return PartialView(items);
            }
            catch (Exception e)
            {
                await errorLogService.InsertException(e, UserId).ConfigureAwait(true);
                return PartialView(items);
            }
        }


        [AllowAnonymous]
        public async Task<IActionResult> ViewListing(Guid? Listing = null, int TimeOffset = 0)
        {
            ViewData["TimezoneOffset"] = TimeOffset;
            ViewData["root"] = hostingEnvironment.ContentRootPath;
            Guid? UserId = null;
            RequestFeedback request = new RequestFeedback();
            try
            {

                UserId = User.Id();
                if (!Listing.HasValue || Listing == Guid.Empty) throw new Exception("No listing provided");
                ViewListingDto model = new ViewListingDto();
                model.IsAuth = UserId.HasValue ? true : false;
                model.IsInCart = false;
                model.IsAdmin = User.Identity.HasAnyPermissions(Permission.Admin);
                model.Listing = await betListingService.GetById(Listing.Value).ConfigureAwait(true);
                model.RelatedListings = await betListingService.GetAllRelatedAvailableForMarketplace(model.Listing.PostedbyId, Listing.Value).ConfigureAwait(true);
                model.HasPurchasedBefore = false;
                bool UserCountsView = true;
                if (UserId.HasValue)
                {

                    User user = await userService.GetById(UserId.Value).ConfigureAwait(true);
                    string message = user.FirstName + " viewed your pick.";

                    model.HasPurchasedBefore = await orderService.HasPurchased(UserId.Value, Listing.Value).ConfigureAwait(true);
                    if (UserId.Value == model.Listing.PostedbyId) UserCountsView = false;
                    if (model.IsAdmin) UserCountsView = false;
                    var isInCart = await shoppingCartService.IsItemInCart(UserId.Value, Listing.Value).ConfigureAwait(true);
                    if (isInCart.HasValue) model.IsInCart = isInCart.Value;

                    if (UserCountsView)
                    {
                        var resultNotification = await notificationService.Insert(model.Listing.PostedbyId, message).ConfigureAwait(true);
                        if (!resultNotification) throw new Exception("Could not save new notification");
                    }
                }

                if (UserCountsView)
                {
                    var resultUpdate = await betListingService.IncreaseViews(model.Listing.Id).ConfigureAwait(true);
                    if (!resultUpdate) throw new Exception("Error increasing views on listing!");
                }

                return View(model);
            }
            catch (Exception e)
            {
                await errorLogService.InsertException(e, UserId).ConfigureAwait(true);
                return RedirectToAction("Index", "Error", request);
            }
        }

        [Permitted(Permission.Capper, Permission.Bettor)]
        [HttpPost]
        public async Task<IActionResult> AddBetListingReport(ListingReportSubmission newReport)
        {
            Guid? UserId = null;
            try
            {
                if (string.IsNullOrWhiteSpace(newReport.ReportTopic)) throw new Exception("Report Subject is mandatory!");
                if (newReport.ReportTopic == ReportListingReasons.Other && string.IsNullOrWhiteSpace(newReport.ReportContent)) throw new Exception("Report Content is mandatory!");
                if (string.IsNullOrWhiteSpace(newReport.ReportedListingId.ToString()) || newReport.ReportedListingId == Guid.Empty) throw new Exception("Reported Listing Id is mandatory!");

                UserId = User.Id();

                if (!UserId.HasValue) throw new Exception("No user found.");

                BetListingReport report = new BetListingReport
                {
                    DateReported = DateTime.UtcNow,
                    IsCorrect = false,
                    ReportContent = newReport.ReportContent,
                    ReportedById = UserId.Value,
                    ReportedListingId = newReport.ReportedListingId,
                    ReportTopic = newReport.ReportTopic,
                };

                var result = await betListingReportService.Insert(report).ConfigureAwait(true);
                if (result)
                {
                    return Json(new { status = true, messageTitle = "Success", message = "Report submitted successfully!" });
                }
                else throw new Exception("Error creating new report. Try again later.");

            }
            catch (Exception e)
            {
                ModelState.AddModelError(string.Empty, "Something went wrong, please try again later");
                await errorLogService.InsertException(e).ConfigureAwait(true);
                return Json(new { status = false, messageTitle = "Error", message = e.Message });
            }
        }
        #endregion

        #region Profile
        [AllowAnonymous]
        public async Task<IActionResult> Profile(Guid Id, int TimeOffset = 0, int page = 1, int pageSize = 20)
        {
            ViewData["TimezoneOffset"] = TimeOffset;
            ViewData["root"] = hostingEnvironment.ContentRootPath;
            ViewData["ShowMyRating"] = false;
            Guid? UserId = null;
            RequestFeedback request = new RequestFeedback();
            try
            {
                UserId = User.Id();
                if (UserId.HasValue && UserId.Value != Id && !User.Identity.HasAnyPermissions(Permission.Admin))
                {
                    ViewData["ShowMyRating"] = true;
                }
                UserProfilePageDto model = await userService.GetProfileById(Id, UserId).ConfigureAwait(true);
                ViewData["Page"] = page;
                ViewData["PageSize"] = pageSize;
                ViewData["PageNumber"] = Utility.PageSize(model.Listings.Count, pageSize);

                model.Listings = model.Listings.Page(page, pageSize).ToList();
                return View(model);


            }
            catch (Exception e)
            {
                await errorLogService.InsertException(e, UserId).ConfigureAwait(true);
                return RedirectToAction("Index", "Error", request);
            }
        }

        [Permitted(Permission.Capper, Permission.Bettor)]
        [HttpPost]
        public async Task<IActionResult> EvaluateUser(Guid UserToEvaluateId, int Rating)
        {
            Guid? UserId = null;
            try
            {

                UserId = User.Id();
                if (UserToEvaluateId == Guid.Empty) throw new Exception("No user found to evaluate with given ID!");
                if (!UserId.HasValue || UserId.Value == Guid.Empty) throw new Exception("No user found!");



                UserRating newEvaluation = new UserRating
                {
                    FromId = UserId.Value,
                    ToId = UserToEvaluateId,
                    RatingValue = Rating
                };

                var result = await userRatingService.Insert(newEvaluation).ConfigureAwait(true);
                if (result)
                {
                    return Json(new { status = true, messageTitle = "Success", message = "User rated successfully!" });
                }
                else throw new Exception("Error rating user. Try again later.");

            }
            catch (Exception e)
            {
                ModelState.AddModelError(string.Empty, "Something went wrong, please try again later");
                await errorLogService.InsertException(e).ConfigureAwait(true);
                return Json(new { status = false, messageTitle = "Error", message = e.Message });
            }
        }


        [Permitted(Permission.Capper, Permission.Bettor)]
        [HttpPost]
        public async Task<IActionResult> ReEvaluateUser(int RatingId, int RatingNewValue)
        {
            Guid? UserId = null;
            try
            {

                UserId = User.Id();
                if (RatingId == 0) throw new Exception("No evaluation found with given ID!");
                if (!UserId.HasValue || UserId.Value == Guid.Empty) throw new Exception("No user found!");



                UserRating reEvaluation = await userRatingService.GetById(RatingId).ConfigureAwait(true);
                reEvaluation.RatingValue = RatingNewValue;
                var result = await userRatingService.Update(reEvaluation).ConfigureAwait(true);
                if (result)
                {
                    return Json(new { status = true, messageTitle = "Success", message = "User rating updated successfully!" });
                }
                else throw new Exception("Error re-rating user. Try again later.");

            }
            catch (Exception e)
            {
                ModelState.AddModelError(string.Empty, "Something went wrong, please try again later");
                await errorLogService.InsertException(e).ConfigureAwait(true);
                return Json(new { status = false, messageTitle = "Error", message = e.Message });
            }
        }

        [Permitted(Permission.Capper, Permission.Bettor)]
        [HttpPost]
        public async Task<IActionResult> FollowUser(Guid UserToFollowId)
        {
            Guid? UserId = null;
            try
            {

                UserId = User.Id();
                if (UserToFollowId == Guid.Empty) throw new Exception("No user found to follow with given ID!");
                if (!UserId.HasValue || UserId.Value == Guid.Empty) throw new Exception("No user found!");

                UserFollow newFollow = new UserFollow
                {
                    FromId = UserId.Value,
                    ToId = UserToFollowId
                };

                var result = await userFollowService.Insert(newFollow).ConfigureAwait(true);
                if (result)
                {
                    User user = await userService.GetById(UserId.Value).ConfigureAwait(true);
                    string message = user.FirstName + " is following you.";
                    var resultNotification = await notificationService.Insert(newFollow.ToId, message).ConfigureAwait(true);
                    if (!resultNotification) throw new Exception("Could not save new notification");
                    return Json(new { status = true, messageTitle = "Success", message = "Started following successfully!" });
                }
                else throw new Exception("Error following user. Try again later.");

            }
            catch (Exception e)
            {
                ModelState.AddModelError(string.Empty, "Something went wrong, please try again later");
                await errorLogService.InsertException(e).ConfigureAwait(true);
                return Json(new { status = false, messageTitle = "Error", message = e.Message });
            }
        }

        [Permitted(Permission.Capper, Permission.Bettor)]
        [HttpPost]
        public async Task<IActionResult> UnfollowUser(Guid UserToUnFollowId)
        {
            Guid? UserId = null;
            try
            {

                UserId = User.Id();
                if (UserToUnFollowId == Guid.Empty) throw new Exception("No user found to follow with given ID!");
                if (!UserId.HasValue || UserId.Value == Guid.Empty) throw new Exception("No user found!");



                UserFollow newUnFollow = await userFollowService.GetByFromTo(UserId.Value, UserToUnFollowId).ConfigureAwait(true);

                var result = await userFollowService.Delete(newUnFollow.Id).ConfigureAwait(true);
                if (result)
                {

                    User user = await userService.GetById(UserId.Value).ConfigureAwait(true);
                    string message = user.FirstName + " unfollowed you.";
                    var resultNotification = await notificationService.Insert(UserToUnFollowId, message).ConfigureAwait(true);
                    if (!resultNotification) throw new Exception("Could not save new notification");
                    return Json(new { status = true, messageTitle = "Success", message = "Stoped following successfully!" });
                }
                else throw new Exception("Error unfollowing user. Try again later.");

            }
            catch (Exception e)
            {
                ModelState.AddModelError(string.Empty, "Something went wrong, please try again later");
                await errorLogService.InsertException(e).ConfigureAwait(true);
                return Json(new { status = false, messageTitle = "Error", message = e.Message });
            }
        }



        [Permitted(Permission.Capper, Permission.Bettor)]
        public async Task<IActionResult> AccountManager(int TimeOffset = 0)
        {
            ViewData["TimezoneOffset"] = TimeOffset;
            try
            {
                User user = await userService.GetById(new Guid(User.Identity.Id())).ConfigureAwait(true);
                var permission = User.Identity.Permissions().FirstOrDefault();

                if (!User.Identity.HasPermissions(Permission.Capper, Permission.Bettor)) throw new Exception("No permissions set.");
                else
                {
                    AccountManagerDto model = new AccountManagerDto
                    {
                        User = user,
                        CurrentPermission = permission,
                        Countries = (await countryService.GetAll().ConfigureAwait(true)).OrderBy(o => o.Name).ToList(),
                        RewardsOptions = await rewardsMappingService.GetAll(true).ConfigureAwait(true),
                        RewardsClaimed = await rewardsClaimedMappingService.GetHistoryFromUser(user.Id).ConfigureAwait(true),
                        UserRating = await userRatingService.GetAverageRatingCorrect(user.Id).ConfigureAwait(true),
                        UserRatingPoints = await userRatingService.GetTotalRatingPoints(user.Id).ConfigureAwait(true),
                        BoisterousActiveSubscription = await userSubscriptionService.GetActiveSubscriptionPlan(user.Id, SubscriptionPlanType.Boisterous).ConfigureAwait(true),
                        ShortchaseProActiveSubscription = await userSubscriptionService.GetActiveSubscriptionPlan(user.Id, SubscriptionPlanType.ShortchasePro).ConfigureAwait(true)
                    };

                    model.LastUpdatedRewards = DateTime.UtcNow;//model.RewardsOptions.OrderBy(o => o.RowDate).Select(r => r.RowDate).FirstOrDefault();

                    return View(model);
                }
            }
            catch (Exception e)
            {
                await errorLogService.InsertException(e).ConfigureAwait(true);
                return RedirectToAction("Index");
            }

        }

        [Permitted(Permission.Capper, Permission.Bettor)]
        public async Task<IActionResult> OrderManager(int TimeOffset = 0)
        {
            ViewData["TimezoneOffset"] = TimeOffset;
            Guid? UserId = null;
            RequestFeedback request = new RequestFeedback();
            try
            {
                UserId = User.Id();
                if (!UserId.HasValue || UserId.Value == Guid.Empty) throw new Exception("No user found!");
                OrderManagerDto model = new OrderManagerDto
                {
                    Orders = new List<OrderManagerOrderDto>(),
                    root = hostingEnvironment.ContentRootPath
                };

                if (User.Identity.HasAnyPermissions(Permission.Bettor))
                {
                    model.IsCapper = false;
                    model.Orders = (await orderService.GetAllOrdersFromUser(UserId.Value).ConfigureAwait(true)).OrderByDescending(o => o.OrderDate).ToList();
                }
                else
                {
                    model.IsCapper = true;
                    model.Orders = (await orderService.GetAllOrdersToUser(UserId.Value).ConfigureAwait(true)).OrderByDescending(o => o.OrderDate).ToList();
                }
                return View(model);
            }
            catch (Exception e)
            {
                await errorLogService.InsertException(e, UserId).ConfigureAwait(true);
                return RedirectToAction("Index", "Error", request);
            }
        }

        [AllowAnonymous]
        public async Task<IActionResult> ResetPassword(string Code = null)
        {
            Guid? UserId = null;
            RequestFeedback request = new RequestFeedback();
            try
            {
                UserId = User.Id();
                if (string.IsNullOrWhiteSpace(Code))
                {
                    return View();
                }
                else
                {
                    return View(Code);
                }
            }
            catch (Exception e)
            {
                await errorLogService.InsertException(e, UserId).ConfigureAwait(true);
                return RedirectToAction("Index", "Error", request);
            }
        }
        #endregion

        #region POTD
        [AllowAnonymous]
        public async Task<IActionResult> PredictPOTD(Guid Id, int TimeOffset = 0)
        {
            ViewData["root"] = hostingEnvironment.ContentRootPath;
            ViewData["TimezoneOffset"] = TimeOffset;
            Guid? UserId = null;
            RequestFeedback request = new RequestFeedback();
            try
            {
                UserId = User.Id();
                if (Id == Guid.Empty)
                {
                    request.Title = "No POTD found with given Id!";
                    request.Text = "No POTD found with given Id!";
                    throw new Exception("No POTD found with given Id");
                }
                else
                {
                    PredictPOTDDto model = new PredictPOTDDto
                    {
                        POTD = await potdListingService.GetById(Id).ConfigureAwait(true),

                    };
                    model.MarketOptions = await marketService.GetAllByCategoryId(model.POTD.CategoryId).ConfigureAwait(true);
                    return View(model);
                }

            }
            catch (Exception e)
            {
                await errorLogService.InsertException(e, UserId).ConfigureAwait(true);
                return RedirectToAction("Index", "Error", request);
            }
        }


        [Permitted(Permission.Capper, Permission.Bettor)]
        [HttpPost]
        public async Task<IActionResult> SavePredictionPOTD(CreatePOTDPredictionDto newPrediction)
        {
            Guid? UserId = null;
            RequestFeedback request = new RequestFeedback();
            try
            {
                UserId = User.Id();
                if (newPrediction.Market == 0) throw new Exception("You need to submit a valid market prediction!");
                if (newPrediction.Tip == 0) throw new Exception("You need to submit a valid tip prediction!");
                if (newPrediction.POTDId == Guid.Empty) throw new Exception("You need to submit a valid POTD item!");
                if (!UserId.HasValue || UserId == Guid.Empty) throw new Exception("You need to submit a valid User!");

                var potd = await potdListingService.GetById(newPrediction.POTDId).ConfigureAwait(true);

                if (potd == null) throw new Exception("You need to submit a valid POTD item!");
                if (DateTime.UtcNow >= potd.Pick.StartTime) throw new Exception("Error: The prediction window has closed.");

                POTDListingPrediction prediction = new POTDListingPrediction
                {
                    POTDId = newPrediction.POTDId,
                    MarketId = newPrediction.Market,
                    TipId = newPrediction.Tip,
                    Prediction = null,
                    PredictedById = UserId.Value,
                    DatePredicted = DateTime.UtcNow,
                    DateVerified = null,
                    Deleted = false,
                    VerifiedAsCorrect = null
                };

                var result = await potdListingPredictionService.Insert(prediction).ConfigureAwait(true);
                if (result)
                {

                    return Json(new { status = true, messageTitle = "Success", message = "Prediction saved successfully!" });
                }
                else throw new Exception("There was an error trying to save your request!");
            }
            catch (Exception e)
            {
                await errorLogService.InsertException(e, UserId).ConfigureAwait(true);
                return Json(new { status = false, messageTitle = "Error", message = e.Message });
            }
        }

        [AllowAnonymous]
        public async Task<IActionResult> ViewPOTD(Guid Id, int TimeOffset = 0)
        {
            ViewData["root"] = hostingEnvironment.ContentRootPath;
            ViewData["TimezoneOffset"] = TimeOffset;
            Guid? UserId = null;
            RequestFeedback request = new RequestFeedback();
            try
            {
                UserId = User.Id();
                if (Id == Guid.Empty)
                {
                    request.Title = "No POTD found with given Id!";
                    request.Text = "No POTD found with given Id!";
                    throw new Exception("No POTD found with given Id");
                }
                else
                {
                    var POTDs = await potdListingService.GetAllAvailable().ConfigureAwait(true);

                    if (POTDs.Count <= 0) throw new Exception("No POTDs found");
                    else
                    {
                        var currentPOTD = POTDs.Where(i => i.Id == Id).SingleOrDefault();
                        if (currentPOTD == null) throw new Exception("No POTD found with given Id");
                        POTDListing next = null;
                        if (POTDs.Count == 1)
                        {
                            next = null;
                        }
                        else if (POTDs.Count == 2)
                        {
                            if (currentPOTD == POTDs.First())
                            {
                                next = POTDs.Last();
                            }
                            else
                            {
                                next = POTDs.First();
                            }
                        }
                        else
                        {
                            int currentPOTDposition = 0;
                            int nextPosition = -1;
                            foreach (var item in POTDs)
                            {
                                currentPOTDposition++;
                                if (item == currentPOTD)
                                {
                                    if (currentPOTDposition + 1 == POTDs.Count)
                                    {
                                        nextPosition = 0;
                                    }
                                    else
                                    {
                                        nextPosition = currentPOTDposition + 1;
                                    }
                                    break;
                                }
                            }
                            int lastIteratorCount = 0;
                            foreach (var item in POTDs)
                            {
                                lastIteratorCount++;
                                if (lastIteratorCount == nextPosition)
                                {
                                    next = item;
                                    break;
                                }
                            }

                        }

                        ViewPOTDWebsite model = new ViewPOTDWebsite
                        {
                            POTD = currentPOTD,
                            NextPOTD = next,
                            UserPrediction = await potdListingPredictionService.GetUserPredictionForPOTD(UserId.Value, Id).ConfigureAwait(true),
                            LiveReportings = await potdListingLiveReportService.GetByPOTDId(Id).ConfigureAwait(true)
                        };
                        return View(model);
                    }

                }
            }
            catch (Exception e)
            {
                await errorLogService.InsertException(e, UserId).ConfigureAwait(true);
                return RedirectToAction("Index", "Error", request);
            }
        }

        [Permitted(Permission.Capper, Permission.Bettor)]
        [HttpPost]
        public async Task<IActionResult> EnterUserInteraction(CreatePOTDLiveReportingUserInteractionDto newInteraction)
        {
            Guid? UserId = null;
            RequestFeedback request = new RequestFeedback();
            try
            {
                if (string.IsNullOrWhiteSpace(newInteraction.InteractionType)) throw new Exception("You need to submit a valid Interaction Type!");
                if (newInteraction.POTDLiveReportId == Guid.Empty) throw new Exception("You need to submit a valid POTD item!");
                if (newInteraction.InteractedById == Guid.Empty) throw new Exception("You need to submit a valid User!");

                POTDListingLiveReportingInteraction interaction = new POTDListingLiveReportingInteraction
                {
                    POTDLiveReportId = newInteraction.POTDLiveReportId,
                    InteractedById = newInteraction.InteractedById,
                    InteractionType = newInteraction.InteractionType,
                };

                var result = await potdListingLiveReportingInteractionService.Insert(interaction).ConfigureAwait(true);
                if (result)
                {

                    return Json(new { status = true, messageTitle = "Success", message = "Interaction saved successfully!" });
                }
                else throw new Exception("There was an error trying to save your request!");
            }
            catch (Exception e)
            {
                await errorLogService.InsertException(e, UserId).ConfigureAwait(true);
                return Json(new { status = false, messageTitle = "Error", message = e.Message });
            }
        }

        [Permitted(Permission.Capper, Permission.Bettor)]
        public async Task<IActionResult> AllPOTD(int TimeOffset = 0, int page = 1, int pageSize = 20)
        {
            ViewData["root"] = hostingEnvironment.ContentRootPath;
            ViewData["TimezoneOffset"] = TimeOffset;
            Guid? UserId = null;
            RequestFeedback request = new RequestFeedback();
            try
            {
                UserId = User.Id();
                WebsiteUserPredictions model = new WebsiteUserPredictions
                {
                    POTDs = await potdListingService.GetAllPredictedByUserId(UserId.Value).ConfigureAwait(true)
                };
                ViewData["Page"] = page;
                ViewData["PageSize"] = pageSize;
                ViewData["PageNumber"] = Utility.PageSize(model.POTDs.Count, pageSize);
                model.POTDs = model.POTDs.Page(page, pageSize).ToList();
                return View(model);
            }
            catch (Exception e)
            {
                await errorLogService.InsertException(e, UserId).ConfigureAwait(true);
                return RedirectToAction("Index", "Error", request);
            }
        }
        #endregion

        #region Shopping Cart

        [AllowAnonymous]
        public async Task<IActionResult> OrderReceipt2(Guid OrderId, int TimeOffset = 0)
        {
            ViewData["TimezoneOffset"] = TimeOffset;
            Guid? UserId = null;
            RequestFeedback request = new RequestFeedback();
            try
            {
                SemiStaticText AppLogo = (await semiStaticTextService.GetByName(SemiStaticTextNames.AppLogo).ConfigureAwait(true));

                string logo = null;
                if (string.IsNullOrWhiteSpace(AppLogo.Value))
                {
                    logo = Url.Content("~/img/shortchase_logo.png");
                }
                else logo = ImageHelper.ConvertImageToBase64(hostingEnvironment.ContentRootPath + AppLogo.Value);
                OrderReceiptDto model = new OrderReceiptDto
                {
                    Details = await orderService.GetOrderDetails(OrderId).ConfigureAwait(true),
                    Logo = logo,
                    TimezoneOffset = TimeOffset
                };

                //var x = await CreateReceiptPDF(OrderId, TimeOffset).ConfigureAwait(true);

                return View(model);
            }
            catch (Exception e)
            {
                await errorLogService.InsertException(e, UserId).ConfigureAwait(true);
                return RedirectToAction("Index", "Error", request);
            }
        }

        [AllowAnonymous]
        public async Task<IActionResult> OrderReceipt(Guid OrderId, int TimeOffset = 0)
        {
            ViewData["TimezoneOffset"] = TimeOffset;
            Guid? UserId = null;
            RequestFeedback request = new RequestFeedback();
            try
            {
                SemiStaticText AppLogo = (await semiStaticTextService.GetByName(SemiStaticTextNames.AppLogo).ConfigureAwait(true));

                string logo = null;
                if (string.IsNullOrWhiteSpace(AppLogo.Value))
                {
                    logo = Url.Content("~/img/shortchase_logo.png");
                }
                else logo = ImageHelper.ConvertImageToBase64(hostingEnvironment.ContentRootPath + AppLogo.Value);
                OrderReceiptPDFDto model = new OrderReceiptPDFDto
                {
                    Details = await orderService.GetOrderDetailsForReceipt(OrderId).ConfigureAwait(true),
                    Logo = logo,
                    TimezoneOffset = TimeOffset,
                    PrimaryAddress = await addressService.GetPrimaryAddress().ConfigureAwait(true)
                };


                return View(model);
            }
            catch (Exception e)
            {
                await errorLogService.InsertException(e, UserId).ConfigureAwait(true);
                return RedirectToAction("Index", "Error", request);
            }
        }

        [AllowAnonymous]
        public async Task<IActionResult> ViewListingsInOrder(string Order, Guid? CurrentItem = null)
        {
            ViewData["TimezoneOffset"] = 0;
            Guid? UserId = null;
            RequestFeedback request = new RequestFeedback();
            try
            {
                if (string.IsNullOrWhiteSpace(Order)) throw new Exception("No order received.");
                Guid orderId = new Guid(Security.Decrypt(Security.LinkDecode(Order)));
                if (orderId == Guid.Empty) throw new Exception("No order received.");
                Entities.Order order = await orderService.GetById(orderId).ConfigureAwait(true);
                if (order == null) throw new Exception("No order received.");
                OrderDetailsDto data = await orderService.GetOrderDetails(orderId).ConfigureAwait(true);

                ViewListingsInOrderDto model = new ViewListingsInOrderDto
                {
                    Order = data.Order,
                    OrderItems = data.OrderItems,
                };

                if (CurrentItem.HasValue)
                {
                    model.CurrentItem = model.OrderItems.Where(i => i.Id == CurrentItem.Value).SingleOrDefault();
                }
                else
                {
                    model.CurrentItem = model.OrderItems.FirstOrDefault();
                }

                return View(model);
            }
            catch (Exception e)
            {
                await errorLogService.InsertException(e, UserId).ConfigureAwait(true);
                return RedirectToAction("Index", "Error", request);
            }
        }


        [AllowAnonymous]
        public async Task<IActionResult> SubscriptionReceipt(SubscriptionReceiptEmail ReceiptData, int TimeOffset = 0)
        {
            ViewData["TimezoneOffset"] = TimeOffset;
            Guid? UserId = null;
            RequestFeedback request = new RequestFeedback();
            try
            {
                SemiStaticText AppLogo = (await semiStaticTextService.GetByName(SemiStaticTextNames.AppLogo).ConfigureAwait(true));

                string logo = null;
                if (string.IsNullOrWhiteSpace(AppLogo.Value))
                {
                    logo = Url.Content("~/img/shortchase_logo.png");
                }
                else logo = ImageHelper.ConvertImageToBase64(hostingEnvironment.ContentRootPath + AppLogo.Value);
                SubscriptionReceiptDto model = new SubscriptionReceiptDto
                {
                    Logo = logo,
                    TimezoneOffset = TimeOffset,
                    End = ReceiptData.End,
                    PaidValue = ReceiptData.PaidValue,
                    PaymentStatus = ReceiptData.PaymentStatus,
                    PaymentType = ReceiptData.PaymentType,
                    PaypalPaidValue = ReceiptData.PaypalPaidValue,
                    Start = ReceiptData.Start,
                    SubscriptionName = ReceiptData.SubscriptionName,
                    SubscriptionPrice = ReceiptData.SubscriptionPrice,
                    WalletBalanceAfter = ReceiptData.WalletBalanceAfter,
                    WalletBalanceBefore = ReceiptData.WalletBalanceBefore
                };

                //var x = await CreateSubscriptionReceiptPDF(OrderId, TimeOffset).ConfigureAwait(true);

                return View(model);
            }
            catch (Exception e)
            {
                await errorLogService.InsertException(e, UserId).ConfigureAwait(true);
                return RedirectToAction("Index", "Error", request);
            }
        }


        [AllowAnonymous]
        public async Task<IActionResult> SubscriptionReceipt2(SubscriptionReceiptEmail ReceiptData, int TimeOffset = 0)
        {
            ViewData["TimezoneOffset"] = TimeOffset;
            Guid? UserId = null;
            RequestFeedback request = new RequestFeedback();
            try
            {
                SemiStaticText AppLogo = (await semiStaticTextService.GetByName(SemiStaticTextNames.AppLogo).ConfigureAwait(true));

                string logo = null;
                if (string.IsNullOrWhiteSpace(AppLogo.Value))
                {
                    logo = Url.Content("~/img/shortchase_logo.png");
                }
                else logo = ImageHelper.ConvertImageToBase64(hostingEnvironment.ContentRootPath + AppLogo.Value);
                SubscriptionReceipt2Dto model = new SubscriptionReceipt2Dto
                {
                    Logo = logo,
                    TimezoneOffset = TimeOffset,
                    End = ReceiptData.End,
                    PaidValue = ReceiptData.PaidValue,
                    PaymentStatus = ReceiptData.PaymentStatus,
                    PaymentType = ReceiptData.PaymentType,
                    PaypalPaidValue = ReceiptData.PaypalPaidValue,
                    Start = ReceiptData.Start,
                    SubscriptionName = ReceiptData.SubscriptionName,
                    SubscriptionPrice = ReceiptData.SubscriptionPrice,
                    WalletBalanceAfter = ReceiptData.WalletBalanceAfter,
                    WalletBalanceBefore = ReceiptData.WalletBalanceBefore
                };


                return View(model);
            }
            catch (Exception e)
            {
                await errorLogService.InsertException(e, UserId).ConfigureAwait(true);
                return RedirectToAction("Index", "Error", request);
            }
        }


        [AllowAnonymous]
        public async Task<bool> CreateReceiptPDF(Guid Id, int TimeOffset = 0)
        {
            try
            {
                if (Id == Guid.Empty) throw new Exception("Invalid order id.");
                var order = await orderService.GetById(Id).ConfigureAwait(true);
                if (order == null) throw new Exception("No order found.");

                string root = hostingEnvironment.ContentRootPath;
                string pathRoot = "\\Media\\Orders\\Receipts\\";
                string name = Guid.NewGuid().ToString() + ".pdf";
                Directory.CreateDirectory(root + pathRoot);

                var pdfname = String.Format("{0}", name);
                var path = Path.Combine(pathRoot, pdfname);

                SemiStaticText AppLogo = (await semiStaticTextService.GetByName(SemiStaticTextNames.AppLogo).ConfigureAwait(true));

                string logo = null;
                if (string.IsNullOrWhiteSpace(AppLogo.Value))
                {
                    logo = Url.Content("~/img/shortchase_logo.png");
                }
                else logo = ImageHelper.ConvertImageToBase64(hostingEnvironment.ContentRootPath + AppLogo.Value);
                OrderReceiptPDFDto model = new OrderReceiptPDFDto
                {
                    Details = await orderService.GetOrderDetailsForReceipt(Id).ConfigureAwait(true),
                    Logo = logo,
                    TimezoneOffset = TimeOffset,
                    PrimaryAddress = await addressService.GetPrimaryAddress().ConfigureAwait(true)
                };

                var pdf = new Rotativa.AspNetCore.ViewAsPdf("OrderReceipt2", model)
                {
                    PageMargins = { Bottom = 20, Left = 10, Right = 10, Top = 20 },
                    PageOrientation = Rotativa.AspNetCore.Options.Orientation.Portrait,
                    FileName = name
                };

                var byteArray = pdf.BuildFile(ControllerContext);
                var fileStream = new FileStream(root + path, FileMode.Create, FileAccess.Write);
                fileStream.Write(byteArray.Result, 0, byteArray.Result.Length);
                fileStream.Close();

                order.ReceiptPDF = path;

                var updateResult = await orderService.Update(order).ConfigureAwait(true);
                if (updateResult)
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
                await errorLogService.InsertException(e).ConfigureAwait(true);
                return false;
            }
        }




        [AllowAnonymous]
        public async Task<bool> CreateSubscriptionReceiptPDF(SubscriptionReceiptEmail ReceiptData, UserSubscription subscription, int TimeOffset = 0)
        {
            try
            {

                string root = hostingEnvironment.ContentRootPath;
                string pathRoot = "\\Media\\Subscriptions\\Receipts\\";
                string name = Guid.NewGuid().ToString() + ".pdf";
                Directory.CreateDirectory(root + pathRoot);

                var pdfname = String.Format("{0}", name);
                var path = Path.Combine(pathRoot, pdfname);

                SemiStaticText AppLogo = (await semiStaticTextService.GetByName(SemiStaticTextNames.AppLogo).ConfigureAwait(true));

                string logo = null;
                if (string.IsNullOrWhiteSpace(AppLogo.Value))
                {
                    logo = Url.Content("~/img/shortchase_logo.png");
                }
                else logo = ImageHelper.ConvertImageToBase64(hostingEnvironment.ContentRootPath + AppLogo.Value);
                SubscriptionReceipt2Dto model = new SubscriptionReceipt2Dto
                {
                    Logo = logo,
                    TimezoneOffset = TimeOffset,
                    End = ReceiptData.End,
                    PaidValue = ReceiptData.PaidValue,
                    PaymentStatus = ReceiptData.PaymentStatus,
                    PaymentType = ReceiptData.PaymentType,
                    PaypalPaidValue = ReceiptData.PaypalPaidValue,
                    Start = ReceiptData.Start,
                    SubscriptionName = ReceiptData.SubscriptionName,
                    SubscriptionPrice = ReceiptData.SubscriptionPrice,
                    WalletBalanceAfter = ReceiptData.WalletBalanceAfter,
                    WalletBalanceBefore = ReceiptData.WalletBalanceBefore,
                    Description = ReceiptData.Description,
                    Qty = ReceiptData.Qty,
                    PrimaryAddress = await addressService.GetPrimaryAddress().ConfigureAwait(true),
                    OrderUser = await userService.GetById(subscription.UserId).ConfigureAwait(true)
                };

                var pdf = new Rotativa.AspNetCore.ViewAsPdf("SubscriptionReceipt2", model)
                {
                    PageMargins = { Bottom = 20, Left = 10, Right = 10, Top = 20 },
                    PageOrientation = Rotativa.AspNetCore.Options.Orientation.Portrait,
                    FileName = name
                };

                var byteArray = pdf.BuildFile(ControllerContext);
                var fileStream = new FileStream(root + path, FileMode.Create, FileAccess.Write);
                fileStream.Write(byteArray.Result, 0, byteArray.Result.Length);
                fileStream.Close();

                subscription.ReceiptPDF = path;

                var updateResult = await userSubscriptionService.Update(subscription).ConfigureAwait(true);
                if (updateResult)
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
                await errorLogService.InsertException(e).ConfigureAwait(true);
                return false;
            }
        }






        [Permitted(Permission.Bettor)]
        public async Task<IActionResult> Cart(int TimeOffset = 0)
        {
            ViewData["TimezoneOffset"] = TimeOffset;
            Guid? UserId = null;
            RequestFeedback request = new RequestFeedback();
            try
            {
                UserId = User.Id();
                if (!UserId.HasValue) throw new Exception("No user to add to a cart.");
                User user = await userService.GetById(UserId.Value).ConfigureAwait(true);
                ShoppingCartDto model = new ShoppingCartDto
                {
                    EstimatedTax = Convert.ToDecimal((await systemConstantsService.GetByName(SystemConstantName.Taxes).ConfigureAwait(true)).Value),
                    ServiceFee = Convert.ToDecimal((await systemConstantsService.GetByName(SystemConstantName.RegularFees).ConfigureAwait(true)).Value),
                    TotalAfterTax = 0.0m,
                    TotalBeforeTaxAndFees = 0.0m,
                    Items = (await shoppingCartService.GetItemsInCart(UserId.Value).ConfigureAwait(true)).OrderBy(o => o.SoldBy).ToList(),
                    Discount = 0.00m,
                    DiscountPercent = 0.00m
                };

                var activeBoisterousPlan = await userSubscriptionService.GetActiveSubscriptionPlan(UserId.Value, SubscriptionPlanType.Boisterous).ConfigureAwait(true);

                if (activeBoisterousPlan != null)
                {
                    model.ServiceFee = Convert.ToDecimal((await systemConstantsService.GetByName(SystemConstantName.BoisterousFees).ConfigureAwait(true)).Value);
                }


                model.TotalBeforeDiscount = model.Items.Sum(i => i.Price);

                var activeDiscount = await userDiscountService.GetActiveByUserId(UserId.Value).ConfigureAwait(true);
                if (activeDiscount != null)
                {
                    model.DiscountPercent = activeDiscount.DiscountPercentageValue;
                    model.Discount = (model.DiscountPercent / 100) * model.TotalBeforeDiscount;
                }
                else
                {
                    bool isUserReferred = !string.IsNullOrWhiteSpace(user.ReferredByEmail);
                    if (isUserReferred)
                    {
                        int countUserOrders = await orderService.CountTotalByUser(user.Id).ConfigureAwait(true);
                        if (countUserOrders <= 0)
                        {
                            model.DiscountPercent = DiscountValues.Standard;
                            model.Discount = (model.DiscountPercent / 100) * model.TotalBeforeDiscount;
                        }
                    }
                }

                model.TotalBeforeTaxAndFees = model.TotalBeforeDiscount - model.Discount;
                model.EstimatedTaxPercent = model.EstimatedTax;
                model.ServiceFeePercent = model.ServiceFee;
                decimal taxes = model.TotalBeforeTaxAndFees * (model.EstimatedTax / 100.0m);
                decimal fees = model.TotalBeforeTaxAndFees * (model.ServiceFee / 100.0m);
                model.TotalAfterTax = model.TotalBeforeTaxAndFees + taxes + fees;
                model.EstimatedTax = taxes;
                model.ServiceFee = fees;
                return View(model);
            }
            catch (Exception e)
            {
                await errorLogService.InsertException(e, UserId).ConfigureAwait(true);
                return RedirectToAction("Index", "Error", request);
            }
        }


        [Permitted(Permission.Capper, Permission.Bettor)]
        [HttpPost]
        public async Task<IActionResult> AddItemCart(Guid Id)
        {
            Guid? UserId = null;
            RequestFeedback request = new RequestFeedback();
            try
            {
                UserId = User.Id();
                if (!UserId.HasValue) throw new Exception("No user to add to a cart.");
                var result = await shoppingCartService.AddItemToCart(UserId.Value, Id).ConfigureAwait(true);
                if (!result.HasValue)
                {
                    throw new Exception("Could not add new item in cart!");
                }
                else
                {
                    var message = "Added to cart successfully!";
                    var messageTitle = "Success!";
                    if (!result.Value)
                    {
                        message = "Item already added to cart!";
                        messageTitle = "Sorry!";
                    }
                    return Json(new { status = result.Value, messageTitle = messageTitle, message = message });
                }
            }
            catch (Exception e)
            {
                await errorLogService.InsertException(e, UserId).ConfigureAwait(true);
                return Json(new { status = false, messageTitle = "Error", message = e.Message });
            }
        }


        [Permitted(Permission.Capper, Permission.Bettor)]
        [HttpPost]
        public async Task<IActionResult> RemoveItemCart(Guid Id)
        {
            Guid? UserId = null;
            RequestFeedback request = new RequestFeedback();
            try
            {
                UserId = User.Id();
                if (!UserId.HasValue) throw new Exception("No user to remove from a cart.");
                var result = await shoppingCartService.RemoveItemToCart(UserId.Value, Id).ConfigureAwait(true);
                if (!result)
                {
                    throw new Exception("Could not remove item from cart!");
                }
                else
                {
                    return Json(new { status = result, messageTitle = "Success!", message = "Item removed from cart successfully!" });
                }
            }
            catch (Exception e)
            {
                await errorLogService.InsertException(e, UserId).ConfigureAwait(true);
                return Json(new { status = false, messageTitle = "Error", message = e.Message });
            }
        }



        [Permitted(Permission.Capper, Permission.Bettor)]
        [HttpPost]
        public async Task<IActionResult> CleanCart()
        {
            Guid? UserId = null;
            RequestFeedback request = new RequestFeedback();
            try
            {
                UserId = User.Id();
                if (!UserId.HasValue) throw new Exception("No user to clean cart.");
                var result = await shoppingCartService.CleanCart(UserId.Value).ConfigureAwait(true);
                if (!result)
                {
                    throw new Exception("Could not clean cart!");
                }
                else
                {
                    return Json(new { status = result, messageTitle = "Success!", message = "Cart cleaned successfully!" });
                }
            }
            catch (Exception e)
            {
                await errorLogService.InsertException(e, UserId).ConfigureAwait(true);
                return Json(new { status = false, messageTitle = "Error", message = e.Message });
            }
        }



        [Permitted(Permission.Capper, Permission.Bettor)]
        public async Task<IActionResult> Checkout(int TimeOffset = 0)
        {
            ViewData["TimezoneOffset"] = TimeOffset;
            Guid? UserId = null;
            RequestFeedback request = new RequestFeedback();
            try
            {
                UserId = User.Id();
                if (!UserId.HasValue) throw new Exception("No user to remove from a cart.");

                User user = await userService.GetById(UserId.Value).ConfigureAwait(true);

                var itemsInCart = (await shoppingCartService.GetOnlyValidItemsInCart(UserId.Value).ConfigureAwait(true)).OrderBy(o => o.SoldBy).ToList();
                bool canCheckout = true;
                if (itemsInCart == null || itemsInCart.Count <= 0) canCheckout = false;
                ShoppingCartDto model = new ShoppingCartDto
                {
                    EstimatedTax = Convert.ToDecimal((await systemConstantsService.GetByName(SystemConstantName.Taxes).ConfigureAwait(true)).Value),
                    ServiceFee = Convert.ToDecimal((await systemConstantsService.GetByName(SystemConstantName.RegularFees).ConfigureAwait(true)).Value),
                    TotalAfterTax = 0.0m,
                    TotalBeforeTaxAndFees = 0.0m,
                    Items = itemsInCart,
                    CanCheckout = canCheckout,
                    Discount = 0.00m,
                    DiscountPercent = 0.00m
                };

                var activeBoisterousPlan = await userSubscriptionService.GetActiveSubscriptionPlan(UserId.Value, SubscriptionPlanType.Boisterous).ConfigureAwait(true);

                if (activeBoisterousPlan != null)
                {
                    model.ServiceFee = Convert.ToDecimal((await systemConstantsService.GetByName(SystemConstantName.BoisterousFees).ConfigureAwait(true)).Value);
                }

                model.TotalBeforeDiscount = model.Items.Sum(i => i.Price);

                var activeDiscount = await userDiscountService.GetActiveByUserId(UserId.Value).ConfigureAwait(true);
                if (activeDiscount != null)
                {
                    model.DiscountPercent = activeDiscount.DiscountPercentageValue;
                    model.Discount = (model.DiscountPercent / 100) * model.TotalBeforeDiscount;
                }
                else
                {
                    bool isUserReferred = !string.IsNullOrWhiteSpace(user.ReferredByEmail);
                    if (isUserReferred)
                    {
                        int countUserOrders = await orderService.CountTotalByUser(user.Id).ConfigureAwait(true);
                        if (countUserOrders <= 0)
                        {
                            model.DiscountPercent = DiscountValues.Standard;
                            model.Discount = (model.DiscountPercent / 100) * model.TotalBeforeDiscount;
                        }
                    }
                }

                model.TotalBeforeTaxAndFees = model.TotalBeforeDiscount - model.Discount;
                model.EstimatedTaxPercent = model.EstimatedTax;
                model.ServiceFeePercent = model.ServiceFee;
                model.WalletFunds = user.WalletBalance;
                decimal taxes = model.TotalBeforeTaxAndFees * (model.EstimatedTax / 100.0m);
                decimal fees = model.TotalBeforeTaxAndFees * (model.ServiceFee / 100.0m);
                model.TotalAfterTax = model.TotalBeforeTaxAndFees + taxes + fees;
                model.EstimatedTax = taxes;
                model.ServiceFee = fees;
                model.WalletOrderDifference = user.WalletBalance - model.TotalAfterTax;
                model.WalletBalanceAfterPurchase = model.WalletOrderDifference;
                if (model.WalletOrderDifference < 0)
                {
                    model.HasEnoughFunds = false;
                    model.FundsNeeded = model.WalletOrderDifference * -1.0m;
                    model.WalletBalanceAfterPurchase = model.WalletBalanceAfterPurchase * -1.0m;
                }
                else
                {
                    model.HasEnoughFunds = true;
                    model.FundsNeeded = 0.00m;
                }
                return View(model);
            }
            catch (Exception e)
            {
                await errorLogService.InsertException(e, UserId).ConfigureAwait(true);
                return RedirectToAction("Index", "Error", request);
            }
        }


        [Permitted(Permission.Capper, Permission.Bettor)]
        [HttpPost]
        public async Task<IActionResult> CheckoutByWallet(int TimezoneOffset = 0)
        {
            Guid? UserId = null;
            RequestFeedback request = new RequestFeedback();
            try
            {
                UserId = User.Id();
                if (!UserId.HasValue) throw new Exception("No user to checkout.");



                User user = await userService.GetById(UserId.Value).ConfigureAwait(true);
                ShoppingCart cart = await shoppingCartService.GetByUserId(UserId.Value).ConfigureAwait(true);
                var itemsInCart = (await shoppingCartService.GetOnlyValidItemsInCart(UserId.Value).ConfigureAwait(true)).OrderBy(o => o.SoldBy).ToList();
                bool canCheckout = true;
                if (itemsInCart == null || itemsInCart.Count <= 0) canCheckout = false;
                ShoppingCartDto model = new ShoppingCartDto
                {
                    EstimatedTax = Convert.ToDecimal((await systemConstantsService.GetByName(SystemConstantName.Taxes).ConfigureAwait(true)).Value),
                    ServiceFee = Convert.ToDecimal((await systemConstantsService.GetByName(SystemConstantName.RegularFees).ConfigureAwait(true)).Value),
                    TotalAfterTax = 0.0m,
                    TotalBeforeTaxAndFees = 0.0m,
                    Items = itemsInCart,
                    CanCheckout = canCheckout,
                    Discount = 0.00m,
                    DiscountPercent = 0.00m
                };
                string OrderType = OrderTypes.Regular;
                var activeBoisterousPlan = await userSubscriptionService.GetActiveSubscriptionPlan(UserId.Value, SubscriptionPlanType.Boisterous).ConfigureAwait(true);

                if (activeBoisterousPlan != null)
                {
                    OrderType = OrderTypes.Boisterous;
                    model.ServiceFee = Convert.ToDecimal((await systemConstantsService.GetByName(SystemConstantName.BoisterousFees).ConfigureAwait(true)).Value);
                }

                model.TotalBeforeDiscount = model.Items.Sum(i => i.Price);

                var activeDiscount = await userDiscountService.GetActiveByUserId(UserId.Value).ConfigureAwait(true);
                if (activeDiscount != null)
                {
                    model.DiscountPercent = activeDiscount.DiscountPercentageValue;
                    model.Discount = (model.DiscountPercent / 100) * model.TotalBeforeDiscount;
                }
                else
                {
                    bool isUserReferred = await userDiscountService.IsUserReferred(user.Email).ConfigureAwait(true);
                    if (isUserReferred)
                    {
                        int countUserOrders = await orderService.CountTotalByUser(user.Id).ConfigureAwait(true);
                        if (countUserOrders <= 0)
                        {
                            model.DiscountPercent = DiscountValues.Standard;
                            model.Discount = (model.DiscountPercent / 100) * model.TotalBeforeDiscount;
                        }
                    }
                }

                model.TotalBeforeTaxAndFees = model.TotalBeforeDiscount - model.Discount;
                model.EstimatedTaxPercent = model.EstimatedTax;
                model.ServiceFeePercent = model.ServiceFee;
                model.WalletFunds = user.WalletBalance;
                decimal taxes = model.TotalBeforeTaxAndFees * (model.EstimatedTax / 100.0m);
                decimal fees = model.TotalBeforeTaxAndFees * (model.ServiceFee / 100.0m);
                model.TotalAfterTax = model.TotalBeforeTaxAndFees + taxes + fees;
                model.EstimatedTax = taxes;
                model.ServiceFee = fees;
                model.WalletOrderDifference = user.WalletBalance - model.TotalAfterTax;
                model.WalletBalanceAfterPurchase = model.WalletOrderDifference;
                if (model.WalletOrderDifference < 0)
                {
                    model.HasEnoughFunds = false;
                    model.FundsNeeded = model.WalletOrderDifference * -1.0m;
                    model.WalletBalanceAfterPurchase = model.WalletBalanceAfterPurchase * -1.0m;
                }
                else
                {
                    model.HasEnoughFunds = true;
                    model.FundsNeeded = 0.00m;
                }

                if (canCheckout)
                {
                    if (model.HasEnoughFunds)
                    {
                        Shortchase.Entities.Order newOrder = new Shortchase.Entities.Order
                        {
                            Id = Guid.NewGuid(),
                            CancelledReason = null,
                            RejectedReason = null,
                            DateCancelled = null,
                            DatePaid = DateTime.UtcNow,
                            DateRejected = null,
                            RowDate = DateTime.UtcNow,
                            CapperComission = 0.0m,
                            EstimatedTax = model.EstimatedTax,
                            EstimatedTaxPercent = model.EstimatedTaxPercent,
                            OrderType = OrderType,
                            PaymentType = OrderPaymentType.Wallet,
                            ServiceFee = model.ServiceFee,
                            ServiceFeePercent = model.ServiceFeePercent,
                            UserId = UserId.Value,
                            WalletBalanceBeforePurchase = model.WalletFunds,
                            WalletBalanceAfterPurchase = model.WalletOrderDifference,
                            TotalBeforeTaxAndFees = model.TotalBeforeTaxAndFees,
                            TotalAfterTax = model.TotalAfterTax,
                            PaymentStatus = OrderPaymentStatus.Paid,
                            Discount = model.Discount,
                            DiscountPercent = model.DiscountPercent,
                            TotalBeforeDiscount = model.TotalBeforeDiscount,
                            PaypalOrderId = null,
                            PaypalOrderStatus = null,
                            TotalPaidOnPaypal = 0.00m,
                            OrderNumber =
                                DateTime.UtcNow.Year.ToString()
                                + DateTime.UtcNow.Month.ToString()
                                + DateTime.UtcNow.Day.ToString()
                                + DateTime.UtcNow.Hour.ToString()
                                + DateTime.UtcNow.Minute.ToString()
                                + DateTime.UtcNow.Second.ToString()
                                + (await orderService.CountTotal().ConfigureAwait(true)).ToString()
                        };
                        ICollection<OrderItem> orderItems = new List<OrderItem>();
                        BetListing FirstBetListingInOrder = await betListingService.GetById(model.Items.First().ListingId).ConfigureAwait(true);
                        foreach (var item in model.Items)
                        {
                            OrderItem orderItem = new OrderItem
                            {
                                BetListingId = item.ListingId,
                                ListingTitle = item.ListingTitle,
                                OrderId = newOrder.Id,
                                Price = item.Price,
                                SoldBy = item.SoldBy,
                                RowDate = DateTime.UtcNow
                            };
                            orderItems.Add(orderItem);
                        }

                        var result = await orderService.PlaceNewOrder(newOrder, orderItems, cart, user).ConfigureAwait(true);
                        if (result)
                        {
                            bool receiptPDFresult = await CreateReceiptPDF(newOrder.Id, TimezoneOffset).ConfigureAwait(true);
                            if (receiptPDFresult)
                            {
                                string message = "Bettor " + user.FirstName + " bought your pick.";
                                ICollection<Guid> usersToNotify = (await betListingService.GetAll().ConfigureAwait(true)).Select(i => i.PostedbyId).ToList();
                                foreach (var item in model.Items)
                                {
                                    var resultNotification = await notificationService.Insert(item.SoldById, message).ConfigureAwait(true);
                                    if (!resultNotification) throw new Exception("Could not save new notification");
                                }


                                if (!string.IsNullOrWhiteSpace(user.ReferredByEmail))
                                {
                                    int countOrdersByUser = await orderService.CountTotalByUser(user.Id).ConfigureAwait(true);
                                    if (countOrdersByUser <= 1)
                                    {
                                        User userToGetDiscount = await userService.GetByEmail(user.ReferredByEmail).ConfigureAwait(true);
                                        if (userToGetDiscount != null)
                                        {
                                            UserDiscount newDiscount = new UserDiscount
                                            {
                                                DateUsed = null,
                                                OriginUserEmail = user.Email,
                                                DiscountPercentageValue = DiscountValues.Standard,
                                                UserId = userToGetDiscount.Id
                                            };
                                            var discountResult = await userDiscountService.Insert(newDiscount).ConfigureAwait(true);
                                            if (!discountResult) throw new Exception("Error saving discount for referred user!  ");


                                            var discountEmailResult = await emailSenderService.SendReferralDiscount(userToGetDiscount.Email, userToGetDiscount.FirstName, newDiscount.DiscountPercentageValue.ToString("0.00")).ConfigureAwait(true);

                                            if (!discountEmailResult) throw new Exception("Error sending discount email for referred user!  ");
                                        }

                                    }
                                }

                                if (activeDiscount != null)
                                {
                                    activeDiscount.DateUsed = DateTime.UtcNow;
                                    var activeDiscountResult = await userDiscountService.Update(activeDiscount).ConfigureAwait(true);
                                    if (!activeDiscountResult) throw new Exception("Error updating discount for referred user!  ");
                                }
                                string root = hostingEnvironment.ContentRootPath;
                                string receiptPdf = root + newOrder.ReceiptPDF;
                                var orderReceiptEmail = await emailSenderService.SendUserBetListingOrder(user.Email, user.FirstName, newOrder, orderItems, receiptPdf, TimezoneOffset).ConfigureAwait(true);

                                if (!orderReceiptEmail) throw new Exception("Error sending email with order receipt!");

                                string pick = FirstBetListingInOrder.Pick.Team1 + " vs " + FirstBetListingInOrder.Pick.Team2;
                                string schedule = DateHelper.DateFormat(FirstBetListingInOrder.Pick.StartTime.FromUTCData(TimezoneOffset));
                                bool hasButton = model.Items.Count > 1;
                                var listingDetailsEmail = await emailSenderService.SendToListingsInOrder(user.Email, user.FirstName, FirstBetListingInOrder.Title, FirstBetListingInOrder.PickType, pick, FirstBetListingInOrder.Odds, FirstBetListingInOrder.Stake, FirstBetListingInOrder.Profit, FirstBetListingInOrder.Description, schedule, FirstBetListingInOrder.Bookmaker.Description, FirstBetListingInOrder.Tip.Description, FirstBetListingInOrder.Analysis, FirstBetListingInOrder.Price.ToString("0.00"), hasButton, newOrder.Id.ToString());

                                if (!listingDetailsEmail) throw new Exception("Error sending email with listing details!");

                                return Json(new { status = true, messageTitle = "Success!", message = "Purchase completed successfully" });
                            }
                            else throw new Exception("Order placed, but failed on email receipt!");
                        }
                        else throw new Exception("Failure trying to place new order!");

                    }
                    else
                    {
                        throw new Exception("Your wallet doesn't have enough funds to proceed by wallet checkout.");
                    }

                }
                else throw new Exception("No items in cart, checkout not allowed!");



            }
            catch (Exception e)
            {
                await errorLogService.InsertException(e, UserId).ConfigureAwait(true);
                return Json(new { status = false, messageTitle = "Error", message = e.Message });
            }
        }


        [Permitted(Permission.Capper, Permission.Bettor)]
        [HttpPost]
        public async Task<IActionResult> CheckoutByPaypal(string orderID, int TimezoneOffset = 0)
        {
            Guid? UserId = null;
            RequestFeedback request = new RequestFeedback();
            try
            {
                UserId = User.Id();
                if (!UserId.HasValue) throw new Exception("No user to checkout.");



                User user = await userService.GetById(UserId.Value).ConfigureAwait(true);
                ShoppingCart cart = await shoppingCartService.GetByUserId(UserId.Value).ConfigureAwait(true);
                var itemsInCart = (await shoppingCartService.GetOnlyValidItemsInCart(UserId.Value).ConfigureAwait(true)).OrderBy(o => o.SoldBy).ToList();
                bool canCheckout = true;
                if (itemsInCart == null || itemsInCart.Count <= 0) canCheckout = false;
                ShoppingCartDto model = new ShoppingCartDto
                {
                    EstimatedTax = Convert.ToDecimal((await systemConstantsService.GetByName(SystemConstantName.Taxes).ConfigureAwait(true)).Value),
                    ServiceFee = Convert.ToDecimal((await systemConstantsService.GetByName(SystemConstantName.RegularFees).ConfigureAwait(true)).Value),
                    TotalAfterTax = 0.0m,
                    TotalBeforeTaxAndFees = 0.0m,
                    Items = itemsInCart,
                    CanCheckout = canCheckout,
                    Discount = 0.00m,
                    DiscountPercent = 0.00m
                };
                string OrderType = OrderTypes.Regular;
                var activeBoisterousPlan = await userSubscriptionService.GetActiveSubscriptionPlan(UserId.Value, SubscriptionPlanType.Boisterous).ConfigureAwait(true);

                if (activeBoisterousPlan != null)
                {
                    OrderType = OrderTypes.Boisterous;
                    model.ServiceFee = Convert.ToDecimal((await systemConstantsService.GetByName(SystemConstantName.BoisterousFees).ConfigureAwait(true)).Value);
                }

                model.TotalBeforeDiscount = model.Items.Sum(i => i.Price);

                var activeDiscount = await userDiscountService.GetActiveByUserId(UserId.Value).ConfigureAwait(true);
                if (activeDiscount != null)
                {
                    model.DiscountPercent = activeDiscount.DiscountPercentageValue;
                    model.Discount = (model.DiscountPercent / 100) * model.TotalBeforeDiscount;
                }

                model.TotalBeforeTaxAndFees = model.TotalBeforeDiscount - model.Discount;
                model.EstimatedTaxPercent = model.EstimatedTax;
                model.ServiceFeePercent = model.ServiceFee;
                model.WalletFunds = user.WalletBalance;
                decimal taxes = model.TotalBeforeTaxAndFees * (model.EstimatedTax / 100.0m);
                decimal fees = model.TotalBeforeTaxAndFees * (model.ServiceFee / 100.0m);
                model.TotalAfterTax = model.TotalBeforeTaxAndFees + taxes + fees;
                model.EstimatedTax = taxes;
                model.ServiceFee = fees;
                model.WalletOrderDifference = user.WalletBalance - model.TotalAfterTax;
                model.WalletBalanceAfterPurchase = model.WalletOrderDifference;
                if (model.WalletOrderDifference < 0)
                {
                    model.HasEnoughFunds = false;
                    model.FundsNeeded = model.WalletOrderDifference * -1.0m;
                    model.WalletBalanceAfterPurchase = model.WalletBalanceAfterPurchase * -1.0m;
                }
                else
                {
                    model.HasEnoughFunds = true;
                    model.FundsNeeded = 0.00m;
                }

                if (canCheckout)
                {
                    if (!model.HasEnoughFunds)
                    {
                        OrdersGetRequest paypalRequest = new OrdersGetRequest(orderID);
                        //3. Call PayPal to get the transaction
                        PaypalSettings paypalSettings = await paypalSettingsService.GetDefault().ConfigureAwait(true);
                        string ClientId = paypalSettings.ClientID;
                        string Secret = paypalSettings.Secret;
                        var paypalResponse = await PayPalClient.client(ClientId, Secret).Execute(paypalRequest).ConfigureAwait(false);
                        //4. Save the transaction in your database. Implement logic to save transaction to your database for future reference.
                        var paypalResult = paypalResponse.Result<PayPalCheckoutSdk.Orders.Order>();


                        string paypalOrderStatus = paypalResult.Status;
                        string PaymentStatus = null;
                        DateTime? DatePaid = null;
                        DateTime? DateRejected = null;
                        if (paypalOrderStatus == PaypalPaymentStatus.COMPLETED)
                        {
                            DatePaid = DateTime.UtcNow;
                            PaymentStatus = OrderPaymentStatus.Paid;
                        }
                        else
                        {
                            if (paypalOrderStatus == PaypalPaymentStatus.VOIDED)
                            {
                                DateRejected = DateTime.UtcNow;
                                PaymentStatus = OrderPaymentStatus.Rejected;
                            }
                            else
                            {
                                PaymentStatus = OrderPaymentStatus.Pending;
                            }
                        }
                        Shortchase.Entities.Order newOrder = new Shortchase.Entities.Order
                        {
                            Id = Guid.NewGuid(),
                            CancelledReason = null,
                            RejectedReason = null,
                            DateCancelled = null,
                            DatePaid = DatePaid,
                            DateRejected = DateRejected,
                            RowDate = DateTime.UtcNow,
                            CapperComission = 0.0m,
                            EstimatedTax = model.EstimatedTax,
                            EstimatedTaxPercent = model.EstimatedTaxPercent,
                            OrderType = OrderType,
                            PaymentType = OrderPaymentType.Paypal,
                            ServiceFee = model.ServiceFee,
                            ServiceFeePercent = model.ServiceFeePercent,
                            UserId = UserId.Value,
                            WalletBalanceBeforePurchase = model.WalletFunds,
                            WalletBalanceAfterPurchase = 0.00m,
                            TotalBeforeTaxAndFees = model.TotalBeforeTaxAndFees,
                            TotalAfterTax = model.TotalAfterTax,
                            PaymentStatus = PaymentStatus,
                            Discount = model.Discount,
                            DiscountPercent = model.DiscountPercent,
                            TotalBeforeDiscount = model.TotalBeforeDiscount,
                            PaypalOrderId = orderID,
                            PaypalOrderStatus = paypalOrderStatus,
                            TotalPaidOnPaypal = model.WalletBalanceAfterPurchase,
                            OrderNumber =
                                DateTime.UtcNow.Year.ToString()
                                + DateTime.UtcNow.Month.ToString()
                                + DateTime.UtcNow.Day.ToString()
                                + DateTime.UtcNow.Hour.ToString()
                                + DateTime.UtcNow.Minute.ToString()
                                + DateTime.UtcNow.Second.ToString()
                                + (await orderService.CountTotal().ConfigureAwait(true)).ToString()
                        };
                        ICollection<OrderItem> orderItems = new List<OrderItem>();
                        BetListing FirstBetListingInOrder = await betListingService.GetById(model.Items.First().ListingId).ConfigureAwait(true);
                        foreach (var item in model.Items)
                        {
                            OrderItem orderItem = new OrderItem
                            {
                                BetListingId = item.ListingId,
                                ListingTitle = item.ListingTitle,
                                OrderId = newOrder.Id,
                                Price = item.Price,
                                SoldBy = item.SoldBy,
                                RowDate = DateTime.UtcNow
                            };
                            orderItems.Add(orderItem);
                        }

                        var result = await orderService.PlaceNewOrder(newOrder, orderItems, cart, user).ConfigureAwait(true);
                        if (result)
                        {
                            bool receiptPDFresult = await CreateReceiptPDF(newOrder.Id, TimezoneOffset).ConfigureAwait(true);
                            if (receiptPDFresult)
                            {
                                string message = "Bettor " + user.FirstName + " bought your pick.";
                                ICollection<Guid> usersToNotify = (await betListingService.GetAll().ConfigureAwait(true)).Select(i => i.PostedbyId).ToList();
                                foreach (var item in model.Items)
                                {
                                    var resultNotification = await notificationService.Insert(item.SoldById, message).ConfigureAwait(true);
                                    if (!resultNotification) throw new Exception("Could not save new notification");
                                }


                                if (!string.IsNullOrWhiteSpace(user.ReferredByEmail))
                                {
                                    int countOrdersByUser = await orderService.CountTotalByUser(user.Id).ConfigureAwait(true);
                                    if (countOrdersByUser <= 1)
                                    {
                                        User userToGetDiscount = await userService.GetByEmail(user.ReferredByEmail).ConfigureAwait(true);
                                        if (userToGetDiscount != null)
                                        {
                                            UserDiscount newDiscount = new UserDiscount
                                            {
                                                DateUsed = null,
                                                OriginUserEmail = user.Email,
                                                DiscountPercentageValue = DiscountValues.Standard,
                                                UserId = userToGetDiscount.Id
                                            };
                                            var discountResult = await userDiscountService.Insert(newDiscount).ConfigureAwait(true);
                                            if (!discountResult) throw new Exception("Error saving discount for referred user!  ");


                                            var discountEmailResult = await emailSenderService.SendReferralDiscount(userToGetDiscount.Email, userToGetDiscount.FirstName, newDiscount.DiscountPercentageValue.ToString("0.00")).ConfigureAwait(true);

                                            if (!discountEmailResult) throw new Exception("Error sending discount email for referred user!  ");
                                        }

                                    }
                                }

                                if (activeDiscount != null)
                                {
                                    activeDiscount.DateUsed = DateTime.UtcNow;
                                    var activeDiscountResult = await userDiscountService.Update(activeDiscount).ConfigureAwait(true);
                                    if (!activeDiscountResult) throw new Exception("Error updating discount for referred user!  ");
                                }


                                string root = hostingEnvironment.ContentRootPath;
                                string receiptPdf = root + newOrder.ReceiptPDF;
                                var orderReceiptEmail = await emailSenderService.SendUserBetListingOrder(user.Email, user.FirstName, newOrder, orderItems, receiptPdf, TimezoneOffset).ConfigureAwait(true);

                                if (!orderReceiptEmail) throw new Exception("Error sending email with order receipt!");

                                string pick = FirstBetListingInOrder.Pick.Team1 + " vs " + FirstBetListingInOrder.Pick.Team2;
                                string schedule = DateHelper.DateFormat(FirstBetListingInOrder.Pick.StartTime.FromUTCData(TimezoneOffset));
                                bool hasButton = model.Items.Count > 1;
                                var listingDetailsEmail = await emailSenderService.SendToListingsInOrder(user.Email, user.FirstName, FirstBetListingInOrder.Title, FirstBetListingInOrder.PickType, pick, FirstBetListingInOrder.Odds, FirstBetListingInOrder.Stake, FirstBetListingInOrder.Profit, FirstBetListingInOrder.Description, schedule, FirstBetListingInOrder.Bookmaker.Description, FirstBetListingInOrder.Tip.Description, FirstBetListingInOrder.Analysis, FirstBetListingInOrder.Price.ToString("0.00"), hasButton, newOrder.Id.ToString());

                                if (!listingDetailsEmail) throw new Exception("Error sending email with listing details!");

                                return Json(new { status = true, messageTitle = "Success!", message = "Purchase completed successfully" });
                            }
                            else throw new Exception("Order placed, but failed emailing receipt.");

                        }
                        else throw new Exception("Failure trying to place new order!");

                    }
                    else
                    {
                        throw new Exception("Your wallet have enough funds to proceed by wallet checkout.");
                    }

                }
                else throw new Exception("No items in cart, checkout not allowed!");



            }
            catch (Exception e)
            {
                await errorLogService.InsertException(e, UserId).ConfigureAwait(true);
                return Json(new { status = false, messageTitle = "Error", message = e.Message });
            }
        }

        #endregion

        #region Subscription Manager

        [Permitted(Permission.Bettor, Permission.Capper)]
        public async Task<IActionResult> Subscription(string Plan, int TimeOffset = 0)
        {
            ViewData["TimezoneOffset"] = TimeOffset;
            Guid? UserId = null;
            RequestFeedback request = new RequestFeedback();
            try
            {
                UserId = User.Id();
                if (!UserId.HasValue) throw new Exception("No user to subscribe to plan.");
                if (string.IsNullOrWhiteSpace(Plan)) throw new Exception("No plan specified.");

                if (User.Identity.HasAnyPermissions(Permission.Capper))
                {
                    Plan = SubscriptionPlanType.ShortchasePro;
                }
                else if (User.Identity.HasAnyPermissions(Permission.Bettor))
                {
                    Plan = SubscriptionPlanType.Boisterous;
                }
                else throw new Exception("User has no permission.");
                WebsiteUserSubscriptionPlanDto model = new WebsiteUserSubscriptionPlanDto
                {
                    Plan = Plan
                };

                UserSubscription activePlan = await userSubscriptionService.GetActiveSubscriptionPlan(UserId.Value, Plan).ConfigureAwait(true);
                if (activePlan == null)
                {
                    model.CanSubscribe = true;
                    model.SubscriptionPlans = await subscriptionPlanService.GetAllFromType(Plan, true).ConfigureAwait(true);
                }
                else
                {
                    model.CanSubscribe = false;
                    model.SubscriptionPlans = new List<SubscriptionPlan>();
                }


                return View(model);
            }
            catch (Exception e)
            {
                await errorLogService.InsertException(e, UserId).ConfigureAwait(true);
                return RedirectToAction("Index", "Error", request);
            }
        }


        [Permitted(Permission.Bettor, Permission.Capper)]
        public async Task<IActionResult> SubscriptionConfirmation(int PlanId = 0, int TimeOffset = 0)
        {
            ViewData["TimezoneOffset"] = TimeOffset;
            Guid? UserId = null;
            RequestFeedback request = new RequestFeedback();
            try
            {
                UserId = User.Id();
                if (!UserId.HasValue) throw new Exception("No user to subscribe to plan.");
                if (PlanId == 0) throw new Exception("No plan specified.");
                SubscriptionPlan plan = await subscriptionPlanService.GetById(PlanId).ConfigureAwait(true);
                User user = await userService.GetById(UserId.Value).ConfigureAwait(true);

                SystemConstants regularFees = (await systemConstantsService.GetByName(SystemConstantName.RegularFees).ConfigureAwait(true));
                SystemConstants taxes = (await systemConstantsService.GetByName(SystemConstantName.Taxes).ConfigureAwait(true));

                if (regularFees == null || taxes == null)
                {
                    if (regularFees == null)
                    {
                        regularFees = new SystemConstants
                        {
                            Name = SystemConstantName.RegularFees,
                            Value = "15.00",
                            RowDate = DateTime.UtcNow,
                            Type = SystemConstantType.Decimal
                        };
                        var resultFees = await systemConstantsService.Insert(regularFees).ConfigureAwait(true);
                        if (!resultFees) throw new Exception("Service Fee could not be saved");
                    }
                    if (taxes == null)
                    {
                        taxes = new SystemConstants
                        {
                            Name = SystemConstantName.Taxes,
                            Value = "15.00",
                            RowDate = DateTime.UtcNow,
                            Type = SystemConstantType.Decimal
                        };
                        var resultTaxes = await systemConstantsService.Insert(taxes).ConfigureAwait(true);
                        if (!resultTaxes) throw new Exception("Estimated Taxes could not be saved");
                    }
                }




                WebsiteUserConfirmSubscriptionPlanDto model = new WebsiteUserConfirmSubscriptionPlanDto
                {
                    SubscriptionPlan = plan,
                    WalletFundsBefore = 0.00m,//user.WalletBalance,
                    WalletFundsAfter = 0.00m,
                    HaveEnoughWalletFunds = false,
                    StartDate = DateTime.UtcNow,
                    EndDate = DateTime.UtcNow.AddMonths(plan.DurationInMonths),
                    FeePercent = Convert.ToDecimal(regularFees.Value),
                    TaxPercent = Convert.ToDecimal(taxes.Value),
                };
                model.FeeValue = (model.FeePercent / 100.00m) * plan.TotalValuePerDuration;
                model.TaxValue = (model.TaxPercent / 100.00m) * plan.TotalValuePerDuration;
                model.WalletFundsAfter = user.WalletBalance;// - plan.TotalValuePerDuration;
                model.HaveEnoughWalletFunds = false;
                /*if (user.WalletBalance >= plan.TotalValuePerDuration)
                {
                    model.HaveEnoughWalletFunds = true;
                }
                else
                {
                    model.HaveEnoughWalletFunds = false;
                    model.WalletFundsAfter = model.WalletFundsAfter * (-1.0m);
                }*/

                UserSubscription activePlan = await userSubscriptionService.GetActiveSubscriptionPlan(UserId.Value, plan.Type).ConfigureAwait(true);
                if (activePlan == null)
                {
                    model.CanSubscribe = true;
                }
                else
                {
                    model.CanSubscribe = false;
                }


                return View(model);
            }
            catch (Exception e)
            {
                await errorLogService.InsertException(e, UserId).ConfigureAwait(true);
                return RedirectToAction("Index", "Error", request);
            }
        }

        [Permitted(Permission.Bettor, Permission.Capper)]
        [HttpPost]
        public async Task<IActionResult> FinishSubscriptionWallet(WebsiteCreateUserSubscriptionDto data)
        {
            Guid? UserId = null;
            try
            {
                UserId = User.Id();
                if (!UserId.HasValue) throw new Exception("No user to subscribe to plan.");
                if (data.SubscriptionId == 0) throw new Exception("You need to provide a Start Date for the new subscription.");
                User user = await userService.GetById(UserId.Value).ConfigureAwait(true);

                DateTime startDate = DateTime.UtcNow;

                SubscriptionPlan chosenPlan = await subscriptionPlanService.GetById(data.SubscriptionId).ConfigureAwait(true);

                if (user.WalletBalance >= chosenPlan.TotalValuePerDuration)
                {
                    DateTime endDate = startDate.AddMonths(chosenPlan.DurationInMonths);
                    decimal newBalance = user.WalletBalance - chosenPlan.TotalValuePerDuration;


                    string subscriptionName = "";
                    if (chosenPlan.Type == SubscriptionPlanType.ShortchasePro)
                    {
                        subscriptionName = "Shortchase Pro";
                    }
                    else
                    {

                        subscriptionName = "Boisterous " + chosenPlan.Name;
                    }
                    UserSubscription newSubscription = new UserSubscription
                    {
                        UserId = UserId.Value,
                        GiftById = null,
                        SubscriptionId = data.SubscriptionId,
                        Deleted = false,
                        Name = subscriptionName,
                        PaidValue = chosenPlan.TotalValuePerDuration,
                        PaymentStatus = UserSubscriptionPaymentStatus.Wallet,
                        Type = chosenPlan.Type,
                        SubscriptionPrice = chosenPlan.TotalValuePerDuration,
                        SubscriptionStart = startDate.Date,
                        SubscriptionEnd = endDate.Date,
                        WalletBalanceBeforePurchase = user.WalletBalance,
                        WalletBalanceAfterPurchase = user.WalletBalance,
                        PaypalOrderId = null,
                        DateCancelled = null,
                        DatePaid = null,
                        DateRejected = null,
                        PaypalOrderStatus = null,
                        TotalPaidOnPaypal = 0.00m
                    };


                    SubscriptionReceiptEmail receiptData = new SubscriptionReceiptEmail
                    {
                        WalletBalanceAfter = user.WalletBalance.ToString("0.00"),
                        WalletBalanceBefore = user.WalletBalance.ToString("0.00"),
                        PaypalPaidValue = "0.00",
                        PaymentStatus = UserSubscriptionPaymentStatus.Wallet,
                        PaymentType = "Wallet",
                        PaidValue = chosenPlan.TotalValuePerDuration.ToString("0.00"),
                        SubscriptionName = subscriptionName,
                        SubscriptionPrice = chosenPlan.TotalValuePerDuration.ToString("0.00"),
                        Start = DateHelper.DateSimpleFormat(startDate.FromUTCData(data.TimezoneOffset), true),
                        End = DateHelper.DateSimpleFormat(endDate.FromUTCData(data.TimezoneOffset), true),
                        Description = chosenPlan.Name,
                        Qty = chosenPlan.DurationInMonths.ToString()
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
                            /*user.WalletBalance = newBalance;
                            var resultWallet = await userService.UpdateAsync(user).ConfigureAwait(true);
                            if (!resultWallet) throw new Exception("Error updating user wallet balance!");*/

                            bool receiptGeneration = await CreateSubscriptionReceiptPDF(receiptData, newSubscription, data.TimezoneOffset).ConfigureAwait(true);
                            if (!receiptGeneration) throw new Exception("Failure generating receipt PDF!");
                            string root = hostingEnvironment.ContentRootPath;
                            string receiptPdf = root + newSubscription.ReceiptPDF;

                            var resultEmailReceipt = await emailSenderService.SendUserSubscriptionOrder(user.Email, user.FirstName, receiptData.SubscriptionName, receiptData.SubscriptionPrice, receiptData.PaidValue, receiptData.PaymentStatus, receiptData.Start, receiptData.End, receiptData.WalletBalanceBefore, receiptData.WalletBalanceAfter, receiptData.PaymentType, receiptPdf).ConfigureAwait(true);

                            if (!resultEmailReceipt) throw new Exception("Error sending email with receipt!");
                            return Json(new { status = true, messageTitle = "Success!", message = "Subscription started successfully." });
                        }
                        else
                        {
                            throw new Exception("Could not save new subscription");
                        }
                    }
                }
                else
                {
                    throw new Exception("Not enough funds on wallet!");
                }



            }
            catch (Exception e)
            {
                ModelState.AddModelError(string.Empty, "Something went wrong, please try again later");
                await errorLogService.InsertException(e).ConfigureAwait(true);
                return Json(new { status = false, messageTitle = "Error", message = e.Message });
            }
        }


        [Permitted(Permission.Bettor, Permission.Capper)]
        [HttpPost]
        public async Task<IActionResult> CancelUserSubscriptionFrontend(Guid Id)
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



        [Permitted(Permission.Bettor, Permission.Capper)]
        [HttpPost]
        public async Task<IActionResult> CancelUserRenewSubscriptionFrontend(Guid Id)
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

        //[Permitted(Permission.Bettor, Permission.Capper)]
        //[HttpPost]
        //public async Task<IActionResult> FinishSubscriptionPaypal(WebsiteCreateUserPaypalSubscriptionDto data)
        //{
        //    Guid? UserId = null;
        //    try
        //    {
        //        UserId = User.Id();
        //        if (!UserId.HasValue) throw new Exception("No user to subscribe to plan.");
        //        if (data.SubscriptionId == 0) throw new Exception("You need to provide a Start Date for the new subscription.");
        //        if (string.IsNullOrWhiteSpace(data.orderID)) throw new Exception("You need to provide a order for the new subscription.");
        //        User user = await userService.GetById(UserId.Value).ConfigureAwait(true);

        //        DateTime startDate = DateTime.UtcNow;

        //        SubscriptionPlan chosenPlan = await subscriptionPlanService.GetById(data.SubscriptionId).ConfigureAwait(true);

        //        if (user.WalletBalance < chosenPlan.TotalValuePerDuration)
        //        {
        //            OrdersGetRequest paypalRequest = new OrdersGetRequest(data.orderID);
        //            //3. Call PayPal to get the transaction
        //            PaypalSettings paypalSettings = await paypalSettingsService.GetDefault().ConfigureAwait(true);
        //            string ClientId = paypalSettings.ClientID;
        //            string Secret = paypalSettings.Secret;
        //            var paypalResponse = await PayPalClient.client(ClientId, Secret).Execute(paypalRequest);
        //            //4. Save the transaction in your database. Implement logic to save transaction to your database for future reference.
        //            var paypalResult = paypalResponse.Result<PayPalCheckoutSdk.Orders.Order>();


        //            string paypalOrderStatus = paypalResult.Status;
        //            string PaymentStatus = null;
        //            DateTime? DatePaid = null;
        //            DateTime? DateRejected = null;
        //            if (paypalOrderStatus == PaypalPaymentStatus.COMPLETED)
        //            {
        //                DatePaid = DateTime.UtcNow;
        //                PaymentStatus = UserSubscriptionPaymentStatus.Paid;
        //            }
        //            else
        //            {
        //                if (paypalOrderStatus == PaypalPaymentStatus.VOIDED)
        //                {
        //                    DateRejected = DateTime.UtcNow;
        //                    PaymentStatus = UserSubscriptionPaymentStatus.Rejected;
        //                }
        //                else
        //                {
        //                    PaymentStatus = UserSubscriptionPaymentStatus.Pending;
        //                }
        //            }
        //            DateTime endDate = startDate.AddMonths(chosenPlan.DurationInMonths);
        //            decimal newBalance = -chosenPlan.TotalValuePerDuration;//user.WalletBalance - chosenPlan.TotalValuePerDuration;
        //            decimal totalPaidOnPaypal = newBalance;
        //            if (totalPaidOnPaypal < 0.00m)
        //            {
        //                totalPaidOnPaypal = totalPaidOnPaypal * (-1.00m);
        //            }
        //            if (newBalance >= 0.00m)
        //            {
        //                newBalance = newBalance;
        //            }
        //            else
        //            {
        //                newBalance = 0.00m;
        //            }

        //            string subscriptionName = "";
        //            if (chosenPlan.Type == SubscriptionPlanType.ShortchasePro)
        //            {
        //                subscriptionName = "Shortchase Pro";
        //            }
        //            else
        //            {

        //                subscriptionName = "Boisterous " + chosenPlan.Name;
        //            }
        //            UserSubscription newSubscription = new UserSubscription
        //            {
        //                UserId = UserId.Value,
        //                GiftById = null,
        //                SubscriptionId = data.SubscriptionId,
        //                Deleted = false,
        //                Name = subscriptionName,
        //                PaidValue = chosenPlan.TotalValuePerDuration,
        //                PaymentStatus = PaymentStatus,
        //                Type = chosenPlan.Type,
        //                SubscriptionPrice = chosenPlan.TotalValuePerDuration,
        //                SubscriptionStart = startDate.Date,
        //                SubscriptionEnd = endDate.Date,
        //                WalletBalanceBeforePurchase = user.WalletBalance,
        //                WalletBalanceAfterPurchase = user.WalletBalance,
        //                PaypalOrderId = data.orderID,
        //                DateCancelled = null,
        //                DatePaid = DatePaid,
        //                DateRejected = DateRejected,
        //                PaypalOrderStatus = paypalOrderStatus,
        //                TotalPaidOnPaypal = totalPaidOnPaypal

        //            };
        //            SubscriptionReceiptEmail receiptData = new SubscriptionReceiptEmail
        //            {
        //                WalletBalanceAfter = user.WalletBalance.ToString("0.00"),
        //                WalletBalanceBefore = user.WalletBalance.ToString("0.00"),
        //                PaypalPaidValue = totalPaidOnPaypal.ToString("0.00"),
        //                PaymentStatus = PaymentStatus,
        //                PaymentType = "Paypal",
        //                PaidValue = chosenPlan.TotalValuePerDuration.ToString("0.00"),
        //                SubscriptionName = subscriptionName,
        //                SubscriptionPrice = chosenPlan.TotalValuePerDuration.ToString("0.00"),
        //                Start = DateHelper.DateSimpleFormat(startDate.FromUTCData(data.TimezoneOffset), true),
        //                End = DateHelper.DateSimpleFormat(endDate.FromUTCData(data.TimezoneOffset), true),
        //                Description = chosenPlan.Name,
        //                Qty = chosenPlan.DurationInMonths.ToString()
        //            };

        //            var UserHasActiveSubscriptionPlan = await userSubscriptionService.UserHasActiveSubscriptionPlan(newSubscription).ConfigureAwait(true);

        //            if (UserHasActiveSubscriptionPlan)
        //            {
        //                throw new Exception("User has active plan of the same type!");
        //            }
        //            else
        //            {
        //                var result = await userSubscriptionService.Insert(newSubscription).ConfigureAwait(true);
        //                if (result)
        //                {
        //                    /*user.WalletBalance = newBalance;
        //                    var resultWallet = await userService.UpdateAsync(user).ConfigureAwait(true);
        //                    if (!resultWallet) throw new Exception("Error updating user wallet balance!");*/


        //                    bool receiptGeneration = await CreateSubscriptionReceiptPDF(receiptData, newSubscription, data.TimezoneOffset).ConfigureAwait(true);
        //                    if (!receiptGeneration) throw new Exception("Failure generating receipt PDF!");
        //                    string root = hostingEnvironment.ContentRootPath;
        //                    string receiptPdf = root + newSubscription.ReceiptPDF;

        //                    var resultEmailReceipt = await emailSenderService.SendUserSubscriptionOrderPaypal(user.Email, user.FirstName, receiptData.SubscriptionName, receiptData.SubscriptionPrice, receiptData.PaidValue, receiptData.PaymentStatus, receiptData.Start, receiptData.End, receiptData.WalletBalanceBefore, receiptData.WalletBalanceAfter, receiptData.PaymentType, receiptData.PaypalPaidValue, receiptPdf).ConfigureAwait(true);

        //                    if (!resultEmailReceipt) throw new Exception("Error sending email with receipt!");
        //                    return Json(new { status = true, messageTitle = "Success!", message = "Subscription started successfully." });
        //                }
        //                else
        //                {
        //                    throw new Exception("Could not save new subscription");
        //                }
        //            }
        //        }
        //        else
        //        {
        //            throw new Exception("Enough funds on wallet!");
        //        }



        //    }
        //    catch (Exception e)
        //    {
        //        ModelState.AddModelError(string.Empty, "Something went wrong, please try again later");
        //        await errorLogService.InsertException(e).ConfigureAwait(true);
        //        return Json(new { status = false, messageTitle = "Error", message = e.Message });
        //    }
        //}



        [Permitted(Permission.Bettor, Permission.Capper)]
        [HttpPost]
        public async Task<IActionResult> FinishSubscriptionPaypal2(WebsiteCreateUserPaypalSubscription2Dto data)
        {
            Guid? UserId = null;
            try
            {
                UserId = User.Id();
                if (!UserId.HasValue) throw new Exception("No user to subscribe to plan.");
                if (data.SubscriptionId == 0) throw new Exception("You need to provide a Start Date for the new subscription.");
                if (string.IsNullOrWhiteSpace(data.OrderId)) throw new Exception("You need to provide a order for the new subscription.");
                User user = await userService.GetById(UserId.Value).ConfigureAwait(true);

                DateTime startDate = DateTime.UtcNow;

                SubscriptionPlan chosenPlan = await subscriptionPlanService.GetById(data.SubscriptionId).ConfigureAwait(true);

                //3. Call PayPal to get the transaction
                PaypalSettings paypalSettings = await paypalSettingsService.GetDefault().ConfigureAwait(true);
                string ClientId = paypalSettings.ClientID;
                string Secret = paypalSettings.Secret;


                var requestPaypal = await PayPalClient.RequestPayPalToken(ClientId, Secret).ConfigureAwait(true);
                if (requestPaypal == null) throw new Exception("No token obtained");
                string paypalToken = requestPaypal.access_token;

                var responseFromPaypalAPI = await PayPalClient.RequestPayPalVerifySubscription(paypalToken, data.PaypalSubscriptionId).ConfigureAwait(true);

                dynamic parsedResponse = JsonConvert.DeserializeObject(responseFromPaypalAPI);



                string paypalOrderStatus = parsedResponse.status;
                string PaymentStatus = null;
                DateTime? DatePaid = null;
                DateTime? DateRejected = null;
                if (paypalOrderStatus == PaypalSubscriptionPaymentStatus.ACTIVE)
                {
                    DatePaid = DateTime.UtcNow;
                    PaymentStatus = UserSubscriptionPaymentStatus.Paid;
                }
                else
                {
                    if (paypalOrderStatus == PaypalSubscriptionPaymentStatus.CANCELLED || paypalOrderStatus == PaypalSubscriptionPaymentStatus.SUSPENDED || paypalOrderStatus == PaypalSubscriptionPaymentStatus.EXPIRED)
                    {
                        DateRejected = DateTime.UtcNow;
                        PaymentStatus = UserSubscriptionPaymentStatus.Rejected;
                    }
                    else
                    {
                        PaymentStatus = UserSubscriptionPaymentStatus.Pending;
                    }
                }
                DateTime endDate = startDate.AddMonths(chosenPlan.DurationInMonths);
                decimal totalPaidOnPaypal = chosenPlan.TotalValuePerDuration;


                string subscriptionName = "";
                if (chosenPlan.Type == SubscriptionPlanType.ShortchasePro)
                {
                    subscriptionName = "Shortchase Pro";
                }
                else
                {
                    subscriptionName = "Boisterous " + chosenPlan.Name;
                }
                UserSubscription newSubscription = new UserSubscription
                {
                    UserId = UserId.Value,
                    GiftById = null,
                    SubscriptionId = data.SubscriptionId,
                    Deleted = false,
                    Name = subscriptionName,
                    PaidValue = chosenPlan.TotalValuePerDuration,
                    PaymentStatus = PaymentStatus,
                    Type = chosenPlan.Type,
                    SubscriptionPrice = chosenPlan.TotalValuePerDuration,
                    SubscriptionStart = startDate.Date,
                    SubscriptionEnd = endDate.Date,
                    WalletBalanceBeforePurchase = user.WalletBalance,
                    WalletBalanceAfterPurchase = user.WalletBalance,
                    PaypalOrderId = data.OrderId,
                    DateCancelled = null,
                    DatePaid = DatePaid,
                    DateRejected = DateRejected,
                    PaypalOrderStatus = paypalOrderStatus,
                    TotalPaidOnPaypal = totalPaidOnPaypal,
                    AutoRenew = true,
                    PaypalSubscriptionId = data.PaypalSubscriptionId,
                    PaypalFacilitatorAccessToken = data.FacilitatorAccessToken,
                    HasBeenAutoRenewed = false

                };

                SubscriptionReceiptEmail receiptData = new SubscriptionReceiptEmail
                {
                    WalletBalanceAfter = user.WalletBalance.ToString("0.00"),
                    WalletBalanceBefore = user.WalletBalance.ToString("0.00"),
                    PaypalPaidValue = totalPaidOnPaypal.ToString("0.00"),
                    PaymentStatus = PaymentStatus,
                    PaymentType = "Paypal",
                    PaidValue = chosenPlan.TotalValuePerDuration.ToString("0.00"),
                    SubscriptionName = subscriptionName,
                    SubscriptionPrice = chosenPlan.TotalValuePerDuration.ToString("0.00"),
                    Start = DateHelper.DateSimpleFormat(startDate.FromUTCData(data.TimezoneOffset), true),
                    End = DateHelper.DateSimpleFormat(endDate.FromUTCData(data.TimezoneOffset), true),
                    Description = chosenPlan.Name,
                    Qty = chosenPlan.DurationInMonths.ToString()
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


                        bool receiptGeneration = await CreateSubscriptionReceiptPDF(receiptData, newSubscription, data.TimezoneOffset).ConfigureAwait(true);
                        if (!receiptGeneration) throw new Exception("Failure generating receipt PDF!");
                        string root = hostingEnvironment.ContentRootPath;
                        string receiptPdf = root + newSubscription.ReceiptPDF;

                        var resultEmailReceipt = await emailSenderService.SendUserSubscriptionOrderPaypal(user.Email, user.FirstName, receiptData.SubscriptionName, receiptData.SubscriptionPrice, receiptData.PaidValue, receiptData.PaymentStatus, receiptData.Start, receiptData.End, receiptData.WalletBalanceBefore, receiptData.WalletBalanceAfter, receiptData.PaymentType, receiptData.PaypalPaidValue, receiptPdf).ConfigureAwait(true);

                        if (!resultEmailReceipt) throw new Exception("Error sending email with receipt!");
                        return Json(new { status = true, messageTitle = "Success!", message = "Subscription started successfully." });
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


        #endregion


        #endregion


        #region Home General Functions
        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> Messaging(Guid id,int TimeOffset = 0)
        {
            Guid? UserId = null;
            ViewData["root"] = hostingEnvironment.ContentRootPath;
            ViewData["TimezoneOffset"] = TimeOffset;
            UserId = User.Id();
            RequestFeedback request = new RequestFeedback();
            try
            {
                var user = (await userService.GetProfileById(id, UserId).ConfigureAwait(true));
                return View(user);
            }
            catch (Exception e)
            {
                await errorLogService.InsertException(e).ConfigureAwait(true);
                return null;
            }
        }

        [Permitted(Permission.Capper, Permission.Bettor)]
        [HttpPost]
        public async Task<IActionResult> SendMessage(string MessageContent, int TimezoneOffset = 0)
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
                    ToId = UserId.Value,
                    DateRead = null,
                    Content = MessageContent
                };

                var result = await messageService.Insert(newMessage).ConfigureAwait(true);
                if (!result) throw new Exception("Message could not be sent!");

                var AllMessages = await messageService.GetAllForUser(UserId.Value).ConfigureAwait(true);
                var allMessages = AllMessages.Select
                        (
                            i =>
                                new MessageRetrievedDto
                                {
                                    Content = i.Content,
                                    DateRead = i.DateRead,
                                    SentById = i.FromId,
                                    SentByName = i.From.FirstName + " " + i.From.LastName,
                                    SentByProfilePicture = !string.IsNullOrWhiteSpace(i.From.ProfilePicture) ? ImageHelper.ConvertImageToBase64(i.From.ProfilePicture) : Url.Content("~/img/avatar.png"),
                                    SentToId = i.ToId,
                                    SentToName = i.To.FirstName + " " + i.To.LastName,
                                    SentToProfilePicture = i.To.ProfilePicture,
                                    DateSent = i.RowDate,
                                    ParsedDateRead = i.DateRead.HasValue ? DateHelper.DateFormat(i.DateRead.Value.FromUTCData(TimezoneOffset)) : null,
                                    ParsedDateSent = DateHelper.DateFormat(i.RowDate.FromUTCData(TimezoneOffset))
                                }
                        )
                    .ToList();
                int unreadMessages = allMessages.Where(i => !i.DateRead.HasValue && i.SentById != UserId.Value).Count();

                string messages = JsonConvert.SerializeObject(allMessages);

                ICollection<Message> updateMessages = new List<Message>();
                if (AllMessages.Count > 0)
                {
                    foreach (var msg in AllMessages)
                    {
                        if (!msg.DateRead.HasValue && msg.FromId != UserId.Value)
                        {
                            msg.DateRead = DateTime.UtcNow;
                            updateMessages.Add(msg);
                        }
                    }

                    var innerResult = await messageService.UpdateBatch(updateMessages).ConfigureAwait(true);
                    if (!innerResult) throw new Exception("Error updating unread messages.");
                }
                return Json(new { status = true, messageTitle = "Success!", message = "Message sent successfully!", messages = messages, unreadmessages = unreadMessages });
            }
            catch (Exception e)
            {
                await errorLogService.InsertException(e, UserId).ConfigureAwait(true);
                return Json(new { status = false, messageTitle = "Error", message = e.Message });
            }
        }


        [Permitted(Permission.Capper, Permission.Bettor)]
        [HttpPost]
        public async Task<IActionResult> RetrieveMessages(int TimezoneOffset = 0)
        {
            Guid? UserId = null;
            RequestFeedback request = new RequestFeedback();
            try
            {
                UserId = User.Id();
                if (!UserId.HasValue) throw new Exception("No user to send message.");
                var AllMessages = await messageService.GetAllForUser(UserId.Value).ConfigureAwait(true);
                var allMessages = AllMessages.Select
                        (
                            i =>
                                new MessageRetrievedDto
                                {
                                    Content = i.Content,
                                    DateRead = i.DateRead,
                                    SentById = i.FromId,
                                    SentByName = i.From.FirstName + " " + i.From.LastName,
                                    SentByProfilePicture = !string.IsNullOrWhiteSpace(i.From.ProfilePicture) ? ImageHelper.ConvertImageToBase64(i.From.ProfilePicture) : Url.Content("~/img/avatar.png"),
                                    SentToId = i.ToId,
                                    SentToName = i.To.FirstName + " " + i.To.LastName,
                                    SentToProfilePicture = i.To.ProfilePicture,
                                    DateSent = i.RowDate,
                                    ParsedDateRead = i.DateRead.HasValue ? DateHelper.DateFormat(i.DateRead.Value.FromUTCData(TimezoneOffset)) : null,
                                    ParsedDateSent = DateHelper.DateFormat(i.RowDate.FromUTCData(TimezoneOffset))
                                }
                        )
                    .ToList();

                int unreadMessages = allMessages.Where(i => !i.DateRead.HasValue && i.SentById != UserId.Value).Count();
                string messages = JsonConvert.SerializeObject(allMessages);


                ICollection<Message> updateMessages = new List<Message>();
                if (AllMessages.Count > 0)
                {
                    foreach (var msg in AllMessages)
                    {
                        if (!msg.DateRead.HasValue && msg.FromId != UserId.Value)
                        {
                            msg.DateRead = DateTime.UtcNow;
                            updateMessages.Add(msg);
                        }
                    }

                    var result = await messageService.UpdateBatch(updateMessages).ConfigureAwait(true);
                    if (!result) throw new Exception("Error updating unread messages.");
                }
                return Json(new { status = true, messageTitle = "Success!", message = "Message retrieved successfully!", messages = messages, unreadmessages = unreadMessages });
            }
            catch (Exception e)
            {
                await errorLogService.InsertException(e, UserId).ConfigureAwait(true);
                return Json(new { status = false, messageTitle = "Error", message = e.Message });
            }
        }



        [Permitted(Permission.Capper, Permission.Bettor)]
        [HttpPost]
        public async Task<IActionResult> ReadAllNotifications(Guid Id)
        {
            Guid? UserId = null;
            RequestFeedback request = new RequestFeedback();
            try
            {
                UserId = User.Id();
                if (!UserId.HasValue) throw new Exception("No user to read notifications.");


                ICollection<Notification> notificationsToUpdate = await notificationService.GetAllNewFromUser(Id).ConfigureAwait(true);

                if (notificationsToUpdate.Count > 0)
                {
                    foreach (var notification in notificationsToUpdate)
                    {
                        notification.DateRead = DateTime.UtcNow;
                        var result = await notificationService.Update(notification).ConfigureAwait(true);
                        if (!result) throw new Exception("Error updating notification");
                    }
                }

                return Json(new { status = true, messageTitle = "Success!", message = "Notifications read successfully!" });
            }
            catch (Exception e)
            {
                await errorLogService.InsertException(e, UserId).ConfigureAwait(true);
                return Json(new { status = false, messageTitle = "Error", message = e.Message });
            }
        }

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> GetDependentDataFromCategory(int Id, int TimezoneOffset = 0)
        {
            try
            {
                ICollection<SubcategoryJSONDto> subcategories = new List<SubcategoryJSONDto>();
                ICollection<MarketJSONDto> markets = new List<MarketJSONDto>();
                ICollection<PickJSONDto> picks = new List<PickJSONDto>();
                subcategories = await listingSubCategoryService.GetAllFromCategory(Id).ConfigureAwait(true);
                markets = await marketService.GetAllByCategoryId(Id).ConfigureAwait(true);
                picks = await pickService.GetAllByCategoryId(Id, TimezoneOffset).ConfigureAwait(true);
                string subcategoriesOptions = JsonConvert.SerializeObject(subcategories);
                string marketsOptions = JsonConvert.SerializeObject(markets);
                string picksOptions = JsonConvert.SerializeObject(picks);

                return Json(new { status = true, messageTitle = "Ok", message = "Ok", subcategories = subcategoriesOptions, markets = marketsOptions, picks = picksOptions });
            }
            catch (Exception e)
            {
                await errorLogService.InsertException(e).ConfigureAwait(true);
                return Json(new { status = false, messageTitle = "Error", message = e.Message });
            }
        }
        [Authorize]
        [HttpPost]
        public async Task<IActionResult> GetDependentDataFromMarket(int Id)
        {
            try
            {
                ICollection<TipsJSONDto> tips = new List<TipsJSONDto>();
                tips = await tipService.GetAllFromMarket(Id).ConfigureAwait(true);
                string tipsOptions = JsonConvert.SerializeObject(tips);

                return Json(new { status = true, messageTitle = "Ok", message = "Ok", tips = tipsOptions });
            }
            catch (Exception e)
            {
                await errorLogService.InsertException(e).ConfigureAwait(true);
                return Json(new { status = false, messageTitle = "Error", message = e.Message });
            }
        }

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> AcceptCookies()
        {
            Guid? UserId = null;
            try
            {

                UserId = User.Id();
                if (!UserId.HasValue || UserId.Value == Guid.Empty) throw new Exception("No user found!");


                var result = await userService.AcceptCookies(UserId.Value).ConfigureAwait(true);
                if (result)
                {
                    return Json(new { status = true, messageTitle = "Success", message = "Cookies accepted successfully!" });
                }
                else throw new Exception("Error accepting cookies. Try again later.");

            }
            catch (Exception e)
            {
                ModelState.AddModelError(string.Empty, "Something went wrong, please try again later");
                await errorLogService.InsertException(e).ConfigureAwait(true);
                return Json(new { status = false, messageTitle = "Error", message = e.Message });
            }
        }

        [Permitted(Permission.Capper, Permission.Bettor)]
        [HttpPost]
        public async Task<IActionResult> SendReferral(ReferAFriendDto data)
        {
            Guid? UserId = null;
            RequestFeedback request = new RequestFeedback();
            try
            {
                UserId = User.Id();
                if (!UserId.HasValue) throw new Exception("No user to send referral.");
                if (string.IsNullOrWhiteSpace(data.EmailReferred)) throw new Exception("No email to send referral.");
                if (string.IsNullOrWhiteSpace(data.UserReferralCode)) throw new Exception("No Referral Code to send referral.");

                User referredUser = await userService.GetByEmail(data.EmailReferred).ConfigureAwait(true);
                if (referredUser != null) throw new Exception("This person is already a Shortchase user!");
                else
                {
                    User referralUser = await userService.GetById(UserId.Value).ConfigureAwait(true);
                    string Name = referralUser.FirstName + " " + referralUser.LastName;
                    var result = await emailSenderService.SendReferralToNewUser(data.EmailReferred, Name, data.UserReferralCode).ConfigureAwait(true);
                    if (result)
                    {
                        return Json(new { status = true, messageTitle = "Success!", message = "Referral sent successfully!" });
                    }
                    else throw new Exception("Could not send referral!");
                }

            }
            catch (Exception e)
            {
                await errorLogService.InsertException(e, UserId).ConfigureAwait(true);
                return Json(new { status = false, messageTitle = "Error", message = e.Message });
            }
        }


        [Permitted(Permission.Capper)]
        [HttpPost]
        public async Task<IActionResult> ConnectUserPaypalAccount(Guid Id, string PaypalAccountEmail)
        {
            Guid? UserId = null;
            RequestFeedback request = new RequestFeedback();
            try
            {
                if (Id == Guid.Empty) throw new Exception("No user found to update!");
                if (string.IsNullOrWhiteSpace(PaypalAccountEmail)) throw new Exception("You need to provide a valid email!");
                User user = await userService.GetById(Id).ConfigureAwait(true);
                if (user == null) throw new Exception("No user found to update!");

                user.PaypalAccountEmail = PaypalAccountEmail.ToLower();

                var result = await userService.UpdateAsync(user).ConfigureAwait(true);
                if (!result) throw new Exception("Could not update user paypal account!");

                return Json(new { status = true, messageTitle = "Success!", message = "Paypal account connected successfully!" });
            }
            catch (Exception e)
            {
                await errorLogService.InsertException(e, UserId).ConfigureAwait(true);
                return Json(new { status = false, messageTitle = "Error", message = e.Message });
            }
        }



        [AllowAnonymous]
        [HttpPost]
        public async Task<IActionResult> ResetUserPassword(string Email)
        {
            Guid? UserId = null;
            RequestFeedback request = new RequestFeedback();
            try
            {
                if (string.IsNullOrWhiteSpace(Email)) throw new Exception("You need to provide a valid email to recover the password!");
                User user = await userService.GetByEmail(Email).ConfigureAwait(true);
                if (user == null) throw new Exception("You need to provide a valid user to recover the password!");

                var result = await userService.SendForgotPwdEmailAsync(user.Email).ConfigureAwait(false);
                if (result) return Json(new { status = true, messageTitle = "Success!", message = "You will receive an email with instructions shortly!" });
                else throw new Exception("Could not send reset password email!");
            }
            catch (Exception e)
            {
                await errorLogService.InsertException(e, UserId).ConfigureAwait(true);
                return Json(new { status = false, messageTitle = "Error", message = e.Message });
            }
        }


        [AllowAnonymous]
        public async Task<IActionResult> CreateNewPassword(string q)
        {
            Guid? UserId = null;
            RequestFeedback request = new RequestFeedback();
            ViewData["User"] = "";
            try
            {
                if (string.IsNullOrWhiteSpace(q)) throw new Exception("You need to provide a valid code to recover the password!");
                string decryptedCode = Security.Decrypt(q);
                User user = await userService.GetAccountFromConfirmationCode(decryptedCode, false).ConfigureAwait(true);
                if (user == null) throw new Exception("You need to provide a valid user to recover the password!");
                ViewData["User"] = user.Id.ToString();
                return View();
            }
            catch (Exception e)
            {
                await errorLogService.InsertException(e, UserId).ConfigureAwait(true);
                return RedirectToAction("Index", "Error", request);
            }
        }

        [AllowAnonymous]
        [HttpPost]
        public async Task<IActionResult> SaveUserNewPassword(Guid Id, string Password, string RepeatPassword)
        {
            Guid? UserId = null;
            RequestFeedback request = new RequestFeedback();
            try
            {
                if (string.IsNullOrWhiteSpace(Password)) throw new Exception("You need to provide a valid password!");
                if (string.IsNullOrWhiteSpace(RepeatPassword)) throw new Exception("You need to provide a valid password!");
                if (Id == Guid.Empty) throw new Exception("You need to provide a valid user to save the new password!");
                if (Password.ToLower() != RepeatPassword.ToLower()) throw new Exception("Re-enter password and repeat the same password on the Repeat Password field.");

                User user = await userService.GetById(Id).ConfigureAwait(true);

                if (user == null) throw new Exception("You need to provide a valid user to save the new password!");

                var result = await userService.ChangePasswordAsync(user.Email, Password).ConfigureAwait(true);
                if (result)
                {
                    var emailResult = await emailSenderService.SendPasswordChangedShortchase(user.Email, user.FirstName).ConfigureAwait(true);
                    if (emailResult) return Json(new { status = true, messageTitle = "Success!", message = "Password changed successfully!" });
                    else throw new Exception("Could not send email after resetting password!");
                }
                else throw new Exception("Could not reset password!");
            }
            catch (Exception e)
            {
                await errorLogService.InsertException(e, UserId).ConfigureAwait(true);
                return Json(new { status = false, messageTitle = "Error", message = e.Message });
            }
        }



        [AllowAnonymous]
        [HttpPost]
        public async Task<IActionResult> SendContactMessage(SendContactMessageDto Contact)
        {
            Guid? UserId = null;
            RequestFeedback request = new RequestFeedback();
            try
            {
                if (string.IsNullOrWhiteSpace(Contact.Name)) throw new Exception("You need to provide your name to send!");
                if (string.IsNullOrWhiteSpace(Contact.Message)) throw new Exception("You need to provide a message to send!");
                if (string.IsNullOrWhiteSpace(Contact.Phone) && string.IsNullOrWhiteSpace(Contact.Email)) throw new Exception("You need to provide a valid email or phone!");
                string sendEmailTo = "support@shortchase.com";
                var result = await emailSenderService.SendContactFormSubmission(sendEmailTo, Contact.TimezoneOffset, Contact.Name, Contact.Email, Contact.Phone, Contact.Message).ConfigureAwait(false);
                if (result) return Json(new { status = true, messageTitle = "Success!", message = "You will receive an email with instructions shortly!" });
                else throw new Exception("Could not send reset password email!");
            }
            catch (Exception e)
            {
                await errorLogService.InsertException(e).ConfigureAwait(true);
                return Json(new { status = false, messageTitle = "Error", message = e.Message });
            }
        }

        #endregion


        #region Import Contacts from Gmail

        [Permitted(Permission.Capper, Permission.Bettor)]
        public async Task<IActionResult> ImportContactsFromGoogle()
        {
            try
            {
                Guid? userId = User.Id();
                if (!userId.HasValue) throw new Exception("No user found to import contacts from Gmail.");
                User user = await userService.GetById(userId.Value).ConfigureAwait(true);
                if (user == null) throw new Exception("No user found to import contacts from Gmail.");

                string clientId = GoogleAPIData.ClientID;
                string clientSecret = GoogleAPIData.ClientSecret;
                string accessCode = user.GmailCode; // You will get this code after GetAccessToken method
                string accessToken = user.GmailToken; // You will get this code after GetAccessToken method
                string refreshToken = user.GmailRefreshToken; // You will get this code after GetAccessToken method
                string accessCodeFromWeb = null; // You will get this code after GetAccessToken method
                string redirectUri = GoogleAPIData.RedirectURL;
                string applicationName = GoogleAPIData.ApplicationName;

                string scopes = GoogleAPIData.Scope;
                string accessType = "offline";
                string tokenType = "refresh";

                OAuth2Parameters parameters = new OAuth2Parameters
                {
                    ClientId = clientId,
                    ClientSecret = clientSecret,
                    RedirectUri = redirectUri,
                    Scope = scopes,
                    AccessType = accessType,
                    TokenType = tokenType,
                    AccessCode = accessCode
                };
                if (accessCode == null)
                {
                    string url = OAuthUtil.CreateOAuth2AuthorizationUrl(parameters);
                    return Redirect(url);
                }
                else throw new Exception("Access code already in place");

            }
            catch (Exception e)
            {
                await errorLogService.InsertException(e).ConfigureAwait(true);
                return RedirectToAction("Index", "Error", "Error");
            }
        }


        [Permitted(Permission.Capper, Permission.Bettor)]
        public async Task<IActionResult> FinishImportContactsFromGoogle(string code, string scope)
        {
            try
            {

                Guid? userId = User.Id();
                if (!userId.HasValue) throw new Exception("No user found to import contacts from Gmail.");
                User user = await userService.GetById(userId.Value).ConfigureAwait(true);
                if (user == null) throw new Exception("No user found to import contacts from Gmail.");

                string clientId = GoogleAPIData.ClientID;
                string clientSecret = GoogleAPIData.ClientSecret;
                string accessCode = code; // You will get this code after GetAccessToken method
                string accessToken = user.GmailToken; // You will get this code after GetAccessToken method
                string refreshToken = user.GmailRefreshToken; // You will get this code after GetAccessToken method
                string accessCodeFromWeb = null; // You will get this code after GetAccessToken method
                string redirectUri = GoogleAPIData.RedirectURL;
                string applicationName = GoogleAPIData.ApplicationName;

                string scopes = GoogleAPIData.Scope;
                string accessType = "offline";
                string tokenType = "refresh";

                OAuth2Parameters parameters = new OAuth2Parameters
                {
                    ClientId = clientId,
                    ClientSecret = clientSecret,
                    RedirectUri = redirectUri,
                    Scope = scopes,
                    AccessType = accessType,
                    TokenType = tokenType,
                    AccessCode = accessCode
                };

                user.GmailCode = accessCode;


                if (accessToken == null || refreshToken == null)
                {
                    OAuthUtil.GetAccessToken(parameters);

                    // Save yours accessToken and refreshToken for next connection
                    accessToken = parameters.AccessToken;
                    refreshToken = parameters.RefreshToken;
                    user.GmailToken = parameters.AccessToken;
                    user.GmailRefreshToken = parameters.RefreshToken;
                }
                else
                {
                    // Restore your token from config file, etc.
                    parameters.AccessToken = accessToken;
                    parameters.RefreshToken = refreshToken;
                    user.GmailToken = accessToken;
                    user.GmailRefreshToken = refreshToken;
                }

                var updateUserResult = await userService.UpdateAsync(user).ConfigureAwait(true);
                if (!updateUserResult) throw new Exception("Could not save user Gmail Token");
                RequestSettings requestSettings = new RequestSettings(applicationName, parameters);
                ContactsRequest contactsRequest = new ContactsRequest(requestSettings);
                ICollection<UserContact> contacts = new List<UserContact>();
                Feed<Contact> feed = contactsRequest.GetContacts();
                if (feed.Entries.Count() > 0)
                {
                    foreach (Contact entry in feed.Entries)
                    {
                        foreach (EMail email in entry.Emails)
                        {

                            UserContact contact = new UserContact
                            {
                                Name = entry.Name.FullName,
                                Origin = UserContactOrigins.Google,
                                UserId = user.Id,
                                Email = email.Address
                            };
                            contacts.Add(contact);
                        }
                    }
                }
                var contactsResult = await userContactService.InsertBatch(contacts).ConfigureAwait(true);
                if (!contactsResult) throw new Exception("Could not save user contacts from Google!");

                return RedirectToAction("AccountManager");
            }
            catch (Exception e)
            {
                await errorLogService.InsertException(e).ConfigureAwait(true);
                return RedirectToAction("Index", "Error", "Error");
            }
        }



        [Permitted(Permission.Capper, Permission.Bettor)]
        public async Task<IActionResult> ImportContactsFromOutlook()
        {
            try
            {
                Guid? userId = User.Id();
                if (!userId.HasValue) throw new Exception("No user found to import contacts from Outlook.");
                User user = await userService.GetById(userId.Value).ConfigureAwait(true);
                if (user == null) throw new Exception("No user found to import contacts from Outlook.");

                string url = OutlookAPIData.GetURL();
                return Redirect(url);

            }
            catch (Exception e)
            {
                await errorLogService.InsertException(e).ConfigureAwait(true);
                return RedirectToAction("Index", "Error", "Error");
            }
        }


        [Permitted(Permission.Capper, Permission.Bettor)]
        public async Task<IActionResult> FinishImportContactsFromOutlook(string code = null, string session_state = null, string state = null)
        {
            try
            {

                Guid? userId = User.Id();
                if (!userId.HasValue) throw new Exception("No user found to import contacts from Outlook.");
                User user = await userService.GetById(userId.Value).ConfigureAwait(true);
                if (user == null) throw new Exception("No user found to import contacts from Outlook.");



                if (string.IsNullOrWhiteSpace(code)) throw new Exception("Error no code received from Outlook.");
                else
                {
                    user.OutlookCode = code;

                    System.Net.Http.HttpClient client = new System.Net.Http.HttpClient();
                    string url = OutlookAPIData.GetTokenURL(code);

                    var values = new Dictionary<string, string>
                    {
                        { "grant_type", "authorization_code" },
                        { "code", code },
                        { "redirect_uri", OutlookAPIData.RedirectURL },
                        { "client_id", OutlookAPIData.ClientID },
                        { "client_secret", OutlookAPIData.ClientSecret }
                    };

                    var content = new FormUrlEncodedContent(values);

                    var response = await client.PostAsync(OutlookAPIData.TokenURL, content);
                    if (response.IsSuccessStatusCode)
                    {
                        var responseString = await response.Content.ReadAsStringAsync();

                        dynamic jsonData = JObject.Parse(responseString);

                        string token = jsonData.access_token;

                        user.OutlookToken = token;
                        user.OutlookRefreshToken = token;

                        var updateUserResult = await userService.UpdateAsync(user).ConfigureAwait(true);
                        if (!updateUserResult) throw new Exception("Could not save user Outlook Token");

                        var httpClient = new System.Net.Http.HttpClient();

                        httpClient.DefaultRequestHeaders.Add("Authorization", "Bearer " + token);
                        var responseFromContacts = await httpClient.GetStringAsync(OutlookAPIData.GetContactsURL).ConfigureAwait(true);
                        // Parse JSON response.

                        dynamic contactsResponse = JObject.Parse(responseFromContacts);
                        JArray contacts = contactsResponse.value;
                        ICollection<UserContact> contactsToAdd = new List<UserContact>();
                        if (contacts.Count > 0)
                        {
                            foreach (var item in contacts)
                            {

                                var emailAddresses = item["EmailAddresses"];
                                var itemName = item["DisplayName"];

                                foreach (var email in emailAddresses)
                                {
                                    var itemEmail = email["Address"];
                                    UserContact contact = new UserContact
                                    {
                                        Name = itemName.ToString(),
                                        Origin = UserContactOrigins.Outlook,
                                        UserId = user.Id,
                                        Email = itemEmail.ToString()
                                    };
                                    contactsToAdd.Add(contact);
                                }

                            }
                        }
                        var contactsResult = await userContactService.InsertBatch(contactsToAdd).ConfigureAwait(true);
                        if (!contactsResult) throw new Exception("Could not save user contacts from Outlook!");

                        return RedirectToAction("AccountManager");
                    }
                    else throw new Exception("Error trying to get token from Outlook.");
                }
            }
            catch (Exception e)
            {
                await errorLogService.InsertException(e).ConfigureAwait(true);
                return RedirectToAction("Index", "Error", "Error");
            }
        }



        [HttpPost]
        [Permitted(Permission.Capper, Permission.Bettor)]
        public async Task<IActionResult> GetGmailContacts()
        {

            RequestFeedback request = new RequestFeedback();
            try
            {
                Guid? userId = User.Id();
                if (!userId.HasValue) throw new Exception("No user found to import contacts from Gmail.");
                User user = await userService.GetById(userId.Value).ConfigureAwait(true);
                if (user == null) throw new Exception("No user found to import contacts from Gmail.");


                var contacts = (await userContactService.GetAllByUserId(user.Id, UserContactOrigins.Google).ConfigureAwait(true)).OrderBy(o => o.Email).ToList();
                return PartialView(contacts);
            }
            catch (Exception e)
            {
                await errorLogService.InsertException(e).ConfigureAwait(true);
                return null;
            }
        }


        [Permitted(Permission.Capper, Permission.Bettor)]
        [HttpPost]
        public async Task<IActionResult> SendGmailReferral(ICollection<string> emails)
        {
            Guid? UserId = null;
            RequestFeedback request = new RequestFeedback();
            try
            {
                Guid? userId = User.Id();
                if (!userId.HasValue) throw new Exception("No user to send referral.");
                User user = await userService.GetById(userId.Value).ConfigureAwait(true);
                if (user == null) throw new Exception("No user to send referral.");

                if (emails.Count <= 0) throw new Exception("No emails to send referral!");
                foreach (var email in emails)
                {
                    User referredUser = await userService.GetByEmail(email).ConfigureAwait(true);
                    if (referredUser != null) continue;
                    else
                    {
                        string Name = user.FirstName + " " + user.LastName;
                        var result = await emailSenderService.SendReferralToNewUser(email, Name, user.ReferralCode).ConfigureAwait(true);
                        if (!result) throw new Exception("Could not send referral!");
                    }
                }

                return Json(new { status = true, messageTitle = "Success!", message = "Referral sent successfully!" });


            }
            catch (Exception e)
            {
                await errorLogService.InsertException(e, UserId).ConfigureAwait(true);
                return Json(new { status = false, messageTitle = "Error", message = e.Message });
            }
        }


        [HttpPost]
        [Permitted(Permission.Capper, Permission.Bettor)]
        public async Task<IActionResult> GetOutlookContacts()
        {

            RequestFeedback request = new RequestFeedback();
            try
            {
                Guid? userId = User.Id();
                if (!userId.HasValue) throw new Exception("No user found to import contacts from Gmail.");
                User user = await userService.GetById(userId.Value).ConfigureAwait(true);
                if (user == null) throw new Exception("No user found to import contacts from Gmail.");


                var contacts = (await userContactService.GetAllByUserId(user.Id, UserContactOrigins.Outlook).ConfigureAwait(true)).OrderBy(o => o.Email).ToList();
                return PartialView(contacts);
            }
            catch (Exception e)
            {
                await errorLogService.InsertException(e).ConfigureAwait(true);
                return null;
            }
        }


        [Permitted(Permission.Capper, Permission.Bettor)]
        [HttpPost]
        public async Task<IActionResult> SendOutlookReferral(ICollection<string> emails)
        {
            Guid? UserId = null;
            RequestFeedback request = new RequestFeedback();
            try
            {
                Guid? userId = User.Id();
                if (!userId.HasValue) throw new Exception("No user to send referral.");
                User user = await userService.GetById(userId.Value).ConfigureAwait(true);
                if (user == null) throw new Exception("No user to send referral.");

                if (emails.Count <= 0) throw new Exception("No emails to send referral!");
                foreach (var email in emails)
                {
                    User referredUser = await userService.GetByEmail(email).ConfigureAwait(true);
                    if (referredUser != null) continue;
                    else
                    {
                        string Name = user.FirstName + " " + user.LastName;
                        var result = await emailSenderService.SendReferralToNewUser(email, Name, user.ReferralCode).ConfigureAwait(true);
                        if (!result) throw new Exception("Could not send referral!");
                    }
                }

                return Json(new { status = true, messageTitle = "Success!", message = "Referral sent successfully!" });


            }
            catch (Exception e)
            {
                await errorLogService.InsertException(e, UserId).ConfigureAwait(true);
                return Json(new { status = false, messageTitle = "Error", message = e.Message });
            }
        }
        #endregion

    }
}