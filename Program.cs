using BlazorUserManagerApp.Data;
using BlazorUserManagerApp.Services;
using ElectronNET.API;
using ElectronNET.API.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);
builder.WebHost.UseElectron(args);

// Add services to the container.
builder.Services.AddRazorPages();
builder.Services.AddServerSideBlazor();
builder.Services.AddScoped<IEmployeeService, EmployeeService>();
builder.Services.AddSingleton<WeatherForecastService>();

var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContextFactory<DataContext>(options => options.UseSqlite(connectionString));
builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("AdminPolicy", policy => policy.RequireClaim("Admin"));
    options.AddPolicy("SupervisorPolicy", policy => policy.RequireClaim("Supervisor"));
    options.AddPolicy("OperatorPolicy", policy => policy.RequireClaim("Operator"));
});

builder
    .Services.AddIdentity<IdentityUser, IdentityRole>(options =>
    {
        options.Password.RequireDigit = false;
        options.Password.RequiredLength = 1;
        options.Password.RequireNonAlphanumeric = false;
        options.SignIn.RequireConfirmedEmail = false;
    })
    .AddEntityFrameworkStores<DataContext>()
    .AddDefaultTokenProviders()
    .AddRoles<IdentityRole>();
builder.Services.AddBlazorBootstrap();

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var dbContext = scope.ServiceProvider.GetRequiredService<DataContext>();
    dbContext.Database.EnsureCreated();

    // To be Tested
    //if (dbContext.Database.CanConnect())
    //{
    //    dbContext.Database.EnsureCreated();
    //}
    //else
    //{
    //    try
    //    {
    //        dbContext.Database.OpenConnection();
    //        SqlCommand sqlCommand = new SqlCommand("create databse MyDatabase");
    //        sqlCommand.ExecuteNonQuery();
    //        dbContext.Database.EnsureCreated();
    //    }
    //    catch (Exception e)
    //    {
    //        Console.WriteLine(e.Message);
    //    }
    //    finally
    //    {
    //        dbContext.Database.CloseConnection();
    //    }
    //}
}

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.MapBlazorHub();
app.MapFallbackToPage("/_Host");

await Task.Run(async () =>
{
    var browserWindow = await Electron.WindowManager.CreateWindowAsync(
        new BrowserWindowOptions
        {
            Width = 1152,
            Height = 940,
            Show = false
        }
    );
    await browserWindow.WebContents.Session.ClearCacheAsync();

    browserWindow.OnReadyToShow += () =>
    {
        browserWindow.Show();
        browserWindow.Maximize();
    };

    Electron.Menu.SetApplicationMenu(new MenuItem[] { });
});

app.Run();
