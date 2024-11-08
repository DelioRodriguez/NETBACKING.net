using AutoMapper;
using NETBACKING.CORE.APPLICATION.ViewModels.Beneficiary;
using NETBACKING.CORE.APPLICATION.ViewModels.Products;
using NETBACKING.CORE.DOMAIN.Entities;

namespace NETBACKING.CORE.APPLICATION.Mappers;

public class GeneralProfile : Profile            
{
    public GeneralProfile()
    {
        CreateMap<Product, ProductViewModel>().ReverseMap();
        CreateMap<Beneficiary, BeneficiaryViewModel>().ReverseMap();
    }
}