using AdvancedEshop.Web.API.Models;

namespace AdvancedEshop.Web.Models
{
    public class Cart
    {

        public List<CartLine> Lines { get; set; } = new List<CartLine>();
        

        public virtual void AddItem(Product product, int quantity)
        {
            // Kiểm tra Lines trước khi thao tác
            if (Lines == null)
            {
                Lines = new List<CartLine>();
            }

            CartLine line = Lines
                .FirstOrDefault(p => p.Product.ProductId == product.ProductId);

            if (line == null)
            {
                Lines.Add(new CartLine
                {
                    Product = product,
                    Quantity = quantity
                });
            }
            else
            {
                line.Quantity += quantity;
            }
        }
        public virtual void GiamItem(Product product, int quantity)
        {
            CartLine line = Lines
                .FirstOrDefault(p => p.Product.ProductId == product.ProductId);

            if (line != null)
            {
                if (line.Quantity > quantity)
                {
                    line.Quantity -= quantity;
                }
                else
                {
                    RemoveLine(product);
                }
            }
        }





        public virtual void RemoveLine(Product product)
        {
            // Kiểm tra Lines trước khi thao tác
            if (Lines == null)
            {
                Lines = new List<CartLine>();
            }

            Lines.RemoveAll(l => l.Product.ProductId == product.ProductId);
        }

        public decimal ComputeTotalValue()
        {
            // Kiểm tra Lines trước khi tính toán
            if (Lines == null)
            {
                Lines = new List<CartLine>();
            }

            return (decimal)Lines.Sum(line => line.Product.ProductPrice*(1- line.Product.ProductDiscount) * line.Quantity);
        }

        public virtual void Clear()
        {
            // Kiểm tra Lines trước khi xóa
            if (Lines == null)
            {
                Lines = new List<CartLine>();
            }

            Lines.Clear();
        }

        //internal void RemoveLine(object product)
        //{
        //    throw new NotImplementedException();
        //}
    }

    public class CartLine
    {
        public int CartLineID { get; set; }
        public Product Product { get; set; } = new Product();
        public int Quantity { get; set; }
    }

}
