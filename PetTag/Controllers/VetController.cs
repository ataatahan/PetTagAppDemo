using Microsoft.AspNetCore.Mvc;
using PetTag.Service.DTOs;
using PetTag.Service.UnitOfWorks;

namespace PetTag.Controllers
{
    public class VetController : Controller
    {
        private readonly IUnitOfWorkService _unitOfWorkService;

        public VetController(IUnitOfWorkService unitOfWorkService)
        {
            _unitOfWorkService = unitOfWorkService;
        }

        // GET: Vet
        public IActionResult Index(string? search)
        {
            var vets = _unitOfWorkService.Vets.GetAll(q: search);
            return View(vets);
        }

        // GET: Vet/Details/5
        public IActionResult Details(int id)
        {
            var vet = _unitOfWorkService.Vets.Get(id);
            if (vet == null)
            {
                return NotFound();
            }
            return View(vet);
        }

        // GET: Vet/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Vet/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(VetCreateDto dto)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    _unitOfWorkService.Vets.Add(dto);
                    TempData["SuccessMessage"] = "Veteriner başarıyla eklendi.";
                    return RedirectToAction(nameof(Index));
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError("", ex.Message);
                }
            }
            return View(dto);
        }

        // GET: Vet/Edit/5
        public IActionResult Edit(int id)
        {
            var vet = _unitOfWorkService.Vets.Get(id);
            if (vet == null)
            {
                return NotFound();
            }

            var updateDto = new VetUpdateDto
            {
                FirstName = vet.FirstName,
                LastName = vet.LastName,
                PhoneNumber = vet.PhoneNumber
            };
            return View(updateDto);
        }

        // POST: Vet/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, VetUpdateDto dto)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    _unitOfWorkService.Vets.Update(id, dto);
                    TempData["SuccessMessage"] = "Veteriner başarıyla güncellendi.";
                    return RedirectToAction(nameof(Index));
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError("", ex.Message);
                }
            }
            return View(dto);
        }

        // POST: Vet/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Delete(int id)
        {
            try
            {
                _unitOfWorkService.Vets.Delete(id);
                TempData["SuccessMessage"] = "Veteriner başarıyla silindi.";
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = ex.Message;
            }
            return RedirectToAction(nameof(Index));
        }

        // POST: Vet/SoftDelete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult SoftDelete(int id)
        {
            try
            {
                _unitOfWorkService.Vets.SoftDelete(id);
                TempData["SuccessMessage"] = "Veteriner pasif hale getirildi.";
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = ex.Message;
            }
            return RedirectToAction(nameof(Index));
        }

        // POST: Vet/UndoDelete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult UndoDelete(int id)
        {
            try
            {
                _unitOfWorkService.Vets.UndoDelete(id);
                TempData["SuccessMessage"] = "Veteriner tekrar aktif hale getirildi.";
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = ex.Message;
            }
            return RedirectToAction(nameof(Index));
        }
    }
}

