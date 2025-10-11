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
    public class PetOwnerService : IPetOwnerService
    {
        private readonly IPetOwnerRepo _repo;

        public PetOwnerService(IPetOwnerRepo repo)
        {
            _repo = repo;
        }

        public PetOwnerService(IUnitOfWork uow) : this(uow.PetOwnerRepo) { }

        // okuma
        public IList<PetOwnerListItemDto> GetAll(string? q = null, int page = 1, int pageSize = 20)
        {
            q = q?.Trim();

            var query = _repo.GetFilteredList(
                select: o => new PetOwnerListItemDto(
                    o.Id,
                    o.FirstName,
                    o.LastName,
                    o.Email,
                    o.FirstName + " " + o.LastName     
                ),
                where: q == null
                    ? null
                    : o => o.FirstName.Contains(q) || o.LastName.Contains(q) || o.Email.Contains(q),
                orderBy: qy => qy.OrderBy(o => o.FirstName),
                include: null
            );

            return query
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToList();
        }

        public PetOwnerDetailDto? Get(int id)
        {
            return _repo.GetFilteredFirstOrDefault(
                select: o => new PetOwnerDetailDto(
                    o.Id,
                    o.FirstName,
                    o.LastName,
                    o.Email,
                    o.FirstName + " " + o.LastName     
                ),
                where: o => o.Id == id,
                include: null
            );
        }

        public PetOwnerDetailDto? GetByEmail(string email)
        {
            if (string.IsNullOrWhiteSpace(email)) return null;

            var owner = _repo.GetOwnerByEmail(email);
            return owner is null
                ? (PetOwnerDetailDto?)null
                : new PetOwnerDetailDto(
                    owner.Id,
                    owner.FirstName,
                    owner.LastName,
                    owner.Email,
                    owner.FirstName + " " + owner.LastName   
                  );
        }

        // yazma
        public void Add(PetOwnerCreateDto dto)
        {
            if (string.IsNullOrWhiteSpace(dto.FirstName)) throw new ArgumentException("FirstName is required.", nameof(dto.FirstName));
            if (string.IsNullOrWhiteSpace(dto.LastName)) throw new ArgumentException("LastName is required.", nameof(dto.LastName));
            if (string.IsNullOrWhiteSpace(dto.Email)) throw new ArgumentException("Email is required.", nameof(dto.Email));

            var owner = new PetOwner(dto.FirstName, dto.LastName)
            {
                Email = dto.Email
            };

            _repo.Add(owner); // eski repo stili: içeride SaveChanges()
        }

        public void Update(int id, PetOwnerUpdateDto dto)
        {
            var owner = _repo.GetById(id) ?? throw new Exception("PetOwner not found");

            if (dto.FirstName is not null) owner.FirstName = dto.FirstName;
            if (dto.LastName is not null) owner.LastName = dto.LastName;
            if (dto.Email is not null) owner.Email = dto.Email;

            _repo.Update(owner); // içeride SaveChanges()
        }

        // silme
        public void Delete(int id) => _repo.Delete(id);
        public void SoftDelete(int id) => _repo.SoftDelete(id);
        public void UndoDelete(int id) => _repo.UndoDelete(id);
    }
}