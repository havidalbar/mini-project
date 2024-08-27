using Microsoft.EntityFrameworkCore;
using Persistence.DatabaseContext;

namespace Persistence.Repositories;

public class GenericRepository<T> : IGenericRepository<T> where T : class
{
    private readonly TableContext _context;

    public GenericRepository(TableContext context)
    {
        _context = context;
    }

    public List<T> GetAll()
    {
        return _context.Set<T>().ToList();
    }

    public T? GetById(Guid id)
    {
        return _context.Set<T>().Find(id);
    }

    public void Remove(T itemToRemove)
    {
        _context.Set<T>().Remove(itemToRemove);
    }

    public async Task<T> Create(T model)
    {
        await _context.Set<T>().AddAsync(model);
        await _context.SaveChangesAsync();
        return model;
    }

    public async Task Update(T model)
    {
        _context.Set<T>().Update(model);
        await _context.SaveChangesAsync();
    }

    public async Task Delete(T model)
    {
        _context.Set<T>().Remove(model);
        await _context.SaveChangesAsync();
    }

    public async Task<List<T>> CreateBatch(List<T> models)
    {
        await _context.Set<T>().AddRangeAsync(models);
        await _context.SaveChangesAsync();
        return models;
    }

    public async Task<List<T>> DeleteBatch(List<T> models)
    {
        _context.Set<T>().RemoveRange(models);
        await _context.SaveChangesAsync();
        return models;
    }

    public async Task<List<T>> GetPaged(int pageNumber, int pageSize)
    {
        return await _context.Set<T>()
            .Skip((pageNumber - 1) * pageSize)
            .Take(pageSize)
            .ToListAsync();
    }

}