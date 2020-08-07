using System.Web;

namespace Shortchase.Helpers
{
    public static class EmailTemplates
    {
        public static class Account
        {
            public const string RegistrationCode = "Account.RegistrationCode";
            public const string ForgotPassword = "Account.ForgotPassword";
            public const string PasswordChanged = "Account.PasswordChanged";
            public const string RegistrationComplete = "Account.RegistrationComplete";
            public const string EmailConfirmed = "Account.EmailConfirmed";
            public const string UserRewardClaimed = "Account.UserRewardClaimed";
            public const string UserDiscount = "Account.UserDiscount";
            public const string SendReferral = "Account.SendReferral";
            public const string SendReferralDiscount = "Account.SendReferralDiscount";
            public const string UserSubscriptionOrder = "Account.UserSubscriptionOrder";
            public const string UserSubscriptionOrderPaypal = "Account.UserSubscriptionOrderPaypal";
            public const string UserBetListingOrder = "Account.UserBetListingOrder";
            public const string SecondaryEmailTemplate = "Account.SecondaryEmailTemplate";
            public const string ContactForm = "Account.ContactForm";
            public const string ListingsInOrder = "Account.ListingsInOrder";
        }
    }

    public static class GoogleAPIData
    {
        public const string ClientID = "974257331478-2m4bgi5p4d0hj858gribllp6tr32pijj.apps.googleusercontent.com";//"208869913751-qmdt66pc4siscm6bunpm939mhq7s0cui.apps.googleusercontent.com";
        public const string ClientSecret = "eb-SAHnAbUtZuPgwFl0flZXA";//"RdJ1QdD8CPHrl1vkFqsRdqVY";
        public const string RedirectURL = "http://shortchase.com/Home/FinishImportContactsFromGoogle";//"https://localhost:44328/Home/FinishImportContactsFromGoogle";
        public const string ApplicationName = "clever-span-265112";//"shortchase-app-test";
        public const string Scope = "https://www.googleapis.com/auth/contacts.readonly";
    }
    public static class OutlookAPIData
    {
        public const string ClientID = "52b9e8c6-33e5-40a0-9f23-9bc0a3f25bf5";//"cd964e96-cf4a-4ff2-917a-604941650f54";
        public const string ClientSecret = "E[E/AdOKT@10e1K4XG=y66b=7PHlWc7H";//"kT785kvE@VdR:OUaZinUfeRxsGkEZ7_?";
        public const string RedirectURL = "http://shortchase.com/Home/FinishImportContactsFromOutlook";//"https://localhost:44328/Home/FinishImportContactsFromOutlook";
        public const string ApplicationName = "Shortchase";//"ShortchaseTest";
        public const string GetContactsURL = "https://outlook.office.com/api/v2.0/me/contacts";
        public const string Scope = "https://outlook.office.com/contacts.read";
        public const string AuthURL = "https://login.microsoftonline.com/common/oauth2/v2.0/authorize";
        public const string TokenURL = "https://login.microsoftonline.com/common/oauth2/v2.0/token";

        public static string GetURL() {
            string url = OutlookAPIData.AuthURL + "?client_id="+ OutlookAPIData.ClientID + "&scope="+ OutlookAPIData.Scope+ "&response_type=code&redirect_uri="+ HttpUtility.UrlEncode(OutlookAPIData.RedirectURL);
            return url;
        }
        public static string GetTokenURL(string code) {
            string url = OutlookAPIData.AuthURL + "?grant_type=authorization_code&code="+ code + "&client_id=" + OutlookAPIData.ClientID + "&client_secret=" + OutlookAPIData.ClientSecret + "&redirect_uri="+ HttpUtility.UrlEncode(OutlookAPIData.RedirectURL) + "&scope=" + OutlookAPIData.Scope;
            return url;

        }
    }
    public static class UserContactOrigins
    {
        public const string Google = "Google";
        public const string Outlook = "Outlook";
    }
    
    public static class POTDPoints
    {
        public const int CorrectPrediction = 5;
    }

    public static class AccessLogType
    {
        public const string SignIn = "Sign In";
        public const string SignOut = "Sign Out";
    }
    public static class SystemConstantType
    {
        public const string Int = "Int";
        public const string Decimal = "Decimal";
        public const string String = "String";
    }
    public static class SystemConstantName
    {
        public const string Taxes = "Estimated Taxes";
        public const string RegularFees = "Regular Service Fee";
        public const string BoisterousFees = "Boisterous Service Fee";
        public const string SessionTimeout = "Session Timeout";
    }

    public static class GlobalURLs
    {
        public const string Website = "http://shortchase.com/";
        public const string LocalWebsite = "https://localhost:44328/";
        public const string DevelopmentWebsite = "http://shortchase.com/";
    }

    public static class DiscountValues
    {
        public const decimal Standard = 25.00m;
    }

    public static class SystemFlagsNames
    {
        public const string DisplayAddresses = "DisplayAddresses";
    }

    public static class SemiStaticTextNames
    {
        public const string TermsOfService = "TermsOfService";
        public const string PrivacyPolicy = "PrivacyPolicy";
        public const string CookieConsent = "CookieConsent";
        public const string AppName = "AppName";
        public const string AppTagline = "AppTagline";
        public const string AppLogo = "AppLogo";
    }

    public static class OrderTypes
    {
        public const string Regular = "Regular";
        public const string Boisterous = "Boisterous";
    }
    
    public static class FAQType
    {
        public const string General = "General";
        public const string Capper = "Capper";
        public const string Bettor = "Bettor";
    }

    public static class Roles
    {
        public const string User = "User";
        public const string Admin = "Admin";
    }

    public static class IdentitySettings
    {
        public const int PasswordLength = 8;
        public const int LockoutTries = 10;
        public const int LockoutTime = 60 * 24 * 30 * 12 * 10; //In Minutes
    }


    public static class BetListingLimits
    {
        public const int Free = 5;
        public const int Premium = 5;
        public const int Live = 0;
    }


    public static class SubscriptionPlanType
    {
        public const string ShortchasePro = "ShortchasePro";
        public const string Boisterous = "Boisterous";
    }

    public static class SortOrderType
    {
        public const string Ascending = "ASC";
        public const string Descending = "DESC";
    }

    public static class SortListingTypeType
    {
        public const string Boisterous = "Boisterous";
        public const string Newest = "Newest";
    }

    public static class BetListingType
    {
        public const string Free = "Free";
        public const string Premium = "Premium";
        public const string Live = "Live";
    }
    public static class BetListingOddsFormat
    {
        public const string Decimal = "Decimal";
        public const string Fractional = "Fractional";
        public const string Moneyline = "Moneyline";
    }

    public static class UserType
    {
        public const string Admin = "Admin";
        public const string Capper = "Capper";
        public const string Bettor = "Bettor";
    }
    public static class UserInteractionType
    {
        public const string Like = "Like";
        public const string Dislike = "Dislike";
    }
    public static class APIType
    {
        public const string Sportradar = "Sportradar";
        public const string LiveScore = "LiveScore";
    }


    public static class UserSubscriptionPaymentStatus
    {
        public const string Gift = "Gift";
        public const string Paid = "Paid";
        public const string Wallet = "Paid with Wallet Balance";
        public const string Pending = "Pending";
        public const string Cancelled = "Cancelled";
        public const string Rejected = "Rejected";
    }

    public static class DiscountType
    {
        public const string Gift = "Gift";
        public const string Claimed = "Claimed";
    }

    public static class ReportListingReasons
    {
        public const string MisleadingScam = "Misleading or Scam";
        public const string Spam = "Spam";
        public const string FakeNews = "Fake News";
        public const string Prohibited = "Prohibited Content";
        public const string Other = "Other";
    }


    public static class CartClaimType
    {
        public const string Cart = "Cart";
    }
    
    public static class OrderPaymentType
    {
        public const string Wallet = "Wallet";
        public const string Paypal = "Paypal";
    }


    public static class OrderPaymentStatus
    {
        public const string Paid = "Paid";
        public const string Pending = "Pending";
        public const string Cancelled = "Cancelled";
        public const string Rejected = "Rejected";
    }

    public static class PaypalPaymentStatus
    {
        public const string CREATED = "CREATED";
        public const string SAVED = "SAVED";
        public const string PENDING = "PENDING";
        public const string APPROVED = "APPROVED";
        public const string VOIDED = "VOIDED";
        public const string COMPLETED = "COMPLETED";
    }
    public static class PaypalSubscriptionPaymentStatus
    {
        public const string APPROVAL_PENDING = "APPROVAL_PENDING";
        public const string APPROVED = "APPROVED";
        public const string ACTIVE = "ACTIVE";
        public const string SUSPENDED = "SUSPENDED";
        public const string CANCELLED = "CANCELLED";
        public const string EXPIRED = "EXPIRED";

        /*
        APPROVAL_PENDING. The subscription is created but not yet approved by the buyer.
        APPROVED. The buyer has approved the subscription.
        ACTIVE. The subscription is active.
        SUSPENDED. The subscription is suspended.
        CANCELLED. The subscription is cancelled.
        EXPIRED. The subscription is expired.
         
         */
    }
    public static class PaypalPayoutStatus
    {
        public const string CREATED = "CREATED";
        public const string PENDING = "PENDING";
        public const string PROCESSING = "PROCESSING";
        public const string DENIED = "DENIED";
        public const string CANCELED = "CANCELED";
        public const string COMPLETED = "SUCCESS";
        public const string SUCCESS = "SUCCESS";
    }
    public static class PredictionValue
    {
        public const string HomeWin = "Home team to win";
        public const string Draw = "Both teams to draw";
        public const string AwayWin = "Away team to win";
    }
    public static class APIValidationCategories
    {
        public const string AmericanFootball = "American Football";
        public const string Badminton = "Badminton";
        public const string Baseball = "Baseball";
        public const string Basketball = "Basketball";
        public const string Cricket = "Cricket";
        public const string Darts = "Darts";
        public const string Handball = "Handball";
        public const string IceHockey = "Ice Hockey";
        public const string Motorsport = "Motorsport";
        public const string Snooker = "Snooker";
        public const string Squash = "Squash";
        public const string UFC = "Martial Arts / UFC";
        public const string Volleyball = "Volleyball";
        public const string Football = "Football";
        public const string TableTennis = "Table Tennis";
    }




    public static class APILinks
    {
        public static class PayPal
        {
            public const string GetToken = "https://api.paypal.com/v1/oauth2/token";
            public const string Payouts = "https://api.paypal.com/v1/payments/payouts";
            public const string Products = "https://api.paypal.com/v1/catalogs/products";
            public const string SubscriptionPlans = "https://api.paypal.com/v1/billing/plans";
            public const string VerifySubscriptionPlans = "https://api.paypal.com/v1/billing/subscriptions/";
        }
        public static class PayPalSandbox
        {
            public const string GetToken = "https://api.sandbox.paypal.com/v1/oauth2/token";
            public const string Payouts = "https://api.sandbox.paypal.com/v1/payments/payouts";
            public const string Products = "https://api.sandbox.paypal.com/v1/catalogs/products";
            public const string SubscriptionPlans = "https://api.sandbox.paypal.com/v1/billing/plans";
            public const string VerifySubscriptionPlans = "https://api.sandbox.paypal.com/v1/billing/subscriptions/";
        }
        public static class CurrencyExchange
        {
            public const string Rates = "https://api.exchangeratesapi.io/latest?base=CAD";
            public const string LatestRates = "http://data.fixer.io/api/latest?access_key=9c194efac54740fe8c76bb350c53b1f6";

        }
    }
}