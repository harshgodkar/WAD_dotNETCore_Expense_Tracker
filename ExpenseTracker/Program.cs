using ExpenseTracker.AppDbContext;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);
var connectionString = builder.Configuration.GetConnectionString("ExpenseDBContextConnection") ?? throw new InvalidOperationException("Connection string 'ExpenseDBContextConnection' not found.");

builder.Services.AddDbContext<ExpenseDBContext>(options =>
    options.UseSqlServer(connectionString)
) ;
    //options.UseSqlServer("Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=C:\\Users\\harsh\\OneDrive\\Documents\\ExpenseDb.mdf;Integrated Security=True;Connect Timeout=30")
// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddScoped<ExpensesDataAcessLayer>();

builder.Services.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = true)
    .AddEntityFrameworkStores<ExpenseDBContext>();
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
app.UseAuthentication();
app.UseAuthorization();
app.MapRazorPages();
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
