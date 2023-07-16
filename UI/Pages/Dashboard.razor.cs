


using System.Security.AccessControl;

namespace UI.Pages;

public partial class Dashboard
{
    [Inject]
    public IDashboardService service { get; set; }

    [Inject]
    public AuthenticationStateProvider authStateprovider { get; set; }

    public UserModel CurrentUser { get; set; } = new();

    public PerformanceModel TodayPerformance { get; set; } = new();
    public PerformanceModel YesterdayPerformance { get; set; } = new();
    public PerformanceModel ThisWeekPerformance { get; set; } = new();
    public PerformanceModel ThisMonthPerformance { get; set; } = new();
    public PerformanceModel YTDPerformance { get; set; } = new();
    public PerformanceModel AllPerformance { get; set; } = new();

    public List<MonthlySaleModel> SaleChartData { get; set; } = new();
    public List<MonthlyPurchaseModel> PurchaseChartData { get; set; } = new();
    public List<MonthlyExpanseModel> ExpanseChartData { get; set; } = new();

    protected override async Task OnInitializedAsync()
    {
        if (await IsUserAuthenticated())
        {
            await GetPerformances();
            await GetChartsData();
        }
    }

    private async Task GetPerformances()
    {
        TodayPerformance = (await service.GetTodayPerformanceAsync()).Data;
        YesterdayPerformance = (await service.GetYesterdayPerformanceAsync()).Data;
        ThisWeekPerformance = (await service.GetThisWeekPerformanceAsync()).Data;
        ThisMonthPerformance = (await service.GetThisMonthPerformanceAsync()).Data;
        YTDPerformance = (await service.GetYTDPerformanceAsync()).Data;
        AllPerformance = (await service.GetAllPerformanceAsync()).Data;
    }

    private bool _ThisYear = true;
    public bool ThisYear
    {
        get { return _ThisYear; }
        set 
        { 
            _ThisYear = value;
            Task.Run(() => GetChartsData());
        }
    }

    

    private async Task GetChartsData()
    {
        await GetSalesChartData();
        await GetPurchaseChartData();
        await GetExpnseChartData();
        await InvokeAsync(StateHasChanged);
    }

    private async Task GetSalesChartData()
    {
        SaleChartData = (await service.GetSaleChartDataAsync(ThisYear)).Data;
    }

    private async Task GetPurchaseChartData()
    {
        PurchaseChartData = (await service.GetPurchaseChartDataAsync(ThisYear)).Data;
    }

    private async Task GetExpnseChartData()
    {
        ExpanseChartData = (await service.GetExpanseChartDataAsync(ThisYear)).Data;
    }

    private async Task<bool> IsUserAuthenticated()
    {
        var spectreAuthStateProvider = (SpectreAuthenticationStateProvider)authStateprovider;
        var authState = await spectreAuthStateProvider.GetAuthenticationStateAsync();
        var user = authState.User;

        if (user.Identity.IsAuthenticated)
        {
            CurrentUser.Username = user.Identity.Name;
            return true;
        }
        else
        {
            return false;
        }
    }
}