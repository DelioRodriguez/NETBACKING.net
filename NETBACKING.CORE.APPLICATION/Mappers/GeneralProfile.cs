using AutoMapper;
using NETBACKING.CORE.APPLICATION.DTOs;
using NETBACKING.CORE.APPLICATION.Interfaces.Services.Products;
using NETBACKING.CORE.APPLICATION.Models;
using NETBACKING.CORE.APPLICATION.ViewModels.Beneficiary;
using NETBACKING.CORE.APPLICATION.ViewModels.Products;
using NETBACKING.CORE.DOMAIN.Entities;

namespace NETBACKING.CORE.APPLICATION.Mappers;

public class GeneralProfile : Profile            
{
    public GeneralProfile()
    {
        CreateMap<Product, ProductViewModel>().ReverseMap()
            .ForMember(dest => dest.ApplicationUserId, opt => opt.Ignore());

        // Mapeo de Beneficiary a BeneficiaryViewModel y viceversa
        CreateMap<Beneficiary, BeneficiaryViewModel>().ReverseMap();

        // Mapeo de ProductCreateViewModel a Product y viceversa, ignorando ApplicationUserId
        CreateMap<ProductCreateViewModel, Product>().ReverseMap()
            .ForMember(dest => dest.ApplicationUserId, opt => opt.Ignore());

        // Mapeo de CreateUserDto a UserModel y viceversa
        CreateMap<CreateUserDto, UserModel>().ReverseMap();
        CreateMap<EditUserDto, UserModel>().ReverseMap();
        // Mapeo de CreateUserDto a ProductCreateViewModel con configuraciones específicas, ignorando ApplicationUserId
        CreateMap<CreateUserDto, ProductCreateViewModel>()
            .ForMember(dest => dest.ProductType, opt => opt.MapFrom(src => "CuentaAhorro"))
            .ForMember(dest => dest.Balance, opt => opt.MapFrom(src => src.InitialAmount))
            .ForMember(dest => dest.IsPrimary, opt => opt.MapFrom(src => true))
            .ForMember(dest => dest.ApplicationUserId, opt => opt.Ignore());
    }
}