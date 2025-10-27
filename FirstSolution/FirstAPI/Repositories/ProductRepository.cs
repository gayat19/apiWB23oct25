using FirstAPI.Exceptions;
using FirstAPI.Interfaces;
using FirstAPI.Models;

namespace FirstAPI.Repositories
{
    public class ProductRepository : IRepository<int, Product>
    {
        static List<Product> products = new List<Product>()
        {
            new Product(){ Id=1, Title="Laptop", Price=80000,   IsDiscontinued=false,Stock=10 },
            new Product(){ Id=2, Title="Mobile", Price=50000,   IsDiscontinued=true,Stock=0 },
            new Product(){ Id=3, Title="Tablet", Price=30000,   IsDiscontinued=false,Stock=0 },
        };
        public async Task<Product> Add(Product item)
        {
           var product = products.SingleOrDefault(p => p.Id == item.Id);
            if (product != null)
            {
                throw new DuplicateIdentifierException($"Product with Id {item.Id} already exists.");
            }
            products.Add(item);
            return item;
        }

        public async Task<Product> Delete(int id)
        {
            var product = await GetById(id);
            if (product != null)
            {
                products.Remove(product);
                return product;
            }
            throw new InvalidIdentifierException($"Unable to find product with Id {id}");
        }

        public async Task<IEnumerable<Product>> GetAll()
        {
            if(products.Count == 0)
            {
                throw new CollectionEmptyException("There are no products available.");
            }
            return products;
        }

        public async Task<Product> GetById(int id)
        {
            if (products.Count == 0)
            {
                throw new CollectionEmptyException("There are no products available.");
            }
            var product = products.SingleOrDefault(p => p.Id == id);//LINQ
            if (product==null)
            {
                throw new InvalidIdentifierException($"Unable to find product with Id {id}");
            }
            return product;
        }

        public async Task<Product> Update(int id, Product item)
        {
            var product = await GetById(id);
            if (product != null)
            {
                product.Title = item.Title;
                product.Price = item.Price;
                return product;
            }
            throw new InvalidIdentifierException($"Unable to find product with Id {id}");
        }
    }
}
