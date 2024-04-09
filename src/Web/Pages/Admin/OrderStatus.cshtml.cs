using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.eShopWeb.ApplicationCore.Entities.OrderAggregate;
using Microsoft.eShopWeb.ApplicationCore.Interfaces;
using Microsoft.eShopWeb.Infrastructure.Data.Queries;
using Microsoft.eShopWeb.Web.Interfaces;
using Microsoft.eShopWeb.Web.ViewModels;

namespace Microsoft.eShopWeb.Web.Pages.Admin
{
    [Authorize(Roles = BlazorShared.Authorization.Constants.Roles.ADMINISTRATORS)]
    public class OrderStatusModel : PageModel
    {
       
        private readonly IOrderStatusQueryService _orderStatusQueryService;
        private readonly IRepository<OrderStatus> _orderRepository;
        public OrderStatusModel(IRepository<OrderStatus> orderRepository, IOrderStatusQueryService orderStatusQueryService)
        {
            _orderRepository = orderRepository;
            _orderStatusQueryService = orderStatusQueryService;
        }

        public OrderViewModel OrderStatusViewModel { get; private set; } = new OrderViewModel();

        public void OnGet(OrderViewModel orderModel)
        {
            OrderStatusViewModel = orderModel;
            var statusList =  _orderStatusQueryService.GetAllOrderStatuses();
            ViewData["StatusList"] = statusList;
        }
        public async Task<IActionResult> OnPostChangeOrderStatusAsync(int Id, string newStatus)
        {
            var result = await _orderStatusQueryService.ChangeOrderStatusAsync(Id, newStatus);
            if (result == null )
            {
                return NotFound();
            }
            return RedirectToPage("/Order/Detail", new { Id });
        }
    



    }
}
