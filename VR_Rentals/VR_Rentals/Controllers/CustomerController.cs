using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using VR_Rentals.Models;
using VR_Rentals.ViewModels;
using VR_Rentals.Repositories;
using VR_Rentals.Services;

namespace VR_customers.Controllers
{
    public class CustomerController : Controller
{
        private readonly ICustomerService _customerService;

        public CustomerController(ICustomerService customerService)
        {
            _customerService = customerService;
        }

        public async Task<IActionResult> Index()
    {
        var customers = await _customerService.GetAllAsync();
        return View(customers);
    }

    public IActionResult Create()
    {
        return View();
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(Customer customer)
    {
        if (ModelState.IsValid)
        {
            await _customerService.AddAsync(customer);
            return RedirectToAction(nameof(Index));
        }
        return View(customer);
    }

    public async Task<IActionResult> Edit(int? id)
    {
        if (id == null)
            return NotFound();

        var customer = await _customerService.GetByIdAsync(id.Value);
        if (customer == null)
            return NotFound();

        return View(customer);
    }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, AccountPageViewModel model)
        {
            if (!ModelState.IsValid)
                return View(model);

            try
            {
                await _customerService.UpdateAccountAsync(id, model);
            }
            catch (ArgumentException ex)
            {
                ModelState.AddModelError("Email", ex.Message);
                return View(model);
            }
            catch (UnauthorizedAccessException ex)
            {
                ModelState.AddModelError("CurrentPassword", ex.Message);
                return View(model);
            }
            catch (KeyNotFoundException)
            {
                return NotFound();
            }
            catch (Exception)
            {
                throw;
            }

            TempData["SuccessMessage"] = "Account updated successfully.";
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Delete(int? id)
    {
        if (id == null)
            return NotFound();

        var customer = await _customerService.GetByIdAsync(id.Value);
        if (customer == null)
            return NotFound();

        return View(customer);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Delete(int id)
    {
        await _customerService.DeleteAsync(id);
        return RedirectToAction(nameof(Index));
    }
}

}
