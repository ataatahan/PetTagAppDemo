using PetTag.Core.Entities;
using PetTag.Repo.Interfaces;
using PetTag.Repo.UnitOfWork;
using PetTag.Service.DTOs;
using PetTag.Service.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PetTag.Service.Concretes
{
    public class VetService : IVetService
    {
        private readonly IVetRepo _repo;

        public VetService(IVetRepo repo)
        {
            _repo = repo;
        }

        public VetService(IUnitOfWork uow) : this(uow.VetRepo) { }

        // okuma
        public IList<VetListItemDto> GetAll(string? q = null, int page = 1, int pageSize = 20)
        {
            q = q?.Trim();

            var query = _repo.GetFilteredList(
                select: v => new VetListItemDto
                {
                    Id = v.Id,
                    FirstName = v.FirstName,
                    LastName = v.LastName,
                    PhoneNumber = v.PhoneNumber
                    
                },
                where: q == null
                    ? null
                    : v => v.FirstName.Contains(q) || v.LastName.Contains(q) || v.PhoneNumber.Contains(q),
                orderBy: qy => qy.OrderBy(v => v.FirstName),
                include: null
            );

            return query
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToList();
        }

        public VetDetailDto? Get(int id)
        {
            return _repo.GetFilteredFirstOrDefault(
                select: v => new VetDetailDto
                {
                    Id = v.Id,
                    FirstName = v.FirstName,
                    LastName = v.LastName,
                    PhoneNumber = v.PhoneNumber
                },
                where: v => v.Id == id,
                include: null
            );
        }

        public VetDetailDto? GetByFullName(string fullName)
        {
            if (string.IsNullOrWhiteSpace(fullName)) return null;

            var vet = _repo.GetVetByFullName(fullName);
            return vet is null
                ? (VetDetailDto?)null
                : new VetDetailDto
                {
                    Id = vet.Id,
                    FirstName = vet.FirstName,
                    LastName = vet.LastName,
                    PhoneNumber = vet.PhoneNumber
                };
        }

        // yazma
        public void Add(VetCreateDto dto)
        {
            if (string.IsNullOrWhiteSpace(dto.FirstName)) throw new ArgumentException("FirstName is required.", nameof(dto.FirstName));
            if (string.IsNullOrWhiteSpace(dto.LastName)) throw new ArgumentException("LastName is required.", nameof(dto.LastName));
            if (string.IsNullOrWhiteSpace(dto.PhoneNumber)) throw new ArgumentException("PhoneNumber is required.", nameof(dto.PhoneNumber));

            var vet = new Vet(dto.FirstName, dto.LastName)
            {
                PhoneNumber = dto.PhoneNumber  // Entity setter'ı validasyonu yapar
            };

            _repo.Add(vet); // içeride SaveChanges()
        }

        public void Update(int id, VetUpdateDto dto)
        {
            var vet = _repo.GetById(id) ?? throw new Exception("Vet not found");

            if (dto.FirstName is not null) vet.FirstName = dto.FirstName;
            if (dto.LastName is not null) vet.LastName = dto.LastName;
            if (dto.PhoneNumber is not null) vet.PhoneNumber = dto.PhoneNumber;

            _repo.Update(vet); // içeride SaveChanges()
        }

        // silme
        public void Delete(int id) => _repo.Delete(id);
        public void SoftDelete(int id) => _repo.SoftDelete(id);
        public void UndoDelete(int id) => _repo.UndoDelete(id);
    }
}