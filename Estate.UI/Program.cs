using Estate.BusinessLayer.Abstract;
using Estate.BusinessLayer.Concrete;
using Estate.DataAccessLayer.Abstract;
using Estate.DataAccessLayer.Concrete;
using Estate.DataAccessLayer.Data;
using Estate.EntityLayer.Entities;
using Estate.UI.Areas.Admin.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Reflection;
using FluentValidation;
using FluentValidation.AspNetCore;
using System.Globalization;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

builder.Services.AddAuthentication();
builder.Services.AddAuthorization();
builder.Services.AddDbContext<DataContext>(conf => conf.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")).UseLazyLoadingProxies());

builder.Services.AddIdentity<UserAdmin, IdentityRole>().AddEntityFrameworkStores<DataContext>().AddDefaultTokenProviders();

builder.Services.AddControllers().AddFluentValidation(opt =>
{
    opt.DisableDataAnnotationsValidation = true;
    opt.ValidatorOptions.LanguageManager.Culture = new CultureInfo("tr");
});
builder.Services.Configure<IdentityOptions>(opt =>
{
    opt.SignIn.RequireConfirmedPhoneNumber = false;
    opt.SignIn.RequireConfirmedEmail = false;
    opt.SignIn.RequireConfirmedAccount = false;

    opt.Password.RequireDigit = false;
    opt.Password.RequiredLength = 8;
    opt.Password.RequireLowercase = false;
    opt.Password.RequireUppercase = false;
    opt.Password.RequireNonAlphanumeric = false;
    opt.User.AllowedUserNameCharacters = "abcçdefgðhýijklmnroöprsþtuüvyzABCÇDEFGÐHIÝJKLMNROÖPRSÞTUÜVYZ0123456789-._";
});

builder.Services.ConfigureApplicationCookie(opt =>
{

    opt.LoginPath = "/Admin/Home/Login/";
    opt.LogoutPath = "/Admin/Home/LogOut";
    opt.AccessDeniedPath = "/Admin/Home/AccessDeniedPath";
    opt.ExpireTimeSpan = TimeSpan.FromMinutes(6);
});
builder.Services.AddSession();

builder.Services.AddScoped<IAdvertService, AdvertManager>();
builder.Services.AddScoped<ICityService, CityManager>();
builder.Services.AddScoped<IDistrictService, DistrictManager>();
builder.Services.AddScoped<IGeneralSettingsService, GeneralSettingsManager>();
builder.Services.AddScoped<IImagesService, ImagesManager>();
builder.Services.AddScoped<INeighbourhoodService, NeighbourhoodManager>();
builder.Services.AddScoped<ISituationService, SituationManager>();
builder.Services.AddScoped<ITypeService, TypeManager>();
builder.Services.AddScoped<IAdvertRepository, EfAdvertRepository>();
builder.Services.AddScoped<ICityRepository, EfCityRepository>();
builder.Services.AddScoped<IDistrictRepository, EfDistrictRepository>();
builder.Services.AddScoped<IGeneralSettingsRepository, EfGeneralSettingsRepository>();
builder.Services.AddScoped<IImagesRepository, EfImagesRepository>();
builder.Services.AddScoped<INeighbourhoodRepository, EfNeighbourhoodRepository>();
builder.Services.AddScoped<ISituationRepository, EfSituationRepository>();
builder.Services.AddScoped<ITypeRepository, EfTypeRepository>();
builder.Services.AddScoped<IAdvertRepository, EfAdvertRepository>();
builder.Services.AddScoped<ICityRepository, EfCityRepository>();

var app = builder.Build();

app.PrepareDatabase().GetAwaiter().GetResult();
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
app.UseSession();
app.UseEndpoints(endpoints =>
{
    endpoints.MapControllerRoute(
      name: "areas",
      pattern: "{area:exists}/{controller=Admin}/{action=Index}/{id?}"
    );
});

app.UseEndpoints(endpoints =>
{
    endpoints.MapControllerRoute(
      name: "areas",
      pattern: "{area:exists}/{controller=User}/{action=Index}/{id?}"
    );
});

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");


app.Run();
