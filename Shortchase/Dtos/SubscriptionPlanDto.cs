using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Shortchase.Entities;
using System;

namespace Shortchase.Dtos
{
    public class CreateSubscriptionPlanDto
    {
        public string Type { get; set; }
        public string Name { get; set; }
        public int DurationInMonths { get; set; }
        public decimal ValuePerMonth { get; set; }
    }
    public class EditSubscriptionPlanDto
    {
        public int Id { get; set; }
        public string Type { get; set; }
        public string Name { get; set; }
        public int DurationInMonths { get; set; }
        public decimal ValuePerMonth { get; set; }
    }

    public class SubscriptionPlanListDto
    {
        public ICollection<SubscriptionPlan> SubscriptionPlans { get; set; }
        public ICollection<SelectListItem> SubscriptionPlanTypeOptions { get; set; }
    }


    public class WebsiteSubscriptionPlanDto
    {
        public ICollection<SubscriptionPlan> SubscriptionPlans { get; set; }
    }


    public class WebsiteUserSubscriptionPlanDto
    {
        public ICollection<SubscriptionPlan> SubscriptionPlans { get; set; }
        public bool CanSubscribe { get; set; }
        public string Plan { get; set; }
    }

    public class WebsiteUserConfirmSubscriptionPlanDto
    {
        public SubscriptionPlan SubscriptionPlan { get; set; }
        public bool CanSubscribe { get; set; }
        public bool HaveEnoughWalletFunds { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public decimal WalletFundsBefore { get; set; }
        public decimal WalletFundsAfter { get; set; }
        public decimal TaxPercent { get; set; }
        public decimal TaxValue { get; set; }
        public decimal FeePercent { get; set; }
        public decimal FeeValue { get; set; }
    }


    public class WebsiteCreateUserSubscriptionDto
    {
        public int SubscriptionId { get; set; }
        public int TimezoneOffset { get; set; }

    }
    public class WebsiteCreateUserPaypalSubscriptionDto
    {
        public int SubscriptionId { get; set; }
        public int TimezoneOffset { get; set; }
        public string orderID { get; set; }

    }
    public class WebsiteCreateUserPaypalSubscription2Dto
    {
        public int SubscriptionId { get; set; }
        public int TimezoneOffset { get; set; }
        public string OrderId { get; set; }
        public string PaypalSubscriptionId { get; set; }
        public string FacilitatorAccessToken { get; set; }

    }
}