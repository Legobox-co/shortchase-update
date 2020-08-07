using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Shortchase.Entities;

namespace Shortchase.Dtos
{
    public class CreateAddressDto
    {
        public string Location { get; set; }
        public string StreetAddress { get; set; }
        public string City { get; set; }
        public string Province { get; set; }
        public string ZipCode { get; set; }
        public int? CountryId { get; set; }
        public bool IsPrimary { get; set; }
        public bool IsEnabled { get; set; }
    }
    public class EditAddressDto
    {
        public int Id { get; set; }
        public string Location { get; set; }
        public string StreetAddress { get; set; }
        public string City { get; set; }
        public string Province { get; set; }
        public string ZipCode { get; set; }
        public int? CountryId { get; set; }
        public bool IsPrimary { get; set; }
        public bool IsEnabled { get; set; }
    }

    public class AddressListDto
    {
        public ICollection<Address> Addresses { get; set; }
        public ICollection<SelectListItem> Countries { get; set; }
        public int DefaultCountryId { get; set; }
        public bool HasDisabled { get; set; }
        public bool DisplayAddresses { get; set; }
    }


    public class WebsiteAddressDto
    {
        public ICollection<Address> Addresses { get; set; }
        public bool DisplayAddresses { get; set; }
        public string CookieConsentMessage { get; set; }
    }

}