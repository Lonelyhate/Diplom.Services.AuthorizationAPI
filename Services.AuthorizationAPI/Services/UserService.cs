using AutoMapper;
using Services.AuthorizationAPI.Helpers.Interfaces;
using Services.AuthorizationAPI.Models.Enums;
using Services.AuthorizationAPI.Models.Repository.Interfaces;
using Services.AuthorizationAPI.Models.RequestModels;
using Services.AuthorizationAPI.Models.Services.Interfaces;
using Services.AuthorizationAPI.Models.ViewModels;

namespace Services.AuthorizationAPI.Models.Services;

public class UserService : IUserService
{
    private readonly IUserRepository _userRepository;
    private readonly IMapper _mapper;
    private readonly IAccountHelper _accountHelper;

    public UserService(IUserRepository userRepository, IMapper mapper, IAccountHelper accountHelper)
    {
        _userRepository = userRepository;
        _mapper = mapper;
        _accountHelper = accountHelper;
    }
    
    public async Task<UserCheckResponseModel> UserCheckByEmail(string email)
    {
        try
        {
            var response = new UserCheckResponseModel();

            response.isSuccess = true;
            var userCheck = await _userRepository.CheckUserByEmail(email);
            if (userCheck is false)
            {
                response.Data = false;
                response.DisplayMessage = "Пользователя нет в системе";
                response.StatusCodes = StatusCode.NotFound;
            }
            else
            {
                response.Data = true;
                response.DisplayMessage = "Пользователь найден в системе";
                response.StatusCodes = StatusCode.OK;
            }

            return response;
        }
        catch(Exception e)
        {
            return new UserCheckResponseModel()
            {
                isSuccess = false,
                StatusCodes = StatusCode.InternalServerError,
                DisplayMessage = "Ошибка сервера",
                ErrorMessage = new List<string>() {e.ToString()}
            };
        }
    }

    public async Task<UserRegistrationResponseModel> Registration(RegistrationRequestModel model)
    {
        try
        {
            var response = new UserRegistrationResponseModel();

            var userCheck = await _userRepository.GetByEmail(model.Email);
            if (userCheck is not null)
            {
                response.isSuccess = false;
                response.DisplayMessage = "Пользователь с таким логином уже есть";
                response.StatusCodes = StatusCode.BadRequest;
                return response;
            }
            
            _accountHelper.CreatedPasswordHash(model.Password, out byte[] passwordHash, out byte[] passwordSalt);

            var user = new User
            {
                Email = model.Email,
                PasswordHash = passwordHash,
                PasswordSalt = passwordSalt
            };

            await _userRepository.Create(user);

            string token = _accountHelper.CreateToken(user);

            response.Data = _mapper.Map<UserViewModel>(user);
            response.Data.token = token;
            response.StatusCodes = StatusCode.OK;
            return response;
        }
        catch (Exception e)
        {
            return new UserRegistrationResponseModel()
            {
                isSuccess = false,
                StatusCodes = StatusCode.InternalServerError,
                DisplayMessage = "Ошибка сервера",
                ErrorMessage = new List<string>() {e.ToString()}
            };
        }
    }

    public async Task<UserLoginResponseModel> Login(LoginRequestModel model)
    {
        try
        {
            var response = new UserLoginResponseModel();

            var userCheck = await _userRepository.GetByEmail(model.Email);
            if (userCheck is null)
            {
                response.isSuccess = false;
                response.DisplayMessage = "Пользователя с таким логином нет";
                response.StatusCodes = StatusCode.BadRequest;
                return response;
            }

            if (!_accountHelper.VerifyPasswordHash(model.Password, userCheck.PasswordHash, userCheck.PasswordSalt))
            {
                response.isSuccess = false;
                response.DisplayMessage = "Пароль неправильный";
                response.StatusCodes = StatusCode.BadRequest;
                return response;
            }

            var token = _accountHelper.CreateToken(userCheck);

            response.Data = _mapper.Map<UserViewModel>(userCheck);
            response.Data.token = token;
            response.StatusCodes = StatusCode.Created;
            return response;
        }
        catch (Exception e)
        {
            return new UserLoginResponseModel
            {
                isSuccess = false,
                StatusCodes = StatusCode.InternalServerError,
                DisplayMessage = "Ошибка сервера",
                ErrorMessage = new List<string>() {e.ToString()}
            };
        }
    }

    public async Task<UserAuthResponseModel> Auth(string login)
    {
        try
        {
            var response = new UserAuthResponseModel();

            var userCheck = await _userRepository.GetByEmail(login);
            if (userCheck is null)
            {
                response.DisplayMessage = "Пользователя с таким логином не существует";
                response.isSuccess = false;
                response.StatusCodes = StatusCode.BadRequest;
                return response;
            }

            string token = _accountHelper.CreateToken(userCheck);

            response.Data = _mapper.Map<UserViewModel>(userCheck);
            response.Data.token = token;
            response.StatusCodes = StatusCode.OK;
            return response;
        }
        catch(Exception e)
        {
            return new UserAuthResponseModel
            {
                isSuccess = false,
                StatusCodes = StatusCode.InternalServerError,
                DisplayMessage = "Ошибка сервера",
                ErrorMessage = new List<string>() { e.ToString() }
            };
        }
    }
    
    public async Task<UserUpdateResponeModel> Update(UpdateUserRequestModel model)
    {
        try
        {
            var response = new UserUpdateResponeModel();
            var user = await _userRepository.GetById(model.Id);
            if (user is null)
            {
                response.isSuccess = false;
                response.DisplayMessage = "Пользователь не найден";
                response.StatusCodes = StatusCode.BadRequest;
                return response;
            }

            user.Email = model.Email;
            user.Firstname = model.Firstname;
            user.Lastname = model.Lastname;
            user.Phone = model.Phone;
            user = await _userRepository.Update(user);

            string token = _accountHelper.CreateToken(user);

            response.Data = _mapper.Map<UserViewModel>(user);
            response.Data.token = token;
            response.StatusCodes = StatusCode.OK;
            return response;
        }
        catch(Exception e)
        {
            return new UserUpdateResponeModel
            {
                isSuccess = false,
                StatusCodes = StatusCode.InternalServerError,
                DisplayMessage = "Ошибка сервера",
                ErrorMessage = new List<string>() { e.ToString() }
            };
        }
    }
}