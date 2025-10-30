using AutoMapper;
using FirstAPI.Interfaces;
using FirstAPI.Models;
using FirstAPI.Models.DTOs;
using System.Threading.Tasks;

namespace FirstAPI.Services
{
    public class ProductService : IProductService
    {
        private readonly IRepository<int, Product> _repository;
        private readonly IMapper _mapper;

        public ProductService(IRepository<int,Product> repository,IMapper mapper) 
        {
            _repository = repository;
            _mapper = mapper;
        }
        public async Task<AddProductResponse> AddProduct(AddProductRequest request)
        {
            if(request == null)
            {
                throw new ArgumentNullException(nameof(request));
            }
            if(request.Stock<=0)
                throw new Exception("Stock cannot be less than or equal to zero.");
            var product = new Product() { Price=request.Price, Title=request.Title, Stock=request.Stock, IsDiscontinued=false };
            //product.Id = await  GenerateId();
            //var category = 
            var addedProduct =  await _repository.Add(product);
            return new AddProductResponse() { Id= product.Id};
        }

        private async Task<int> GenerateId()
        {
            var products = await _repository.GetAll();
            if (products == null || !products.Any())
            {
                return 1; // Start IDs from 1 if there are no products
            }
            return products.Max(p => p.Id) + 1;

        }

        public async Task<IEnumerable<ProductListResponse>> GetAllProducts()
        {
            var products = await _repository.GetAll();
            var result = products.Where(p => !p.IsDiscontinued && p.Stock>0)
                                 .Select(p => _mapper.Map<ProductListResponse>(p));
            if(!result.Any())
            {
                throw new Exception("No available products found.");
            }
            return result;

        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <param name="discontinue">true for discontining false for making available</param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public async Task<bool> ChangeStatus(int id, bool discontinue)
        {
            var product = await _repository.GetById(id);
            if (product == null)
            {
                throw new Exception($"Product with id {id} not found.");
            }
            if (product.IsDiscontinued == false && discontinue==false)
            {
                throw new Exception($"Product with id {id} is already available.");
            }
            if (product.IsDiscontinued == true && discontinue == true)
            {
                throw new Exception($"Product with id {id} is already discontiued.");
            }
            product.IsDiscontinued = discontinue;
            await _repository.Update(id, product);
            return true;
        }
    }
}
