using Microsoft.AspNetCore.Mvc;
using MinimalAPIwithDapper.Models;
using Dapper;
using System.Data.SqlClient;

namespace MinimalAPIwithDapper
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly SqlConnection _connection;

        public ProductsController(SqlConnection connection)
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

        [HttpPost("create")]
        public async Task<ActionResult<Product>> PostProduct(Product prod)
        {
            string query = "INSERT INTO Products (ProductName, SupplierId, CategoryId) VALUES (@ProductName, @SupplierId, @CategoryId)";

            var product = new Product
            {
                ProductName = prod.ProductName,
                SupplierId = prod.SupplierId,
                CategoryId = prod.CategoryId
            };

            await _connection.ExecuteAsync(query, product);

            return CreatedAtAction(nameof(GetProduct), new { id = product.ProductId }, product);
        }

        [HttpPost("update")]
        public async Task<ActionResult<Product>> UpdateProduct(Product prod)
        {
            const string sql = @"update Products 
                                 set ProductName = @productName,
                                 SupplierId = @supplierId, 
                                 CategoryId = @categoryId
                                 where ProductId = @productId";

            await _connection.ExecuteAsync(sql, new
            {
                productId = prod.ProductId,
                productName = prod.ProductName,
                supplierId = prod.SupplierId,
                categoryId = prod.CategoryId
            });

            return CreatedAtAction(nameof(GetProduct), new { id = prod.ProductId }, prod);
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteProduct()
        {
            string query = "DELETE FROM Products WHERE ProductID = (SELECT TOP 1 ProductID FROM Products ORDER BY ProductID DESC)";

            await _connection.ExecuteAsync(query);

            return Ok();
        }
    }
}
