using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Shortchase.Entities;

namespace Shortchase.Dtos
{
    public class CreateListingSubCategoryDto
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public int CategoryId { get; set; }
    }
    public class EditListingSubCategoryDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int CategoryId { get; set; }
    }

    public class ListingSubCategoryListDto
    {
        public ICollection<ListingSubCategory> ListingSubCategories { get; set; }
        public ICollection<SelectListItem> CategoriesOptions { get; set; }
    }


    public class WebsiteListingSubCategoryDto
    {
        public ICollection<ListingSubCategory> ListingSubCategories { get; set; }
    }

}