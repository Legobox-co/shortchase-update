using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Shortchase.Entities;
using Microsoft.AspNetCore.Http;

namespace Shortchase.Dtos
{
    public class CreatePickDto
    {
        public string Team1 { get; set; }
        public string Team2 { get; set; }
        public string StartTime { get; set; }
        public string FinishTime { get; set; }
        public IFormFile Team1PhotoFile { get; set; }
        public IFormFile Team2PhotoFile { get; set; }
        public int CategoryId { get; set; }
        public int TimezoneOffset { get; set; }
    }
    public class EditPickDto
    {
        public int Id { get; set; }
        public string Team1 { get; set; }
        public string Team2 { get; set; }
        public string StartTime { get; set; }
        public string FinishTime { get; set; }

        public string Team1Photo { get; set; }
        public string Team2Photo { get; set; }
        public int CategoryId { get; set; }
        public int TimezoneOffset { get; set; }
    }

    public class PickListDto
    {
        public ICollection<Pick> Picks { get; set; }
        public ICollection<SelectListItem> CategoriesOptions { get; set; }
    }


}