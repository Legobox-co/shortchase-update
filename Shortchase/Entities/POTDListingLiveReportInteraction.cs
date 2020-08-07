using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace Shortchase.Entities
{
    public class POTDListingLiveReportingInteraction : GuidBase
    {
        public string InteractionType { get; set; }

        [ForeignKey(nameof(InteractedBy))]
        public Guid InteractedById { get; set; }
        public virtual User InteractedBy { get; set; }

        [ForeignKey(nameof(POTDLiveReport))]
        public Guid POTDLiveReportId { get; set; }
        public virtual POTDListingLiveReport POTDLiveReport { get; set; }

    }
}