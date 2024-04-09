using Ardalis.GuardClauses;
using AutoMapper;
using MediatR;
using Microsoft.eShopWeb.ApplicationCore.Entities.OrderAggregate;
using Microsoft.eShopWeb.ApplicationCore.Interfaces;
using Microsoft.eShopWeb.Web.ViewModels;

namespace Microsoft.eShopWeb.Web.Features.OrderDetails;
public class GetOrderStatusQuery : IRequest<OrderDetailViewModel>
{
    public int OrderStatusId { get; set; }
}
public class OrderStatusHandler : IRequestHandler<GetOrderStatusQuery, OrderDetailViewModel?>
{
    private readonly IReadRepository<OrderStatus> _orderStatusRepository;
    private readonly IMapper _mapper;

    public OrderStatusHandler(IReadRepository<OrderStatus> orderStatusRepository, IMapper mapper)
    {
        _orderStatusRepository = orderStatusRepository;
        _mapper = mapper;
    }

    public async Task<OrderDetailViewModel?> Handle(GetOrderStatusQuery request, CancellationToken cancellationToken)
    {
        var order = await _orderStatusRepository.GetByIdAsync(request.OrderStatusId);
        if (order == null)
            return null;

        var orderDetailViewModel = _mapper.Map<OrderDetailViewModel>(order);
        orderDetailViewModel = new OrderDetailViewModel
        {
           
            Status = order.Name
        };

        return orderDetailViewModel;
    }
}

