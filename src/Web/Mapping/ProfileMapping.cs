using AutoMapper;
using Microsoft.eShopWeb.ApplicationCore.Entities.OrderAggregate;
using Microsoft.eShopWeb.Web.Pages.Admin;
using Microsoft.eShopWeb.Web.ViewModels;

namespace Microsoft.eShopWeb.Web.Mapping;

public class ProfileMapping:Profile
{
    public ProfileMapping()
    {
        CreateMap<OrderStatusViewModel, OrderViewModel>()
            .ForMember(dest => dest.OrderStatusId, opt => opt.MapFrom(src => src.OrderStatusId))
            .ReverseMap(); 

        CreateMap<OrderStatus, OrderStatusViewModel>
            ().ForMember(dest => dest.OrderStatusId, opt => opt.MapFrom(src => src.OrderStatusId));
    }
}
