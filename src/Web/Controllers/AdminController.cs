using Ardalis.GuardClauses;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.eShopWeb.ApplicationCore.Entities.OrderAggregate;
using Microsoft.eShopWeb.ApplicationCore.Interfaces;
using Microsoft.eShopWeb.ApplicationCore.Services;
using Microsoft.eShopWeb.Infrastructure.Data;
using Microsoft.eShopWeb.Web.Features.MyOrders;
using Microsoft.eShopWeb.Web.Features.OrderDetails;
using Microsoft.eShopWeb.Web.Interfaces;
using Microsoft.eShopWeb.Web.Pages.Admin;
using Microsoft.eShopWeb.Web.ViewModels;

namespace Microsoft.eShopWeb.Web.Controllers;
[ApiExplorerSettings(IgnoreApi = true)]
[Authorize] 
[Route("[controller]/[action]")]

public class AdminController : Controller
{
 
    private readonly IOrderStatusQueryService _orderStatusService;
    private readonly IOrderStatusViewModelService _orderStatusView;

    private readonly IMediator _mediator;

    public AdminController(IMediator mediator, IOrderStatusQueryService orderStatusService, IOrderStatusViewModelService orderStatusView )
    {
        _mediator = mediator;
        _orderStatusService = orderStatusService;
        _orderStatusView = orderStatusView;
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
  
    public async Task<IActionResult> ApprovedUpdated(int orderId, int newStatusId)
    {
        var success = await _orderStatusService.UpdateOrderStatusAsync(orderId, newStatusId);

        if (success)
        {
            return RedirectToAction("Detail", new { orderId = orderId });
        }
        else
        {
            return RedirectToAction("Error");
        }

    }

}
