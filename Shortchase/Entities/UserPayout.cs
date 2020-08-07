using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace Shortchase.Entities
{
    public class UserPayout : GuidBase
    {

        public string PayoutBatchId { get; set; }
        public string PayoutSenderBatchId { get; set; }
        public string PayoutBatchStatus { get; set; }
        public string PayoutBatchCheckLink { get; set; }
        public decimal Value { get; set; }
        public decimal Fees { get; set; }
        public DateTime? PayoutBatchCompletedDate { get; set; }
        public DateTime? PayoutBatchCancelledDate { get; set; }
        public DateTime? PayoutBatchCreatedDate { get; set; }

        [ForeignKey(nameof(User))]
        public Guid UserId { get; set; }
        public virtual User User { get; set; }

        public virtual ICollection<OrderItem> OrderItems { get; }

    }
}