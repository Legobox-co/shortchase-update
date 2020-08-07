using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;

namespace Shortchase.Entities
{
    public class SystemConstants : IntBase
    {
        public string Name { get; set; }
        public string Value { get; set; }
        public string Type { get; set; }
    }
}