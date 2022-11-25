using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using MiniSurveys.Domain.Data;
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
        options.Password.RequireNonAlphanumeric = false;   // требуются ли не алфавитно-цифровые символы
        options.Password.RequireLowercase = false; // требуются ли символы в нижнем регистре
        options.Password.RequireUppercase = false; // требуются ли символы в верхнем регистре
        options.Password.RequireDigit = false; // требуются ли цифры
        options.User.RequireUniqueEmail = true; // требуется уникальная элек. почта
        options.SignIn.RequireConfirmedEmail = false; // обязательное подтверждение почты
    }).AddEntityFrameworkStores<ApplicationDbContext>();

var app = builder.Build();

//app.Run(async context =>
//{
//    var userManager = app.Services.GetRequiredService<UserManager<User>>();
//    var rolesManager = app.Services.GetRequiredService<RoleManager<IdentityRole>>();
//    await RoleInitializer.InitializeAsync(app.Services.ServicePr);
//});


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
