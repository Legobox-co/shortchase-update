using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace Shortchase.Entities
{
    public class BetListingReport : GuidBase
    {
        public string ReportContent { get; set; }
        public string ReportTopic { get; set; }
        public bool IsCorrect { get; set; }
        public DateTime DateReported { get; set; }

        [ForeignKey(nameof(ReportedListing))]
        public Guid ReportedListingId { get; set; }
        public virtual BetListing ReportedListing { get; set; }

        [ForeignKey(nameof(ReportedBy))]
        public Guid ReportedById { get; set; }
        public virtual User ReportedBy { get; set; }


    }
}