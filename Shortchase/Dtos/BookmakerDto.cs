using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Shortchase.Entities;

namespace Shortchase.Dtos
{
    public class CreateBookmakerDto
    {
        public string Description { get; set; }
    }
    public class EditBookmakerDto
    {
        public int Id { get; set; }
        public string Description { get; set; }
    }

    public class BookmakerListDto
    {
        public ICollection<Bookmaker> Bookmakers { get; set; }
    }


}