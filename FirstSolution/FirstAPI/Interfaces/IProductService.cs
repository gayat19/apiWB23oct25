using FirstAPI.Models.DTOs;

namespace FirstAPI.Interfaces
{
    public interface IProductService
    {
        public Task<AddProductResponse> AddProduct(AddProductRequest request);
        public Task<IEnumerable<ProductListResponse>> GetAllProducts();
    }
}
