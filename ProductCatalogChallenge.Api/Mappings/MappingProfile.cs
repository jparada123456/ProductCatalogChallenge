using AutoMapper;
using ProductCatalogChallenge.Api.Models.Requests;
using ProductCatalogChallenge.Api.Models.Responses;
using ProductCatalogChallenge.Application.Commands;
using ProductCatalogChallenge.Domain.Entities;

namespace ProductCatalogChallenge.Api.Mappings
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<CreateProductRequest, CreateProductCommand>();
            CreateMap<UpdateProductRequest, UpdateProductCommand>();
            CreateMap<Product, CreateProductResponse>()
                .ForMember(dest => dest.ProductId, opt => opt.MapFrom(src => src.ProductId))
                .ForMember(dest => dest.Message, opt => opt.MapFrom(src => "Product created successfully."));
        }
    }
}
