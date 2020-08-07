using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace Shortchase.Entities
{
    public class Currency : GuidBase
    {
        public string Name { get; set; }
        public string Acronym { get; set; }
        public bool Deleted { get; set; }
    }
}