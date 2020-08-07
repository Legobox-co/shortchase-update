using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using Shortchase.Authorization;
using Shortchase.Entities;

namespace Shortchase.Helpers
{
    public class DataContext : IdentityDbContext<User, Role, Guid>
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            //Additional Configurations
            modelBuilder.Entity<EmailTemplate>().HasAlternateKey(c => c.Name);
            modelBuilder.Entity<Permissions>().HasAlternateKey(c => c.Name);
            modelBuilder.Entity<SystemFlags>().HasAlternateKey(c => c.Name);
            modelBuilder.Entity<User>().HasMany(c => c.UserSubscriptions).WithOne(u => u.User);
            modelBuilder.Entity<User>().HasMany(c => c.GiftedBySubscriptions).WithOne(u => u.GiftBy);
            modelBuilder.Entity<UserRating>().HasOne(c => c.From).WithMany(c => c.FromRatings);
            modelBuilder.Entity<UserRating>().HasOne(c => c.To).WithMany(c => c.ToRatings);
            modelBuilder.Entity<UserFollow>().HasOne(c => c.From).WithMany(c => c.FromFollows);
            modelBuilder.Entity<UserFollow>().HasOne(c => c.To).WithMany(c => c.ToFollows);
            modelBuilder.Entity<Message>().HasOne(c => c.From).WithMany(c => c.FromMessages);
            modelBuilder.Entity<Message>().HasOne(c => c.To).WithMany(c => c.ToMessages);

            modelBuilder.Entity<UserPermissions>().HasIndex(e => new { e.PermissionsId, e.UserId }).IsUnique();

            //Seed Data
            //modelBuilder.Entity<Role>().HasData(
            //    new Role { Name = Helpers.Roles.User, Description = "", Id = new Guid("cbf2840e-72be-4e6b-86ef-157386a45883"), NormalizedName = Helpers.Roles.User.ToUpper() },
            //    new Role { Name = Helpers.Roles.Admin, Description = "", Id = new Guid("2c9dbdcc-c1bb-4cd0-bdf5-7d8aeb60cd4d"), NormalizedName = Helpers.Roles.Admin.ToUpper() }
            //);

            modelBuilder.Entity<EmailTemplate>().HasData(
                new EmailTemplate { Id = 1, Name = Helpers.EmailTemplates.Account.RegistrationCode, RowDate = DateTime.Now, Subject = "Registration Code", Body = "{0}" },
                new EmailTemplate { Id = 2, Name = Helpers.EmailTemplates.Account.ForgotPassword, RowDate = DateTime.Now, Subject = "Forgot Password", Body = "{0}" }
            );

            modelBuilder.Entity<EmailConfig>().HasData(
                new EmailConfig { Active = true, Display_name = "EmailSender", Email = "info@calibreconsulting.ca", Enable_ssl = true, Host = "smtp.office365.com", Id = 1, Is_default_email_account = true, Password = "2254273Cc+", Port = 587, RowDate = DateTime.Now, User_name = "info@calibreconsulting.ca" }
            );


            modelBuilder.Entity<Permissions>().HasData(
                new Permissions { Id = 1, Description = "This allows the user to access every feature", Name = "AccessAll", GroupName = "SuperAdmin", Value = (ushort)Permission.AccessAll, RowDate = DateTime.Now },
                new Permissions { Id = 2, Description = "Basic User Role", Name = "User", GroupName = "Basic User", Value = (ushort)Permission.User, RowDate = DateTime.Now },
                new Permissions { Id = 3, Description = "User Has no Role", Name = "NotSet", GroupName = null, Value = (ushort)Permission.NotSet, RowDate = DateTime.Now }
            );
        }

        public DbSet<ErrorLog> ErrorLogs { get; set; }
        public DbSet<EmailConfig> EmailConfigs { get; set; }
        public DbSet<EmailTemplate> EmailTemplates { get; set; }
        public DbSet<Permissions> Permissions { get; set; }
        public DbSet<UserPermissions> UserPermissions { get; set; }
        public DbSet<Country> Countries { get; set; }
        public DbSet<RewardsMapping> RewardsMappings { get; set; }
        public DbSet<RewardsClaimedMapping> RewardsClaimedMappings { get; set; }
        public DbSet<Address> Addresses { get; set; }
        public DbSet<SystemFlags> SystemFlags { get; set; }
        public DbSet<SocialMedia> SocialMedias { get; set; }
        public DbSet<SemiStaticText> SemiStaticTexts { get; set; }
        public DbSet<BlogPost> BlogPosts { get; set; }
        public DbSet<FAQItem> FAQItems { get; set; }
        public DbSet<NewsPost> NewsPosts { get; set; }
        public DbSet<PromotionPost> PromotionPosts { get; set; }
        public DbSet<ListingCategory> ListingCategories { get; set; }
        public DbSet<ListingSubCategory> ListingSubCategories { get; set; }
        public DbSet<SubscriptionPlan> SubscriptionPlans { get; set; }
        public DbSet<UserSubscription> UserSubscriptions { get; set; }
        public DbSet<BetListing> BetListings { get; set; }
        public DbSet<BetListingReport> BetListingReports { get; set; }
        public DbSet<POTDListing> POTDListings { get; set; }
        public DbSet<POTDListingPrediction> POTDListingPredictions { get; set; }
        public DbSet<POTDListingLiveReport> POTDListingLiveReports { get; set; }
        public DbSet<POTDListingLiveReportingInteraction> POTDListingLiveReportingInteractions { get; set; }
        public DbSet<Currency> Currencies { get; set; }
        public DbSet<UserRating> UserRatings { get; set; }
        public DbSet<UserFollow> UserFollows { get; set; }
        public DbSet<AccessLog> AccessLogs { get; set; }
        public DbSet<Market> Markets { get; set; }
        public DbSet<Tip> Tips { get; set; }
        public DbSet<Bookmaker> Bookmakers { get; set; }
        public DbSet<Pick> Picks { get; set; }
        public DbSet<PaypalSettings> PaypalSettings { get; set; }
        public DbSet<ShoppingCart> ShoppingCarts { get; set; }
        public DbSet<SystemConstants> SystemConstants { get; set; }
        public DbSet<OrderItem> OrderItems { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<Notification> Notifications { get; set; }
        public DbSet<Message> Messages { get; set; }
        public DbSet<UserDiscount> UserDiscounts { get; set; }
        public DbSet<UserPayout> UserPayouts { get; set; }
        public DbSet<SecondaryEmailTemplate> SecondaryEmailTemplates { get; set; }
        public DbSet<MediaFolder> MediaFolders { get; set; }
        public DbSet<MediaFile> MediaFiles { get; set; }
        public DbSet<UserContact> UserContacts { get; set; }
    }
}