using AutoMapper;
using VR_Rentals.Models;
using VR_Rentals.ViewModels;

namespace VR_Rentals.Mapping
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Customer, AccountPageViewModel>();
            CreateMap<AccountPageViewModel, Customer>();

            CreateMap<Rental, MyOrdersPageViewModel>()
               .ForMember(dest => dest.EquipmentName, opt => opt.MapFrom(src => src.VrEquipment.EquipmentName))
               .ForMember(dest => dest.TypeName, opt => opt.MapFrom(src => src.VrEquipment.TypeName))
               .ForMember(dest => dest.Manufacturer, opt => opt.MapFrom(src => src.VrEquipment.Manufacturer))
               .ForMember(dest => dest.PricePerDay, opt => opt.MapFrom(src => src.VrEquipment.PricePerDay))
               .ForMember(dest => dest.Image, opt => opt.MapFrom(src => src.VrEquipment.Image));

            CreateMap<RegisterPageViewModel, Customer>()
                .ForMember(dest => dest.PasswordHash, opt => opt.Ignore())
                .ForMember(dest => dest.CustomerId, opt => opt.Ignore())
                .ForMember(dest => dest.Rentals, opt => opt.Ignore());

        }
    }
}
