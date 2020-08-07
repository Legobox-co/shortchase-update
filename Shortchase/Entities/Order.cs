using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace Shortchase.Entities
{
    public class Order : GuidBase
    {
        public string PaymentType { get; set; }
        public string PaymentStatus { get; set; }
        public string OrderType { get; set; }
        public string OrderNumber { get; set; }
        public DateTime? DatePaid { get; set; }
        public DateTime? DateCancelled { get; set; }
        public DateTime? DateRejected { get; set; }
        public string CancelledReason { get; set; }
        public string RejectedReason { get; set; }
        public decimal TotalBeforeTaxAndFees { get; set; }
        public decimal TotalAfterTax { get; set; }
        public decimal ServiceFee { get; set; }
        public decimal EstimatedTax { get; set; }
        public decimal ServiceFeePercent { get; set; }
        public decimal EstimatedTaxPercent { get; set; }
        public decimal WalletBalanceBeforePurchase { get; set; }
        public decimal WalletBalanceAfterPurchase { get; set; }
        public decimal CapperComission { get; set; }
        public decimal DiscountPercent { get; set; }
        public decimal Discount { get; set; }
        public decimal TotalBeforeDiscount { get; set; }
        public decimal TotalPaidOnPaypal { get; set; }
        public string PaypalOrderId { get; set; }
        public string PaypalOrderStatus { get; set; }
        public string ReceiptPDF { get; set; }

        public virtual ICollection<OrderItem> Items { get; }

        [ForeignKey(nameof(User))]
        public Guid UserId { get; set; }
        public virtual User User { get; set; }

    }
}