using GeekShopping.web.Models;
using GeekShopping.web.Services.IServices;
using GeekShopping.web.Ultils;

namespace GeekShopping.web.Services
{   
    /// <summary>
    /// Classe de configuração para requisitar serviços
    /// </summary>
    public class ProductService : IProductService
    {   
        private readonly HttpClient _client;
        /// <summary>
        /// Path para onde será feita a requisição
        /// </summary>
        public const string BasePath = "api/v1/Product";

        public ProductService(HttpClient client)
        {
            _client = client ?? throw new ArgumentNullException(nameof(client));
        }

        public async Task<ProductModel> FindProductById(long id)
        {
            HttpResponseMessage httpResponseMessage = await _client.GetAsync($"{BasePath}/{id}");
            return await httpResponseMessage.ReadContentAs<ProductModel>();
        }

        public async Task<IEnumerable<ProductModel>> GetAllProdut()
        {
            HttpResponseMessage httpResponseMessage = await _client.GetAsync(BasePath);
            return await httpResponseMessage.ReadContentAs<List<ProductModel>>();
        }

        public async Task<ProductModel> Create(ProductModel productModel)
        {
            HttpResponseMessage httpResponseMessage = await _client.PostAsJson(BasePath, productModel);
            if(httpResponseMessage.IsSuccessStatusCode) return await httpResponseMessage.ReadContentAs<ProductModel>();
            else throw new Exception("Something went wrong when calling API");
        }

        public async Task<ProductModel> Update(ProductModel productModel)
        {
            HttpResponseMessage httpResponseMessage = await _client.PutAsJson(BasePath, productModel);
            if (httpResponseMessage.IsSuccessStatusCode) return await httpResponseMessage.ReadContentAs<ProductModel>();
            else throw new Exception("Something went wrong when calling API");
        }

        public async Task<bool> DeleteProductById(long id)
        {
            HttpResponseMessage httpResponseMessage = await _client.DeleteAsync($"{BasePath}/{id}");
            if (httpResponseMessage.IsSuccessStatusCode) return await httpResponseMessage.ReadContentAs<bool>();
            else throw new Exception("Something went wrong when calling API");
        }
    }
}
