using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace CompositeUI.Products.Web.Models
{
    public class Product
    {
        public Guid Id { get; set; }

        [Required]
        public string Title { get; set; }

        [Required]
        public string Author { get; set; }

        [Required]
        [DataType(DataType.MultilineText)]
        public string Description { get; set; }

        public List<string> CategoriesIds { get; set; }

        public bool IsPublished { get; set; }
    }
}