using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Shortchase.Entities;
using System;
using Microsoft.AspNetCore.Http;

namespace Shortchase.Dtos
{


    public class POTDLiveReportingListDto
    {
        public POTDListing POTD { get; set; }
        public ICollection<POTDListingLiveReport> LiveReportings { get; set; }

    }

    public class POTDViewPredictionsListDto
    {
        public POTDListing POTD { get; set; }
        public ICollection<POTDListingPrediction> Predictions { get; set; }

    }

    public class CreatePOTDLiveReportingDto
    {
        public string Report { get; set; }
        public string DateTimeReported { get; set; }

        public Guid ReportedById { get; set; }
        
        public Guid POTDId { get; set; }
        public int TimezoneOffset { get; set; }

    }



    public class CreatePOTDLiveReportingUserInteractionDto
    {
        public string InteractionType { get; set; }

        public Guid POTDLiveReportId { get; set; }

        public Guid InteractedById { get; set; }
    }
}