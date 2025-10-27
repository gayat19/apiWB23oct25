namespace FirstAPI.Interfaces
{
    public interface IRepository<K,T>
    {
        Task<IEnumerable<T>> GetAll();
        Task<T> GetById(K id);
        Task<T> Add(T item);
        Task<T> Update(K id, T item);
        Task<T> Delete(K id);
    }
}
