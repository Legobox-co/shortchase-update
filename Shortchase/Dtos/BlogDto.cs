using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Shortchase.Entities;

namespace Shortchase.Dtos
{
    public class BlogListItemDto
    {

        public DateTime DatePublished { get; set; }
        public DateTime DateCreated { get; set; }
        public string Slug { get; set; }
        public int Id { get; set; }
        public string Title { get; set; }
        public string Excerpt { get; set; }
        public bool IsPublished { get; set; }
    }

    public class CreateBlogPostDto
    {
        public string DatePublished { get; set; }
        public string Content { get; set; }
        public string Slug { get; set; }
        public string Title { get; set; }
        public bool IsPublished { get; set; }
        public string Excerpt { get; set; }
        public Guid? File { get; set; }
        //public IFormFile File { get; set; }
        public int TimezoneOffset { get; set; }
    }


    public class UpdateBlogPostDto
    {
        public int Id { get; set; }
        public string DatePublished { get; set; }
        public string Content { get; set; }
        public string Slug { get; set; }
        public string Title { get; set; }
        public string Excerpt { get; set; }
        public Guid? File { get; set; }
        //public IFormFile File { get; set; }
        public int TimezoneOffset { get; set; }
    }

    public class BlogListWebsiteItemDto
    {
        public DateTime DatePublished { get; set; }
        public string Slug { get; set; }
        public string Title { get; set; }
        public string FeaturedImage { get; set; }
        public string Excerpt { get; set; }
        public bool IsPublished { get; set; }
    }
}