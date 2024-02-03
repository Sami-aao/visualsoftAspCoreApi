using VisualSoftAspCoreApi.Contracts;
using VisualSoftAspCoreApi.Repository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;


namespace VisualSoftAspCoreApi.Controllers
{

    [ApiController]
    [Route("api/products")]

    public class ProductsController: ControllerBase
    {
        private readonly IProductRepository _productRepo;
        public ProductsController(IProductRepository productRepo)=> 
        _productRepo = productRepo;

        [HttpGet]
        public async Task<IActionResult> GetProducts()
        {
            var products = await _productRepo.GetProducts();
            return Ok(products);
        }    

        [HttpGet({id}, Name = "ProductById")]
        public async Task<IActionResult> GetProduct(int id)
        {
            var product = await _productRepo.GetProduct(id);
            if(product is null)
                return NotFound();

            return Ok(product);
        }   


        [HttpPost]
        public async Task<IActionResult> CreateProduct([FromBody]ProductCreationDto product)
        {
            var createProduct = await _productRepo.CreateProduct(product);
            return CreatedAtRoute("ProductById", new { id = createProduct.Id }, createProduct);
        }


          [HttpPut("{id}")]
        public async Task<IActionResult> UpdateProduct([FromBody]ProductUpdateDto product)
        {
            var product = await _productRepo.GetProduct(id);
            if(product is null)
            return NotFound();

            await _productRepo.UpdateProduct(id,product);            
            return NoContent
        }


         [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProduct(int id)
        {
            var product = await _productRepo.GetProduct(id);
            if(product is null)
            return NotFound();

            await _productRepo.DeleteProduct(id);            
            return NoContent
        }


    }



}