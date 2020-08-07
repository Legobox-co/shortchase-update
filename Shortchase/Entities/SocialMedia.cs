using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace Shortchase.Entities
{
    public class SocialMedia : IntBase
    {
        public string Name { get; set; }
        public string Link { get; set; }
        public string Icon { get; set; }
        public bool IsEnabled { get; set; }
    }
}