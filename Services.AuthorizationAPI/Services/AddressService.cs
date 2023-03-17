using AutoMapper;
using Services.AuthorizationAPI.Database;
using Services.AuthorizationAPI.Models.Address;
using Services.AuthorizationAPI.Models.Enums;
using Services.AuthorizationAPI.Models.Repository.Interfaces;
using Services.AuthorizationAPI.Models.RequestModels.Address;
using Services.AuthorizationAPI.Models.Services.Interfaces;
using Services.AuthorizationAPI.Models.ViewModels;

namespace Services.AuthorizationAPI.Models.Services;

public class AddressService : IAddressService
{
    private readonly IAddressRepository _addressRepository;
    private readonly IUserRepository _userRepository;
    private readonly IMapper _mapper;

    public AddressService(IAddressRepository addressRepository, IMapper mapper, IUserRepository userRepository)
    {
        _addressRepository = addressRepository;
        _mapper = mapper;
        _userRepository = userRepository;
    }
    
    public async Task<AddressAddResponseModel> AddressAdd(AddressAddRequestModel model, int? userId)
    {
        try
        {
            var response = new AddressAddResponseModel();

            if (userId is null)
            {
                response.isSuccess = false;
                response.DisplayMessage = "Пользователь не авторизован";
                response.StatusCodes = StatusCode.BadRequest;
                return response;
            }

            var user = await _userRepository.GetById((int)userId!);
            if (user is null)
            {
                response.isSuccess = false;
                response.DisplayMessage = "Пользователь не найден";
                response.StatusCodes = StatusCode.BadRequest;
                return response;
            }
            
            var address = new AddressesUser
            {
                Address = model.Address,
                UserId = (int)userId,
                User = user
            };
            
            var addresses = await _addressRepository.AddressesGet((int)userId);
            if (addresses.Count() == 0)
            {
                address.isActiveAddress = 1;
            }

            var addressResponse = await _addressRepository.Create(address);

            response.Data = _mapper.Map<AddressViewModel>(addressResponse);
            response.StatusCodes = StatusCode.Created;
            return response;
        }
        catch (Exception e)
        {
            return new AddressAddResponseModel
            {
                isSuccess = false,
                DisplayMessage = "Server error",
                StatusCodes = StatusCode.InternalServerError,
                ErrorMessage = new List<string> { e.ToString() }
            };
        }
    }

    public async Task<AddressesGetResponseModel> AddressesGet(int? userId)
    {
        try
        {
            var response = new AddressesGetResponseModel();
            
            if (userId is null)
            {
                response.isSuccess = false;
                response.DisplayMessage = "Пользователь не авторизован";
                response.StatusCodes = StatusCode.BadRequest;
                return response;
            }
            
            var user = await _userRepository.GetById((int)userId!);
            if (user is null)
            {
                response.isSuccess = false;
                response.DisplayMessage = "Пользователь не найден";
                response.StatusCodes = StatusCode.BadRequest;
                return response;
            }

            var address = await _addressRepository.AddressesGet((int)userId);

            response.Data = _mapper.Map<List<AddressViewModel>>(address);
            response.StatusCodes = StatusCode.OK;
            return response;
        }
        catch (Exception e)
        {
            return new AddressesGetResponseModel
            {
                isSuccess = false,
                DisplayMessage = "Server error",
                StatusCodes = StatusCode.InternalServerError,
                ErrorMessage = new List<string> { e.ToString() }
            };
        }
    }

    public async Task<AddressUpdateResponseModel> AddressUpdate(AddressUpdateRequestModel model, int? userId)
    {
        try
        {
            var response = new AddressUpdateResponseModel();
            
            if (userId is null)
            {
                response.isSuccess = false;
                response.DisplayMessage = "Пользователь не авторизован";
                response.StatusCodes = StatusCode.BadRequest;
                return response;
            }
            
            var user = await _userRepository.GetById((int)userId!);
            if (user is null)
            {
                response.isSuccess = false;
                response.DisplayMessage = "Пользователь не найден";
                response.StatusCodes = StatusCode.BadRequest;
                return response;
            }

            var address = await _addressRepository.GetById(model.Id);
            address.Address = model.Address;

            var addressResponse = await _addressRepository.Update(address);

            response.Data = _mapper.Map<AddressViewModel>(addressResponse);
            response.StatusCodes = StatusCode.OK;
            return response;
        }
        catch (Exception e)
        {
            return new AddressUpdateResponseModel
            {
                isSuccess = false,
                DisplayMessage = "Server error",
                StatusCodes = StatusCode.InternalServerError,
                ErrorMessage = new List<string> { e.ToString() }
            };
        }
    }

    public async Task<AddressRemoveResponseModel> AddressRemove(int addressId)
    {
        try
        {
            var response = new AddressRemoveResponseModel();

            var address = await _addressRepository.GetById(addressId);

            if (!await _addressRepository.Delete(address))
            {
                response.isSuccess = false;
                response.StatusCodes = StatusCode.BadRequest;
                response.DisplayMessage = "Не удалилось";
                return response;
            }

            response.Data = true;
            response.StatusCodes = StatusCode.OK;
            return response;
        }
        catch (Exception e)
        {
            return new AddressRemoveResponseModel
            {
                isSuccess = false,
                DisplayMessage = "Server error",
                StatusCodes = StatusCode.InternalServerError,
                ErrorMessage = new List<string> { e.ToString() }
            };
        }
    }

    public async Task<AddressSetActiveResponseModel> AddressSetActive(int addressId, int? userId)
    {
        try
        {
            var response = new AddressSetActiveResponseModel();
            
            if (userId is null)
            {
                response.isSuccess = false;
                response.DisplayMessage = "Пользователь не авторизован";
                response.StatusCodes = StatusCode.BadRequest;
                return response;
            }
            
            var user = await _userRepository.GetById((int)userId!);
            if (user is null)
            {
                response.isSuccess = false;
                response.DisplayMessage = "Пользователь не найден";
                response.StatusCodes = StatusCode.BadRequest;
                return response;
            }

            var addresses = await _addressRepository.AddressesGet((int)userId);
            foreach (var address in addresses)
            {
                address.isActiveAddress = 0;
                await _addressRepository.Update(address);
            }

            var addressActive = await _addressRepository.GetById(addressId);
            addressActive.isActiveAddress = 1;
            await _addressRepository.Update(addressActive);

            response.Data = _mapper.Map<AddressViewModel>(addressActive);
            response.StatusCodes = StatusCode.OK;
            return response;
        }
        catch (Exception e)
        {
            return new AddressSetActiveResponseModel()
            {
                isSuccess = false,
                DisplayMessage = "Server error",
                StatusCodes = StatusCode.InternalServerError,
                ErrorMessage = new List<string> { e.ToString() }
            };
        }
    }
}