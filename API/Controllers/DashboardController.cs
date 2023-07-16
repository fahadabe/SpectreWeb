namespace Spectre.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
public class DashboardController : ControllerBase
{
    private readonly IDashboardRepository service;

    public DashboardController(IDashboardRepository service)
    {
        this.service = service;
    }

    [HttpGet("today")]
    public async Task<IActionResult> GetTodayPerformanceAsync()
    {
        try
        {
            var result = await service.GetTodayPerformance();
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
    public async Task<IActionResult> GetYesterdayPerformanceAsync()
    {
        try
        {
            var result = await service.GetYesterdayPerformance();
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

    [HttpGet("thisweek")]
    public async Task<IActionResult> GetThisWeekPerformanceAsync()
    {
        try
        {
            var result = await service.GetThisWeekPerformance();
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

    [HttpGet("thismonth")]
    public async Task<IActionResult> GetThisMonthPerformanceAsync()
    {
        try
        {
            var result = await service.GetThisMonthPerformance();
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

    [HttpGet("ytd")]
    public async Task<IActionResult> GetYTDPerformanceAsync()
    {
        try
        {
            var result = await service.GetYTDPerformance();
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
    public async Task<IActionResult> GetAllPerformanceAsync()
    {
        try
        {
            var result = await service.GetAllPerformance();
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

    [HttpGet("salechartdata")]
    public async Task<IActionResult> GetSaleChartDataAsync(bool onlyCurrentYear)
    {
        try
        {
            var result = await service.GetSaleChartData(onlyCurrentYear);
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

    [HttpGet("expansechartdata")]
    public async Task<IActionResult> GetExpanseChartDataAsync(bool onlyCurrentYear)
    {
        try
        {
            var result = await service.GetExpanseChartData(onlyCurrentYear);
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

    [HttpGet("purchasechartdata")]
    public async Task<IActionResult> GetPurchaseChartDataAsync(bool onlyCurrentYear)
    {
        try
        {
            var result = await service.GetPurchaseChartData(onlyCurrentYear);
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