namespace Spectre.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
public class PurchaseController : ControllerBase
{
    private readonly IPurchaseRepository service;

    public PurchaseController(IPurchaseRepository service)
    {
        this.service = service;
    }

    [HttpPost]
    public async Task<IActionResult> AddPurchaseAsync(PurchaseModel objPurchase)
    {
        try
        {
            var result = await service.AddPurchaseAsync(objPurchase);
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
    public async Task<IActionResult> GetTodayPurchaseAsync()
    {
        try
        {
            var result = await service.GetTodayPurchaseAsync();
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
    public async Task<IActionResult> GetYesterdayPurchaseAsync()
    {
        try
        {
            var result = await service.GetYesterdayPurchaseAsync();
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
    public async Task<IActionResult> GetPurchaseBetweenTwoDatesAsync(DateTime fromDate, DateTime toDate)
    {
        try
        {
            var result = await service.GetPurchaseBetweenTwoDatesAsync(fromDate, toDate);
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
    public async Task<IActionResult> DeletePurchase(int id)
    {
        try
        {
            var result = await service.DeletePurchase(id);
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