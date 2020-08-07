using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace Shortchase.Entities
{
    public class FAQItem : IntBase
    {
        public string Question { get; set; }
        public string Answer { get; set; }
        public string Type { get; set; }
        public bool IsActive { get; set; }
    }
}