using Microsoft.AspNetCore.Mvc;
using ContactManager.DTOs;
using ContactManager.Services;

namespace ContactManager.Controllers
{
    public class ContactController : Controller
    {
        private readonly IContactService _service;

        public ContactController(IContactService service) => _service = service;

        // LIST ALL

        public async Task<IActionResult> Index(string? filter, string? sortBy)
        {
            var contacts = await _service.GetAllAsync(filter, sortBy);
            return View(contacts); // Views/Contact/Index.cshtml
        }

        // DETAILS

        public async Task<IActionResult> Details(int id)
        {
            var contact = await _service.GetByIdAsync(id);
            if (contact == null) return NotFound();

            return View(contact); // Views/Contact/Details.cshtml
        }

        // CREATE

        public IActionResult Create()
        {
            return View(); // shows empty form
        }

        [HttpPost]
        public async Task<IActionResult> Create(ContactRequest request)
        {
            if (!ModelState.IsValid)
                return View(request); // return form with validation errors

            await _service.CreateAsync(request);
            return RedirectToAction("Index");
        }

        // EDIT

        public async Task<IActionResult> Edit(int id)
        {
            var contact = await _service.GetByIdAsync(id);
            if (contact == null) return NotFound();

            ViewBag.Current = contact;

            var request = MapToRequest(contact);
            return View(request);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(int id, ContactRequest request)
        {
            if (!ModelState.IsValid)
                return View(request); // return form with errors

            var success = await _service.UpdateAsync(id, request);
            return success ? RedirectToAction("Index") : NotFound();
        }

        // DELETE

        public async Task<IActionResult> Delete(int id)
        {
            var contact = await _service.GetByIdAsync(id);
            if (contact == null) return NotFound();

            return View(contact); // Confirm delete form
        }

        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> ConfirmDelete(int id)
        {
            await _service.DeleteAsync(id);
            return RedirectToAction("Index");
        }
        private ContactRequest MapToRequest(ContactResponse response) => new()
        {
            FirstName = response.FirstName,
            LastName = response.LastName,
            Phone = response.Phone,
            Email = response.Email,
            City = response.City
        };
    }
}
