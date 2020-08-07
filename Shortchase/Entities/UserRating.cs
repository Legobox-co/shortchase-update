using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace Shortchase.Entities
{
    public class UserRating : IntBase
    {
        public int RatingValue { get; set; }

        [ForeignKey(nameof(From))]
        public Guid FromId { get; set; }
        public virtual User From { get; set; }

        [ForeignKey(nameof(To))]
        public Guid ToId { get; set; }
        public virtual User To { get; set; }

    }
}