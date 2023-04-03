using System.Security.Claims;
using Microsoft.AspNetCore.Mvc;
using Services.AuthorizationAPI.Models.RequestModels.Discount;
using Services.AuthorizationAPI.Models.Services.Interfaces;
using Services.AuthorizationAPI.Models.ViewModels;

namespace Services.AuthorizationAPI.Models;

[Route("api/[controller]")]
[ApiController]
public class DiscountController : Controller
{
    private readonly IDiscountService _discountService;

    public DiscountController(IDiscountService discountService)
    {
        _discountService = discountService;
    }
    
    [HttpGet("create")]
    public async Task<IActionResult> DiscountCreate()
    {
        var userId = HttpContext.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        var response = await _discountService.DiscountCreate(Int32.TryParse(userId, out int idUser) ? idUser : null);

        if (response.StatusCodes == Enums.StatusCode.BadRequest) return BadRequest(response);
        if (response.StatusCodes == Enums.StatusCode.InternalServerError) return StatusCode(500, response);
        if (response.StatusCodes == Enums.StatusCode.Created) return StatusCode(201, response);
        
        return Ok(response);
    }

    [HttpGet("get")]
    public async Task<IActionResult> DiscountGetById()
    {
        var userId = HttpContext.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        var response = await _discountService.DiscountGetById(Int32.TryParse(userId, out int idUser) ? idUser : null);
        
        if (response.StatusCodes == Enums.StatusCode.BadRequest) return BadRequest(response);
        if (response.StatusCodes == Enums.StatusCode.InternalServerError) return StatusCode(500, response);

        return Json(response);
    }

    [HttpPut("update")]
    public async Task<IActionResult> DiscountUpdate(DiscountUpdateRequestModel model)
    {
        var userId = HttpContext.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        var response = await _discountService.DiscountUpdate(model, Int32.TryParse(userId, out int idUser) ? idUser : null);
        
        if (response.StatusCodes == Enums.StatusCode.BadRequest) return BadRequest(response);
        if (response.StatusCodes == Enums.StatusCode.InternalServerError) return StatusCode(500, response);

        return Json(response);
    }

}