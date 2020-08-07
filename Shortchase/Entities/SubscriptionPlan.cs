using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace Shortchase.Entities
{
    public class SubscriptionPlan : IntBase
    {
        public string Type { get; set; }
        public string Name { get; set; }
        public string PaypalProductId { get; set; }
        public string PaypalProductName { get; set; }
        public string PaypalSubscriptionPlanId { get; set; }
        public string PaypalSubscriptionPlanName { get; set; }
        public string PaypalSubscriptionPlanDescription { get; set; }
        public int DurationInMonths { get; set; }
        public decimal ValuePerMonth { get; set; }
        public decimal TotalValuePerDuration { get; set; }
        public bool IsActive { get; set; }

        public virtual ICollection<UserSubscription> UserSubscriptions { get; }


    }
}