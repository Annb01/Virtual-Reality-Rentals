using Microsoft.AspNetCore.Mvc;
using VR_Rentals.ViewModels;
using VR_Rentals.Models;
using VR_Rentals.Services;

public class AccountController : Controller
{
    private readonly IAccountService _accountService;

    public AccountController(IAccountService accountService)
    {
        _accountService = accountService;
    }

    public IActionResult Login()
    {
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> Login(LoginPageViewModel model)
    {
        if (!ModelState.IsValid)
            return View(model);

        var customer = await _accountService.AuthenticateAsync(model.Email, model.Password);
        if (customer == null)
        {
            ModelState.AddModelError("", "Incorrect Email or Password");
            return View(model);
        }

        HttpContext.Session.SetInt32("CustomerId", customer.CustomerId);
        HttpContext.Session.SetString("CustomerName", customer.CustomerName);

        return RedirectToAction("Index", "Home");
    }

    public IActionResult Register()
    {
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> Register(RegisterPageViewModel model)
    {
        if (!ModelState.IsValid)
            return View(model);

        try
        {
            await _accountService.RegisterAsync(model);
            TempData["SuccessMessage"] = "Registration successful! You can now log in.";
            return RedirectToAction("Login");
        }
        catch (ArgumentException ex)
        {
            ModelState.AddModelError("Email", ex.Message);
            return View(model);
        }
    }

    public IActionResult Logout()
    {
        HttpContext.Session.Clear();
        return RedirectToAction("Login");
    }
}
