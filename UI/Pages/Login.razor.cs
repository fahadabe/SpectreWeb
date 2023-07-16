using Microsoft.AspNetCore.Components;
using Radzen;

namespace UI.Pages;
public partial class Login
{
    [Inject]
    public ILoginService service { get; set; }

    [Inject]
    public NotificationService Notification { get; set; }

    [Inject]
    public AuthenticationStateProvider authStateprovider { get; set; }

    [Inject]
    public NavigationManager NavigationManager { get; set; }

    public UserModel User { get; set; } = new();

   

    private async Task LoginUser()
    {
        try
        {
            var result = await service.LoginAsync(User);
            if (!result.HasError)
            {
                var user = result.Data;
                if (user != null)
                {
                    var spectreAuthStateProvider = (SpectreAuthenticationStateProvider)authStateprovider;
                    await spectreAuthStateProvider.UpdatAuthenticationState(user);

                    NavigationManager.NavigateTo("/dashboard", true);
                }
                else
                {
                    Notification.Notify(NotificationSeverity.Error, "No Such User");
                }
            }
            else
            {
                Notification.Notify(NotificationSeverity.Error, "No Such User");
            }
        }
        catch (Exception e)
        {
            Notification.Notify(NotificationSeverity.Error, e.Message);
        }
    }
}