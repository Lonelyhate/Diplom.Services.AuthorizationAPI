using AutoMapper;
using Services.AuthorizationAPI.Models.DiscountModels;
using Services.AuthorizationAPI.Models.Enums;
using Services.AuthorizationAPI.Models.Repository.Interfaces;
using Services.AuthorizationAPI.Models.RequestModels.Discount;
using Services.AuthorizationAPI.Models.Services.Interfaces;
using Services.AuthorizationAPI.Models.ViewModels;

namespace Services.AuthorizationAPI.Models.Services;

public class DiscountService : IDiscountService
{
    private readonly IDiscountRepository _discountRepository;
    private readonly IUserRepository _userRepository;
    private readonly IMapper _mapper;

    public DiscountService(IDiscountRepository discountRepository, IMapper mapper, IUserRepository userRepository)
    {
        _discountRepository = discountRepository;
        _mapper = mapper;
        _userRepository = userRepository;
    }
    
    public async Task<DiscountCreateResponseModel> DiscountCreate(int? userId)
    {
        try
        {
            var response = new DiscountCreateResponseModel();
            
            if (userId is null)
            {
                response.isSuccess = false;
                response.DisplayMessage = "Пользователь не авторизован";
                response.StatusCodes = StatusCode.BadRequest;
                return response;
            }

            var user = await _userRepository.GetById((int)userId);
            if (user is null)
            {
                response.isSuccess = false;
                response.DisplayMessage = "Пользователя таким ID нет";
                response.StatusCodes = StatusCode.BadRequest;
                return response;
            }

            var discountCheck = await _discountRepository.GetById((int)userId);
            if (discountCheck is not null)
            {
                response.DisplayMessage = "Скидочная карта уже создана";
                response.Data = _mapper.Map<DiscountViewModel>(discountCheck);
                response.StatusCodes = StatusCode.OK;
                return response;
            }

            string numberCard = $"4460{new Random().Next(100000000, 999999999)}";
            Discount model = new Discount
            {
                User = user,
                UserId = user.Id,
                AmountPurchases = 0,
                NumberCard = numberCard,
                SizeDiscount = 0,
                AmountBeforeDiscount = 50000,
                currentSumBerforeDiscount = 0
            };

            var discount = await _discountRepository.Create(model);
            response.Data = _mapper.Map<DiscountViewModel>(discount);
            response.StatusCodes = StatusCode.Created;
            return response;
        }
        catch (Exception e)
        {
            return new DiscountCreateResponseModel
            {
                isSuccess = false,
                DisplayMessage = "Server error",
                StatusCodes = StatusCode.InternalServerError,
                ErrorMessage = new List<string> { e.ToString() }
            };
        }
    }

    public async Task<DiscountUpdateResponseModel> DiscountUpdate(DiscountUpdateRequestModel model, int? userId)
    {
        try
        {
            var response = new DiscountUpdateResponseModel();

            if (userId is null)
            {
                response.isSuccess = false;
                response.DisplayMessage = "Пользователь не авторизован";
                response.StatusCodes = StatusCode.BadRequest;
                return response;
            }
            
            
            var discount = await _discountRepository.GetById((int)userId);
            if (discount is null)
            {
                response.isSuccess = false;
                response.DisplayMessage = "Карта не найдена";
                response.StatusCodes = StatusCode.BadRequest;
                return response;
            }

            discount.AmountPurchases += model.Amount;
            discount.currentSumBerforeDiscount += (int)model.Amount;
            decimal discountDifference = discount.AmountBeforeDiscount - discount.currentSumBerforeDiscount;
            if (discountDifference <= 0 && discount.SizeDiscount != 20)
            {
                discount.currentSumBerforeDiscount = 0 - (int)discountDifference;
                discount.SizeDiscount += 5;
                discount.AmountBeforeDiscount = 50000 + (int)discountDifference;
            }

            var discountResponse = await _discountRepository.Update(discount);
            if (discountResponse is null)
            {
                response.isSuccess = false;
                response.DisplayMessage = "Не получилось обновить";
                response.StatusCodes = StatusCode.InternalServerError;
                return response;
            }
            
            response.StatusCodes = StatusCode.OK;
            response.Data = _mapper.Map<DiscountViewModel>(discountResponse);
            return response;
        }
        catch (Exception e)
        {
            return new DiscountUpdateResponseModel
            {
                isSuccess = false,
                DisplayMessage = "Server error",
                StatusCodes = StatusCode.InternalServerError,
                ErrorMessage = new List<string> { e.ToString() }
            };
        }
    }

    public async Task<DiscountGetByIdResponseModel> DiscountGetById(int? userId)
    {
        try
        {
            var response = new DiscountGetByIdResponseModel();
            
            if (userId is null)
            {
                response.isSuccess = false;
                response.DisplayMessage = "Пользователь не авторизован";
                response.StatusCodes = StatusCode.BadRequest;
                return response;
            }

            var discount = await _discountRepository.GetById((int)userId);
            if (discount is null)
            {
                response.isSuccess = false;
                response.DisplayMessage = "Скидочная карта не найдена";
                response.StatusCodes = StatusCode.BadRequest;
                return response;
            }

            response.Data = _mapper.Map<DiscountViewModel>(discount);
            response.StatusCodes = StatusCode.OK;
            return response;
        }
        catch (Exception e)
        {
            return new DiscountGetByIdResponseModel
            {
                isSuccess = false,
                DisplayMessage = "Server error",
                StatusCodes = StatusCode.InternalServerError,
                ErrorMessage = new List<string> { e.ToString() }
            };
        }
    }
}