using Microsoft.EntityFrameworkCore;
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
    public class PetService : IPetService
    {
        private readonly IPetRepo _repo;

        public PetService(IPetRepo repo)
        {
            _repo = repo;
        }

        public PetService(IUnitOfWork uow) : this(uow.PetRepo) { }

        // okuma
        public IList<PetListItemDto> GetAll()
        {
            // HasChip alanı için PetChip'e bakıyoruz → include eklendi
            var query = _repo.GetFilteredList(
                select: p => new PetListItemDto(
                    p.Id,
                    p.Name,
                    p.Type,
                    p.Age,
                    p.Weight,
                    p.PetOwnerId,
                    p.VetId,
                    p.PetChip != null   
                ),
                where: null,
                orderBy: q => q.OrderBy(x => x.Name),
                include: q => q.Include(x => x.PetChip)
            );

            return query.ToList();
        }

        public PetDetailDto? Get(int id)
        {
            return _repo.GetFilteredFirstOrDefault(
                select: p => new PetDetailDto(
                    p.Id,
                    p.Name,
                    p.Type,
                    p.Age,
                    p.Weight,
                    p.PetOwnerId,
                    p.VetId,
                    p.PetChip != null  
                ),
                where: p => p.Id == id,
                include: q => q.Include(x => x.PetChip)
            );
        }

        // yazma
        public void Add(PetCreateDto dto)
        {
            if (string.IsNullOrWhiteSpace(dto.Name))
                throw new ArgumentException("Name is required.", nameof(dto.Name));

            var pet = new Pet(dto.Name, dto.Age)
            {
                Type = dto.Type,
                PetOwnerId = dto.PetOwnerId,
                VetId = dto.VetId
            };

            if (dto.Weight.HasValue)
                pet.Weight = dto.Weight.Value; // domain setter'ı <=0 ise exception atıyor

            _repo.Add(pet); // eski repo stili: içeride SaveChanges()
        }

        public void Update(int id, PetUpdateDto dto)
        {
            var pet = _repo.GetById(id) ?? throw new Exception("Pet not found");

            if (dto.Name is not null) pet.Name = dto.Name;
            if (dto.Type.HasValue) pet.Type = dto.Type.Value;
            if (dto.Age.HasValue) pet.Age = dto.Age.Value;
            if (dto.Weight.HasValue) pet.Weight = dto.Weight.Value;
            if (dto.PetOwnerId.HasValue) pet.PetOwnerId = dto.PetOwnerId.Value;
            if (dto.VetId.HasValue) pet.VetId = dto.VetId.Value;

            _repo.Update(pet); // içeride SaveChanges()
        }

        // silme
        public void Delete(int id) => _repo.Delete(id);
        public void SoftDelete(int id) => _repo.SoftDelete(id);
        public void UndoDelete(int id) => _repo.UndoDelete(id);
    }
}
