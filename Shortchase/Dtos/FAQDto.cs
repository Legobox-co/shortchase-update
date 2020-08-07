using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Shortchase.Entities;

namespace Shortchase.Dtos
{
    public class FAQListDto
    {

        public ICollection<FAQItem> Items { get; set; }
        public ICollection<SelectListItem> FAQTypes { get; set; }
    }


    public class FAQWebsiteListDto
    {

        public ICollection<FAQItem> GeneralItems { get; set; }
        public ICollection<FAQItem> CapperItems { get; set; }
        public ICollection<FAQItem> BettorItems { get; set; }
    }
    public class CreateFAQListDto
    {
        public string Question { get; set; }
        public string Answer { get; set; }
        public string Type { get; set; }
    }
    public class UpdateFAQListDto
    {
        public int Id { get; set; }
        public string Question { get; set; }
        public string Answer { get; set; }
        public string Type { get; set; }
    }

}