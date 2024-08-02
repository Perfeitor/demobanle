using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using MudBlazor.Services;
using MUDTEMPLATE.Components;
using MUDTEMPLATE.Components.Account;
using MUDTEMPLATE.Data;
using MUDTEMPLATE.DBData;
using MUDTEMPLATE;
using EntityFrameworkCore.UseRowNumberForPaging;
using DBDATA.Context;
using MUDTEMPLATE.Services;
using Microsoft.AspNetCore.Authentication.Cookies;
using System.Globalization;
var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();

builder.Services.AddMudServices();

builder.Services.AddCascadingAuthenticationState();
builder.Services.AddScoped<IdentityUserAccessor>();
builder.Services.AddScoped<IdentityRedirectManager>();
builder.Services.AddScoped<AuthenticationStateProvider, IdentityRevalidatingAuthenticationStateProvider>();

builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme).AddCookie(options =>
{
    options.Cookie.HttpOnly = true;
    options.LoginPath = "/Account/Login";
    options.AccessDeniedPath = "/Account/Login";
    options.SlidingExpiration = true;
}).AddIdentityCookies();

builder.Services.AddHttpContextAccessor();

var connectionString = builder.Configuration.GetConnectionString("DefaultConnection") ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(connectionString, o => o.UseRowNumberForPaging()));

builder.Services.AddAntiforgery();

builder.Services.AddQuickGridEntityFrameworkAdapter();

builder.Services.AddDbContext<DATAContext>(options =>
    options.UseSqlServer(connectionString, o => o.UseRowNumberForPaging()), ServiceLifetime.Transient);

builder.Services.AddScoped<INguoidungService, NguoidungService>();
builder.Services.AddScoped<INganhhangService, NganhhangService>();
builder.Services.AddScoped<IKhachhangService, KhachhangService>();
builder.Services.AddScoped<INhomhangService, NhomhangService>();
builder.Services.AddScoped<IMathangService, MathangService>();
builder.Services.AddScoped<IBarcodeService, BarcodeService>();
builder.Services.AddScoped<IDonvitinhService, DonvitinhService>();
builder.Services.AddScoped<IVatService, VatService>();
builder.Services.AddScoped<IKhohangService, KhohangService>();

builder.Services.AddDistributedMemoryCache();

builder.Services.AddDatabaseDeveloperPageExceptionFilter();

builder.Services.AddIdentityCore<ApplicationUser>(options => options.SignIn.RequireConfirmedAccount = true)
    .AddEntityFrameworkStores<ApplicationDbContext>()
    .AddSignInManager()
    .AddDefaultTokenProviders();

builder.Services.AddSingleton<IEmailSender<ApplicationUser>, IdentityNoOpEmailSender>();
builder.Services.AddServerSideBlazor().AddCircuitOptions(options => { options.DetailedErrors = true; });

builder.Services.AddScoped<DB>();
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseMigrationsEndPoint();
}
else
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseStaticFiles();
app.UseAntiforgery();

app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode();
app.MapAdditionalIdentityEndpoints();

app.Run();
 