namespace PV221Chat.Core.Interfaces;

public interface IDataRepository<T>
{
    Task<IEnumerable<T>> GetListDataAsync();

    Task<T?> GetDataAsync(int id);

    Task<bool> DeleteDataAsync(int id);

    Task<bool> AddDataAsync(T data);

    Task<bool> UpdateDataAsync(int? id, T data);
}
