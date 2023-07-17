using AspNetCoreHero.ToastNotification;
using AspNetCoreHero.ToastNotification.Extensions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using PatientMgmtfinal.Data;
using PatientMgmtfinal.Permission;


var builder = WebApplication.CreateBuilder(args);
//builder.Services.AddScoped<UserManager<IdentityUser>>();
//builder.Services.AddScoped<RoleManager<IdentityRole>>();
//using (var scope = builder.Services.CreateScope())
//{
//    var userManager = scope.ServiceProvider.GetRequiredService<UserManager<Usuario>>();
//    var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();
//    await DefaultRoles.SeedAsync(roleManager);
//    await DefaultAdmin.SeedAsync(userManager);
//}

builder.Services.AddNotyf(config => { config.DurationInSeconds = 10; config.IsDismissable = true; config.Position = NotyfPosition.BottomRight; });

// Add services to the container.
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(connectionString));
builder.Services.AddDatabaseDeveloperPageExceptionFilter();

builder.Services.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = true)
    .AddRoles<IdentityRole>()
    .AddEntityFrameworkStores<ApplicationDbContext>()
    .AddDefaultUI()
    .AddDefaultTokenProviders();
//builder.Services.AddIdentity<IdentityUser, IdentityRole>()
//           .AddEntityFrameworkStores<ApplicationDbContext>()
//           .AddDefaultUI()
//           .AddDefaultTokenProviders();
builder.Services.AddControllersWithViews();
builder.Services.AddSingleton<IAuthorizationPolicyProvider, PermissionPolicyProvider>();
builder.Services.AddScoped<IAuthorizationHandler, PermissionAuthorizationHandler>();
builder.Services.AddScoped<IServiceProvider, ServiceProvider>();
//builder.Services.AddScoped<IServiceCollection, ServiceProvider>();
//var myService = services.BuildServiceProvider().CreateScope().ServiceProvider.GetRequiredService<IServiceCollection>();
var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    var loggerFactory = services.GetRequiredService<ILoggerFactory>();
    var logger = loggerFactory.CreateLogger("app");
    try
    {
        var userManager = services.GetRequiredService<UserManager<IdentityUser>>();
        var roleManager = services.GetRequiredService<RoleManager<IdentityRole>>();
        await PatientMgmtfinal.Data.DefaultRoles.SeedAsync(userManager, roleManager);
        await PatientMgmtfinal.Data.DefaultUsers.SeedBasicUserAsync(userManager, roleManager);
        await PatientMgmtfinal.Data.DefaultUsers.SeedSuperAdminAsync(userManager, roleManager);
        logger.LogInformation("Finished Seeding Default Data");
        logger.LogInformation("Application Starting");
    }
    catch (Exception ex)
    {
        logger.LogWarning(ex, "An error occurred seeding the DB");
    }
}



// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
    app.UseMigrationsEndPoint();
}
else
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");
app.MapRazorPages();
app.UseNotyf();
app.Run();
