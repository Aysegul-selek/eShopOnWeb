using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.eShopWeb.ApplicationCore.Entities.OrderAggregate;
using Microsoft.eShopWeb.ApplicationCore.Interfaces;
using Microsoft.eShopWeb.Web.Interfaces;
using Microsoft.eShopWeb.Web.Pages.Admin;
using Microsoft.eShopWeb.Web.ViewModels;

namespace Microsoft.eShopWeb.Web.Services;

public class OrderStatusViewModelService : IOrderStatusViewModelService
{
    private readonly IMapper _mapper;
    private readonly IRepository<OrderStatus> _orderStatusRepository;
    private readonly IUriComposer _uriComposer;
    private readonly IOrderStatusQueryService _orderStatusQueryService;

    public OrderStatusViewModelService(IOrderStatusQueryService orderStatusQueryService, IUriComposer uriComposer, IRepository<OrderStatus> orderStatusRepository, IMapper mapper)
    {
        _orderStatusQueryService = orderStatusQueryService;
        _uriComposer = uriComposer;
        _orderStatusRepository = orderStatusRepository;
        _mapper = mapper;
    }

    public async Task<List<OrderStatusViewModel>> GetAllOrderStatuses()
    {
      
        var orderStatuses = await _orderStatusRepository.ListAsync();
        var orderStatusViewModels = _mapper.Map<List<OrderStatus>, List<OrderStatusViewModel>>(orderStatuses);

        return orderStatusViewModels;
    }


    public async Task<string> GetOrderStatusName(int orderStatusId)
    {
        var orderStatusName = await _orderStatusQueryService.GetOrderStatusById(orderStatusId);

        return orderStatusName;
    }

    public Task<OrderStatus> UpdateOrderStatusAsync(int Id, string newStatus)
    {
        throw new NotImplementedException();
    }
}
