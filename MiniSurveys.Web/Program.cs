using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using MiniSurveys.Domain.Data;
using MiniSurveys.Domain.Modals;

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

builder.Services.AddCors();

builder.Services.AddDistributedMemoryCache();
builder.Services.AddSession(options =>
{
    options.Cookie.Name = ".MiniSurveys.Session";
    options.Cookie.IsEssential = true;
});

builder.Services.AddControllersWithViews();

var app = builder.Build();

app.UseSession();

using var scope = app.Services.GetRequiredService<IServiceScopeFactory>().CreateScope();
await InitialUser.InitializeAsync(scope.ServiceProvider);

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error/{0}");
    app.UseHsts();
}

app.UseStatusCodePagesWithReExecute("/Error/{0}");
app.UseHsts();

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseCors(builder => builder.AllowAnyOrigin()
                              .AllowAnyHeader());

app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Survey}/{action=Index}/{id?}");

app.Run();