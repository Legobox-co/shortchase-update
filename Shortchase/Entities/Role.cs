using Microsoft.AspNetCore.Identity;
using System;

namespace Shortchase.Entities
{
    public class Role : IdentityRole<Guid>
    {
        public string Description { get; set; }
    }
}