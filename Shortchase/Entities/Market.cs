using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace Shortchase.Entities
{
    public class Market : IntBase
    {
        public string Name { get; set; }
        public bool IsEnabled { get; set; }
        public bool Deleted { get; set; }
        public virtual ICollection<Tip> Tips { get; }

        [ForeignKey(nameof(Category))]
        public int CategoryId { get; set; }

        public virtual ListingCategory Category { get; set; }
        public virtual ICollection<BetListing> BetListings { get; }
        public virtual ICollection<POTDListing> POTDListings { get; }
    }
}