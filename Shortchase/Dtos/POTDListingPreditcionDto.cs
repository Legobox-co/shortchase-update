using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Shortchase.Entities;
using System;
using Microsoft.AspNetCore.Http;

namespace Shortchase.Dtos
{


    public class CreatePOTDPredictionDto
    {
        public int Market { get; set; }
        public int Tip { get; set; }
        public Guid POTDId { get; set; }

    }

    
    public class WebsiteUserPredictions
    {
        public ICollection<POTDListing> POTDs { get; set; }

    }





}