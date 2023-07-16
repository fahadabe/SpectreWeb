





namespace Spectre.Presentation.Authentication;

public class SpectreAuthenticationStateProvider : AuthenticationStateProvider
{
    private readonly ProtectedSessionStorage sessionStorage;
    private ClaimsPrincipal anonymous = new ClaimsPrincipal(new ClaimsIdentity());

    public UserModel CurrentUser { get; private set; }

    public SpectreAuthenticationStateProvider(ProtectedSessionStorage sessionStorage)
    {
        this.sessionStorage = sessionStorage;
    }

    public override async Task<AuthenticationState> GetAuthenticationStateAsync()
    {
        try
        {
            var userSessionStorageResult = await sessionStorage.GetAsync<UserModel>("UserSession");
            var userSession = userSessionStorageResult.Success ? userSessionStorageResult.Value : null;
            if (userSession == null)
            {
                return await Task.FromResult(new AuthenticationState(anonymous));
            }

            CurrentUser = userSession;
            var claimsPrinciple = GetClaimPrimciple(userSession);

            return await Task.FromResult(new AuthenticationState(claimsPrinciple));
        }
        catch (Exception)
        {
            return await Task.FromResult(new AuthenticationState(anonymous));
        }
    }

    public async Task UpdatAuthenticationState(UserModel userSession)
    {
        ClaimsPrincipal claimsPrincipal;
        if (userSession is not null)
        {
            await sessionStorage.SetAsync("UserSession", userSession);
            claimsPrincipal = GetClaimPrimciple(userSession);
            CurrentUser = userSession;
        }
        else
        {
            await sessionStorage.DeleteAsync("UserSession");
            claimsPrincipal = anonymous;
            CurrentUser = null;
        }

        NotifyAuthenticationStateChanged(Task.FromResult(new AuthenticationState(claimsPrincipal)));
    }

    private ClaimsPrincipal GetClaimPrimciple(UserModel userSession)
    {
        var claimsPrinciple = new ClaimsPrincipal(new ClaimsIdentity(new List<Claim>
        {
            new Claim(ClaimTypes.Name, userSession.Username),
            new Claim("CanAddExpanse", userSession.CanAddExpanse.ToString()),
            new Claim("CanDeleteExpanse", userSession.CanDeleteExpanse.ToString()),
            new Claim("CanAddSale", userSession.CanAddSale.ToString()),
            new Claim("CanDeleteSale", userSession.CanDeleteSale.ToString()),
            new Claim("CanAddPurchase", userSession.CanAddPurchase.ToString()),
            new Claim("CanDeletePurchase", userSession.CanDeletePurchase.ToString()),
            new Claim("CanViewReport", userSession.CanViewReport.ToString()),
            new Claim("ManageUsers", userSession.ManageUsers.ToString())
        }, "SpectreAuth"));

        return claimsPrinciple;
    }
}