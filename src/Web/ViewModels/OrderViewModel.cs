using Microsoft.eShopWeb.ApplicationCore.Entities.OrderAggregate;

namespace Microsoft.eShopWeb.Web.ViewModels;

public class OrderViewModel
{
    private const string DEFAULT_STATUS = "Pending";

    public int OrderNumber { get; set; }
    public DateTimeOffset OrderDate { get; set; }
    public string BuyerId { get; set; }
    public decimal Total { get; set; }
    public List<OrderStatus> OrderStatusList { get; set; }
    public string Status { get; set; } = DEFAULT_STATUS;
    public string OrderStatusName { get; set; } = DEFAULT_STATUS;
    public int OrderStatusId { get; set; }
    public Address? ShippingAddress { get; set; }
}
