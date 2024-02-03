using VisualSoftAspCoreApi.Entities;
using VisualSoftAspCoreApi.Dto;

namespace VisualSoftAspCoreApi.Contracts
{
    public interface  IProductRepository
    {
        public Task<IEnumerable<Product>> GetProducts();
        public Task <Product> GetProduct(int id);
        public Task <Product> CreateProduct(ProductCreationDto product);
        public Task UpdateProduct(int id, ProductUpdateDto product);
        public Task DeleteProduct(int id);
    }
}