// OrderTableViewModel.cs

using System.Collections.Generic;
using AdvancedEshop.Web.Models;

public class OrderTableViewModel
{
    public string TableTitle { get; set; } = "Orders";
    public IEnumerable<Order> Orders { get; set; } = new List<Order>();
    public string ButtonLabel { get; set; } = "Ship";
}
