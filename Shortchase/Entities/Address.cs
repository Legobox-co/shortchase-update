using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace Shortchase.Entities
{
    public class Address : IntBase
    {
        public string Location { get; set; }
        public string StreetAddress { get; set; }
        public string City { get; set; }
        public string Province { get; set; }
        public string ZipCode { get; set; }

        [ForeignKey(nameof(Country))]
        public int? CountryId { get; set; }

        public virtual Country Country { get; set; }
        public bool IsPrimary { get; set; }
        public bool IsEnabled { get; set; }
    }
}