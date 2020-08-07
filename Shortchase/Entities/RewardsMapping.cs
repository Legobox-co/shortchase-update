using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;

namespace Shortchase.Entities
{
    public class RewardsMapping : IntBase
    {
        public int Points { get; set; }
        public decimal EquivalentAmount { get; set; }
        public bool IsAvailable { get; set; }
    }
}