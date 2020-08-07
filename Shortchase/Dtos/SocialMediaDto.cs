using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Shortchase.Entities;

namespace Shortchase.Dtos
{
    
    public class SocialMediaList
    {
        public ICollection<SocialMedia> Medias { get; set; }
    }

    public class CreateSocialMediaDto
    {
        public string Name { get; set; }
        public string Link { get; set; }
        public string Icon { get; set; }
    }
    public class EditSocialMediaDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Link { get; set; }
        public string Icon { get; set; }
    }

}