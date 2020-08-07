using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace Shortchase.Entities
{
    public class MediaFolder : GuidBase
    {
        public string Name { get; set; }
        public DateTime LastUpdated { get; set; }
        public bool Deleted { get; set; }

        [ForeignKey(nameof(ParentFolder))]
        public Guid? ParentFolderId { get; set; }

        public virtual MediaFolder ParentFolder { get; set; }
    }
}