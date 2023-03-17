using Services.AuthorizationAPI.Models.Address;
using Services.AuthorizationAPI.Models.RequestModels.Address;

namespace Services.AuthorizationAPI.Models.Services.Interfaces;

public interface IAddressService
{
    Task<AddressAddResponseModel> AddressAdd(AddressAddRequestModel model, int? userId);

    Task<AddressesGetResponseModel> AddressesGet(int? userId);

    Task<AddressUpdateResponseModel> AddressUpdate(AddressUpdateRequestModel model, int? userId);

    Task<AddressRemoveResponseModel> AddressRemove(int addressId);

    Task<AddressSetActiveResponseModel> AddressSetActive(int addressId, int? userId);
}