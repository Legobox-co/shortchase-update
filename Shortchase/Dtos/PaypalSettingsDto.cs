using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Rendering;
using Shortchase.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Shortchase.Dtos
{
    public class PaypalSettingsListDto
    {
        public ICollection<PaypalSettings> Settings { get; set; }
    }
    
    public class CreatePaypalSettingsDto
    {
        public string Name { get; set; }
        public string ClientID { get; set; }
        public string Secret { get; set; }
        public bool IsDefault { get; set; }
    }
    public class EditPaypalSettingsDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string ClientID { get; set; }
        public string Secret { get; set; }
        public bool IsDefault { get; set; }
    }


    public class MassPaypalPayoutFormSubmissionDto
    {
        public IFormFile CsvFile { get; set; }
    }


    public class MassPaypalPayoutListItemDto
    {
        public string Email { get; set; }
        public string Reason { get; set; }
        public decimal Value { get; set; }
    }
    public class MassPaypalPayoutUserListDto
    {
        public string PayoutJsonString { get; set; }
        public ICollection<MassPaypalPayoutUserListItemDto> Users { get; set; }
        public ICollection<MassPaypalPayoutListItemDto> NonUsers { get; set; }
    }
    public class MassPaypalPayoutUserListItemDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public decimal Value { get; set; }
        public string PaypalEmail { get; set; }
        public bool PaypalConnected { get; set; }
    }
    public class MassPaypalPayoutRetrieveUserListItemDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string PaypalEmail { get; set; }
    }

    public class PaypalMassBatchPayoutDto
    {
        public Guid UserId { get; set; }
        public decimal Value { get; set; }
    }
}