using Microsoft.AspNetCore.Mvc;
using MinimalAPIwithDapper.Models;
using Dapper;
using System.Data;

namespace MinimalAPIwithDapper
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly IDbConnection _connection;

        public ProductsController(IDbConnection connection)
        {
            _connection = connection;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Product>>> GetProducts()
        {
            var products = await _connection.QueryAsync<Product>("SELECT * FROM Products");

            return Ok(products);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Product>> GetProduct(int id)
        {
            var product = await _connection.QueryFirstOrDefaultAsync<Product>("SELECT * FROM Products WHERE ProductID = @ProductId", new { ProductId = id });

            return Ok(product);
        }

        [HttpPost]
        public async Task<ActionResult<Product>> PostProduct(Product product)
        {
            string query = "INSERT INTO Products SELECT @product";

            await _connection.ExecuteAsync(query, product);

            return CreatedAtAction(nameof(GetProduct), new { id = product.ProductId }, product);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProduct(int id)
        {
            string query = "DELETE FROM Products WHERE ProductID = @ProductId";

            await _connection.ExecuteAsync(query, id);

            return CreatedAtAction(nameof(GetProduct), new { id = id });
        }
    }
}
