using Microsoft.eShopWeb.ApplicationCore.Entities.OrderAggregate;
using Microsoft.eShopWeb.Web.Pages.Admin;

namespace Microsoft.eShopWeb.Web.Interfaces;

public interface IOrderStatusViewModelService
{
    Task<OrderStatus> UpdateOrderStatusAsync(int Id,string newStatus);
    Task<string> GetOrderStatusName(int orderStatusId);
    Task<List<OrderStatusViewModel>> GetAllOrderStatuses();
}
