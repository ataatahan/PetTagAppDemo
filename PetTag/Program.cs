using Microsoft.EntityFrameworkCore;
using PetTag.Repo.Contexts;
using PetTag.Repo.UnitOfWork;
using PetTag.Service.UnitOfWorks;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

// DbContext Configuration
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection") 
    ?? "Data Source=Ata;Initial Catalog=PetTagAppDemo41;Integrated Security=True;TrustServerCertificate=True;";

builder.Services.AddDbContext<PetTagAppDbContext>(options =>
    options.UseSqlServer(connectionString));

// Repository and Service Registration
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
builder.Services.AddScoped<IUnitOfWorkService, UnitOfWorkService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
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
