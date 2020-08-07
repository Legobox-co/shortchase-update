using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Shortchase.Entities;
using System;

namespace Shortchase.Dtos
{

    public class CreateBetListingReportDto
    {
        public string ReportContent { get; set; }
        public Guid ReportedListingId { get; set; }
        public Guid ReportedById { get; set; }
        public int TimezoneOffset { get; set; }
    }


}