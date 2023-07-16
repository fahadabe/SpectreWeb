using Microsoft.AspNetCore.Components;

namespace UI.Shared.Report;

public partial class ExpanseReport
{
    [Inject]
    public IExpanseService service { get; set; }
    [Inject]
    public DialogService DialogService { get; set; }
    public ExpanseFilter SelectedFilter { get; set; }

    private async Task DisplayExpanse()
    {
        ExpanseList.Clear();
        if (SelectedFilter == ExpanseFilter.Today)
        {
            var today = await service.GetTodayExpanseAsync();
            if (!today.HasError)
            {
                ExpanseList = today.Data;
            }
        }
        else if (SelectedFilter == ExpanseFilter.Yesterday)
        {
            var yesterday = await service.GetYesterdayExpanseAsync();
            if (!yesterday.HasError)
            {
                ExpanseList = yesterday.Data;
            }
        }
        else if (SelectedFilter == ExpanseFilter.All)
        {
            var all = await service.GetAllCollectionAsync();
            if (!all.HasError)
            {
                ExpanseList = all.Data;
            }
        }
        else if (SelectedFilter == ExpanseFilter.BetweenDates)
        {
            if (ToDate < FromDate)
            {
                return;
            }

            var betweenDates = await service.GetExpanseBetweenTwoDatesAsync(FromDate, ToDate);
            if (!betweenDates.HasError)
            {
                ExpanseList = betweenDates.Data;
            }
        }
    }

    

    public async Task DeleteSelectedExpanseExpanse(ExpanseModel item)
    {
        var dialogResult = DialogService.Confirm("Are you sure?", "Confirm", new ConfirmOptions() { OkButtonText = "Yes", CancelButtonText = "No" });

        if ((bool)await dialogResult)
        { 
                if (item is not null)
            {
                var result = await service.DeleteExpanse(item.ExpanseID);
                if (result.Data)
                {
                    ExpanseList = ExpanseList.Where(x => x != item).ToList();
                }
            }
        }
    }

    public List<ExpanseModel> ExpanseList { get; set; } = new();
    public DateTime? FromDate { get; set; } = DateTime.Now;
    public DateTime? ToDate { get; set; } = DateTime.Now;
    public string SearchTerm { get; set; } = "";
}

public enum ExpanseFilter
{
    Today,
    Yesterday,
    All,
    BetweenDates
}