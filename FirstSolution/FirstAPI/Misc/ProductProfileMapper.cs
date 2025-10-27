using AutoMapper;

namespace FirstAPI.Misc
{
    public class ProductProfileMapper :Profile
    {
        public ProductProfileMapper()
        {
            CreateMap<Models.Product, Models.DTOs.ProductListResponse>();

        }
    }
}
