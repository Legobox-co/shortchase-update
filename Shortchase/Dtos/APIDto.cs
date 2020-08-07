using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Shortchase.Entities;

namespace Shortchase.Dtos
{
    public class APINBA
    {
        public object date { get; set; }
        public object league { get; set; }
        public object games { get; set; }
    }
    

}