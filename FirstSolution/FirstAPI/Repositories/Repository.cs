using FirstAPI.Contexts;
using FirstAPI.Interfaces;

namespace FirstAPI.Repositories
{
    public abstract class Repository<K, T> : IRepository<K, T>
    {
        protected readonly ShoppingContext28Oct25 _context;

        protected Repository(ShoppingContext28Oct25 context) {
            _context = context;
        }
        public async Task<T> Add(T item)
        {
            _context.Add(item);
             await _context.SaveChangesAsync();
            return item;
        }

        public async Task<T> Delete(K id)
        {
            var item = await GetById(id);
            _context.Remove(item);
            await _context.SaveChangesAsync();
            return item;
        }

        public abstract Task<IEnumerable<T>> GetAll();

        public abstract Task<T> GetById(K id);
        
        public async Task<T> Update(K id, T item)
        {
            var existingProduct = await GetById(id);
            _context.Entry(existingProduct).CurrentValues.SetValues(item);
            await _context.SaveChangesAsync();
            return item;
        }
    }
}
