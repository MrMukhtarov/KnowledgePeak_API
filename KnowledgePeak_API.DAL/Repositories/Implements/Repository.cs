using KnowledgePeak_API.Core.Entities.Commons;
using KnowledgePeak_API.DAL.Contexts;
using KnowledgePeak_API.DAL.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace KnowledgePeak_API.DAL.Repositories.Implements;

public class Repository<T> : IRepository<T> where T : BaseEntity, new()
{
    readonly AppDbContext _context;

    public Repository(AppDbContext context)
    {
        _context = context;
    }

    public DbSet<T> Table { get => _context.Set<T>(); }

    public async Task CreateAsync(T entity)
    {
        await Table.AddAsync(entity);
    }

    public void Delete(T entity)
    {
        Table.Remove(entity);
    }

    public async Task DeleteAsync(int id)
    {
        var entity = await FIndByIdAsync(id);
        Table.Remove(entity);
    }

    public IQueryable<T> FindAll(Expression<Func<T, bool>> expression, params string[] includes)
    {
        return _getIncludes(Table.AsQueryable(), includes).Where(expression);
    }

    public async Task<T> FIndByIdAsync(int id, params string[] includes)
    {
        if (includes.Length == 0)
        {
            return await Table.FindAsync(id);
        }
        return await _getIncludes(Table.AsQueryable(), includes).SingleOrDefaultAsync(i => i.Id == id);
    }

    public IQueryable<T> GetAll(params string[] includes)
    {
        return _getIncludes(Table.AsQueryable(), includes);
    }

    public Task<T> GetSingleAsync(Expression<Func<T, bool>> expression, params string[] includes)
    {
        return _getIncludes(Table.AsQueryable(), includes).SingleOrDefaultAsync(expression);
    }

    public async Task<bool> IsExistAsync(Expression<Func<T, bool>> expression)
    {
        return await Table.AnyAsync(expression);
    }

    public void RevertSoftDelete(T entity)
    {
        entity.IsDeleted = false;
    }

    public async Task SaveAsync()
    {
        await _context.SaveChangesAsync();
    }

    public void SoftDelete(T entity)
    {
        entity.IsDeleted = true;
    }
    IQueryable<T> _getIncludes(IQueryable<T> query, params string[] includes)
    {
        foreach (var item in includes)
        {
            query = query.Include(item);
        }
        return query;
    }
}
