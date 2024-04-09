using AutoMapper;
using Microsoft.eShopWeb.ApplicationCore.Entities.OrderAggregate;


public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<Order, OrderStatus>()
            .ForMember(dest => dest.OrderStatusId, opt => opt.MapFrom(src => src.OrderStatusId))
            .ReverseMap(); 
       

    }
}
