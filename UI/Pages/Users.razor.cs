

namespace UI.Pages;

public partial class Users
{
    [Inject]
    public IUserService service { get; set; }

    [Inject]
    public Radzen.DialogService DialogService { get; set; }

    [Inject]
    public AuthenticationStateProvider authStateprovider { get; set; }

    [Inject]
    public NotificationService Notification { get; set; }

    public UserModel SelectedUser { get; set; } = new();
    public UserModel CurrentUser { get; set; } = new();

    public List<UserModel> UserList { get; set; } = new();
    public UserModel NewUser { get; set; } = new();

    private bool _ToggleAllPermissionsForNewUser = false;

    public bool ToggleAllPermissionsForNewUser
    {
        get { return _ToggleAllPermissionsForNewUser; }
        set
        {
            _ToggleAllPermissionsForNewUser = value;
            ToggleNewUserPermissions();
        }
    }

    protected override async Task OnInitializedAsync()
    {
        if (IsUserAuthenticated())
        {
            await GetUserList();
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

    private async Task GetUserList()
    {
        var result = await service.GetUserListAsync();
        if (!result.HasError)
        {
            UserList = result.Data;
        }
    }

    private async Task DeleteUser(UserModel user)
    {
        if (string.Equals(user.Username, CurrentUser.Username))
        {
            Notification.Notify(NotificationSeverity.Warning, "Cannot delete yourself");
            return;
        }

        if (UserList.Count == 1)
        {
            Notification.Notify(NotificationSeverity.Warning, "Cannot delete last remaining user");
            return;
        }

        var dialogResult = DialogService.Confirm("Are you sure?", "Confirm", new ConfirmOptions() { OkButtonText = "Yes", CancelButtonText = "No" });

        if ((bool)await dialogResult)
        {
            var result = await service.DeleteUserAsync(user.UserID);
            if (!result.HasError)
            {
                UserList = UserList.Where(x => x != user).ToList();
            }
            else
            {
                Notification.Notify(NotificationSeverity.Error, result.ErrorMessage);
            }
        }
    }

    private async Task SaveNewUser()
    {
        if (!string.IsNullOrWhiteSpace(NewUser.Username) && !string.IsNullOrWhiteSpace(NewUser.Password))
        {
            var result = await service.CheckIfUserExist(NewUser.Username);

            if (result.Data)
            {
                Notification.Notify(NotificationSeverity.Warning, "User with the same name already exist");
            }
            else
            {
                var result2 = await service.AddNewUser(NewUser);
                if (!result2.HasError)
                {
                    NewUser = new();
                    await GetUserList();
                }
            }
        }
        else
        {
            Notification.Notify(NotificationSeverity.Warning, "Username or Password cannot be empty");
        }
    }

    private async Task UpdateUser()
    {
        if (string.Equals(SelectedUser.Username, CurrentUser.Username))
        {
            Notification.Notify(NotificationSeverity.Warning, "Cannot edit yourself");
            return;
        }

        if (SelectedUser is null)
        {
            return;
        }

        var result = await service.UpdateUser(SelectedUser);
        if (result.Data)
        {
            Notification.Notify(NotificationSeverity.Success, "User Updated");
        }
        else
        {
            Notification.Notify(NotificationSeverity.Error, result.ErrorMessage);
        }
    }

    private void ToggleNewUserPermissions()
    {
        if (ToggleAllPermissionsForNewUser)
        {
            NewUser.CanAddSale = true;
            NewUser.CanDeleteSale = true;
            NewUser.CanAddPurchase = true;
            NewUser.CanDeletePurchase = true;
            NewUser.CanAddExpanse = true;
            NewUser.CanDeleteExpanse = true;
            NewUser.ManageUsers = true;
            NewUser.CanViewReport = true;
        }
        else
        {
            NewUser.CanAddSale = false;
            NewUser.CanDeleteSale = false;
            NewUser.CanAddPurchase = false;
            NewUser.CanDeletePurchase = false;
            NewUser.CanAddExpanse = false;
            NewUser.CanDeleteExpanse = false;
            NewUser.ManageUsers = false;
            NewUser.CanViewReport = false;
        }
    }
}