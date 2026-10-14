using System.ComponentModel.DataAnnotations;

namespace practical_8.Models
{
    public class Product
    {
        public int ProductID { get; set; }

        [Required(ErrorMessage = "Product name is required.")]
        [StringLength(100)]
        [Display(Name = "Product Name")]
        public string ProductName { get; set; } = string.Empty;

        [Required(ErrorMessage = "Category is required.")]
        [StringLength(50)]
        public string Category { get; set; } = string.Empty;

        [Range(0, int.MaxValue,
            ErrorMessage = "Quantity cannot be negative.")]
        public int Quantity { get; set; }

        [Range(typeof(decimal), "0", "99999999.99",
            ErrorMessage = "Enter a valid price.")]
        [Display(Name = "Unit Price")]
        public decimal UnitPrice { get; set; }
    }
}