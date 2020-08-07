using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace Shortchase.Entities
{
    public class MediaFile : GuidBase
    {
        public string PhysicalPath { get; set; }
        public string Title { get; set; }
        public string URL { get; set; }
        public decimal Size { get; set; }
        public decimal Width { get; set; }
        public decimal Height { get; set; }
        public DateTime LastUpdated { get; set; }
        public bool Deleted { get; set; }

        [ForeignKey(nameof(Folder))]
        public Guid? FolderId { get; set; }

        public virtual MediaFolder Folder { get; set; }
    }
}