using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace Shortchase.Entities
{
    public class UserSubscription : GuidBase
    {
        public string Type { get; set; }
        public string Name { get; set; }
        public decimal SubscriptionPrice { get; set; }
        public decimal PaidValue { get; set; }
        public decimal WalletBalanceBeforePurchase { get; set; }
        public decimal WalletBalanceAfterPurchase { get; set; }
        public bool Deleted { get; set; }
        public bool AutoRenew { get; set; }
        public bool HasBeenAutoRenewed { get; set; }
        public DateTime SubscriptionStart { get; set; }
        public DateTime SubscriptionEnd { get; set; }
        public DateTime? SubscriptionCancelDate { get; set; }
        public DateTime? SubscriptionDeleteDate { get; set; }
        public DateTime? DatePaid { get; set; }
        public DateTime? DateCancelled { get; set; }
        public DateTime? DateRejected { get; set; }
        public string PaypalOrderId { get; set; }
        public string PaypalOrderStatus { get; set; }
        public string ReceiptPDF { get; set; }
        public decimal TotalPaidOnPaypal { get; set; }

        public string PaymentStatus { get; set; }
        public string PaypalSubscriptionId { get; set; }
        public string PaypalFacilitatorAccessToken { get; set; }

        [ForeignKey(nameof(User))]
        public Guid UserId { get; set; }
        public virtual User User { get; set; }

        [ForeignKey(nameof(Subscription))]
        public int SubscriptionId { get; set; }
        public virtual SubscriptionPlan Subscription { get; set; }

        [ForeignKey(nameof(GiftBy))]
        public Guid? GiftById { get; set; }

        public virtual User GiftBy { get; set; }

    }
}