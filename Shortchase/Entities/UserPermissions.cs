using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Shortchase.Entities
{
    public class UserPermissions : IntBase
    {
        [ForeignKey("User")]
        public Guid UserId { get; set; }

        public virtual User User { get; set; }

        [ForeignKey("Permissions")]
        public int PermissionsId { get; set; }

        public virtual Permissions Permissions { get; set; }
    }
}