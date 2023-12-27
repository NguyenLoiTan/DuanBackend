using AdvancedEshop.Web.API.Models;

namespace AdvancedEshop.Web.Models
{
    public interface IOrderRepository
    {
        IQueryable<Order> Orders { get; }
        void SaveOrder(Order order);
        IQueryable<Product> Products { get; }
    }

}
