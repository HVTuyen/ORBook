namespace ORBook.Service
{
    public interface ICommonDataService<T> where T : class
    {
        Task<T?> GetByIdAsync(int id);
        Task<List<T>> GetAllAsync();
        Task<T> CreateAsync(T data);
        Task<T?> UpdateAsync(T data);
        Task<bool> DeleteAsync(int id);
    }
}
