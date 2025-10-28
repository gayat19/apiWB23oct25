using FirstAPI.Contexts;
using FirstAPI.Interfaces;
using FirstAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace FirstAPI.Repositories
{
    public class ProductRepositoryDB : IRepository<int, Product>
    {
        private readonly ShoppingContext28Oct25 _context;

        public ProductRepositoryDB(ShoppingContext28Oct25 context) {
            _context = context;
        }
        public async Task<Product> Add(Product item)
        {
           _context.Products.Add(item);
            await _context.SaveChangesAsync();
            return item;
        }

        public async Task<Product> Delete(int id)
        {
            var product = await GetById(id);
            if(product == null)
            {
                return null;
            }
            _context.Products.Remove(product);
            await _context.SaveChangesAsync();
            return product;
        }

        public async Task<IEnumerable<Product>> GetAll()
        {
            var products = await _context.Products.ToListAsync();
            if(products == null || products.Count == 0)
            {
                throw new Exception("No products found in the database.");
            }
            return products;

        }

        public async Task<Product> GetById(int id)
        {
            var product =  await _context.Products.SingleOrDefaultAsync(p => p.Id == id);
            if(product == null) 
                throw new Exception($"Product with id {id} not found.");
            return product;
        }

        public async Task<Product> Update(int id, Product item)
        {
            var existingProduct = await GetById(id);
            if (existingProduct == null)
            {
                throw new Exception($"Product with id {id} not found.");
            }
            _context.Entry(existingProduct).CurrentValues.SetValues(item);
            await _context.SaveChangesAsync();
            return existingProduct;
        }
    }
}
