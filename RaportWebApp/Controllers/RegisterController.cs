using Microsoft.AspNetCore.Mvc;

public class AccountController : Controller
{
    public IActionResult Register()
    {
        return View("~/Views/Home/Register.cshtml");
    }

    public IActionResult Login()
    {
        return View("~/Views/Home/Login.cshtml");
    }

}