using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.Server.ProtectedBrowserStorage;
using Radzen;
using Application;
using Infrastructure;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();
builder.Services.AddServerSideBlazor();
builder.Services.AddAuthenticationCore();
builder.Services.AddScoped<DialogService>();
builder.Services.AddScoped<NotificationService>();
builder.Services.AddScoped<TooltipService>();
builder.Services.AddScoped<ContextMenuService>();
builder.Services.AddScoped<ProtectedSessionStorage>();

builder.Services.AddScoped<AuthenticationStateProvider, SpectreAuthenticationStateProvider>();

builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("CanAddSale", policy =>
        policy.RequireClaim("CanAddSale", "True"));
    options.AddPolicy("CanDeleteSale", policy =>
        policy.RequireClaim("CanDeleteSale", "True"));
    options.AddPolicy("CanAddPurchase", policy =>
        policy.RequireClaim("CanAddPurchase", "True"));
    options.AddPolicy("CanDeletePurchase", policy =>
        policy.RequireClaim("CanDeletePurchase", "True"));
    options.AddPolicy("CanAddExpanse", policy =>
        policy.RequireClaim("CanAddExpanse", "True"));
    options.AddPolicy("CanDeleteExpanse", policy =>
        policy.RequireClaim("CanDeleteExpanse", "True"));
    options.AddPolicy("CanViewReport", policy =>
        policy.RequireClaim("CanViewReport", "True"));
    options.AddPolicy("ManageUsers", policy =>
        policy.RequireClaim("ManageUsers", "True"));
});

builder.Services.AddScoped<IDashboardService, DashboardService>();
builder.Services.AddScoped<IExpanseService, ExpanseService>();
builder.Services.AddScoped<ILoginService, LoginService>();
builder.Services.AddScoped<IPurchaseService, PurchaseService>();
builder.Services.AddScoped<ISaleService, SaleService>();
builder.Services.AddScoped<IUserService, UserService>();

builder.Services.AddHttpClient("dashboard", client =>
{
    client.BaseAddress = new Uri("https://localhost:7034/api/dashboard/");
});

builder.Services.AddHttpClient("expanse", client =>
{
    client.BaseAddress = new Uri("https://localhost:7034/api/expanse/");
});

builder.Services.AddHttpClient("login", client =>
{
    client.BaseAddress = new Uri("https://localhost:7034/api/login/");
});

builder.Services.AddHttpClient("purchase", client =>
{
    client.BaseAddress = new Uri("https://localhost:7034/api/purchase/");
});

builder.Services.AddHttpClient("sale", client =>
{
    client.BaseAddress = new Uri("https://localhost:7034/api/sale/");
});

builder.Services.AddHttpClient("user", client =>
{
    client.BaseAddress = new Uri("https://localhost:7034/api/user/");
});

var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseStaticFiles();
//app.UseAuthentication();
//app.UseAuthorization();
app.UseRouting();

app.MapBlazorHub();
app.MapFallbackToPage("/_Host");

app.Run();