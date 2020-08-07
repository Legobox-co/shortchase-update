using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Shortchase.Entities;
using System;

namespace Shortchase.Dtos
{
    public class CreateListingCategoryDto
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public Guid? DisplayImage { get; set; }
        //public IFormFile DisplayImage { get; set; }
        public Guid? MarketplaceImage { get; set; }
        //public IFormFile MarketplaceImage { get; set; }
    }
    public class EditListingCategoryDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public Guid? DisplayImage { get; set; }
        //public IFormFile DisplayImage { get; set; }
        public Guid? MarketplaceImage { get; set; }
        //public IFormFile MarketplaceImage { get; set; }
    }

    public class ListingCategoryListDto
    {
        public ICollection<ListingCategory> ListingCategories { get; set; }
    }


    public class WebsiteListingCategoryDto
    {
        public ICollection<ListingCategory> ListingCategories { get; set; }
    }

}