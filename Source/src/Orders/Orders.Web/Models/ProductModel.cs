using System.Collections.Generic;
using CompositeUI.Web.Common.Attributes;

namespace CompositeUI.Orders.Web.Models
{
    public class ProductModel
    {
        [Select(Multiple = true)]
        public Dictionary<string, string> ProductCategories { get; set; } 
    }
}