using Microsoft.AspNetCore.Mvc;
using RaportDB.Data;
using RaportDB.Models;
using RaportWebApp.Models;
using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using BCrypt.Net;
using Microsoft.EntityFrameworkCore;

namespace RaportWebApp.Controllers
{
    public class AccountController : Controller
    {
        private readonly RaportDbContext _context;

        public AccountController(RaportDbContext context)
        {
            _context = context;
        }

        // Rejestracja - widok
        public IActionResult Register()
        {
            return View("~/Views/Home/Register.cshtml");
        }

        // Rejestracja - przetwarzanie
        [HttpPost]
        public async Task<IActionResult> Register(RegisterModel model)
        {
            try
            {
                if (!ModelState.IsValid)
                    return View("~/Views/Home/Register.cshtml", model);

                if (await _context.Users.AnyAsync(u => u.Email == model.Email))
                {
                    ModelState.AddModelError("Email", "Email jest już zajęty.");
                    return View("~/Views/Home/Register.cshtml", model);
                }

                var user = new User
                {
                    Name = model.Name,
                    Email = model.Email,
                    Password = BCrypt.Net.BCrypt.HashPassword(model.Password), // Hashowanie hasła
                    BirthDate = DateTime.UtcNow,
                    isAdmin = false,
                    CreatedAt = DateTime.UtcNow
                };

                await _context.Users.AddAsync(user);
                await _context.SaveChangesAsync();

                return RedirectToAction("Login");
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", "Błąd podczas rejestracji: " + ex.Message);
                return View("~/Views/Home/Register.cshtml", model);
            }
        }

        // Logowanie - widok
        public IActionResult Login()
        {
            return View("~/Views/Home/Login.cshtml");
        }

        // Logowanie - przetwarzanie
        [HttpPost]
        public async Task<IActionResult> Login(LoginModel model)
        {
            try
            {
                if (!ModelState.IsValid)
                    return View("~/Views/Home/Login.cshtml", model);

                var user = await _context.Users
                    .FirstOrDefaultAsync(u => u.Email == model.Email);

                if (user != null && BCrypt.Net.BCrypt.Verify(model.Password, user.Password))
                {
                    HttpContext.Session.SetInt32("UserId", user.Id);
                    HttpContext.Session.SetString("UserEmail", user.Email);

                    return RedirectToAction("Privacy", "Home");
                }

                // Jeśli użytkownik nie istnieje lub hasło jest nieprawidłowe
                ModelState.AddModelError(string.Empty, "Nieprawidłowy email lub hasło.");
                return View("~/Views/Home/Login.cshtml", model);
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, "Błąd logowania: " + ex.Message);
                return View("~/Views/Home/Login.cshtml", model);
            }
        }

        // Wylogowanie
        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("Index", "Home");
        }

        // Profil użytkownika
        public async Task<IActionResult> Profile()
        {
            var userId = HttpContext.Session.GetInt32("UserId");
            if (userId == null) return RedirectToAction("Login");

            var user = await _context.Users
                .FirstOrDefaultAsync(u => u.Id == userId);

            return user == null
                ? RedirectToAction("Login")
                : View("~/Views/Home/Profile.cshtml", user); // Upewnij się, że widok istnieje
        }
    }
}