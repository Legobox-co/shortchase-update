using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Shortchase.Entities;
using System;
using System.Linq;

namespace Shortchase.Dtos
{
    public class OrderDetailsDto
    {

        public Order Order { get; set; }
        public ICollection<OrderItem> OrderItems { get; set; }
    }
    
    public class OrderDetailsReceiptDto
    {

        public Order Order { get; set; }
        public ICollection<IGrouping<string, OrderItem>> OrderItems { get; set; }
    }


    public class ViewListingsInOrderDto
    {

        public Order Order { get; set; }
        public ICollection<OrderItem> OrderItems { get; set; }
        public OrderItem CurrentItem { get; set; }
    }

    public class OrderReceiptDto
    {

        public OrderDetailsDto Details { get; set; }
        public Address PrimaryAddress { get; set; }
        public string Logo { get; set; }
        public int TimezoneOffset { get; set; }
    }
    public class OrderReceiptPDFDto
    {

        public OrderDetailsReceiptDto Details { get; set; }
        public Address PrimaryAddress { get; set; }
        public string Logo { get; set; }
        public int TimezoneOffset { get; set; }
    }

    public class SubscriptionReceiptDto
    {
        public string SubscriptionName { get; set; }
        public string SubscriptionPrice { get; set; }
        public string PaidValue { get; set; }
        public string PaymentStatus { get; set; }
        public string Start { get; set; }
        public string End { get; set; }
        public string WalletBalanceBefore { get; set; }
        public string WalletBalanceAfter { get; set; }
        public string PaymentType { get; set; }
        public string PaypalPaidValue { get; set; }
        public string Description { get; set; }
        public string Qty { get; set; }
        public string Logo { get; set; }
        public int TimezoneOffset { get; set; }
    }
    public class SubscriptionReceipt2Dto
    {
        public string SubscriptionName { get; set; }
        public string SubscriptionPrice { get; set; }
        public string PaidValue { get; set; }
        public string PaymentStatus { get; set; }
        public string Start { get; set; }
        public string End { get; set; }
        public string WalletBalanceBefore { get; set; }
        public string WalletBalanceAfter { get; set; }
        public string PaymentType { get; set; }
        public string PaypalPaidValue { get; set; }
        public string Description { get; set; }
        public string Qty { get; set; }
        public string Logo { get; set; }
        public int TimezoneOffset { get; set; }
        public Address PrimaryAddress { get; set; }
        public DateTime OrderDate { get; set; }
        public User OrderUser { get; set; }
    }
}