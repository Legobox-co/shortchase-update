using Shortchase.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Shortchase.Helpers
{
    public class PotdPredictionVM
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string UserId { get; set; }
        public string Prediction { get; set; }
        public DateTime DatePredicted { get; set; }
        public string Image { get; set; }
        public DateTime Rowdate { get; set; }
        public Guid POTDID { get; set; }
       public virtual Pick Picks { get; set; }
    }
}
