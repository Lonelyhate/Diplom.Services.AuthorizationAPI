using Microsoft.AspNetCore.Mvc;

namespace Services.AuthorizationAPI.Models;

[Route("api/[controller]")]
[ApiController]
public class AddressController : Controller
{
    public async Task<IActionResult> AddAddresses()
    {
        return Ok();
    }
}