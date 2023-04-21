using GeekShopping.ProductAPI.Data.ValueObjects;
using GeekShopping.ProductAPI.Model;
using GeekShopping.ProductAPI.Repository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace GeekShopping.ProductAPI.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private IProductRepository _repository;

        public ProductController(IProductRepository repository)
        {
            _repository = repository;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ProductVO>> Get(long id)
        {
            var productVO = await _repository.FindById(id);
            if (productVO == null) return NotFound();
            return Ok(productVO);
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<ProductVO>>> GetAll()
        {
            IEnumerable<Data.ValueObjects.ProductVO> productVO = await _repository.FindAll();
            return Ok(productVO);
        }

        [HttpPost]
        public async Task<ActionResult<ProductVO>> Create(ProductVO productVO)
        {
            if (productVO == null)
            {
                return BadRequest();
            }
            ProductVO productVOResponse = await _repository.Create(productVO);
            return Ok(productVOResponse);
        }

        [HttpPut]
        public async Task<ActionResult<IEnumerable<ProductVO>>> Update(ProductVO productVO)
        {
            bool productExist = await _repository.ProductExists(productVO.Id);
            if (!productExist)
            {
                return NotFound();
            }
            ProductVO productVOResponse = await _repository.Update(productVO);
            return Ok(productVOResponse);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(long id)
        {
            bool isDeleted = await _repository.Delete(id);
            if(!isDeleted) return NotFound();
            return Ok(isDeleted);
        }
    }
}
