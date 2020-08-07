using Microsoft.AspNetCore.Mvc;
using Shortchase.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Security.Claims;
using Shortchase.Helpers;
using System.IO;
using Shortchase.Entities;
using Shortchase.Dtos;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Shortchase.Helpers.Extensions;
using Shortchase.Authorization;
using Microsoft.AspNetCore.Hosting;

namespace Shortchase.ViewComponents
{
    [ViewComponent(Name = "AuthenticatedMenu")]
    public class VCAuthenticatedMenu : ViewComponent
    {
        private readonly IUserService userService;
        private readonly IErrorLogService errorLogService;
        private readonly IUserSubscriptionService userSubscriptionService;
        private readonly IShoppingCartService shoppingCartService;
        private readonly INotificationService notificationService;

        public VCAuthenticatedMenu(IUserService userService, IErrorLogService errorLogService, IUserSubscriptionService userSubscriptionService, IShoppingCartService shoppingCartService, INotificationService notificationService)
        {
            this.userService = userService;
            this.errorLogService = errorLogService;
            this.userSubscriptionService = userSubscriptionService;
            this.shoppingCartService = shoppingCartService;
            this.notificationService = notificationService;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            try
            {
                User user = await userService.GetById(new Guid(User.Identity.Id())).ConfigureAwait(true);
                var permission = User.Identity.Permissions().FirstOrDefault();

                if (permission == null) throw new Exception("No permissions set.");
                else
                {
                    var activeProPlan = await userSubscriptionService.GetActiveSubscriptionPlan(user.Id, SubscriptionPlanType.ShortchasePro).ConfigureAwait(true);
                    var activeBoisterousPlan = await userSubscriptionService.GetActiveSubscriptionPlan(user.Id, SubscriptionPlanType.Boisterous).ConfigureAwait(true);
                    AuthenticatedUserDto model = new AuthenticatedUserDto
                    {
                        User = user,
                        FirstName = user.FirstName,
                        LastName = user.LastName,
                        ProfilePicture = user.ProfilePicture,
                        CurrentPermission = permission,
                        IsBoisterous = activeBoisterousPlan == null ? false : true,
                        IsProCapper = activeProPlan == null ? false : true,
                        TotalItemsInCart = await shoppingCartService.CountItems(user.Id).ConfigureAwait(true),
                        NewNotifications = await notificationService.GetAllNewFromUser(user.Id).ConfigureAwait(true)
                    };
                    return View(model);
                }
            }
            catch (Exception e)
            {
                await errorLogService.InsertException(e).ConfigureAwait(true);
                return View();
            }

        }
    }



    [ViewComponent(Name = "SignUpForm")]
    public class VCSignUpForm : ViewComponent
    {
        private readonly ICountryService countryService;
        private readonly IErrorLogService errorLogService;

        public VCSignUpForm(ICountryService countryService, IErrorLogService errorLogService)
        {
            this.countryService = countryService;
            this.errorLogService = errorLogService;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            try
            {
                var model = await countryService.GetAll().ConfigureAwait(true);
                model = model.OrderBy(o => o.Name).ToList();
                return View(model);
            }
            catch (Exception e)
            {
                await errorLogService.InsertException(e).ConfigureAwait(true);
                return View();
            }

        }
    }



    [ViewComponent(Name = "WebsiteFooterContent")]
    public class VCWebsiteFooterContent : ViewComponent
    {
        private readonly ISystemFlagsService systemFlagsService;
        private readonly IAddressService addressService;
        private readonly ISocialMediaService socialMediaService;
        private readonly IErrorLogService errorLogService;
        private readonly IUserService userService;
        private readonly ISemiStaticTextService semiStaticTextService;

        public VCWebsiteFooterContent(IUserService userService, ISystemFlagsService systemFlagsService, IAddressService addressService, ISocialMediaService socialMediaService, IErrorLogService errorLogService, ISemiStaticTextService semiStaticTextService)
        {
            this.systemFlagsService = systemFlagsService;
            this.addressService = addressService;
            this.socialMediaService = socialMediaService;
            this.errorLogService = errorLogService;
            this.userService = userService;
            this.semiStaticTextService = semiStaticTextService;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {

            try
            {
                bool showCookieMessage = true;
                bool IsAdmin = false;
                var id = User.Identity.Id();
                Guid UserId;
                if (!string.IsNullOrWhiteSpace(id))
                {
                    UserId = new Guid(id);
                    showCookieMessage = !(await userService.HasAcceptedCookies(UserId).ConfigureAwait(true));
                    if (User.Identity.HasAnyPermissions(Permission.Admin))
                    {
                        IsAdmin = true;
                    }
                }
                else
                {
                    showCookieMessage = true;
                }
                string CookieConsentMessage = (await semiStaticTextService.GetByName(SemiStaticTextNames.CookieConsent).ConfigureAwait(true)).Value;

                FooterData model = new FooterData();
                WebsiteAddressDto addressData = new WebsiteAddressDto
                {
                    DisplayAddresses = (await systemFlagsService.GetByName(SystemFlagsNames.DisplayAddresses).ConfigureAwait(true)).Value,
                    Addresses = (await addressService.GetAll(true, true).ConfigureAwait(true)).OrderBy(o => o.IsPrimary == true).ThenBy(o => o.Location).ToList(),
                    CookieConsentMessage = CookieConsentMessage
                };
                model.AddressData = addressData;
                model.SocialMediaData = (await socialMediaService.GetAll(true).ConfigureAwait(true)).OrderBy(o => o.Id).ToList();
                model.ShowCookieMessage = showCookieMessage;
                model.IsAdmin = IsAdmin;
                return View(model);
            }
            catch (Exception e)
            {
                await errorLogService.InsertException(e).ConfigureAwait(true);
                return View();
            }

        }
    }



    [ViewComponent(Name = "Modal")]
    public class VCModal : ViewComponent
    {
        public IViewComponentResult Invoke(Func<dynamic, object> Content, string Id, string Title, bool Locked)
        {
            ViewData["Id"] = Id;
            ViewData["Title"] = Title;
            if (Locked) ViewData["Locked"] = "data-backdrop=static data-keyboard=false";
            else ViewData["Locked"] = "";
            return View(Content);
        }
    }



    [ViewComponent(Name = "ModalFullscreen")]
    public class VCModalFullscreen : ViewComponent
    {
        public IViewComponentResult Invoke(Func<dynamic, object> Content, string Id, string Title, bool Locked)
        {
            ViewData["Id"] = Id;
            ViewData["Title"] = Title;
            if (Locked) ViewData["Locked"] = "data-backdrop=static data-keyboard=false";
            else ViewData["Locked"] = "";
            return View(Content);
        }
    }

    [ViewComponent(Name = "Input")]
    public class VCInput : ViewComponent
    {
        public IViewComponentResult Invoke(string Id, string Label, string Type, string Class, string Placeholder, string Value, bool Required, bool Disabled)
        {
            ViewData["Id"] = Id;
            ViewData["Label"] = Label;
            ViewData["Required"] = Required;
            ViewData["Type"] = Type;
            ViewData["Class"] = Class;
            ViewData["Placeholder"] = Placeholder;
            ViewData["Value"] = Value;
            ViewData["Disabled"] = Disabled;
            return View();
        }
    }



    [ViewComponent(Name = "NumberInput")]
    public class VCNumberInput : ViewComponent
    {
        public IViewComponentResult Invoke(string Id, string Label, double MinValue, string MaxValue, string Step, string Class, string Placeholder, string Value, bool Required, bool Disabled)
        {
            ViewData["Id"] = Id;
            ViewData["Label"] = Label;
            ViewData["Required"] = Required;
            ViewData["MinValue"] = MinValue;
            ViewData["MaxValue"] = MaxValue;
            ViewData["Step"] = Step;
            ViewData["Class"] = Class;
            ViewData["Placeholder"] = Placeholder;
            ViewData["Value"] = Value;
            ViewData["Disabled"] = Disabled;
            return View();
        }
    }


    [ViewComponent(Name = "FileInput")]
    public class VCFileInput : ViewComponent
    {
        public IViewComponentResult Invoke(string Id, string Label, string Class)
        {
            ViewData["Id"] = Id;
            ViewData["Label"] = Label;
            ViewData["Class"] = Class;
            return View();
        }
    }

    [ViewComponent(Name = "Textarea")]
    public class VCTextarea : ViewComponent
    {
        public IViewComponentResult Invoke(string Id, string Label, string Class, string Placeholder, string Value, bool Required, bool Disabled)
        {
            ViewData["Id"] = Id;
            ViewData["Label"] = Label;
            ViewData["Required"] = Required;
            ViewData["Class"] = Class;
            ViewData["Placeholder"] = Placeholder;
            ViewData["Value"] = Value;
            ViewData["Disabled"] = Disabled;
            return View();
        }
    }



    [ViewComponent(Name = "Checkbox")]
    public class VCCheckbox : ViewComponent
    {
        public IViewComponentResult Invoke(string Id, string Label, string Class, bool Required, bool Checked)
        {
            ViewData["Id"] = Id;
            ViewData["Label"] = Label;
            ViewData["Required"] = Required;
            ViewData["Class"] = Class;
            ViewData["Checked"] = Checked;
            return View();
        }
    }


    [ViewComponent(Name = "FAQCard")]
    public class VCFAQCard : ViewComponent
    {
        public IViewComponentResult Invoke(string ItemId, string Question, string Answer)
        {
            ViewData["ItemId"] = ItemId;
            ViewData["Question"] = Question;
            ViewData["Answer"] = Answer;
            return View();
        }
    }



    [ViewComponent(Name = "Select")]
    public class VCSelect : ViewComponent
    {
        public IViewComponentResult Invoke(string Id, string Label, string Class, bool Required, bool Searchable, string Default, ICollection<SelectListItem> options, string SelectedValue)
        {
            ViewData["Id"] = Id;
            ViewData["Label"] = Label;
            ViewData["Required"] = Required;
            ViewData["Searchable"] = Searchable;
            ViewData["Class"] = Class;
            ViewData["DefaultOption"] = Default;
            ViewData["SelectedValue"] = SelectedValue;
            return View(options);
        }
    }




    [ViewComponent(Name = "PostCard")]
    public class VCPostCard : ViewComponent
    {
        public IViewComponentResult Invoke(string Controller, string Action, string Slug, string Title, string Content, string Image, string Date)
        {
            ViewData["Controller"] = Controller;
            ViewData["Action"] = Action;
            ViewData["Slug"] = Slug;
            ViewData["Title"] = Title;
            ViewData["Content"] = Content;
            ViewData["Image"] = Image;
            ViewData["Date"] = Date;
            return View();
        }
    }




    [ViewComponent(Name = "HomeCategoriesDisplay")]
    public class VCHomeCategoriesDisplay : ViewComponent
    {
        private readonly IListingCategoryService listingCategoryService;
        private readonly IErrorLogService errorLogService;
        private readonly IWebHostEnvironment hostingEnvironment;

        public VCHomeCategoriesDisplay(IListingCategoryService listingCategoryService, IErrorLogService errorLogService, IWebHostEnvironment hostingEnvironment)
        {
            this.listingCategoryService = listingCategoryService;
            this.errorLogService = errorLogService;
            this.hostingEnvironment = hostingEnvironment;
        }

        public async Task<IViewComponentResult> InvokeAsync(bool ShowAllItems = false)
        {
            try
            {
                ViewData["ShowAllItems"] = ShowAllItems;
                ViewData["root"] = hostingEnvironment.ContentRootPath;
                HomeCategoriesDisplayData model = new HomeCategoriesDisplayData
                {
                    Categories = (await listingCategoryService.GetAll(true).ConfigureAwait(true)).OrderBy(o => o.Name).ToList()
                };
                return View(model);
            }
            catch (Exception e)
            {
                await errorLogService.InsertException(e).ConfigureAwait(true);
                return View();
            }

        }
    
    }


    [ViewComponent(Name = "HomeTopRankingCategroies")]
    public class VCHomeTopRankingCategroies : ViewComponent
    {
        private readonly IListingCategoryService listingCategoryService;
        private readonly IErrorLogService errorLogService;
        private readonly IWebHostEnvironment hostingEnvironment;

        public VCHomeTopRankingCategroies(IListingCategoryService listingCategoryService, IErrorLogService errorLogService, IWebHostEnvironment hostingEnvironment)
        {
            this.listingCategoryService = listingCategoryService;
            this.errorLogService = errorLogService;
            this.hostingEnvironment = hostingEnvironment;
        }

        public async Task<IViewComponentResult> InvokeAsync(bool ShowAllItems = false)
        {
            try
            {
                ViewData["ShowAllItems"] = ShowAllItems;
                ViewData["root"] = hostingEnvironment.ContentRootPath;
                HomeCategoriesDisplayData model = new HomeCategoriesDisplayData
                {
                    Categories = (await listingCategoryService.GetAll(true).ConfigureAwait(true)).OrderBy(o => o.Name).ToList()
                };
                return View(model);
            }
            catch (Exception e)
            {
                await errorLogService.InsertException(e).ConfigureAwait(true);
                return View();
            }

        }
    }


    [ViewComponent(Name = "HomeCategoriesMarketplaceDisplay")]
    public class VCHomeCategoriesMarketplaceDisplay : ViewComponent
    {
        private readonly IListingCategoryService listingCategoryService;
        private readonly IErrorLogService errorLogService;
        private readonly IWebHostEnvironment hostingEnvironment;

        public VCHomeCategoriesMarketplaceDisplay(IListingCategoryService listingCategoryService, IErrorLogService errorLogService, IWebHostEnvironment hostingEnvironment)
        {
            this.listingCategoryService = listingCategoryService;
            this.errorLogService = errorLogService;
            this.hostingEnvironment = hostingEnvironment;
        }

        public async Task<IViewComponentResult> InvokeAsync(bool ShowAllItems = false)
        {
            try
            {
                ViewData["ShowAllItems"] = ShowAllItems;
                ViewData["root"] = hostingEnvironment.ContentRootPath;
                HomeCategoriesDisplayData model = new HomeCategoriesDisplayData
                {
                    Categories = (await listingCategoryService.GetAll(true).ConfigureAwait(true)).OrderBy(o => o.Name).ToList()
                };
                return View(model);
            }
            catch (Exception e)
            {
                await errorLogService.InsertException(e).ConfigureAwait(true);
                return View();
            }

        }
    }



    [ViewComponent(Name = "BoisterousModal")]
    public class VCBoisterousModal : ViewComponent
    {
        private readonly ISubscriptionPlanService subscriptionPlanService;
        private readonly IErrorLogService errorLogService;

        public VCBoisterousModal(ISubscriptionPlanService subscriptionPlanService, IErrorLogService errorLogService)
        {
            this.subscriptionPlanService = subscriptionPlanService;
            this.errorLogService = errorLogService;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            try
            {
                WebsiteSubscriptionPlanDto model = new WebsiteSubscriptionPlanDto
                {
                    SubscriptionPlans = await subscriptionPlanService.GetAllFromType(SubscriptionPlanType.Boisterous).ConfigureAwait(true)
                };
                return View(model);
            }
            catch (Exception e)
            {
                await errorLogService.InsertException(e).ConfigureAwait(true);
                return View();
            }

        }
    }



    [ViewComponent(Name = "ShortchaseProModal")]
    public class VCShortchaseProModal : ViewComponent
    {
        private readonly ISubscriptionPlanService subscriptionPlanService;
        private readonly IErrorLogService errorLogService;

        public VCShortchaseProModal(ISubscriptionPlanService subscriptionPlanService, IErrorLogService errorLogService)
        {
            this.subscriptionPlanService = subscriptionPlanService;
            this.errorLogService = errorLogService;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            try
            {
                WebsiteSubscriptionPlanDto model = new WebsiteSubscriptionPlanDto
                {
                    SubscriptionPlans = await subscriptionPlanService.GetAllFromType(SubscriptionPlanType.ShortchasePro).ConfigureAwait(true)
                };
                return View(model);
            }
            catch (Exception e)
            {
                await errorLogService.InsertException(e).ConfigureAwait(true);
                return View();
            }

        }
    }



    [ViewComponent(Name = "BetListingInput")]
    public class VCBetListingInput : ViewComponent
    {
        public IViewComponentResult Invoke(string Id, int MaxLength, string Placeholder)
        {
            ViewData["Id"] = Id;
            ViewData["MaxLength"] = MaxLength;
            ViewData["Placeholder"] = Placeholder;
            return View();
        }
    }


    [ViewComponent(Name = "BetListingDecimalInput")]
    public class VCBetListingDecimalInput : ViewComponent
    {
        public IViewComponentResult Invoke(string Id, string Placeholder)
        {
            ViewData["Id"] = Id;
            ViewData["Placeholder"] = Placeholder;
            return View();
        }
    }

    [ViewComponent(Name = "BetListingDateInput")]
    public class VCBetListingDateInput : ViewComponent
    {
        public IViewComponentResult Invoke(string Id, string Label, string Value)
        {
            ViewData["Id"] = Id;
            ViewData["Label"] = Label;
            ViewData["Value"] = Value;
            return View();
        }
    }

    [ViewComponent(Name = "BetListingDateReadOnlyInput")]
    public class VCBetListingDateReadOnlyInput : ViewComponent
    {
        public IViewComponentResult Invoke(string Id, string Label)
        {
            ViewData["Id"] = Id;
            ViewData["Label"] = Label;
            return View();
        }
    }
    [ViewComponent(Name = "BetListingTextarea")]
    public class VCBetListingTextarea : ViewComponent
    {
        public IViewComponentResult Invoke(string Id, string Label, string Placeholder, int MaxLength, int MinLength)
        {
            ViewData["Id"] = Id;
            ViewData["Label"] = Label;
            ViewData["Placeholder"] = Placeholder;
            ViewData["MaxLength"] = MaxLength;
            ViewData["MinLength"] = MinLength;
            return View();
        }
    }


    [ViewComponent(Name = "BetListingSimpleInput")]
    public class VCBetListingSimpleInput : ViewComponent
    {
        public IViewComponentResult Invoke(string Id, string Placeholder)
        {
            ViewData["Id"] = Id;
            ViewData["Placeholder"] = Placeholder;
            return View();
        }
    }

    [ViewComponent(Name = "BetListingSelect")]
    public class VCBetListingSelect : ViewComponent
    {
        public IViewComponentResult Invoke(string Id, string Placeholder, ICollection<SelectListItem> Options)
        {
            ICollection<SelectListItem> model = Options;
            ViewData["Id"] = Id;
            ViewData["Placeholder"] = Placeholder;
            return View(model);
        }
    }



    [ViewComponent(Name = "Pagination")]
    public class VCPagination : ViewComponent
    {
        public IViewComponentResult Invoke(int CurrentPage, int PageSize, int TotalPages, string Action)
        {
            Pagination model = new Pagination
            {
                Action = Action,
                CurrentPage = CurrentPage,
                PageSize = PageSize,
                TotalPages = TotalPages

            };
            return View(model);
        }
    }

    [ViewComponent(Name = "ProfilePagination")]
    public class VCProfilePagination : ViewComponent
    {
        public IViewComponentResult Invoke(Guid Id, int CurrentPage, int PageSize, int TotalPages, string Action)
        {
            ProfilePagination model = new ProfilePagination
            {
                UserId = Id,
                Action = Action,
                CurrentPage = CurrentPage,
                PageSize = PageSize,
                TotalPages = TotalPages

            };
            return View(model);
        }
    }



    [ViewComponent(Name = "MarketplacePagination")]
    public class VCMarketplacePagination : ViewComponent
    {
        public IViewComponentResult Invoke(int CurrentPage, int PageSize, int TotalPages, string Action)
        {
            Pagination model = new Pagination
            {
                Action = Action,
                CurrentPage = CurrentPage,
                PageSize = PageSize,
                TotalPages = TotalPages

            };
            return View(model);
        }
    }


    [ViewComponent(Name = "ListingCardCollection")]
    public class VCListingCardCollection : ViewComponent
    {
        private readonly IWebHostEnvironment hostingEnvironment;
        private readonly IErrorLogService errorLogService;

        public VCListingCardCollection(IWebHostEnvironment hostingEnvironment, IErrorLogService errorLogService)
        {
            this.hostingEnvironment = hostingEnvironment;
            this.errorLogService = errorLogService;
        }

        public async Task<IViewComponentResult> InvokeAsync(bool IsAuth, bool HasAnyFilter, string Currency, ICollection<BetListing> Listings, int Offset = 0)
        {
            try
            {

                ListingCardCollectionDto model = new ListingCardCollectionDto
                {
                    HasAnyFilter = HasAnyFilter,
                    IsAuth = IsAuth,
                    Listings = Listings,
                    Currency = Currency,
                    Offset = Offset,
                    root = hostingEnvironment.ContentRootPath
                };
                return View(model);
            }
            catch (Exception e)
            {
                await errorLogService.InsertException(e).ConfigureAwait(true);
                return View();
            }

        }
    }



    [ViewComponent(Name = "ListingRelatedCardCollection")]
    public class VCListingRelatedCardCollection : ViewComponent
    {
        private readonly IWebHostEnvironment hostingEnvironment;
        private readonly IErrorLogService errorLogService;

        public VCListingRelatedCardCollection(IWebHostEnvironment hostingEnvironment, IErrorLogService errorLogService)
        {
            this.hostingEnvironment = hostingEnvironment;
            this.errorLogService = errorLogService;
        }

        public async Task<IViewComponentResult> InvokeAsync(bool IsAuth, ICollection<BetListing> Listings, int Offset = 0)
        {
            try
            {

                ListingCardCollectionDto model = new ListingCardCollectionDto
                {
                    HasAnyFilter = false,
                    IsAuth = IsAuth,
                    Listings = Listings,
                    Offset = Offset,
                    root = hostingEnvironment.ContentRootPath
                };
                return View(model);
            }
            catch (Exception e)
            {
                await errorLogService.InsertException(e).ConfigureAwait(true);
                return View();
            }

        }
    }


    [ViewComponent(Name = "BetListingToggleCard")]
    public class VCBetListingToggleCard : ViewComponent
    {
        public IViewComponentResult Invoke(BetListing Item, int Iteration, string Name, string Picture)
        {
            BetListingToggleCardDto model = new BetListingToggleCardDto
            {
                BetListing = Item,
                IterationCounter = Iteration,
                UserName = Name,
                ProfilePicture = Picture
            };
            return View(model);
        }
    }



    [ViewComponent(Name = "UserMessageBoard")]
    public class VCUserMessageBoard : ViewComponent
    {
        private readonly IUserService userService;
        private readonly IErrorLogService errorLogService;
        private readonly IMessageService messageService;
        private readonly IWebHostEnvironment hostingEnvironment;
        private readonly IAccessLogService accessLogService;

        public VCUserMessageBoard(IUserService userService, IErrorLogService errorLogService, IMessageService messageService, IWebHostEnvironment hostingEnvironment, IAccessLogService accessLogService)
        {
            this.userService = userService;
            this.errorLogService = errorLogService;
            this.hostingEnvironment = hostingEnvironment;
            this.messageService = messageService;
            this.accessLogService = accessLogService;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            MessagerModel model = null;
            try
            {
                User user = await userService.GetById(new Guid(User.Identity.Id())).ConfigureAwait(true);
                model = new MessagerModel
                {
                    UserId = user.Id,
                    UserName = user.FirstName + " " + user.LastName,
                    UserFirstName = user.FirstName,
                    UserProfilePicture = user.ProfilePicture,
                    root = hostingEnvironment.ContentRootPath,
                    AdminName = "Shortchase Admin",
                    AdminProfilePicture = Url.Content("~/img/businessmanicon.png"),
                    IsAdminOnline = await accessLogService.IsAnyAdminOnline().ConfigureAwait(true),
                    Messages = new List<Message>()//await messageService.GetAllForUser(user.Id).ConfigureAwait(true)
                };
                model.UnreadMessagesCount = 0;// model.Messages.Where(i => !i.DateRead.HasValue && i.FromId != user.Id).Count();
                return View(model);

            }
            catch (Exception e)
            {
                await errorLogService.InsertException(e).ConfigureAwait(true);
                return View(model);
            }

        }
    }




    [ViewComponent(Name = "AdminSidebar")]
    public class VCAdminSidebar : ViewComponent
    {
        private readonly IUserService userService;
        private readonly IErrorLogService errorLogService;
        private readonly IUserSubscriptionService userSubscriptionService;
        private readonly ISemiStaticTextService semiStaticTextService;
        private readonly IWebHostEnvironment hostingEnvironment;
        private readonly ISystemConstantsService systemConstantsService;
        private readonly IOrderService orderService;

        public VCAdminSidebar(IUserService userService, IErrorLogService errorLogService, IUserSubscriptionService userSubscriptionService, ISemiStaticTextService semiStaticTextService, IWebHostEnvironment hostingEnvironment, ISystemConstantsService systemConstantsService, IOrderService orderService)
        {
            this.userService = userService;
            this.errorLogService = errorLogService;
            this.userSubscriptionService = userSubscriptionService;
            this.semiStaticTextService = semiStaticTextService;
            this.hostingEnvironment = hostingEnvironment;
            this.systemConstantsService = systemConstantsService;
            this.orderService = orderService;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            try
            {
                 
                SemiStaticText AppLogo = (await semiStaticTextService.GetByName(SemiStaticTextNames.AppLogo).ConfigureAwait(true));

                string logo = null;
                if (string.IsNullOrWhiteSpace(AppLogo.Value))
                {
                    logo = Url.Content("~/img/shortchase_logo.png");
                }
                else logo = ImageHelper.ConvertImageToBase64(hostingEnvironment.ContentRootPath + AppLogo.Value);
                AdminSidebarDto model = new AdminSidebarDto { 
                    AppLogo = logo,
                    TotalUsers = (await userService.GetUserListCount().ConfigureAwait(true)),
                    TotalBoisterous = (await userService.GetBoisterousUserListCount().ConfigureAwait(true)),
                    TotalPro = (await userService.GetProUserListCount().ConfigureAwait(true)),
                    SalesTotalAmount = (await orderService.GetTotalAmount().ConfigureAwait(true))
                };

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

                model.SessionTimeout = Convert.ToInt32(Convert.ToDecimal(SessionTimeout.Value));

                return View(model);

            }
            catch (Exception e)
            {
                await errorLogService.InsertException(e).ConfigureAwait(true);
                return View();
            }

        }
    }




    [ViewComponent(Name = "PaypalJSScript")]
    public class VCPaypalJSScript : ViewComponent
    {
        private readonly IErrorLogService errorLogService;
        private readonly IPaypalSettingsService paypalSettingsService;

        public VCPaypalJSScript(IErrorLogService errorLogService, IPaypalSettingsService paypalSettingsService)
        {
            this.errorLogService = errorLogService;
            this.paypalSettingsService = paypalSettingsService;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            try
            {
                PaypalSettings settings = await paypalSettingsService.GetDefault().ConfigureAwait(true);
                return View(settings);

            }
            catch (Exception e)
            {
                await errorLogService.InsertException(e).ConfigureAwait(true);
                return View();
            }

        }
    }


    [ViewComponent(Name = "PaypalSubscriptionScript")]
    public class VCPaypalSubscriptionScript : ViewComponent
    {
        private readonly IErrorLogService errorLogService;
        private readonly IPaypalSettingsService paypalSettingsService;

        public VCPaypalSubscriptionScript(IErrorLogService errorLogService, IPaypalSettingsService paypalSettingsService)
        {
            this.errorLogService = errorLogService;
            this.paypalSettingsService = paypalSettingsService;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            try
            {
                PaypalSettings settings = await paypalSettingsService.GetDefault().ConfigureAwait(true);
                return View(settings);

            }
            catch (Exception e)
            {
                await errorLogService.InsertException(e).ConfigureAwait(true);
                return View();
            }

        }
    }

    [ViewComponent(Name = "AppTitle")]
    public class VCAppTitle : ViewComponent
    {
        private readonly IErrorLogService errorLogService;
        private readonly ISemiStaticTextService semiStaticTextService;

        public VCAppTitle(IErrorLogService errorLogService, ISemiStaticTextService semiStaticTextService)
        {
            this.errorLogService = errorLogService;
            this.semiStaticTextService = semiStaticTextService;
        }

        public async Task<IViewComponentResult> InvokeAsync(string AppEnd = "front")
        {
            try
            {
                SemiStaticText AppName = (await semiStaticTextService.GetByName(SemiStaticTextNames.AppName).ConfigureAwait(true));
                SemiStaticText AppTagline = (await semiStaticTextService.GetByName(SemiStaticTextNames.AppTagline).ConfigureAwait(true));
                SemiStaticText AppLogo = (await semiStaticTextService.GetByName(SemiStaticTextNames.AppLogo).ConfigureAwait(true));

                string value = "";
                if (AppEnd == "front")
                {
                    value = AppName.Value + " - " + AppTagline.Value;
                }
                else {
                    value = "Dashboard - " + AppName.Value;
                }
                AppTitleDto model = new AppTitleDto {
                    Value = value
                };
                return View(model);

            }
            catch (Exception e)
            {
                await errorLogService.InsertException(e).ConfigureAwait(true);
                return View();
            }

        }
    }



    [ViewComponent(Name = "WebsiteHeader")]
    public class VCWebsiteHeader : ViewComponent
    {
        private readonly IErrorLogService errorLogService;
        private readonly ISemiStaticTextService semiStaticTextService;
        private readonly IWebHostEnvironment hostingEnvironment;

        public VCWebsiteHeader(IErrorLogService errorLogService, ISemiStaticTextService semiStaticTextService, IWebHostEnvironment hostingEnvironment)
        {
            this.errorLogService = errorLogService;
            this.semiStaticTextService = semiStaticTextService;
            this.hostingEnvironment = hostingEnvironment;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            try
            {
                SemiStaticText AppLogo = (await semiStaticTextService.GetByName(SemiStaticTextNames.AppLogo).ConfigureAwait(true));

                string logo = null;
                if (string.IsNullOrWhiteSpace(AppLogo.Value))
                {
                    logo = Url.Content("~/img/shortchase_logo.png");
                }
                else logo = ImageHelper.ConvertImageToBase64(hostingEnvironment.ContentRootPath + AppLogo.Value);
                AppLogoDto model = new AppLogoDto
                {
                    Value = logo
                };
                return View(model);

            }
            catch (Exception e)
            {
                await errorLogService.InsertException(e).ConfigureAwait(true);
                return View();
            }

        }
    }



    [ViewComponent(Name = "SessionManager")]
    public class VCSessionManager : ViewComponent
    {
        private readonly IErrorLogService errorLogService;
        private readonly ISystemConstantsService systemConstantsService;

        public VCSessionManager(IErrorLogService errorLogService, ISystemConstantsService systemConstantsService)
        {
            this.errorLogService = errorLogService;
            this.systemConstantsService = systemConstantsService;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {

            SessionManagerDto model = new SessionManagerDto
            {
                SessionTimeout = 20,
            };
            try
            {
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

                model.SessionTimeout = Convert.ToInt32(Convert.ToDecimal(SessionTimeout.Value));
                
                return View(model);

            }
            catch (Exception e)
            {
                await errorLogService.InsertException(e).ConfigureAwait(true);
                return View(model);
            }

        }
    }




    [ViewComponent(Name = "MediaSelectorButton")]
    public class VCMediaSelectorButton : ViewComponent
    {
        public IViewComponentResult Invoke(string Id, string FieldLabel, string Width, string Height)
        {
            ViewData["Id"] = Id;
            ViewData["FieldLabel"] = FieldLabel;
            ViewData["Width"] = Width;
            ViewData["Height"] = Height;
            return View();
        }
    }

    [ViewComponent(Name = "MediaSelectorButton2")]
    public class VCMediaSelectorButton2 : ViewComponent
    {
        public IViewComponentResult Invoke(string Id, string FieldLabel)
        {
            ViewData["Id"] = Id;
            ViewData["FieldLabel"] = FieldLabel;
            return View();
        }
    }
}
