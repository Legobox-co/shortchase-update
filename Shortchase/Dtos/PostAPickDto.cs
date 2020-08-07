using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Shortchase.Entities;
using System;

namespace Shortchase.Dtos
{
    public class SubcategoryJSONDto
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public int Id { get; set; }
    }

    public class MarketJSONDto
    {
        public string Name { get; set; }
        public int Id { get; set; }
    }

    public class PickJSONDto
    {
        public string Team1 { get; set; }
        public string Team2 { get; set; }
        public string StartTime { get; set; }
        public string FinishTime { get; set; }
        public int Id { get; set; }
    }
    public class TipsJSONDto
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public int Id { get; set; }
    }
}