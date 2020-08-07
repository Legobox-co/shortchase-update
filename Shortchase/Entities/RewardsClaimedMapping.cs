using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Shortchase.Entities
{
    public class RewardsClaimedMapping : GuidBase
    {
        public int PointsClaimed { get; set; }
        public decimal EquivalentAmount { get; set; }
        public bool IsUsed { get; set; }
        public DateTime? UsedDate { get; set; }
        public string ClaimedIPAddress { get; set; }
        public string UsedIPAddress { get; set; }
        public string Type { get; set; }

        [Required]
        [ForeignKey(nameof(User))]
        public Guid UserId { get; set; }

        [Required]
        public virtual User User { get; set; }
        public string CouponCode { get; set; }

    }
}