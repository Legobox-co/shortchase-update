using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;

namespace Shortchase.Entities
{
    public class SystemFlags : IntBase
    {
        public string Name { get; set; }
        public bool Value { get; set; }
    }
}