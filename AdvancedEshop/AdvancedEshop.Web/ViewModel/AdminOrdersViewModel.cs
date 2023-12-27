// AdminOrdersViewModel.cs

using System.Collections.Generic;
using AdvancedEshop.Web.Models;

public class AdminOrdersViewModel
{
    public IEnumerable<Order> AllOrders { get; set; }
    public IEnumerable<Order> UnshippedOrders { get; set; }
    public IEnumerable<Order> ShippedOrders { get; set; }
}
