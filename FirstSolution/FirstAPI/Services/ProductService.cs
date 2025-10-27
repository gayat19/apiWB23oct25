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
            product.Id = await  GenerateId();
            var addedProduct =  _repository.Add(product);
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
    }
}
