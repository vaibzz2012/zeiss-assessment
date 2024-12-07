using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ZeissAssessment.Controllers
{
    [Route("api/products")]
    [ApiController]
    [Authorize]
    public class ProductsController : ControllerBase
    {
        private readonly IProductRepository _repository;
        private readonly ILogger<ProductsController> _logger;

        public ProductsController(IProductRepository repository, ILogger<ProductsController> logger)
        {
            _repository = repository;
            _logger = logger;
            Console.WriteLine("ProductsController initialized.");
        }

        [HttpPost]
        public IActionResult AddProduct([FromBody] Product product)
        {
            
            try
            {
                Console.WriteLine("POST /api/products called.");
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                var createdProduct = _repository.AddProduct(product);
                return CreatedAtAction(nameof(GetProductById), new { id = createdProduct.Id }, createdProduct);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error in AddProduct endpoint.");
                return StatusCode(500, "Internal server error.");
            }
        }

        [HttpGet]
        public IActionResult GetAllProducts()
        {
            
            try
            {
                Console.WriteLine("GET /api/products called.");
                return Ok(_repository.GetAllProducts());
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error in GetAllProducts endpoint.");
                return StatusCode(500, "Internal server error.");
            }
        }

        [HttpGet("{id}")]
        public IActionResult GetProductById(int id)
        {
           
            try
            {
                Console.WriteLine($"GET /api/products/{id} called.");
                
                var product = _repository.GetProductById(id);
                return product == null ? NotFound() : Ok(product);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error in GetProductById endpoint.");
                return StatusCode(500, "Internal server error.");
            }
        }

        [HttpPut("{id}")]
        public IActionResult UpdateProduct(int id, [FromBody] Product updatedProduct)
        {
            
            try
            {
                Console.WriteLine($"PUT /api/products/{id} called.");
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                var product = _repository.UpdateProduct(id, updatedProduct);
                return product == null ? NotFound() : Ok(product);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error in UpdateProduct endpoint.");
                return StatusCode(500, "Internal server error.");
            }
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteProduct(int id)
        {
            
            try
            {
                Console.WriteLine($"DELETE /api/products/{id} called.");
                var product = _repository.GetProductById(id);
                if (product == null)
                {
                    return NotFound();
                }

                _repository.DeleteProduct(id);
                return NoContent();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error in DeleteProduct endpoint.");
                return StatusCode(500, "Internal server error.");
            }
        }

        [HttpPut("decrement-stock/{id}/{quantity}")]
        public IActionResult DecrementStock(int id, int quantity)
        {
            
            try
            {
                Console.WriteLine($"PUT /api/products/decrement-stock/{id}/{quantity} called.");
                if (_repository.UpdateStock(id, quantity, true, out string errorMessage))
                {
                    return Ok("Stock decremented successfully.");
                }

                return BadRequest(errorMessage);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error in DecrementStock endpoint.");
                return StatusCode(500, "Internal server error.");
            }
        }

        [HttpPut("add-to-stock/{id}/{quantity}")]
        public IActionResult AddToStock(int id, int quantity)
        {
           
            try
            {
                Console.WriteLine($"PUT /api/products/add-to-stock/{id}/{quantity} called.");
                if (_repository.UpdateStock(id, quantity, false, out var errorMessage))
                {
                    return Ok("Stock incremented successfully.");
                }

                return BadRequest(errorMessage);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error in AddToStock endpoint.");
                return StatusCode(500, "Internal server error.");
            }
        }
    }
}
