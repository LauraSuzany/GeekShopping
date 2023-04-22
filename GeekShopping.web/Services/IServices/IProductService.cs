using GeekShopping.web.Models;

namespace GeekShopping.web.Services.IServices
{
    public interface IProductService
    {
        Task<IEnumerable<ProductModel>> GetAllProdut();
        Task<ProductModel> FindProductById(long Id);
        Task<ProductModel> Create(ProductModel productModel);
        Task<ProductModel> Update(ProductModel productModel);
        Task<bool> DeleteProductById(long id);
    }
}
