using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace Shortchase.Entities
{
    public class POTDListingLiveReport : GuidBase
    {
        public string Report { get; set; }
        public bool Deleted { get; set; }
        public DateTime DateTimeReported { get; set; }

        [ForeignKey(nameof(ReportedBy))]
        public Guid ReportedById { get; set; }
        public virtual User ReportedBy { get; set; }

        [ForeignKey(nameof(POTD))]
        public Guid POTDId { get; set; }
        public virtual POTDListing POTD { get; set; }


        public virtual ICollection<POTDListingLiveReportingInteraction> UserInteractions { get; }

    }
}