using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Services.AuthorizationAPI.Models.RequestModels;
using Services.AuthorizationAPI.Models.Services.Interfaces;

namespace Services.AuthorizationAPI.Models;

[Route("api/[controller]")]
[ApiController]
public class UserController : Controller
{
    private readonly IUserService _userService;

    public UserController(IUserService userService)
    {
        _userService = userService;
    }

    [HttpGet("check")]
    public async Task<IActionResult> CheckUserByEmail(string email)
    {
        var response = await _userService.UserCheckByEmail(email);

        if (response.StatusCodes == Enums.StatusCode.InternalServerError) return StatusCode(500, response);

        if (response.StatusCodes == Enums.StatusCode.NotFound) return NotFound(response);

        return Json(response);
    }
    
    [HttpPost("registration")]
    public async Task<IActionResult> Registration(RegistrationRequestModel model)
    {
        var response = await _userService.Registration(model);
        
        if (response.StatusCodes == Enums.StatusCode.InternalServerError) return StatusCode(500, response);

        if (response.StatusCodes == Enums.StatusCode.BadRequest) return StatusCode(400, response);

        return Json(response);
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login(LoginRequestModel model)
    {
        var response = await _userService.Login(model);
        
        if (response.StatusCodes == Enums.StatusCode.InternalServerError) return StatusCode(500, response);

        if (response.StatusCodes == Enums.StatusCode.BadRequest) return StatusCode(400, response);

        return Json(response);
    }

    [HttpGet("auth")]
    public async Task<IActionResult> Auth()
    {
        string? login = HttpContext.User.FindFirst(ClaimTypes.Name)?.Value;
        
        Console.WriteLine(login);

        var response = await _userService.Auth(login);

        if (response.StatusCodes == Enums.StatusCode.InternalServerError) return StatusCode(500, response);

        if (response.StatusCodes == Enums.StatusCode.BadRequest) return StatusCode(400, response);

        return Json(response);
    }
}