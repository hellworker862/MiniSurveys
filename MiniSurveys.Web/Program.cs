using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using MiniSurveys.Domain.Data;
using MiniSurveys.Domain.Data.Initializers;
using MiniSurveys.Domain.Modals;

var builder = WebApplication.CreateBuilder(args);
var connectionString = builder.Configuration.GetValue<string>("ConnectionString");

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddDbContext<ApplicationDbContext>(options => options.UseSqlServer(connectionString));
builder.Services.AddIdentity<User, IdentityRole<int>>(options =>
{
    options.SignIn.RequireConfirmedAccount = true;
    options.Password.RequiredLength = 5;
    options.Password.RequireNonAlphanumeric = false;   // ��������� �� �� ���������-�������� �������
    options.Password.RequireLowercase = false; // ��������� �� ������� � ������ ��������
    options.Password.RequireUppercase = false; // ��������� �� ������� � ������� ��������
    options.Password.RequireDigit = false; // ��������� �� �����
    options.User.RequireUniqueEmail = true; // ��������� ���������� ����. �����
    options.SignIn.RequireConfirmedEmail = false; // ������������ ������������� �����
}).AddEntityFrameworkStores<ApplicationDbContext>();

var app = builder.Build();

using var scope = app.Services.GetRequiredService<IServiceScopeFactory>().CreateScope();
await RoleInitializer.InitializeAsync(scope.ServiceProvider);


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
app.UseAuthentication();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
