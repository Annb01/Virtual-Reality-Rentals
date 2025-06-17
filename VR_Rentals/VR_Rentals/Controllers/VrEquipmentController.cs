using Microsoft.AspNetCore.Mvc;
using VR_Rentals.Models;
using VR_Rentals.Services;

namespace VR_Rentals.Controllers
{
    public class VrEquipmentController : Controller
    {
        private readonly IVrEquipmentService _equipmentService;

        public VrEquipmentController(IVrEquipmentService equipmentService)
        {
            _equipmentService = equipmentService;
        }

        public async Task<IActionResult> Index()
        {
            var equipments = await _equipmentService.GetAllAsync();
            return View(equipments);
        }

        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
                return NotFound();

            var equipment = await _equipmentService.GetByIdAsync(id.Value);
            if (equipment == null)
                return NotFound();

            return View(equipment);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(VrEquipment equipment)
        {
            if (ModelState.IsValid)
            {
                await _equipmentService.AddAsync(equipment);
                return RedirectToAction(nameof(Index));
            }
            return View(equipment);
        }

        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
                return NotFound();

            var equipment = await _equipmentService.GetByIdAsync(id.Value);
            if (equipment == null)
                return NotFound();

            return View(equipment);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, VrEquipment equipment)
        {
            if (id != equipment.EquipmentId)
                return NotFound();

            if (ModelState.IsValid)
            {
                try
                {
                    await _equipmentService.UpdateAsync(equipment);
                }
                catch (Exception ex)
                {
                    throw;
                }
                return RedirectToAction(nameof(Index));
            }
            return View(equipment);
        }

        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
                return NotFound();

            var equipment = await _equipmentService.GetByIdAsync(id.Value);
            if (equipment == null)
                return NotFound();

            return View(equipment);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await _equipmentService.DeleteAsync(id);
            return RedirectToAction(nameof(Index));
        }
    }
}
