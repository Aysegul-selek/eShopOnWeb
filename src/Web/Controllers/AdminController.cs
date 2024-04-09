using Ardalis.GuardClauses;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.eShopWeb.ApplicationCore.Entities.OrderAggregate;
using Microsoft.eShopWeb.ApplicationCore.Interfaces;
using Microsoft.eShopWeb.Infrastructure.Data;
using Microsoft.eShopWeb.Web.Features.MyOrders;
using Microsoft.eShopWeb.Web.Features.OrderDetails;
using Microsoft.eShopWeb.Web.Interfaces;
using Microsoft.eShopWeb.Web.Pages.Admin;
using Microsoft.eShopWeb.Web.ViewModels;

namespace Microsoft.eShopWeb.Web.Controllers;
[ApiExplorerSettings(IgnoreApi = true)]
[Authorize] // Controllers that mainly require Authorization still use Controller/View; other pages use Pages
[Route("[controller]/[action]")]

public class AdminController : Controller
{
    private readonly CatalogContext _dbContext;
    private readonly IOrderStatusQueryService _orderStatusService;
    private readonly IOrderStatusViewModelService _orderStatusView;

    private readonly IMediator _mediator;

    public AdminController(IMediator mediator, IOrderStatusQueryService orderStatusService, IOrderStatusViewModelService orderStatusView, CatalogContext dbContext)
    {
        _mediator = mediator;
        _orderStatusService = orderStatusService;
        _orderStatusView = orderStatusView;
        _dbContext = dbContext;
    }


    [HttpGet]
    public async Task<IActionResult> Orders()
    {
        Guard.Against.Null(User?.Identity?.Name, nameof(User.Identity.Name));
        var viewModel = await _mediator.Send(new GetMyOrders(User.Identity.Name));

        return View(viewModel);
    }
    [HttpGet("{orderId}")]
    public async Task<IActionResult> Detail(int orderId)
    {
        Guard.Against.Null(User?.Identity?.Name, nameof(User.Identity.Name));

        var viewModel = await _mediator.Send(new GetOrderDetails(User.Identity.Name, orderId));

        if (viewModel == null)
        {
            return BadRequest("No such order found for this user.");
        }

        var orderDetailViewModel = new OrderDetailViewModel();
        orderDetailViewModel.OrderStatusName = await _orderStatusService.GetOrderStatusById(viewModel.OrderStatusId);
        return View(viewModel);
    }
    [HttpGet("{orderId}")]
    public async Task<IActionResult> OrderStatusDetail(int orderId)
    {
        Guard.Against.Null(User?.Identity?.Name, nameof(User.Identity.Name));
        var viewModel = await _mediator.Send(new GetOrderDetails(User.Identity.Name, orderId));

        if (viewModel == null)
        {
            return BadRequest("No such order found for this user.");
        }
        var orderStatusViewModel = new OrderStatusViewModel
        {
            OrderNumber = viewModel.OrderNumber,
            OrderDate = viewModel.OrderDate,
            Total = viewModel.Total,
        };

       
        orderStatusViewModel.Statuses = await _orderStatusService.GetAllOrderStatuses();

        return View(orderStatusViewModel);
    }
    [HttpGet]
    public async Task<IActionResult> Approve()
    {
     
        var viewModel = new OrderViewModel
        {
            OrderStatusId = 1, 
            OrderStatusList = await _orderStatusService.GetAllOrderStatuses()
    };

        return View(viewModel);
    }


    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Approve(int orderId, int orderStatusId)
    {
        var order = await _dbContext.Orders.FindAsync(orderId);
        if (order == null)
        {
            return NotFound();
        }
        order.OrderStatusId = orderStatusId;
        _dbContext.Update(order);
        await _dbContext.SaveChangesAsync();

        return RedirectToAction("Detail", new { orderId = orderId });
    }

}
