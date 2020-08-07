using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Shortchase.Entities;
using System;

namespace Shortchase.Dtos
{
    public class MessagesListDto
    {
        public string UserName { get; set; }
        public int UnreadMessagesCount { get; set; }
        public Guid UserId { get; set; }
    }

    public class MessagesList2Dto
    {
        public string UserName { get; set; }
        public IEnumerable<int> UnreadMessagesCount { get; set; }
        public Guid UserId { get; set; }
    }
    
    public class MessageRetrievedAdminDto
    {
        public string UserName { get; set; }
        public ICollection<MessageRetrievedDto> Messages { get; set; }
        public Guid UserId { get; set; }
    }

}