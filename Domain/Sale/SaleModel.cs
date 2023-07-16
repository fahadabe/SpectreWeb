namespace Domain;

public class SaleModel
{
    public int SaleID { get; set; }

    public DateTime Date { get; set; } = DateTime.Now;

    public string? Description { get; set; } = "";

    public double Amount { get; set; }

    public string? AddedBy { get; set; } = "";
}