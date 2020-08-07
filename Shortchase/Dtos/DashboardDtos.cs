using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Shortchase.Entities;

namespace Shortchase.Dtos
{
    public class DashboardMainDto
    {
        public int TotalUsers { get; set; }
        public int TotalBoisterous { get; set; }
        public int TotalShortchasePro { get; set; }
        public AccessLog LatestAccessLog { get; set; }
    }


}