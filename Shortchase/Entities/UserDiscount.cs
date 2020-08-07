using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace Shortchase.Entities
{
    public class UserDiscount : IntBase
    {
        public decimal DiscountPercentageValue { get; set; }
        public DateTime? DateUsed { get; set; }
        public string OriginUserEmail { get; set; }

        [ForeignKey(nameof(User))]
        public Guid UserId { get; set; }

        public virtual User User { get; set; }
    }
}