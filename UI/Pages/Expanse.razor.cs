

namespace UI.Pages;

public partial class Expanse
{
    [Inject]
    public IExpanseService service { get; set; }

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
            await GetTodayExpanse();
            await GetYesterdayExpanse();
        }
    }

    private async Task GetTodayExpanse()
    {
        var today = await service.GetTodayExpanseAsync();
        if (!today.HasError)
        {
            TodayExpanselist = today.Data!;
        }
    }

    private async Task GetYesterdayExpanse()
    {
        var yesterday = await service.GetYesterdayExpanseAsync();
        if (!yesterday.HasError)
        {
            YesterdayExpanselist = yesterday.Data!;
        }
    }

    public List<ExpanseModel> TodayExpanselist { get; set; } = new();
    public List<ExpanseModel> YesterdayExpanselist { get; set; } = new();
    public ExpanseModel NewExpanse { get; set; } = new();
    public string TodaySearchTerm { get; set; } = "";
    public string YesterdaySearchTerm { get; set; } = "";

    public async Task SaveNewExpanse()
    {
        if (NewExpanse.Amount <= 0)
        {
            Notification.Notify(NotificationSeverity.Warning, "Amount must be greater then 0");
            return;
        }
        NewExpanse.AddedBy = CurrentUser.Username;
        var result = await service.AddExpanseAsync(NewExpanse);
        if (result.Data)
        {
            await GetTodayExpanse();
            NewExpanse = new();
            Notification.Notify(NotificationSeverity.Info, "Expanse Added");
        }
        else
        {
            Notification.Notify(NotificationSeverity.Error, result.ErrorMessage);
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

    public async Task DeleteTodayExpanse(ExpanseModel item)
    {
        var dialogResult = DialogService.Confirm("Are you sure?", "Confirm", new ConfirmOptions() { OkButtonText = "Yes", CancelButtonText = "No" });

        if ((bool)await dialogResult)
        {
            if (item is not null)
            {
                var result = await service.DeleteExpanse(item.ExpanseID);
                if (result.Data)
                {
                    TodayExpanselist = TodayExpanselist.Where(x => x != item).ToList();
                }
            }
        }
    }

    public async Task DeleteYesterdayExpanse(ExpanseModel item)
    {
        var dialogResult = DialogService.Confirm("Are you sure?", "Confirm", new ConfirmOptions() { OkButtonText = "Yes", CancelButtonText = "No" });

        if ((bool)await dialogResult)
        {
            if (item is not null)
            {
                var result = await service.DeleteExpanse(item.ExpanseID);
                if (result.Data)
                {
                    YesterdayExpanselist = YesterdayExpanselist.Where(x => x != item).ToList();
                }
            }
        }
    }

    private Func<ExpanseModel, bool> FilterTodayExpanses => x =>
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

    private Func<ExpanseModel, bool> FilterYesterdayExpanses => x =>
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