using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace Shortchase.Entities
{
    public class PromotionPost : IntBase
    {
        public string Slug { get; set; }
        public string Title { get; set; }
        public string FeaturedImage { get; set; }
        public bool IsPublished { get; set; }
        public string Excerpt { get; set; }
        public string Content { get; set; }
        public DateTime? DatePublished { get; set; }
    }
}