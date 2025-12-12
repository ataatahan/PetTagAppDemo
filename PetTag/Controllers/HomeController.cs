using Microsoft.AspNetCore.Mvc;
using PetTag.Models;
using PetTag.Service.UnitOfWorks;
using System.Diagnostics;

namespace PetTag.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IUnitOfWorkService _unitOfWorkService;

        public HomeController(ILogger<HomeController> logger, IUnitOfWorkService unitOfWorkService)
        {
            _logger = logger;
            _unitOfWorkService = unitOfWorkService;
        }

        public IActionResult Index()
        {
            var allPets = _unitOfWorkService.Pets.GetAll();
            var allOwners = _unitOfWorkService.PetOwners.GetAll();
            var allVets = _unitOfWorkService.Vets.GetAll();
            
            // Tüm randevuları saymak için tüm pet'lerin randevularını topla
            int totalAppointments = 0;
            foreach (var pet in allPets)
            {
                totalAppointments += _unitOfWorkService.VetAppointments.GetAllByPet(pet.Id).Count;
            }

            // Pet türü dağılımı
            var petTypeDistribution = allPets
                .GroupBy(p => p.Type)
                .Select(g => new { Type = g.Key, Count = g.Count() })
                .ToList();

            // Chip'li pet sayısı
            int petsWithChip = allPets.Count(p => p.HasChip);

            var stats = new DashboardViewModel
            {
                TotalPetOwners = allOwners.Count,
                TotalPets = allPets.Count,
                TotalVets = allVets.Count,
                TotalAppointments = totalAppointments,
                PetsWithChip = petsWithChip,
                RecentPets = allPets.Take(5).ToList(),
                RecentOwners = allOwners.Take(5).ToList(),
                PetTypeDistribution = petTypeDistribution.Select(p => new PetTypeCount { Type = p.Type.ToString(), Count = p.Count }).ToList()
            };
            return View(stats);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }

    public class DashboardViewModel
    {
        public int TotalPetOwners { get; set; }
        public int TotalPets { get; set; }
        public int TotalVets { get; set; }
        public int TotalAppointments { get; set; }
        public int PetsWithChip { get; set; }
        public IList<PetTag.Service.DTOs.PetListItemDto> RecentPets { get; set; } = new List<PetTag.Service.DTOs.PetListItemDto>();
        public IList<PetTag.Service.DTOs.PetOwnerListItemDto> RecentOwners { get; set; } = new List<PetTag.Service.DTOs.PetOwnerListItemDto>();
        public IList<PetTypeCount> PetTypeDistribution { get; set; } = new List<PetTypeCount>();
    }

    public class PetTypeCount
    {
        public string Type { get; set; } = string.Empty;
        public int Count { get; set; }
    }
}
