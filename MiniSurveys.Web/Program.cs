using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using MiniSurveys.Domain.Data;
using MiniSurveys.Domain.Modals;
using Microsoft.AspNetCore.DataProtection;

var builder = WebApplication.CreateBuilder(args);
string connectionString;

if (builder.Environment.IsDevelopment()) connectionString = builder.Configuration.GetConnectionString("DevelopmentConnection")!;
else connectionString = builder.Configuration.GetConnectionString("ProductionConnection")!;

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
}).AddEntityFrameworkStores<ApplicationDbContext>().AddDefaultTokenProviders();

//if (builder.Environment.IsDevelopment())
//{
//    builder.Services.AddDataProtection()
//              .PersistKeysToFileSystem(new DirectoryInfo(@"ftp://minisurveys.somee.com/www.MiniSurveys.somee.com/temp-keys/"));
//    //.PersistKeysToDbContext<ApplicationDbContext>();
//}

builder.Services.AddControllersWithViews();

var app = builder.Build();

//using var scope = app.Services.GetRequiredService<IServiceScopeFactory>().CreateScope();
//await RoleInitializer.InitializeAsync(scope.ServiceProvider);

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}
app.UseDeveloperExceptionPage();    
app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();
app.UseAuthentication();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
//TrustServerCertificate=true;