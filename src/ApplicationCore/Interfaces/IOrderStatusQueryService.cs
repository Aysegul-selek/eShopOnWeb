using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.eShopWeb.ApplicationCore.Entities.OrderAggregate;

namespace Microsoft.eShopWeb.ApplicationCore.Interfaces;
public interface IOrderStatusQueryService
{
    Task<OrderStatus> ChangeOrderStatusAsync(int orderStatusId, string newStatus);
    Task<string> GetOrderStatusById(int orderStatusId);
    Task<List<OrderStatus>> GetAllOrderStatuses();
  
}
