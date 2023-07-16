using Microsoft.AspNetCore.Components;

namespace UI.Shared.Report;

public partial class PurchaseReport
{
    [Inject]
    public IPurchaseService service { get; set; }
    [Inject]
    public DialogService DialogService { get; set; }
    public PurchaseFilter SelectedFilter { get; set; }

    private async Task DisplayPurchase()
    {
        PurchaseList.Clear();
        if (SelectedFilter == PurchaseFilter.Today)
        {
            var today = await service.GetTodayPurchaseAsync();
            if (!today.HasError)
            {
                PurchaseList = today.Data;
            }
        }
        else if (SelectedFilter == PurchaseFilter.Yesterday)
        {
            var yesterday = await service.GetYesterdayPurchaseAsync();
            if (!yesterday.HasError)
            {
                PurchaseList = yesterday.Data;
            }
        }
        else if (SelectedFilter == PurchaseFilter.All)
        {
            var all = await service.GetAllCollectionAsync();
            if (!all.HasError)
            {
                PurchaseList = all.Data;
            }
        }
        else if (SelectedFilter == PurchaseFilter.BetweenDates)
        {
            if (ToDate < FromDate)
            {
                return;
            }

            var betweenDates = await service.GetPurchaseBetweenTwoDatesAsync(FromDate, ToDate);
            if (!betweenDates.HasError)
            {
                PurchaseList = betweenDates.Data;
            }
        }
    }

    
    public async Task DeleteSelectedPurchasePurchase(PurchaseModel item)
    {
        var dialogResult = DialogService.Confirm("Are you sure?", "Confirm", new ConfirmOptions() { OkButtonText = "Yes", CancelButtonText = "No" });

        if ((bool)await dialogResult)
        { 
        
            if (item is not null)
        {
            var result = await service.DeletePurchase(item.PurchaseID);
            if (result.Data)
            {
                    PurchaseList = PurchaseList.Where(x => x != item).ToList();
                }
        }
        }
    }

    public List<PurchaseModel> PurchaseList { get; set; } = new();
    public DateTime? FromDate { get; set; } = DateTime.Now;
    public DateTime? ToDate { get; set; } = DateTime.Now;
    public string SearchTerm { get; set; } = "";
}

public enum PurchaseFilter
{
    Today,
    Yesterday,
    All,
    BetweenDates
}