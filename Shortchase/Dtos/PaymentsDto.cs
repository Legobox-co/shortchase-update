using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Rendering;
using Shortchase.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Shortchase.Dtos
{
    public class OrdersPayoutDto
    {
        public ICollection<OrdersPayoutItemDto> Orders { get; set; }
    }

    public class OrdersPayoutItemDto
    {
        public Order Order { get; set; }
        public bool HasPendingPayouts { get; set; }
        public string PayoutJson { get; set; }
    }

    public class OrderPayoutDetailsDto
    {

        public Order Order { get; set; }
        public string PayoutsJson { get; set; }
        public ICollection<OrderItem> OrderItems { get; set; }
        public ICollection<OrderItemPayoutDto> Payouts { get; set; }


        public OrderPayoutDetailsDto FromOrderDetailsDto(OrderDetailsDto data)
        {
            return new OrderPayoutDetailsDto
            {
                Order = data.Order,
                OrderItems = data.OrderItems,
                Payouts = new List<OrderItemPayoutDto>(),
                PayoutsJson = ""
            };
        }
    }


    public class OrderItemPayoutDto
    {
        public Guid UserToPayId { get; set; }
        public string UserToPay { get; set; }
        public string UserEmailToPay { get; set; }
        public bool PaypalConnected { get; set; }
        public decimal ValueToPay { get; set; }
    }
}