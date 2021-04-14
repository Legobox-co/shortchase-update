using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Shortchase.Entities
{
    public class PredictionComment
    {
        [Key]
        public int Id { get; set; }
        public Guid? CommenterId { get; set; }
        public Guid PredictionId { get; set; }
        public string Comment { get; set; }

    }
}
