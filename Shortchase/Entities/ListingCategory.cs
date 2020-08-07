using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace Shortchase.Entities
{
    public class ListingCategory : IntBase
    {
        public string Name { get; set; }
        public bool Deleted { get; set; }
        public string Description { get; set; }
        public string DisplayImage { get; set; }
        public string MarketplaceImage { get; set; }
        public string APIKey { get; set; }
        public string APIType { get; set; }
        public string APIURL { get; set; }
        public string APIMethod { get; set; }
        public bool IsEnabled { get; set; }
        public virtual ICollection<ListingSubCategory> SubCategories { get; }
        public virtual ICollection<BetListing> BetListings { get; }
        public virtual ICollection<POTDListing> POTDsListings { get; }
        public virtual ICollection<Market> Markets { get; }
        public virtual ICollection<Pick> Picks { get; }
    }
}