using GeekShopping.web.Models;
using GeekShopping.web.Services.IServices;
using Microsoft.AspNetCore.Mvc;

namespace GeekShopping.web.Controllers
{
    public class ProductController : Controller
    {
        private readonly IProductService _productService;

        public ProductController(IProductService productService)
        {
            _productService = productService ?? throw new ArgumentNullException(nameof(productService));
        }

        public async Task<IActionResult> ProductIndex()
        {
            var products = await _productService.GetAllProdut();
            return View(products);
        }

        /// <summary>
        /// Chamar a page para criar produto
        /// </summary>
        /// <returns></returns>
        public async Task<IActionResult> ProductCreate() => View();

        /// <summary>
        /// Persistir de fatos os produtos no banco
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> ProductCreate(ProductModel productModel)
        {
            if (ModelState.IsValid)
            {
                ProductModel respnse = await _productService.Create(productModel);
                if (respnse != null)
                {
                    return RedirectToAction(nameof(ProductIndex));
                }
            }

            return View(productModel);
        }

        /// <summary>
        /// Chamar a page para atualizar produto
        /// </summary>
        /// <returns></returns>
        public async Task<IActionResult> ProductUpdate(int id)
        {
            var product = await _productService.FindProductById(id);
            if (product != null) return View(product);
            return NotFound();
        }

        /// <summary>
        /// Atualizar produto selecionado
        /// </summary>
        /// <returns>Tela com todos os Produtos com produto que foi atualizado</returns>
        ///Observação para comunicação entre serviços usa-se post e get verbos como httpPut etc... não funcionam
        [HttpPost]
        public async Task<IActionResult> ProductUpdate(ProductModel productModel)
        {
            if (ModelState.IsValid)
            {
                ProductModel respnse = await _productService.Update(productModel);
                if (respnse != null)
                {
                    return RedirectToAction(nameof(ProductIndex));
                }
            }

            return View(productModel);
        }

        public async Task<IActionResult> ProductDelete(int id)
        {
            var product = await _productService.FindProductById(id);
            if (product != null) return View(product);
            return NotFound();
        }

        /// <summary>
        /// Deletar produto selecionado
        /// </summary>
        [HttpPost]
        public async Task<IActionResult> ProductDelete(ProductModel productModel)
        {
            bool respnse = await _productService.DeleteProductById(productModel.Id);
            if (respnse)
            {
                return RedirectToAction(nameof(ProductIndex));
            }


            return View(productModel);
        }
    }
}
