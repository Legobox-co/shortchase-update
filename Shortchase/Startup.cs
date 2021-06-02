using Amazon.SimpleNotificationService;
using FeatureAuthorize.PolicyCode;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpOverrides;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Hosting;
using Hangfire;
using Shortchase.Authorization;
using Shortchase.Entities;
using Shortchase.Helpers;
using Shortchase.Services;
using Rotativa.AspNetCore;
using System;
using System.Globalization;

namespace Shortchase
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.Configure<CookiePolicyOptions>(options =>
            {
                // This lambda determines whether user consent for non-essential cookies is needed for a given request.
                options.CheckConsentNeeded = context => true;
                options.MinimumSameSitePolicy = SameSiteMode.None;
            });

            services.ConfigureApplicationCookie(options =>
            {
                options.AccessDeniedPath = "/Home/Index";
            });

            services.AddDbContext<DataContext>(options =>
                options.UseSqlServer(
                    Configuration.GetConnectionString("DataConnection")));

           // services.AddResponseCaching();
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_3_0);
            services.AddControllersWithViews().AddRazorRuntimeCompilation();
            services.AddResponseCompression(options =>
            {
                options.EnableForHttps = true;
                options.MimeTypes = new[] { "text/plain", "text/css","application/javascript","text/html","application/xml",
                                             "text/xml","application/json","text/json","image/png" };
            });


            //Configure Dependency Injection for application services

            //Scoped
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IErrorLogService, ErrorLogService>();
            services.AddScoped<IEmailConfigService, EmailConfigService>();
            services.AddScoped<IEmailTemplateService, EmailTemplateService>();
            services.AddScoped<IEmailSenderService, EmailSenderService>();
            services.AddScoped<IPermissionService, PermissionService>();
            services.AddScoped<ICountryService, CountryService>();
            services.AddScoped<IRewardsMappingService, RewardsMappingService>();
            services.AddScoped<IRewardsClaimedMappingService, RewardsClaimedMappingService>();
            services.AddScoped<ISMSSenderService, SMSSenderService>();
            services.AddScoped<IAddressService, AddressService>();
            services.AddScoped<ISystemFlagsService, SystemFlagsService>();
            services.AddScoped<ISocialMediaService, SocialMediaService>();
            services.AddScoped<ISemiStaticTextService, SemiStaticTextService>();
            services.AddScoped<IBlogPostsService, BlogPostsService>();
            services.AddScoped<IFAQItemService, FAQItemService>();
            services.AddScoped<INewsPostService, NewsPostService>();
            services.AddScoped<IPromotionPostService, PromotionPostService>();
            services.AddScoped<IListingCategoryService, ListingCategoryService>();
            services.AddScoped<IListingSubCategoryService, ListingSubCategoryService>();
            services.AddScoped<ISubscriptionPlanService, SubscriptionPlanService>();
            services.AddScoped<IUserSubscriptionService, UserSubscriptionService>();
            services.AddScoped<IBetListingService, BetListingService>();
            services.AddScoped<IBetListingReportService, BetListingReportService>();
            services.AddScoped<IPOTDListingService, POTDListingService>();
            services.AddScoped<IPOTDListingPredictionService, POTDListingPredictionService>();
            services.AddScoped<IPOTDListingLiveReportService, POTDListingLiveReportService>();
            services.AddScoped<IPOTDListingLiveReportingInteractionService, POTDListingLiveReportingInteractionService>();
            services.AddScoped<ICurrencyService, CurrencyService>();
            services.AddScoped<IUserRatingService, UserRatingService>();
            services.AddScoped<IUserFollowService, UserFollowService>();
            services.AddScoped<IAccessLogService, AccessLogService>();
            services.AddScoped<IMarketService, MarketService>();
            services.AddScoped<ITipService, TipService>();
            services.AddScoped<IBookmakerService, BookmakerService>();
            services.AddScoped<IPickService, PickService>();
            services.AddScoped<IPaypalSettingsService, PaypalSettingsService>();
            services.AddScoped<IShoppingCartService, ShoppingCartService>();
            services.AddScoped<ISystemConstantsService, SystemConstantsservice>();
            services.AddScoped<IOrderItemService, OrderItemService>();
            services.AddScoped<IOrderService, OrderService>();
            services.AddScoped<INotificationService, NotificationService>();
            services.AddScoped<IMessageService, MessageService>();
            services.AddScoped<IUserDiscountService, UserDiscountService>();
            services.AddScoped<IUserPayoutService, UserPayoutService>();
            services.AddScoped<IAPIValidationService, APIValidationService>();
            services.AddScoped<ISchedulerTasksService, SchedulerTasksService>();
            services.AddScoped<ISecondaryEmailTemplateService, SecondaryEmailTemplateService>();
            services.AddScoped<IMediaFolderService, MediaFolderService>();
            services.AddScoped<IMediaFileService, MediaFileService>();
            services.AddScoped<IUserContactService, UserContactService>();

            //AWS SNS (Simple Notification System) Config. Used for SMS messaging
            services.AddDefaultAWSOptions(Configuration.GetAWSOptions());
            services.AddAWSService<IAmazonSimpleNotificationService>();


            //Identities
            services.AddIdentity<User, Role>(options =>
            {
                options.Password.RequiredLength = IdentitySettings.PasswordLength;
                options.Lockout.MaxFailedAccessAttempts = IdentitySettings.LockoutTries;
            })
            .AddEntityFrameworkStores<DataContext>()
            .AddDefaultTokenProviders()
            .AddClaimsPrincipalFactory<PermissionClaimsConfiguration>();

            //Singletons
            services.TryAddSingleton<IHttpContextAccessor, HttpContextAccessor>();

            //Register the Permission policy handlers
            services.AddSingleton<IAuthorizationPolicyProvider, AuthorizationPolicyProvider>();
            services.AddScoped<IAuthorizationHandler, PermissionHandler>();


            services.AddHangfire(
                x => x.UseSqlServerStorage(Configuration.GetConnectionString("DataConnection"))
            );
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            RotativaConfiguration.Setup(env.ContentRootPath, "wwwroot//Rotativa");

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseDeveloperExceptionPage();
                app.UseExceptionHandler("/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

           

            app.UseResponseCompression();
            app.UseHttpsRedirection();
            app.UseStaticFiles(new StaticFileOptions
            {
                OnPrepareResponse = ctx =>
                {
                    // Cache static files for 30 days
                    ctx.Context.Response.Headers.Append("Cache-Control", "public,max-age=2592000");
                    ctx.Context.Response.Headers.Append("Expires", DateTime.UtcNow.AddDays(30).ToString("R", CultureInfo.InvariantCulture));
                }
            });
            app.UseForwardedHeaders(new ForwardedHeadersOptions
            {
                ForwardedHeaders = ForwardedHeaders.XForwardedFor | ForwardedHeaders.XForwardedProto
            });
            app.UseAuthentication();
            app.UseCookiePolicy();
            app.UseRouting();
            app.UseAuthorization();

            app.UseHangfireDashboard();
            app.UseHangfireServer();


            //RecurringJob.AddOrUpdate<ISchedulerTasksService>(scheduler => scheduler.ValidateByAPIJob(), "30 23 * * *");
            //RecurringJob.AddOrUpdate<ISchedulerTasksService>(scheduler => scheduler.RenewSubscriptions(), "* 1,7,13,19 * * *");

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}