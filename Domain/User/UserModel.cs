namespace Domain;

public class UserModel
{
    public int UserID { get; set; }

    public string Username { get; set; } = "";

    public string Password { get; set; } = "";

    public bool CanAddExpanse { get; set; }

    public bool CanDeleteExpanse { get; set; }

    public bool CanAddSale { get; set; }

    public bool CanDeleteSale { get; set; }

    public bool CanAddPurchase { get; set; }

    public bool CanDeletePurchase { get; set; }

    public bool CanViewReport { get; set; }

    public bool ManageUsers { get; set; }
}