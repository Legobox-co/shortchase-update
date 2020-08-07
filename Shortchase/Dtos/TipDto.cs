using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Shortchase.Entities;

namespace Shortchase.Dtos
{
    public class CreateTipDto
    {
        public string Name { get; set; }
        public int MarketId { get; set; }
        public string Description { get; set; }
    }
    public class EditTipDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int MarketId { get; set; }
        public string Description { get; set; }
    }

    public class TipListDto
    {
        public ICollection<Tip> Tips { get; set; }
        public ICollection<SelectListItem> Markets { get; set; }
    }


}