using Services.AuthorizationAPI.Models.RequestModels;

namespace Services.AuthorizationAPI.Models.Services.Interfaces;

public interface IUserService
{
    Task<UserCheckResponseModel> UserCheckByEmail(string email);

    Task<UserRegistrationResponseModel> Registration(RegistrationRequestModel model);

    Task<UserLoginResponseModel> Login(LoginRequestModel model);

    Task<UserAuthResponseModel> Auth(string login);

    Task<UserUpdateResponeModel> Update(UpdateUserRequestModel model);
}