var builder = WebApplication.CreateBuilder(args);

// Dodaj us³ugi do kontenera.
builder.Services.AddControllersWithViews();

var app = builder.Build();

// Skonfiguruj pipeline HTTP.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // Domyœlna wartoœæ HSTS to 30 dni. Mo¿esz to zmieniæ dla scenariuszy produkcyjnych, zobacz https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

// Konfiguracja domyœlnej trasy
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Login}/{id?}");

app.Run();