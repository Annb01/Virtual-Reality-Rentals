using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using VR_Rentals.Models;
using VR_Rentals.Services;

namespace VR_Rentals.Controllers
{
    public class RentalController : Controller
    {
        private readonly IRentalService _rentalService;
        private readonly ICustomerService _customerService;
        private readonly IVrEquipmentService _equipmentService;

        public RentalController(IRentalService rentalService, ICustomerService customerService, IVrEquipmentService equipmentService)
        {
            _rentalService = rentalService;
            _customerService = customerService;
            _equipmentService = equipmentService;
        }

        public async Task<IActionResult> Index()
        {
            var rentals = await _rentalService.GetAllAsync();
            return View(rentals);
        }

        public async Task<IActionResult> Create()
        {
            ViewBag.Equipments = await _equipmentService.GetAvailableAsync();
            return View();
        }

        public async Task<IActionResult> MyOrders()
        {
            int customerId = HttpContext.Session.GetInt32("CustomerId") ?? 0;
            var orders = await _rentalService.MyOrdersAsync(customerId);
            return View(orders);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Rental rental)
        {
            if (ModelState.IsValid)
            {
                await _rentalService.AddAsync(rental);
                return RedirectToAction(nameof(Index));
            }

            ViewBag.Equipments = await _equipmentService.GetAvailableAsync();
            return View(rental);
        }

        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
                return NotFound();

            var rental = await _rentalService.GetByIdAsync(id.Value);
            if (rental == null)
                return NotFound();

            ViewBag.Equipments = await _equipmentService.GetAvailableAsync();
            return View(rental);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Rental rental)
        {
            if (id != rental.RentalId)
                return NotFound();

            if (ModelState.IsValid)
            {
                try
                {
                    await _rentalService.UpdateAsync(rental);
                }
                catch (DbUpdateConcurrencyException)
                {
                    var exists = await _rentalService.ExistsAsync(id);
                    if (!exists)
                        return NotFound();
                    else
                        throw;
                }

                return RedirectToAction(nameof(Index));
            }

            ViewBag.Equipments = await _equipmentService.GetAvailableAsync();
            return View(rental);
        }

        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
                return NotFound();

            var rental = await _rentalService.GetByIdAsync(id.Value);
            if (rental == null)
                return NotFound();

            return View(rental);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await _rentalService.DeleteAsync(id);
            return RedirectToAction(nameof(Index));
        }
    }
}
