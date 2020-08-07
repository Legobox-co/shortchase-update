using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace Shortchase.Entities
{
    public class SemiStaticText : IntBase
    {
        public string Name { get; set; }
        public string Value { get; set; }
        public bool IsEnabled { get; set; }
    }
}