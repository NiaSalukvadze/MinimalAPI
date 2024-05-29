using MediatR;
using MinimalAPIwithCQRS_ADONET_Dapper.Models;
using System.Data.SqlClient;

namespace MinimalAPIwithCQRS_ADONET_Dapper.CQRS.Queries.GetProducts
{
    public class GetProductsQueryHandler : IRequestHandler<GetProductsQuery, List<Product>>
    {
        private readonly string _connectionString;

        public GetProductsQueryHandler(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("DefaultConnection");
        }
        public async Task<List<Product>> Handle(GetProductsQuery request, CancellationToken cancellationToken)
        {
            const string sql = @"SELECT * FROM Products";

            var products = new List<Product>();

            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync(cancellationToken);

                using (var command = new SqlCommand(sql, connection))
                {
                    using (var reader = await command.ExecuteReaderAsync(cancellationToken))
                    {
                        while (await reader.ReadAsync(cancellationToken))
                        {
                            var product = new Product
                            {
                                ProductId = reader.GetInt32(reader.GetOrdinal("ProductId")),
                                ProductName = reader.IsDBNull(reader.GetOrdinal("ProductName")) ? null : reader.GetString(reader.GetOrdinal("ProductName")),
                                SupplierId = reader.IsDBNull(reader.GetOrdinal("SupplierId")) ? (int?)null : reader.GetInt32(reader.GetOrdinal("SupplierId")),
                                CategoryId = reader.IsDBNull(reader.GetOrdinal("CategoryId")) ? (int?)null : reader.GetInt32(reader.GetOrdinal("CategoryId")),
                                QuantityPerUnit = reader.IsDBNull(reader.GetOrdinal("QuantityPerUnit")) ? null : reader.GetString(reader.GetOrdinal("QuantityPerUnit")),
                                UnitPrice = reader.IsDBNull(reader.GetOrdinal("UnitPrice")) ? (decimal?)null : reader.GetDecimal(reader.GetOrdinal("UnitPrice")),
                                UnitsInStock = reader.IsDBNull(reader.GetOrdinal("UnitsInStock")) ? (short?)null : reader.GetInt16(reader.GetOrdinal("UnitsInStock")),
                                UnitsOnOrder = reader.IsDBNull(reader.GetOrdinal("UnitsOnOrder")) ? (short?)null : reader.GetInt16(reader.GetOrdinal("UnitsOnOrder")),
                                ReorderLevel = reader.IsDBNull(reader.GetOrdinal("ReorderLevel")) ? (short?)null : reader.GetInt16(reader.GetOrdinal("ReorderLevel")),
                                Discontinued = reader.GetBoolean(reader.GetOrdinal("Discontinued"))
                            };

                            products.Add(product);
                        }
                    }
                }
            }

            return products;
        }
    }
}
