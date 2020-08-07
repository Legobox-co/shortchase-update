using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace Shortchase.Entities
{
    public class UserContact : IntBase
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public string Origin { get; set; }
        
        [ForeignKey(nameof(User))]
        public Guid? UserId { get; set; }

        public virtual User User { get; set; }
    }
}