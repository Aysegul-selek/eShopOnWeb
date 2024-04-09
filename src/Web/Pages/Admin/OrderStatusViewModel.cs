using Microsoft.eShopWeb.ApplicationCore.Entities.OrderAggregate;
using Microsoft.eShopWeb.Web.ViewModels;

namespace Microsoft.eShopWeb.Web.Pages.Admin;

public class OrderStatusViewModel: OrderDetailViewModel
{
    public int OrderStatusId { get; set; }
    public string Name { get; set; }
    public List<OrderStatus> Statuses { get; set; }
}
