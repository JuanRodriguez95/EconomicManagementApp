using EconomicManagementAPP.Data;
using EconomicManagementAPP.Services;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddTransient<RepositorieAccountTypes>();
builder.Services.AddTransient<RepositorieUsers>();

builder.Services.AddTransient<RepositorieOperationTypes>();
builder.Services.AddTransient<RepositorieAccounts>();
builder.Services.AddTransient<RepositorieCategories>();
builder.Services.AddTransient<RepositorieTransactions>();


builder.Services.AddDbContext<EconomicContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));



var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
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

app.Run();
