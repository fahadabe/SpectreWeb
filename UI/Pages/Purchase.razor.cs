

namespace UI.Pages;

public partial class Purchase
{
    [Inject]
    public IPurchaseService service { get; set; }

    [Inject]
    public NotificationService Notification { get; set; }

    [Inject]
    public Radzen.DialogService DialogService { get; set; }

    [Inject]
    public AuthenticationStateProvider authStateprovider { get; set; }

    public UserModel CurrentUser { get; set; } = new();

    protected override async Task OnInitializedAsync()
    {
        if (IsUserAuthenticated())
        {
            await GetTodayPurchase();
            await GetYesterdayPurchase();
        }
    }

    private async Task GetTodayPurchase()
    {
        var today = await service.GetTodayPurchaseAsync();
        if (!today.HasError)
        {
            TodayPurchaselist = today.Data!;
        }
    }

    private async Task GetYesterdayPurchase()
    {
        var yesterday = await service.GetYesterdayPurchaseAsync();
        if (!yesterday.HasError)
        {
            YesterdayPurchaselist = yesterday.Data!;
        }
    }

    public List<PurchaseModel> TodayPurchaselist { get; set; } = new();
    public List<PurchaseModel> YesterdayPurchaselist { get; set; } = new();
    public PurchaseModel NewPurchase { get; set; } = new();

    public string TodaySearchTerm { get; set; } = "";
    public string YesterdaySearchTerm { get; set; } = "";

    public async Task SaveNewPurchase()
    {
        if (NewPurchase.Amount <= 0)
        {
            Notification.Notify(NotificationSeverity.Warning, "Amount must be greater then 0");
            return;
        }
        NewPurchase.AddedBy = CurrentUser.Username;
        var result = await service.AddPurchaseAsync(NewPurchase);
        if (result.Data)
        {
            await GetTodayPurchase();
            NewPurchase = new();
            Notification.Notify(NotificationSeverity.Info, "Purchase Added");
        }
        else
        {
            Notification.Notify(NotificationSeverity.Error, result.ErrorMessage);
        }
    }

    public async Task DeleteTodayPurchase(PurchaseModel item)
    {
        var dialogResult = DialogService.Confirm("Are you sure?", "Confirm", new ConfirmOptions() { OkButtonText = "Yes", CancelButtonText = "No" });

        if ((bool)await dialogResult)
        {
            if (item is not null)
            {
                var result = await service.DeletePurchase(item.PurchaseID);
                if (result.Data)
                {
                    TodayPurchaselist = TodayPurchaselist.Where(x => x != item).ToList();
                }
            }
        }
    }

    public async Task DeleteYesterdayPurchase(PurchaseModel item)
    {
        var dialogResult = DialogService.Confirm("Are you sure?", "Confirm", new ConfirmOptions() { OkButtonText = "Yes", CancelButtonText = "No" });

        if ((bool)await dialogResult)
        {
            if (item is not null)
            {
                var result = await service.DeletePurchase(item.PurchaseID);
                if (result.Data)
                {
                    YesterdayPurchaselist = YesterdayPurchaselist.Where(x => x != item).ToList();
                }
            }
        }
    }

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

    private Func<PurchaseModel, bool> FilterTodayPurchases => x =>
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

    private Func<PurchaseModel, bool> FilterYesterdayPurchases => x =>
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