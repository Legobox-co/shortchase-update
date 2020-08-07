using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace Shortchase.Entities
{
    public class BetListing : GuidBase
    {
        public string PickType { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }

        [ForeignKey(nameof(Pick))]
        public int PickId { get; set; }
        public virtual Pick Pick { get; set; }

        public string Odds { get; set; }
        public string OddsFormat { get; set; }

        [ForeignKey(nameof(Bookmaker))]
        public int BookmakerId { get; set; }
        public virtual Bookmaker Bookmaker { get; set; }
        public string Analysis { get; set; }

        [ForeignKey(nameof(Market))]
        public int MarketId { get; set; }
        public virtual Market Market { get; set; }

        [ForeignKey(nameof(Tip))]
        public int TipId { get; set; }
        public virtual Tip Tip { get; set; }

        public decimal Stake { get; set; }
        public decimal Profit { get; set; }
        public int TimezoneOffset { get; set; }
        public bool Deleted { get; set; }
        public bool IsProCapperListing { get; set; }
        public bool IsBoisterousListing { get; set; }
        public int Views { get; set; }
        public bool IsReported { get; set; }
        public bool? IsCorrect { get; set; }
        public bool? ResultVerificationByApi { get; set; }
        public DateTime? DateVerifiedByApi { get; set; }

        [ForeignKey(nameof(Postedby))]
        public Guid PostedbyId { get; set; }
        public virtual User Postedby { get; set; }

        [ForeignKey(nameof(Category))]
        public int CategoryId { get; set; }
        public virtual ListingCategory Category { get; set; }

        [ForeignKey(nameof(SubCategory))]
        public int? SubCategoryId { get; set; }
        public virtual ListingSubCategory SubCategory { get; set; }
        public virtual ICollection<BetListingReport> Reports { get; }
        public virtual ICollection<OrderItem> Items { get; }


    }
}