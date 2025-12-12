using Microsoft.AspNetCore.Mvc;
using PetTag.Service.DTOs;
using PetTag.Service.UnitOfWorks;

namespace PetTag.Controllers
{
    public class VetAppointmentController : Controller
    {
        private readonly IUnitOfWorkService _unitOfWorkService;

        public VetAppointmentController(IUnitOfWorkService unitOfWorkService)
        {
            _unitOfWorkService = unitOfWorkService;
        }

        // GET: VetAppointment
        public IActionResult Index()
        {
            // Tüm randevuları göster - tüm pet'lerin randevularını topla
            var allPets = _unitOfWorkService.Pets.GetAll();
            var allAppointments = new List<VetAppointmentListItemDto>();
            
            foreach (var pet in allPets)
            {
                var appointments = _unitOfWorkService.VetAppointments.GetAllByPet(pet.Id);
                allAppointments.AddRange(appointments);
            }

            return View(allAppointments.OrderByDescending(a => a.AppointmentDate).ToList());
        }

        // GET: VetAppointment/Details/5
        public IActionResult Details(int id)
        {
            var appointment = _unitOfWorkService.VetAppointments.Get(id);
            if (appointment == null)
            {
                return NotFound();
            }
            return View(appointment);
        }

        // GET: VetAppointment/Create
        public IActionResult Create()
        {
            ViewBag.Pets = _unitOfWorkService.Pets.GetAll();
            ViewBag.Vets = _unitOfWorkService.Vets.GetAll();
            return View();
        }

        // POST: VetAppointment/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(VetAppointmentCreateDto dto)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    _unitOfWorkService.VetAppointments.Add(dto);
                    TempData["SuccessMessage"] = "Randevu başarıyla eklendi.";
                    return RedirectToAction(nameof(Index));
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError("", ex.Message);
                }
            }
            ViewBag.Pets = _unitOfWorkService.Pets.GetAll();
            ViewBag.Vets = _unitOfWorkService.Vets.GetAll();
            return View(dto);
        }

        // GET: VetAppointment/Edit/5
        public IActionResult Edit(int id)
        {
            var appointment = _unitOfWorkService.VetAppointments.Get(id);
            if (appointment == null)
            {
                return NotFound();
            }

            var updateDto = new VetAppointmentUpdateDto
            {
                AppointmentDate = appointment.AppointmentDate,
                VetId = appointment.VetId,
                PetId = appointment.PetId,
                Notes = appointment.Notes
            };
            ViewBag.Pets = _unitOfWorkService.Pets.GetAll();
            ViewBag.Vets = _unitOfWorkService.Vets.GetAll();
            return View(updateDto);
        }

        // POST: VetAppointment/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, VetAppointmentUpdateDto dto)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    _unitOfWorkService.VetAppointments.Update(id, dto);
                    TempData["SuccessMessage"] = "Randevu başarıyla güncellendi.";
                    return RedirectToAction(nameof(Index));
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError("", ex.Message);
                }
            }
            ViewBag.Pets = _unitOfWorkService.Pets.GetAll();
            ViewBag.Vets = _unitOfWorkService.Vets.GetAll();
            return View(dto);
        }

        // POST: VetAppointment/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Delete(int id)
        {
            try
            {
                _unitOfWorkService.VetAppointments.Delete(id);
                TempData["SuccessMessage"] = "Randevu başarıyla silindi.";
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = ex.Message;
            }
            return RedirectToAction(nameof(Index));
        }
    }
}

