using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;
using PetTag.Core.BaseEntities;
using PetTag.Core.Enums;
using PetTag.Repo.Contexts;
using PetTag.Repo.Interfaces;
using System;
using System.Linq.Expressions;

namespace PetTag.Repo.Concretes
{
    public class GenericRepo<T> : IGenericRepo<T> where T : BaseEntity
    {
        protected readonly PetTagAppDbContext _context;
        protected readonly DbSet<T> _dbSet;

        public GenericRepo(PetTagAppDbContext context)
        {
            _context = context;
            _dbSet = _context.Set<T>();
        }

        public bool Add(T entity)
        {
            _dbSet.Add(entity);
            return _context.SaveChanges() > 0;
        }

        public bool Update(T entity)
        {
            _dbSet.Update(entity);
            return _context.SaveChanges() > 0;
        }

        public bool Delete(int id)
        {
            var entity = GetById(id, EntityStatus.Active);
            _dbSet.Remove(entity);
            return _context.SaveChanges() > 0;
        }

        public bool SoftDelete(int id)
        {
            var entity = GetById(id, EntityStatus.Active);
            entity.EntityAsPassive();
            return _context.SaveChanges() > 0;
        }

        public bool UndoDelete(int id)
        {
            var entity = GetById(id, EntityStatus.Pasive);
            entity.EntityAsActive();
            return _context.SaveChanges() > 0;
        }

        public ICollection<T> GetAll(bool isTrack = true, EntityStatus status = EntityStatus.Active)
        {
            IQueryable<T> query = _dbSet;

            if (status == EntityStatus.Active)
                query = query.Where(x => x.Status == EntityStatus.Active);
            else if (status == EntityStatus.Pasive)
                query = query.Where(x => x.Status == EntityStatus.Pasive);

            if (!isTrack)
                query = query.AsNoTracking();

            return query.ToList();
        }

        public T? GetById(int id, EntityStatus status = EntityStatus.Active)
        {
            return _dbSet.FirstOrDefault(x => x.Id == id && x.Status == status);
        }

        public ICollection<T> Find(Expression<Func<T, bool>> predicate, bool isTrack = true, EntityStatus status = EntityStatus.Active)
        {
            IQueryable<T> query = _dbSet;

            if (status == EntityStatus.Active)
                query = query.Where(x => x.Status == EntityStatus.Active);
            else if (status == EntityStatus.Pasive)
                query = query.Where(x => x.Status == EntityStatus.Pasive);

            if (!isTrack)
                query = query.AsNoTracking();

            return query.Where(predicate).ToList();
        }

        public T? FirstOrDefault(Expression<Func<T, bool>> predicate, bool isTrack = true, EntityStatus status = EntityStatus.Active)
        {
            IQueryable<T> query = _dbSet;

            if (status == EntityStatus.Active)
                query = query.Where(x => x.Status == EntityStatus.Active);
            else if (status == EntityStatus.Pasive)
                query = query.Where(x => x.Status == EntityStatus.Pasive);

            if (!isTrack)
                query = query.AsNoTracking();

            return query.FirstOrDefault(predicate);
        }

        public ICollection<TResult> GetFilteredList<TResult>(
            Expression<Func<T, TResult>> select,
            Expression<Func<T, bool>> where = null,
            Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null,
            Func<IQueryable<T>, IIncludableQueryable<T, object>> include = null)
        {
            IQueryable<T> query = _dbSet;

            if (include != null)
                query = include(query);

            if (where != null)
                query = query.Where(where);

            if (orderBy != null)
                return orderBy(query).Select(select).ToList();
            else
                return query.Select(select).ToList();
        }

        public TResult GetFilteredFirstOrDefault<TResult>(
            Expression<Func<T, TResult>> select,
            Expression<Func<T, bool>> where = null,
            Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null,
            Func<IQueryable<T>, IIncludableQueryable<T, object>> include = null)
        {
            IQueryable<T> query = _dbSet;

            if (include != null)
                query = include(query);

            if (where != null)
                query = query.Where(where);

            if (orderBy != null)
                return orderBy(query).Select(select).FirstOrDefault();
            else
                return query.Select(select).FirstOrDefault();
        }
    }
}
