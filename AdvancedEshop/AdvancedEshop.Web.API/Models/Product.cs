using Microsoft.AspNetCore.Mvc.ModelBinding;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AdvancedEshop.Web.API.Models
{
    public class Product
    {
        [Key]
        public int ProductId { get; set; }
        [Required(ErrorMessage = "Please enter a product name")]
        [StringLength(150)]

        public string? ProductName { get; set; }
        [StringLength(3000)]
        [Required(ErrorMessage = "Please enter a description")]

        public string? ProductDescription { get; set; }
        [ForeignKey("Category")]
        [Required(ErrorMessage = "Please specify a category")]
        public int CategoryId { get; set; }
        /*[BindNever]
        public Category? Category { get; set; }*/
        [Required]
        [Range(0.01, double.MaxValue, ErrorMessage = "Please enter a positive price")]
        [Column(TypeName = "decimal(8,2)")]
        public decimal? ProductPrice { get; set; }
        [Column(TypeName = "decimal(8,2)")]
        public decimal? ProductDiscount { get; set;}
        [StringLength(300)]
        public string? ProductPhoto {  get; set; }
        [ForeignKey("Size")]
        public int SizeId { get; set; }
        /*[BindNever]
        public Size? Size { get; set; }*/
        [ForeignKey("Color")]
        public int ColorId { get; set; }
        /*[BindNever]
        public Color? Color { get; set; }*/
        public bool IsTrandy { get; set; }
        public bool IsArrived { get; set; }
    }
}
