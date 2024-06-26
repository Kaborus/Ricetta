using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Ricetta.Data;
using Ricetta.Data.Entities;
using Ricetta.Hubs;
//using SignalRChat.Hubs;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
var connectionString = builder.Configuration.GetConnectionString("RicettaDb");

builder.Services.AddDbContext<RicettaDbContext>(options => options.UseSqlite(connectionString));

builder.Services.AddDatabaseDeveloperPageExceptionFilter();

builder.Services.AddDefaultIdentity<Member>(options => options.SignIn.RequireConfirmedAccount = true)
    .AddRoles<IdentityRole>()
    .AddEntityFrameworkStores<RicettaDbContext>();

builder.Services.AddSignalR();
builder.Services.AddControllersWithViews();

builder.Services.AddScoped<IRecipesRepository,RecipeEFRepository>();
//builder.Services.AddSingleton<ChatHub>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
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

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");
app.MapRazorPages();

app.MapHub<ChatHub>("/chatHub");

app.Run();
