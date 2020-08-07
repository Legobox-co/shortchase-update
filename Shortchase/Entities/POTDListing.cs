using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace Shortchase.Entities
{
    public class POTDListing : GuidBase
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public string Venue { get; set; }
        public string Result { get; set; }
        //public string Name { get; set; }
        public string BackgroundImage { get; set; }
        
        [ForeignKey(nameof(Pick))]
        public int PickId { get; set; }
        public virtual Pick Pick { get; set; }

        [ForeignKey(nameof(Market))]
        public int MarketId { get; set; }
        public virtual Market Market { get; set; }


        [ForeignKey(nameof(Tip))]
        public int TipId { get; set; }
        public virtual Tip Tip { get; set; }
        public DateTime? DateResultInformed { get; set; }
        public bool Deleted { get; set; }
        
        [ForeignKey(nameof(Postedby))]
        public Guid PostedbyId { get; set; }
        public virtual User Postedby { get; set; }

        [ForeignKey(nameof(Category))]
        public int CategoryId { get; set; }
        public virtual ListingCategory Category { get; set; }


        [ForeignKey(nameof(SubCategory))]
        public int? SubCategoryId { get; set; }
        public virtual ListingSubCategory SubCategory { get; set; }

        public virtual ICollection<POTDListingPrediction> POTDListingPredictions { get; }

    }
}