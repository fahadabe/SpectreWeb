namespace Spectre.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
public class LoginController : ControllerBase
{
    private readonly ILoginRepository service;

    public LoginController(ILoginRepository service)
    {
        this.service = service;
    }

    [HttpPost("login")]
    public async Task<IActionResult> LoginAsync(UserModel login)
    {
        try
        {
            var result = await service.LoginAsync(login);
            if (result.HasError)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, result.ErrorMessage);
            }
            else
            {
                return Ok(result.Data);
            }
        }
        catch (Exception e)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, e.Message);
        }
    }
}