using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace Shortchase.Entities
{
    public class PaypalSettings : IntBase
    {
        public string Name { get; set; }
        public string ClientID { get; set; }
        public string Secret { get; set; }
        public bool IsActive { get; set; }
        public bool IsDefault { get; set; }
    }
}