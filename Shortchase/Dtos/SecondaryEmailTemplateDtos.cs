using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.Rendering;
using Shortchase.Entities;

namespace Shortchase.Dtos
{
    public class SecondaryEmailTemplateDto
    {
        public int Id { get; set; }
        public string Subject { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
    }

    public class SecondaryEmailTemplateListDto
    {
        public ICollection<SecondaryEmailTemplate> Templates { get; set; }
        public ICollection<SecondaryEmailTemplateUserListItemDto> Users { get; set; }
    }

    public class SecondaryEmailTemplateUserListItemDto
    {
        public Guid Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
    }

    public class SecondaryEmailTemplateSendToUserDto
    {
        public int TemplateId { get; set; }
        public string Email { get; set; }
        public string Name { get; set; }
    }


}