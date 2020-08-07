using Microsoft.AspNetCore.Mvc.Rendering;
using Shortchase.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Shortchase.Dtos
{
    public class CreateClaimRewardDto
    {
        public int PointsClaimed { get; set; }
        public string DiscountType { get; set; }
        public decimal EquivalentAmount { get; set; }
        public Guid UserId { get; set; }
    }
    public class DiscountsListDto
    {
        public ICollection<RewardsClaimedMapping> Discounts { get; set; }
        public ICollection<SelectListItem> RewardsOptions { get; set; }
        public ICollection<SelectListItem> UsersOptions { get; set; }
    }


    public class CreateDiscountRewardDto
    {
        public decimal EquivalentAmount { get; set; }
        public Guid UserId { get; set; }
    }

}