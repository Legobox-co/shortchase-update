using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace Shortchase.Entities
{
    public class POTDListingPrediction : GuidBase
    {
        public string Prediction { get; set; }
        public bool Deleted { get; set; }
        //public bool UserValidate { get; set; }
        public DateTime DatePredicted { get; set; }
        public DateTime? DateVerified { get; set; }
        public bool? VerifiedAsCorrect { get; set; }

        [ForeignKey(nameof(Market))]
        public int MarketId { get; set; }
        public virtual Market Market { get; set; }

        [ForeignKey(nameof(Tip))]
        public int TipId { get; set; }
        public virtual Tip Tip { get; set; }

        [ForeignKey(nameof(PredictedBy))]
        public Guid PredictedById { get; set; }
        public virtual User PredictedBy { get; set; }

        [ForeignKey(nameof(POTD))]
        public Guid POTDId { get; set; }
        public virtual POTDListing POTD { get; set; }

    }
}