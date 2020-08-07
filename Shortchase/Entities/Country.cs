using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;

namespace Shortchase.Entities
{
    public class Country : IntBase
    {
        public string Name { get; set; }
        public string Code { get; set; }
        public bool IsDefault { get; set; }
        public virtual ICollection<User> Users { get; }
        public virtual ICollection<Address> Addresses { get; }
    }
}