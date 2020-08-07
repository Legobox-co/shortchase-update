using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace Shortchase.Entities
{
    public class Tip : IntBase
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public bool IsEnabled { get; set; }
        public bool Deleted { get; set; }

        [ForeignKey(nameof(Market))]
        public int MarketId { get; set; }

        public virtual Market Market { get; set; }
        public virtual ICollection<BetListing> BetListings { get; }
        public virtual ICollection<POTDListing> POTDListings { get; }
    }
}