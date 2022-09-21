﻿using API.DTO;
using AutoMapper;
using Core.Entities;
using Core.Entities.Entites;

namespace API.Helpers
{
    public class MappingProfiles : Profile
    {
        public MappingProfiles()
        {
            CreateMap<Product, ProductToReturnDTO>()
                .ForMember(destination => destination.ProductBrand, options => options.MapFrom(source => source.ProductBrand.Name))
                .ForMember(destination => destination.ProductType, options => options.MapFrom(source => source.ProductType.Name))
                .ForMember(destination => destination.PictureUrl, options => options.MapFrom<ProductUrlResolver>());
            CreateMap<Address, AddressDTO>().ReverseMap();
            CreateMap<CustomerBasketDTO, CustomerBasket>();
            CreateMap<BasketItemDTO, BasketItem>();
        }
    }
}
