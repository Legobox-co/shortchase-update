using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace Shortchase.Entities
{
    public class User : IdentityUser<Guid>
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string ConfirmationCode { get; set; }
        public DateTime RowDate { get; set; }
        public DateTime BirthDate { get; set; }
        public string ProfilePicture { get; set; }
        public string BioDescription { get; set; }
        public string ReferredByEmail { get; set; }
        public string PaypalAccountEmail { get; set; }

        [ForeignKey(nameof(PhoneCountry))]
        public int? PhoneCountryId { get; set; }

        public virtual Country PhoneCountry { get; set; }
        public string Company { get; set; }
        public int TotalPointsAvailable { get; set; }
        public int TotalPointsClaimed { get; set; }
        public decimal WalletBalance { get; set; }
        public int Rating { get; set; }
        public bool IsActive { get; set; }
        public bool IsDeleted { get; set; }
        public bool AcceptedCookies { get; set; }
        public string ReferralCode { get; set; }
        public string GmailToken { get; set; }
        public string GmailRefreshToken { get; set; }
        public string GmailCode { get; set; }
        public string OutlookToken { get; set; }
        public string OutlookRefreshToken { get; set; }
        public string OutlookCode { get; set; }
        public virtual ICollection<UserPermissions> Permissions { get; }
        public virtual ICollection<UserContact> Contacts { get; }
        public virtual ICollection<UserSubscription> UserSubscriptions { get; }
        public virtual ICollection<UserSubscription> GiftedBySubscriptions { get; }
        public virtual ICollection<BetListing> BetListings { get; }
        public virtual ICollection<BetListingReport> BetListingsReported { get; }
        public virtual ICollection<POTDListingPrediction> POTDListingsPredicted { get; }
        public virtual ICollection<UserRating> FromRatings { get; }
        public virtual ICollection<UserRating> ToRatings { get; }
        public virtual ICollection<UserFollow> FromFollows { get; }
        public virtual ICollection<UserFollow> ToFollows { get; }
        public virtual ICollection<AccessLog> AccessLogs { get; }
        public virtual ICollection<ShoppingCart> Carts { get; }
        public virtual ICollection<Order> Orders { get; }
        public virtual ICollection<Notification> Notifications { get; }
        public virtual ICollection<Message> FromMessages { get; }
        public virtual ICollection<Message> ToMessages { get; }
        public virtual ICollection<UserDiscount> Discounts { get; }
        public virtual ICollection<UserPayout> Payouts { get; }
    }
}