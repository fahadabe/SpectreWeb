

namespace UI.Pages;

public partial class Sale
{
    [Inject]
    public ISaleService service { get; set; }

    [Inject]
    public NotificationService Notification { get; set; }

    [Inject]
    public DialogService DialogService { get; set; }

    [Inject]
    public AuthenticationStateProvider authStateprovider { get; set; }

    public UserModel CurrentUser { get; set; } = new();

    protected override async Task OnInitializedAsync()
    {
        if (IsUserAuthenticated())
        {
            await GetTodaySale();
            await GetYesterdaySale();
        }
    }

    private async Task GetTodaySale()
    {
        var today = await service.GetTodaySaleAsync();
        if (!today.HasError)
        {
            TodaySalelist = today.Data!;
        }
    }

    private async Task GetYesterdaySale()
    {
        var yesterday = await service.GetYesterdaySaleAsync();
        if (!yesterday.HasError)
        {
            YesterdaySalelist = yesterday.Data!;
        }
    }

    public List<SaleModel> TodaySalelist { get; set; } = new();
    public List<SaleModel> YesterdaySalelist { get; set; } = new();
    public SaleModel NewSale { get; set; } = new();

    public string TodaySearchTerm { get; set; } = "";
    public string YesterdaySearchTerm { get; set; } = "";

    private bool IsUserAuthenticated()
    {
        var spectreAuthStateProvider = (SpectreAuthenticationStateProvider)authStateprovider;
        var currentUser = spectreAuthStateProvider.CurrentUser;

        if (currentUser is not null)
        {
            CurrentUser = currentUser;
            return true;
        }
        else
        {
            return false;
        }
    }

    public async Task SaveNewSale()
    {
        if (CurrentUser.CanAddSale)
        {
            if (NewSale.Amount <= 0)
            {
                Notification.Notify(NotificationSeverity.Warning, "Amount must be greater then 0");
                return;
            }
            NewSale.AddedBy = CurrentUser.Username;
            var result = await service.AddSaleAsync(NewSale);
            if (result.Data)
            {
                await GetTodaySale();
                NewSale = new();
                Notification.Notify(NotificationSeverity.Info, "Sale Added");
            }
            else
            {
                Notification.Notify(NotificationSeverity.Error, result.ErrorMessage);
            }
        }
        else
        {
            Notification.Notify(NotificationSeverity.Warning, "Not authorized to perform this action");
        }
    }

    public async Task DeleteTodaySale(SaleModel item)
    {
        var dialogResult = DialogService.Confirm("Are you sure?", "Confirm", new ConfirmOptions() { OkButtonText = "Yes", CancelButtonText = "No" });

        if ((bool)await dialogResult)
        {
            if (item is not null)
            {
                var result = await service.DeleteSale(item.SaleID);
                if (result.Data)
                {
                    TodaySalelist = TodaySalelist.Where(x => x != item).ToList();
                }
            }
        }
    }

    public async Task DeleteYesterdaySale(SaleModel item)
    {
        var dialogResult = DialogService.Confirm("Are you sure?", "Confirm", new ConfirmOptions() { OkButtonText = "Yes", CancelButtonText = "No" });

        if ((bool)await dialogResult)
        {
            if (item is not null)
            {
                var result = await service.DeleteSale(item.SaleID);
                if (result.Data)
                {
                    YesterdaySalelist = YesterdaySalelist.Where(x => x != item).ToList();
                }
            }
        }
    }

    private Func<SaleModel, bool> FilterTodaySales => x =>
    {
        if (string.IsNullOrWhiteSpace(TodaySearchTerm))
            return true;

        if (x.Description.Contains(TodaySearchTerm, StringComparison.OrdinalIgnoreCase))
            return true;

        if (x.Amount.ToString().Contains(TodaySearchTerm, StringComparison.OrdinalIgnoreCase))
            return true;

        if (x.AddedBy.ToString().Contains(TodaySearchTerm, StringComparison.OrdinalIgnoreCase))
            return true;

        return false;
    };

    private Func<SaleModel, bool> FilterYesterdaySales => x =>
    {
        if (string.IsNullOrWhiteSpace(YesterdaySearchTerm))
            return true;

        if (x.Description.Contains(YesterdaySearchTerm, StringComparison.OrdinalIgnoreCase))
            return true;

        if (x.Amount.ToString().Contains(YesterdaySearchTerm, StringComparison.OrdinalIgnoreCase))
            return true;

        if (x.AddedBy.ToString().Contains(YesterdaySearchTerm, StringComparison.OrdinalIgnoreCase))
            return true;

        return false;
    };
}