using KnowledgePeak_API.Core.Entities.Commons;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace KnowledgePeak_API.DAL.Repositories.Interfaces;

public interface IRepository<T> where T : BaseEntity, new()
{
    DbSet<T> Table { get; }
    IQueryable<T> GetAll(params string[] includes);
    IQueryable<T> FindAll(Expression<Func<T, bool>> expression, params string[] includes);
    Task<T> GetSingleAsync(Expression<Func<T, bool>> expression, params string[] includes);
    Task<T> FIndByIdAsync(int id, params string[] includes);
    Task<bool> IsExistAsync(Expression<Func<T, bool>> expression);
    Task CreateAsync(T entity);
    void Delete(T entity);
    Task DeleteAsync(int id);
    void SoftDelete(T entity);
    void RevertSoftDelete(T entity);
    Task SaveAsync();
}
