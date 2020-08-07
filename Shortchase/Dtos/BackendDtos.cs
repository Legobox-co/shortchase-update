using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Rendering;
using Shortchase.Entities;

namespace Shortchase.Dtos
{
    public class AppConfigs
    {
        public SemiStaticText AppName { get; set; }
        public SemiStaticText AppTagline { get; set; }
        public SemiStaticText AppLogo { get; set; }
    }

    public class AppConfigsUpdateDto
    {
        public string AppName { get; set; }
        public string AppTagline { get; set; }
        public Guid? AppLogo { get; set; }
        //public IFormFile AppLogo { get; set; }
    }
    public class AdminSidebarDto
    {
        public string AppLogo { get; set; }
        public decimal SalesTotalAmount { get; set; }
        public int SessionTimeout { get; set; }
        public int TotalUsers { get; set; }
        public int TotalBoisterous { get; set; }
        public int TotalPro { get; set; }
    }
}