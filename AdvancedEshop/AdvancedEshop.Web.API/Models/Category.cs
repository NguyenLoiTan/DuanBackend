using System.ComponentModel.DataAnnotations;

namespace AdvancedEshop.Web.API.Models
{
    public class Category
    {
        [Key]
        public int CategoryId { get; set; }
        [StringLength(150)]
        public string? CategoryName { get; set; }
        [StringLength(300)]
        public string? CategoryDescription { get; set; }
        public int CategoryOrder { get; set; }
    }
}
