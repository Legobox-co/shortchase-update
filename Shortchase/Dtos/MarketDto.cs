using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Shortchase.Entities;

namespace Shortchase.Dtos
{
    public class CreateMarketDto
    {
        public string Name { get; set; }
        public int CategoryId { get; set; }
    }
    public class EditMarketDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int CategoryId { get; set; }
    }

    public class MarketListDto
    {
        public ICollection<Market> Markets { get; set; }
        public ICollection<SelectListItem> CategoriesOptions { get; set; }
    }


}