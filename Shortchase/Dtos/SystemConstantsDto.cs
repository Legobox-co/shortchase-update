using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Shortchase.Entities;

namespace Shortchase.Dtos
{
    public class ServiceFeeAndTaxesDto
    {
        public decimal Fee { get; set; }
        public decimal BoisterousFee { get; set; }
        public decimal Taxes { get; set; }
    }

    public class SessionManagerDto
    {
        public int SessionTimeout { get; set; }
    }


}