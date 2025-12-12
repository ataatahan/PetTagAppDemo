using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PetTag.Service.DTOs;
using PetTag.Service.UnitOfWorks;

namespace PetTag.Controllers
{
    public class PetController : Controller
    {
        private readonly IUnitOfWorkService _unitOfWorkService;

        public PetController(IUnitOfWorkService unitOfWorkService)
        {
            _unitOfWorkService = unitOfWorkService;
        }

        // GET: Pet
        public IActionResult Index()
        {
            var pets = _unitOfWorkService.Pets.GetAll();
            return View(pets);
        }

        // GET: Pet/Details/5
        public IActionResult Details(int id)
        {
            var pet = _unitOfWorkService.Pets.Get(id);
            if (pet == null || !pet.HasValue)
            {
                return NotFound();
            }

            var petValue = pet.Value;
            
            // İlgili kayıtları getir
            var appointments = _unitOfWorkService.VetAppointments.GetAllByPet(id);
            var healthRecords = _unitOfWorkService.HealthRecords.GetAllByPet(id);
            var alerts = _unitOfWorkService.Alerts.GetAllByPet(id);
            var activityLogs = _unitOfWorkService.ActivityLogs.GetAllByPet(id);
            var petChip = _unitOfWorkService.PetChips.GetByPetId(id);

            ViewBag.Appointments = appointments.OrderByDescending(a => a.AppointmentDate).Take(5).ToList();
            ViewBag.HealthRecords = healthRecords.Take(5).ToList();
            ViewBag.Alerts = alerts.OrderByDescending(a => a.AlertDate).Take(5).ToList();
            ViewBag.ActivityLogs = activityLogs.OrderByDescending(a => a.LogDate).Take(5).ToList();
            ViewBag.PetChip = petChip;
            ViewBag.TotalAppointments = appointments.Count;
            ViewBag.TotalHealthRecords = healthRecords.Count;
            ViewBag.TotalAlerts = alerts.Count;
            ViewBag.TotalActivityLogs = activityLogs.Count;

            return View(petValue);
        }

        // GET: Pet/Create
        public IActionResult Create()
        {
            // Pagination olmadan tüm kayıtları al
            ViewBag.PetOwners = _unitOfWorkService.PetOwners.GetAll(q: null, page: 1, pageSize: int.MaxValue);
            ViewBag.Vets = _unitOfWorkService.Vets.GetAll(q: null, page: 1, pageSize: int.MaxValue);
            return View();
        }

        // POST: Pet/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(PetCreateDto dto)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    // PetOwner ve Vet kontrolü - nullable record struct için HasValue kontrolü
                    var petOwner = _unitOfWorkService.PetOwners.Get(dto.PetOwnerId);
                    if (!petOwner.HasValue)
                    {
                        ModelState.AddModelError("PetOwnerId", $"ID {dto.PetOwnerId} ile pet sahibi bulunamadı. Lütfen geçerli bir pet sahibi seçin.");
                        ViewBag.PetOwners = _unitOfWorkService.PetOwners.GetAll(q: null, page: 1, pageSize: int.MaxValue);
                        ViewBag.Vets = _unitOfWorkService.Vets.GetAll(q: null, page: 1, pageSize: int.MaxValue);
                        return View(dto);
                    }

                    var vet = _unitOfWorkService.Vets.Get(dto.VetId);
                    if (vet == null)
                    {
                        ModelState.AddModelError("VetId", $"ID {dto.VetId} ile veteriner bulunamadı. Lütfen geçerli bir veteriner seçin.");
                        ViewBag.PetOwners = _unitOfWorkService.PetOwners.GetAll(q: null, page: 1, pageSize: int.MaxValue);
                        ViewBag.Vets = _unitOfWorkService.Vets.GetAll(q: null, page: 1, pageSize: int.MaxValue);
                        return View(dto);
                    }

                    _unitOfWorkService.Pets.Add(dto);
                    TempData["SuccessMessage"] = "Evcil hayvan başarıyla eklendi.";
                    return RedirectToAction(nameof(Index));
                }
                catch (DbUpdateException dbEx)
                {
                    string errorMessage = "Veritabanı hatası oluştu: ";
                    
                    if (dbEx.InnerException != null)
                    {
                        var innerEx = dbEx.InnerException;
                        if (innerEx.Message.Contains("FOREIGN KEY"))
                        {
                            errorMessage = "Seçilen pet sahibi veya veteriner geçersiz. Lütfen geçerli bir pet sahibi ve veteriner seçin.";
                        }
                        else if (innerEx.Message.Contains("PRIMARY KEY") || innerEx.Message.Contains("UNIQUE"))
                        {
                            errorMessage = "Bu kayıt zaten mevcut. Lütfen farklı bir kayıt deneyin.";
                        }
                        else
                        {
                            errorMessage += innerEx.Message;
                        }
                    }
                    else
                    {
                        errorMessage += dbEx.Message;
                    }
                    
                    ModelState.AddModelError("", errorMessage);
                }
                catch (ArgumentException argEx)
                {
                    ModelState.AddModelError("", argEx.Message);
                }
                catch (PetTag.Core.Exceptions.PetException.InvalidPetNameException)
                {
                    ModelState.AddModelError("Name", "Pet ismi boş olamaz veya 50 karakterden uzun olamaz.");
                }
                catch (PetTag.Core.Exceptions.PetException.InvalidPetAgeException)
                {
                    ModelState.AddModelError("Age", "Pet yaşı 0 ile 150 arasında olmalıdır.");
                }
                catch (PetTag.Core.Exceptions.PetException.InvalidPetWeightException)
                {
                    ModelState.AddModelError("Weight", "Pet kilosu 0'dan büyük olmalıdır.");
                }
                catch (Exception ex)
                {
                    string errorMessage = "Bir hata oluştu: " + ex.Message;
                    if (ex.InnerException != null)
                    {
                        errorMessage += " Detay: " + ex.InnerException.Message;
                    }
                    ModelState.AddModelError("", errorMessage);
                }
            }
            ViewBag.PetOwners = _unitOfWorkService.PetOwners.GetAll(q: null, page: 1, pageSize: int.MaxValue);
            ViewBag.Vets = _unitOfWorkService.Vets.GetAll(q: null, page: 1, pageSize: int.MaxValue);
            return View(dto);
        }

        // GET: Pet/Edit/5
        public IActionResult Edit(int id)
        {
            var pet = _unitOfWorkService.Pets.Get(id);
            if (pet == null)
            {
                return NotFound();
            }

            var updateDto = new PetUpdateDto
            {
                Name = pet.Value.Name,
                Type = pet.Value.Type,
                Age = pet.Value.Age,
                Weight = pet.Value.Weight,
                PetOwnerId = pet.Value.PetOwnerId,
                VetId = pet.Value.VetId
            };
            ViewBag.PetOwners = _unitOfWorkService.PetOwners.GetAll(q: null, page: 1, pageSize: int.MaxValue);
            ViewBag.Vets = _unitOfWorkService.Vets.GetAll(q: null, page: 1, pageSize: int.MaxValue);
            return View(updateDto);
        }

        // POST: Pet/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, PetUpdateDto dto)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    // PetOwner ve Vet kontrolü (eğer değiştiriliyorsa)
                    if (dto.PetOwnerId.HasValue)
                    {
                        var petOwner = _unitOfWorkService.PetOwners.Get(dto.PetOwnerId.Value);
                        if (!petOwner.HasValue)
                        {
                            ModelState.AddModelError("PetOwnerId", $"ID {dto.PetOwnerId.Value} ile pet sahibi bulunamadı.");
                            ViewBag.PetOwners = _unitOfWorkService.PetOwners.GetAll(q: null, page: 1, pageSize: int.MaxValue);
                            ViewBag.Vets = _unitOfWorkService.Vets.GetAll(q: null, page: 1, pageSize: int.MaxValue);
                            return View(dto);
                        }
                    }

                    if (dto.VetId.HasValue)
                    {
                        var vet = _unitOfWorkService.Vets.Get(dto.VetId.Value);
                        if (vet == null)
                        {
                            ModelState.AddModelError("VetId", $"ID {dto.VetId.Value} ile veteriner bulunamadı.");
                            ViewBag.PetOwners = _unitOfWorkService.PetOwners.GetAll(q: null, page: 1, pageSize: int.MaxValue);
                            ViewBag.Vets = _unitOfWorkService.Vets.GetAll(q: null, page: 1, pageSize: int.MaxValue);
                            return View(dto);
                        }
                    }

                    _unitOfWorkService.Pets.Update(id, dto);
                    TempData["SuccessMessage"] = "Evcil hayvan başarıyla güncellendi.";
                    return RedirectToAction(nameof(Index));
                }
                catch (DbUpdateException dbEx)
                {
                    string errorMessage = "Veritabanı hatası oluştu: ";
                    
                    if (dbEx.InnerException != null)
                    {
                        var innerEx = dbEx.InnerException;
                        if (innerEx.Message.Contains("FOREIGN KEY"))
                        {
                            errorMessage = "Seçilen pet sahibi veya veteriner geçersiz. Lütfen geçerli bir pet sahibi ve veteriner seçin.";
                        }
                        else
                        {
                            errorMessage += innerEx.Message;
                        }
                    }
                    else
                    {
                        errorMessage += dbEx.Message;
                    }
                    
                    ModelState.AddModelError("", errorMessage);
                }
                catch (PetTag.Core.Exceptions.PetException.InvalidPetNameException)
                {
                    ModelState.AddModelError("Name", "Pet ismi boş olamaz veya 50 karakterden uzun olamaz.");
                }
                catch (PetTag.Core.Exceptions.PetException.InvalidPetAgeException)
                {
                    ModelState.AddModelError("Age", "Pet yaşı 0 ile 150 arasında olmalıdır.");
                }
                catch (PetTag.Core.Exceptions.PetException.InvalidPetWeightException)
                {
                    ModelState.AddModelError("Weight", "Pet kilosu 0'dan büyük olmalıdır.");
                }
                catch (Exception ex)
                {
                    string errorMessage = "Bir hata oluştu: " + ex.Message;
                    if (ex.InnerException != null)
                    {
                        errorMessage += " Detay: " + ex.InnerException.Message;
                    }
                    ModelState.AddModelError("", errorMessage);
                }
            }
            ViewBag.PetOwners = _unitOfWorkService.PetOwners.GetAll(q: null, page: 1, pageSize: int.MaxValue);
            ViewBag.Vets = _unitOfWorkService.Vets.GetAll(q: null, page: 1, pageSize: int.MaxValue);
            return View(dto);
        }

        // POST: Pet/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Delete(int id)
        {
            try
            {
                _unitOfWorkService.Pets.Delete(id);
                TempData["SuccessMessage"] = "Evcil hayvan başarıyla silindi.";
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = ex.Message;
            }
            return RedirectToAction(nameof(Index));
        }

        // POST: Pet/SoftDelete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult SoftDelete(int id)
        {
            try
            {
                _unitOfWorkService.Pets.SoftDelete(id);
                TempData["SuccessMessage"] = "Evcil hayvan pasif hale getirildi.";
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = ex.Message;
            }
            return RedirectToAction(nameof(Index));
        }

        // POST: Pet/UndoDelete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult UndoDelete(int id)
        {
            try
            {
                _unitOfWorkService.Pets.UndoDelete(id);
                TempData["SuccessMessage"] = "Evcil hayvan tekrar aktif hale getirildi.";
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = ex.Message;
            }
            return RedirectToAction(nameof(Index));
        }
    }
}

