using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace Shortchase.Entities
{
    public class SecondaryEmailTemplate : IntBase
    {
        public string Subject { get; set; }
        public string Title { get; set; }
        public string Greeting { get; set; }
        public string Content { get; set; }
    }
}