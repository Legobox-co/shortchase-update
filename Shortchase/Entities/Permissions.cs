using System.Collections.Generic;
using Shortchase.Authorization;

namespace Shortchase.Entities
{
    public class Permissions : IntBase
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public ushort Value { get; set; }
        public string GroupName { get; set; }
        public bool Disabled { get; set; }
        public virtual ICollection<UserPermissions> Users { get; }

        public Permission? Enum => this.Name.FindPermissionViaName();
    }
}