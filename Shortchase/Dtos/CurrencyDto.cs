using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Shortchase.Entities;
using System;

namespace Shortchase.Dtos
{
    public class CreateCurrencyDto
    {
        public string Name { get; set; }
        public string Acronym { get; set; }
    }
    public class EditCurrencyDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Acronym { get; set; }
    }

    public class CurrencyListDto
    {
        public ICollection<Currency> Currencies { get; set; }
    }



}