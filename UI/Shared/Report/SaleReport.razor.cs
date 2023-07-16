using Microsoft.AspNetCore.Components;

namespace UI.Shared.Report;

public partial class SaleReport
{
    [Inject]
    public ISaleService service { get; set; }
    [Inject]
    public DialogService DialogService { get; set; }
    public SaleFilter SelectedFilter { get; set; }

    private async Task DisplaySale()
    {
        SaleList.Clear();
        if (SelectedFilter == SaleFilter.Today)
        {
            var today = await service.GetTodaySaleAsync();
            if (!today.HasError)
            {
                SaleList = today.Data;
            }
        }
        else if (SelectedFilter == SaleFilter.Yesterday)
        {
            var yesterday = await service.GetYesterdaySaleAsync();
            if (!yesterday.HasError)
            {
                SaleList = yesterday.Data;
            }
        }
        else if (SelectedFilter == SaleFilter.All)
        {
            var all = await service.GetAllCollectionAsync();
            if (!all.HasError)
            {
                SaleList = all.Data;
            }
        }
        else if (SelectedFilter == SaleFilter.BetweenDates)
        {
            if (ToDate < FromDate)
            {
                return;
            }

            var betweenDates = await service.GetSaleBetweenTwoDatesAsync(FromDate, ToDate);
            if (!betweenDates.HasError)
            {
                SaleList = betweenDates.Data;
            }
        }
    }

    public async Task DeleteSelectedSaleSale(SaleModel item)
    {
        var dialogResult = DialogService.Confirm("Are you sure?", "Confirm", new ConfirmOptions() { OkButtonText = "Yes", CancelButtonText = "No" });

        if ((bool)await dialogResult)
        { 
        
            if (item is not null)
        {
            var result = await service.DeleteSale(item.SaleID);
            if (result.Data)
            {
                    SaleList = SaleList.Where(x => x != item).ToList();
                }
        }
        }
    }

    private Func<SaleModel, bool> FilterSales => x =>
    {
        if (string.IsNullOrWhiteSpace(SearchTerm))
            return true;

        if (x.Description.Contains(SearchTerm, StringComparison.OrdinalIgnoreCase))
            return true;

        if (x.Amount.ToString().Contains(SearchTerm, StringComparison.OrdinalIgnoreCase))
            return true;

        if (x.AddedBy.ToString().Contains(SearchTerm, StringComparison.OrdinalIgnoreCase))
            return true;

        return false;
    };

    public List<SaleModel> SaleList { get; set; } = new();
    public DateTime? FromDate { get; set; } = DateTime.Now;
    public DateTime? ToDate { get; set; } = DateTime.Now;
    public string SearchTerm { get; set; } = "";
}

public enum SaleFilter
{
    Today,
    Yesterday,
    All,
    BetweenDates
}