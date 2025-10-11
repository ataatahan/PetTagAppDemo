using Microsoft.EntityFrameworkCore.Query;
using PetTag.Core.BaseEntities;
using PetTag.Core.Enums;
using System.Linq.Expressions;

namespace PetTag.Repo.Interfaces
{
    public interface IGenericRepo<T> where T : BaseEntity
    {
        ICollection<T> GetAll(bool isTrack = true, EntityStatus status = EntityStatus.Active);
        T? GetById(int id, EntityStatus status = EntityStatus.Active);

        ICollection<T> Find(Expression<Func<T, bool>> predicate, bool isTrack = true, EntityStatus status = EntityStatus.Active);
        T? FirstOrDefault(Expression<Func<T, bool>> predicate, bool isTrack = true, EntityStatus status = EntityStatus.Active);

        ICollection<TResult> GetFilteredList<TResult>(
            Expression<Func<T, TResult>> select,
            Expression<Func<T, bool>> where = null,
            Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null,
            Func<IQueryable<T>, IIncludableQueryable<T, object>> include = null);

        TResult GetFilteredFirstOrDefault<TResult>(
            Expression<Func<T, TResult>> select,
            Expression<Func<T, bool>> where = null,
            Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null,
            Func<IQueryable<T>, IIncludableQueryable<T, object>> include = null);

        bool Add(T entity);
        bool Update(T entity);
        bool Delete(int id);
        bool SoftDelete(int id);
        bool UndoDelete(int id);
    }
}
