namespace Domain;

public class ExpanseModel
{
    public int ExpanseID { get; set; }
    public bool ShowUsername { get; set; }

    public DateTime Date { get; set; } = DateTime.Now;

    public string? Description { get; set; } = "";

    public double Amount { get; set; }

    public string? AddedBy { get; set; } = "";
}