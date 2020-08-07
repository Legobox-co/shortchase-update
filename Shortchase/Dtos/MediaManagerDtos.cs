using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Shortchase.Entities;
using System;
using Microsoft.AspNetCore.Http;

namespace Shortchase.Dtos
{
    public class MediaFolderListDto
    {
        public ICollection<SelectListItem> ParentFolderOptions { get; set; }
        public ICollection<MediaFolder> Folders { get; set; }
        public ICollection<MediaFile> FilesInFolder { get; set; }
        public Guid? TopLevelId { get; set; }
        public MediaFolder CurrentFolder { get; set; }
    }

    public class MediaFolderItemDto
    {
        public Guid? Id { get; set; }
        public string Name { get; set; }
        public Guid? ParentFolderId { get; set; }

    }

    public class CreateMediaFileItemDto
    {
        public IFormFile File { get; set; }
        public string Title { get; set; }
        public Guid? FolderId { get; set; }

    }

    public class MediaFileItemDto
    {
        public Guid Id { get; set; }
        public string Title { get; set; }

    }
    public class CroppedMediaFileItemDto
    {
        public Guid Id { get; set; }
        public string CroppedMedia { get; set; }
        public decimal CroppedMediaHeight { get; set; }
        public decimal CroppedMediaWidth { get; set; }

    }
    public class MediaManagerPickerItemDto
    {
        public string Id { get; set; }
        public string Title { get; set; }
        public string Media { get; set; }
        public string LastUpdated { get; set; }

    }
    public class MoveMediaFileDto
    {
        public Guid Id { get; set; }
        public Guid? FolderToId { get; set; }

    }
}