namespace Persistence.Repositories;

public interface IGenericRepository<T> where T : class
{
    List<T> GetAll();
    T? GetById(Guid id);
    void Remove(T itemToRemove);
    Task<T> Create(T model);
    Task Update(T model);
    Task Delete(T model);
    Task<List<T>> CreateBatch(List<T> model);
    Task<List<T>> DeleteBatch(List<T> model);
    Task<List<T>> GetPaged(int pageNumber, int pageSize);
}