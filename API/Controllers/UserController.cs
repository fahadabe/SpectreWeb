namespace Spectre.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
public class UserController : ControllerBase
{
    private readonly IUserRepository service;

    public UserController(IUserRepository service)
    {
        this.service = service;
    }

    [HttpGet("all")]
    public async Task<IActionResult> GetUserListAsync()
    {
        try
        {
            var result = await service.GetUserListAsync();
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

    [HttpGet("exist/{username}")]
    public async Task<IActionResult> CheckIfUserExist(string username)
    {
        try
        {
            var result = await service.CheckIfUserExist(username);
            if (result.HasError)
            {
                return Ok(result.Data);
            }
            else
            {
                return StatusCode(StatusCodes.Status500InternalServerError, result.ErrorMessage);
            }
        }
        catch (Exception e)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, e.Message);
        }
    }

    [HttpPost]
    public async Task<IActionResult> AddNewUser(UserModel newUser)
    {
        try
        {
            var result = await service.AddNewUser(newUser);
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

    [HttpPut]
    public async Task<IActionResult> UpdateUser(UserModel updateUser)
    {
        try
        {
            var result = await service.UpdateUser(updateUser);
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

    [HttpDelete("{id:int}")]
    public async Task<IActionResult> DeleteUserAsync(int id)
    {
        try
        {
            var result = await service.DeleteUserAsync(id);
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