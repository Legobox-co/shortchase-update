using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace Shortchase.Entities
{
    public class OrderItem : GuidBase
    {

        public decimal Price { get; set; }
        public string ListingTitle { get; set; }
        public string SoldBy { get; set; }

        [ForeignKey(nameof(Payout))]
        public Guid? PayoutId { get; set; }
        public virtual UserPayout Payout { get; set; }

        [ForeignKey(nameof(BetListing))]
        public Guid BetListingId { get; set; }
        public virtual BetListing BetListing { get; set; }

        [ForeignKey(nameof(Order))]
        public Guid OrderId { get; set; }
        public virtual Order Order { get; set; }

    }
}