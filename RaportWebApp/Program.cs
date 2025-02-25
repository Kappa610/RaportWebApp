using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using RaportDB.Data;
using Microsoft.AspNetCore.Http;

var builder = WebApplication.CreateBuilder(args);

// Dodaj us³ugi do kontenera.
builder.Services.AddControllersWithViews();

// Dodaj obs³ugê sesji
builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromMinutes(30); // Czas trwania sesji
    options.Cookie.HttpOnly = true; // Zabezpieczenie ciasteczka
    options.Cookie.IsEssential = true; // Wymagane dla GDPR
});

// Konfiguracja bazy danych
builder.Services.AddDbContext<RaportDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

var app = builder.Build();

// Skonfiguruj pipeline HTTP.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

// U¿yj sesji
app.UseSession();

app.UseAuthorization();

// Konfiguracja domyœlnej trasy
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Login}/{id?}");

app.Run();