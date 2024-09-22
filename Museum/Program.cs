using Museum.Controllers.UtilityControllers;
using Museum.Contexts;
using Microsoft.AspNetCore.Authentication.Cookies;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

var connection = builder.Configuration.GetConnectionString("DefaultConnection");

builder.Services.Add(new ServiceDescriptor(typeof(ExhibitContext), new ExhibitContext(connection)));
builder.Services.Add(new ServiceDescriptor(typeof(HallContext), new HallContext(connection)));
builder.Services.Add(new ServiceDescriptor(typeof(ExhibitionContext), new ExhibitionContext(connection)));
builder.Services.Add(new ServiceDescriptor(typeof(UserContext), new UserContext(connection)));
builder.Services.Add(new ServiceDescriptor(typeof(CategoryContext), new CategoryContext(connection)));
builder.Services.Add(new ServiceDescriptor(typeof(FileContext), new FileContext(connection)));
builder.Services.Add(new ServiceDescriptor (typeof(AddExhibitContext), new AddExhibitContext(connection)));


builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
                .AddCookie(options =>
                {
                    options.LoginPath = new Microsoft.AspNetCore.Http.PathString("/Login/Index");
                    options.AccessDeniedPath = new Microsoft.AspNetCore.Http.PathString("/Login/Index");
                });



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
