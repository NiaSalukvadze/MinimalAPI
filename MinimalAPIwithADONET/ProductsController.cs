using Microsoft.AspNetCore.Mvc;
using MinimalAPIwithADONET.Models;
using System.Data.Common;

namespace MinimalAPIwithADONET
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly DbConnection _connection;

        public ProductsController(DbConnection connection)
        {
            _connection = connection;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Product>>> GetProducts()
        {
            using (var command = _connection.CreateCommand())
            {
                command.CommandText = "SELECT * FROM Products";
                await _connection.OpenAsync();
                var products = new List<Product>();
                using (var reader = await command.ExecuteReaderAsync())
                {
                    while (await reader.ReadAsync())
                    {
                        products.Add(new Product
                        {
                            ProductId = (int)reader["ProductId"],
                            ProductName = reader["ProductName"].ToString()!,
                            SupplierId = (int)reader["SupplierId"],
                            CategoryId = (int)reader["CategoryId"],
                            QuantityPerUnit = reader["QuantityPerUnit"].ToString(),
                            UnitPrice = (int)reader["ProductId"],
                            UnitsInStock = (short)reader["UnitsInStock"],
                            UnitsOnOrder = (short)reader["UnitsOnOrder"],
                            ReorderLevel = (short)reader["ReorderLevel"],
                            Discontinued = (bool)reader["Discontinued"]
                        });
                    }
                }
                return Ok(products);
            }
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Product>> GetProduct(int id)
        {
            using (var command = _connection.CreateCommand())
            {
                command.CommandText = "SELECT * FROM Products WHERE ProductID = @ProductId";
                var parameter = command.CreateParameter();
                parameter.ParameterName = "@ProductId";
                parameter.Value = id;
                command.Parameters.Add(parameter);

                await _connection.OpenAsync();

                using (var reader = await command.ExecuteReaderAsync())
                {
                    if (await reader.ReadAsync())
                    {
                        var product = new Product
                        {
                            ProductId = (int)reader["ProductId"],
                            ProductName = reader["ProductName"].ToString()!,
                            SupplierId = (int)reader["SupplierId"],
                            CategoryId = (int)reader["CategoryId"],
                            QuantityPerUnit = reader["QuantityPerUnit"].ToString(),
                            UnitPrice = (int)reader["ProductId"],
                            UnitsInStock = (short)reader["UnitsInStock"],
                            UnitsOnOrder = (short)reader["UnitsOnOrder"],
                            ReorderLevel = (short)reader["ReorderLevel"],
                            Discontinued = (bool)reader["Discontinued"]
                        };

                        return Ok(product);
                    }
                    else
                    {
                        return NotFound();
                    }
                }
            }
        }

        [HttpPost]
        public async Task<ActionResult<Product>> PostProduct(Product product)
        {
            using (var command = _connection.CreateCommand())
            {
                command.CommandText = "INSERT INTO Products (ProductName, SupplierId, CategoryId) VALUES (@Name, @SupplierId, @CategoryId)";

                var productId = command.CreateParameter();
                productId.ParameterName = "ProductName";
                productId.Value = product.ProductName;
                command.Parameters.Add(productId);

                var supplierId = command.CreateParameter();
                supplierId.ParameterName = "SupplierId";
                supplierId.Value = product.SupplierId;
                command.Parameters.Add(supplierId);

                var categoryId = command.CreateParameter();
                categoryId.ParameterName = "CategoryId";
                categoryId.Value = product.CategoryId;
                command.Parameters.Add(categoryId);

                await _connection.OpenAsync();
                await command.ExecuteNonQueryAsync();
                return CreatedAtAction(nameof(GetProduct), new { id = product.ProductId }, product);
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProduct(int id)
        {
            using (var command = _connection.CreateCommand())
            {
                command.CommandText = "DELETE FROM Products WHERE ProductID = @ProductId";
                var productId = command.CreateParameter();
                productId.ParameterName = "ProductId";
                productId.Value = id;
                command.Parameters.Add(productId);

                _connection.Open();
                await command.ExecuteNonQueryAsync();
                return NoContent();
            }
        }
    }
}