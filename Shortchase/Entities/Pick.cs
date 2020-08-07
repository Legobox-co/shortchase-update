using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace Shortchase.Entities
{
    public class Pick : IntBase
    {
        public string Team1 { get; set; }
        public string Team2 { get; set; }
        public bool IsEnabled { get; set; }
        public bool Deleted { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime FinishTime { get; set; }
        public string Team1PhotoPath { get; set; }
        public string Team2PhotoPath { get; set; }

        [ForeignKey(nameof(Category))]
        public int CategoryId { get; set; }

        public virtual ListingCategory Category { get; set; }
        public virtual ICollection<BetListing> BetListings { get; }
        public virtual ICollection<POTDListing> POTDListings { get; }
    }
}