using Services.AuthorizationAPI.Models.DiscountModels;
using Services.AuthorizationAPI.Models.RequestModels.Discount;
using Services.AuthorizationAPI.Models.ViewModels;

namespace Services.AuthorizationAPI.Models.Services.Interfaces;

public interface IDiscountService
{
    public Task<DiscountCreateResponseModel> DiscountCreate(int? userId);

    public Task<DiscountUpdateResponseModel> DiscountUpdate(DiscountUpdateRequestModel model, int? userId);

    public Task<DiscountGetByIdResponseModel> DiscountGetById(int? userId);
}