using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace Shortchase.Entities
{
    public class Bookmaker : IntBase
    {
        public string Description { get; set; }
        public bool IsEnabled { get; set; }
        public bool Deleted { get; set; }
        public virtual ICollection<BetListing> BetListings { get; }
    }
}