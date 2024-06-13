using Microsoft.AspNetCore.Mvc;
using MinimalAPIwithADONET.Models;
using System.Data.Common;
using System.Data.SqlClient;

namespace MinimalAPIwithADONET
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

        [HttpPost("create")]
        public async Task<ActionResult> PostProduct(Product prod)
        {
            using (var command = _connection.CreateCommand())
            {
                command.CommandText = "INSERT INTO Products (ProductName, SupplierId, CategoryId) VALUES (@ProductName, @SupplierId, @CategoryId)";

                var productNameParam = command.CreateParameter();
                productNameParam.ParameterName = "ProductName";
                productNameParam.Value = prod.ProductName;
                command.Parameters.Add(productNameParam);

                var supplierIdParam = command.CreateParameter();
                supplierIdParam.ParameterName = "SupplierId";
                supplierIdParam.Value = prod.SupplierId;
                command.Parameters.Add(supplierIdParam);

                var categoryIdParam = command.CreateParameter();
                categoryIdParam.ParameterName = "CategoryId";
                categoryIdParam.Value = prod.CategoryId;
                command.Parameters.Add(categoryIdParam);

                await _connection.OpenAsync();
                await command.ExecuteNonQueryAsync();
                return Ok();
            }
        }

        [HttpPost("update")]
        public async Task<ActionResult> UpdateProduct(Product prod)
        {
            using (var command = _connection.CreateCommand())
            {
                const string sql = @"update Products 
                                 set ProductName = @productName,
                                 SupplierId = @supplierId, 
                                 CategoryId = @categoryId
                                 where ProductId = @productId";

                var productNameParam = command.CreateParameter();
                productNameParam.ParameterName = "ProductName";
                productNameParam.Value = prod.ProductName;
                command.Parameters.Add(productNameParam);

                var supplierIdParam = command.CreateParameter();
                supplierIdParam.ParameterName = "SupplierId";
                supplierIdParam.Value = prod.SupplierId;
                command.Parameters.Add(supplierIdParam);

                var categoryIdParam = command.CreateParameter();
                categoryIdParam.ParameterName = "CategoryId";
                categoryIdParam.Value = prod.CategoryId;
                command.Parameters.Add(categoryIdParam);

                await _connection.OpenAsync();
                await command.ExecuteNonQueryAsync();
            }

            return Ok();
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteProduct()
        {
            using (var command = _connection.CreateCommand())
            {
                command.CommandText = "DELETE FROM Products WHERE ProductID = (SELECT TOP 1 ProductID FROM Products ORDER BY ProductID DESC)";

                _connection.Open();
                await command.ExecuteNonQueryAsync();
                return NoContent();
            }
        }
    }
}