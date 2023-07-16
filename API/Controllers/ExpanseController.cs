namespace Spectre.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ExpanseController : ControllerBase
{
    private readonly IExpanseRepository service;

    public ExpanseController(IExpanseRepository service)
    {
        this.service = service;
    }

    [HttpPost]
    public async Task<IActionResult> AddExpanseAsync(ExpanseModel objExpanse)
    {
        try
        {
            var result = await service.AddExpanseAsync(objExpanse);
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

    [HttpGet("all")]
    public async Task<IActionResult> GetAllCollectionAsync()
    {
        try
        {
            var result = await service.GetAllCollectionAsync();
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

    [HttpGet("today")]
    public async Task<IActionResult> GetTodayExpanseAsync()
    {
        try
        {
            var result = await service.GetTodayExpanseAsync();
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

    [HttpGet("yesterday")]
    public async Task<IActionResult> GetYesterdayExpanseAsync()
    {
        try
        {
            var result = await service.GetYesterdayExpanseAsync();
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

    [HttpGet("between/{fromDate}/{toDate}")]
    public async Task<IActionResult> GetExpanseBetweenTwoDatesAsync(DateTime fromDate, DateTime toDate)
    {
        try
        {
            var result = await service.GetExpanseBetweenTwoDatesAsync(fromDate, toDate);
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

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteExpanse(int id)
    {
        try
        {
            var result = await service.DeleteExpanse(id);
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