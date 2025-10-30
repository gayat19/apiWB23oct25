using FirstAPI.Contexts;
using FirstAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace FirstAPI.Repositories
{
    public class CategoryRepository : Repository<int, Category>
    {
        public CategoryRepository(ShoppingContext28Oct25 context): base(context)
        {
            
        }
        public override async Task<IEnumerable<Category>> GetAll()
        {
           return await _context.Categories.ToListAsync();
        }

        public override async Task<Category> GetById(int id)
        {
            return await _context.Categories.SingleOrDefaultAsync(c => c.Id == id);
        }
    }
}
