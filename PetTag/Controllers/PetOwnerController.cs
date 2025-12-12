using Microsoft.AspNetCore.Mvc;
using PetTag.Service.DTOs;
using PetTag.Service.UnitOfWorks;

namespace PetTag.Controllers
{
    public class PetOwnerController : Controller
    {
        private readonly IUnitOfWorkService _unitOfWorkService;

        public PetOwnerController(IUnitOfWorkService unitOfWorkService)
        {
            _unitOfWorkService = unitOfWorkService;
        }

        // GET: PetOwner
        public IActionResult Index(string? search)
        {
            var owners = _unitOfWorkService.PetOwners.GetAll(q: search);
            return View(owners);
        }

        // GET: PetOwner/Details/5
        public IActionResult Details(int id)
        {
            var owner = _unitOfWorkService.PetOwners.Get(id);
            if (owner == null)
            {
                return NotFound();
            }
            return View(owner);
        }

        // GET: PetOwner/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: PetOwner/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(PetOwnerCreateDto dto)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    _unitOfWorkService.PetOwners.Add(dto);
                    TempData["SuccessMessage"] = "Pet sahibi başarıyla eklendi.";
                    return RedirectToAction(nameof(Index));
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError("", ex.Message);
                }
            }
            return View(dto);
        }

        // GET: PetOwner/Edit/5
        public IActionResult Edit(int id)
        {
            var owner = _unitOfWorkService.PetOwners.Get(id);
            if (owner == null)
            {
                return NotFound();
            }

            var updateDto = new PetOwnerUpdateDto
            {
                FirstName = owner.Value.FirstName,
                LastName = owner.Value.LastName,
                Email = owner.Value.Email
            };
            return View(updateDto);
        }

        // POST: PetOwner/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, PetOwnerUpdateDto dto)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    _unitOfWorkService.PetOwners.Update(id, dto);
                    TempData["SuccessMessage"] = "Pet sahibi başarıyla güncellendi.";
                    return RedirectToAction(nameof(Index));
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError("", ex.Message);
                }
            }
            return View(dto);
        }

        // POST: PetOwner/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Delete(int id)
        {
            try
            {
                _unitOfWorkService.PetOwners.Delete(id);
                TempData["SuccessMessage"] = "Pet sahibi başarıyla silindi.";
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = ex.Message;
            }
            return RedirectToAction(nameof(Index));
        }

        // POST: PetOwner/SoftDelete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult SoftDelete(int id)
        {
            try
            {
                _unitOfWorkService.PetOwners.SoftDelete(id);
                TempData["SuccessMessage"] = "Pet sahibi pasif hale getirildi.";
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = ex.Message;
            }
            return RedirectToAction(nameof(Index));
        }

        // POST: PetOwner/UndoDelete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult UndoDelete(int id)
        {
            try
            {
                _unitOfWorkService.PetOwners.UndoDelete(id);
                TempData["SuccessMessage"] = "Pet sahibi tekrar aktif hale getirildi.";
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = ex.Message;
            }
            return RedirectToAction(nameof(Index));
        }
    }
}

