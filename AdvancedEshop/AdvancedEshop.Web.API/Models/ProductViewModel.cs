namespace AdvancedEshop.Web.API.Models
{
    public class ProductViewModel
    {
        public int ProductId { get; set; }
        public string? ProductName { get; set; }
        public string? ProductDescription { get; set; }
        public string? CategoryName { get; set; }
        public decimal ProductPrice { get; set; }
        public decimal? ProductDiscount { get; set; }
        public string? ProductPhoto { get; set; }
        public int SizeId { get; set; }
        public int ColorId { get; set; }
        public bool IsTrandy { get; set; }
        public bool IsArrived { get; set; }
        public string? ColorName { get; set; }
        public string? SizeName { get; set; }
    }
}
