using AutoMapper;
using Services.AuthorizationAPI.Models;
using Services.AuthorizationAPI.Models.ViewModels;

namespace Services.AuthorizationAPI.Extensions;

public class MappingExtension
{
    public static MapperConfiguration RegisterMaps()
    {
        var mappingConfiguration = new MapperConfiguration(config =>
        {
            config.CreateMap<UserViewModel, User>();
            config.CreateMap<User, UserViewModel>();
            config.CreateMap<AddressViewModel, AddressesUser>();
            config.CreateMap<AddressesUser, AddressViewModel>();
        });

        return mappingConfiguration;
    }
}