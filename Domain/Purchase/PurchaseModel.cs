namespace Domain;

public class PurchaseModel
{
    public int PurchaseID { get; set; }

    public DateTime Date { get; set; } = DateTime.Now;

    public string? Description { get; set; } = "";

    public double Amount { get; set; }

    public string AddedBy { get; set; } = "";
}