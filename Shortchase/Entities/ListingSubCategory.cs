using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace Shortchase.Entities
{
    public class ListingSubCategory : IntBase
    {
        public string Name { get; set; }
        public bool Deleted { get; set; }
        public string Description { get; set; }
        public string DisplayImage { get; set; }
        public string MarketplaceImage { get; set; }
        public bool IsEnabled { get; set; }

        [ForeignKey(nameof(Category))]
        public int? CategoryId { get; set; }
        public virtual ListingCategory Category { get; set; }
        public virtual ICollection<BetListing> BetListings { get; }
        public virtual ICollection<POTDListing> POTDsListings { get; }
    }
}