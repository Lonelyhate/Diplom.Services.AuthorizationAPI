using System.Security.Claims;
using Microsoft.AspNetCore.Mvc;
using Services.AuthorizationAPI.Models.RequestModels.Address;
using Services.AuthorizationAPI.Models.Services.Interfaces;

namespace Services.AuthorizationAPI.Models;

[Route("api/[controller]")]
[ApiController]
public class AddressController : Controller
{
    private readonly IAddressService _addressService;

    public AddressController(IAddressService addressService)
    {
        _addressService = addressService;
    }
    
    [HttpPost]
    public async Task<IActionResult> AddAddresses(AddressAddRequestModel model)
    {
        var userId = HttpContext.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        var response = await _addressService.AddressAdd(model, Int32.TryParse(userId, out var id) ? id : null);

        if (response.StatusCodes == Enums.StatusCode.BadRequest) return BadRequest(response);
        if (response.StatusCodes == Enums.StatusCode.InternalServerError) return StatusCode(500, response);
        
        return StatusCode(201, response);
    }

    [HttpGet]
    public async Task<IActionResult> AddressesGet()
    {
        var userId = HttpContext.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        var response = await _addressService.AddressesGet(Int32.TryParse(userId, out var id) ? id : null);

        if (response.StatusCodes == Enums.StatusCode.BadRequest) return BadRequest(response);
        if (response.StatusCodes == Enums.StatusCode.InternalServerError) return StatusCode(500, response);
        
        return StatusCode(200, response);
    }

    [HttpPut]
    public async Task<IActionResult> AddressUpdate(AddressUpdateRequestModel model)
    {
        var userId = HttpContext.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        var response = await _addressService.AddressUpdate(model, Int32.TryParse(userId, out var id) ? id : null);

        if (response.StatusCodes == Enums.StatusCode.BadRequest) return BadRequest(response);
        if (response.StatusCodes == Enums.StatusCode.InternalServerError) return StatusCode(500, response);
        
        return StatusCode(200, response);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> AddressDelete(int id)
    {
        var response = await _addressService.AddressRemove(id);

        if (response.StatusCodes == Enums.StatusCode.BadRequest) return BadRequest(response);
        if (response.StatusCodes == Enums.StatusCode.InternalServerError) return StatusCode(500, response);
        
        return StatusCode(200, response);
    }

    [HttpGet("active/{id}")]
    public async Task<IActionResult> SetActiveAddress(int id)
    {
        var userId = HttpContext.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        var response = await _addressService.AddressSetActive(id, Int32.TryParse(userId, out var idUser) ? idUser : null);

        if (response.StatusCodes == Enums.StatusCode.BadRequest) return BadRequest(response);
        if (response.StatusCodes == Enums.StatusCode.InternalServerError) return StatusCode(500, response);
        
        return StatusCode(200, response);
    }
}