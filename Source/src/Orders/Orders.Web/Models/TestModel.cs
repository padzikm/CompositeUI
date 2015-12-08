using System.ComponentModel.DataAnnotations;

namespace CompositeUI.Orders.Web.Models
{
    public class TestModel
    {
        [CustomValidation]
        [Display(Description = "some description")]
        public int Age { get; set; }
    }

    public class CustomValidationAttribute : ValidationAttribute
    {
        public override bool IsValid(object value)
        {
            if (value == null)
                return false;
            var casted = (int) value;
            return casted > 0;
        }
    }
}