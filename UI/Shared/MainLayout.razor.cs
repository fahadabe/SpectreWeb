using Microsoft.AspNetCore.Components;
using Radzen.Blazor;

namespace UI.Shared;

public partial class MainLayout
{
    [Inject]
    public NavigationManager NavigationManager { get; set; }

    [Inject]
    public AuthenticationStateProvider authStateprovider { get; set; }

    private bool isOpen = true;

    private void ToggleSidebar()
    {
        isOpen = !isOpen;
    }

    private async Task OnProfileMenuClick(RadzenProfileMenuItem item)
    {
        if (item.Text == "Logout")
        {
            await SignOut();
        }
    }

    private async Task SignOut()
    {
        var spectreAuthenticationprovider = (SpectreAuthenticationStateProvider)authStateprovider;
        await spectreAuthenticationprovider.UpdatAuthenticationState(null);
        NavigationManager.NavigateTo("/");
    }
}