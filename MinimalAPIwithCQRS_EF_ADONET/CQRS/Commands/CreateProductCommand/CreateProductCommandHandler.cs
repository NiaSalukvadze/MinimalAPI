using MediatR;
using System.Data;
using System.Data.SqlClient;

namespace MinimalAPIwithCQRS_EF_ADONET.CQRS.Commands.CreateProductCommand
{
    public class CreateProductCommandHandler : IRequestHandler<CreateProductCommand>
    {
        private readonly string _connectionString;
        public CreateProductCommandHandler(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("DefaultConnection");
        }
        public async Task Handle(CreateProductCommand request, CancellationToken cancellationToken)
        {
            var product = request.Product;

            const string sql = @"INSERT INTO Products (ProductName, SupplierId, CategoryId) 
                             VALUES (@productName, @supplierId, @categoryId)";

            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync(cancellationToken);

                using (var command = new SqlCommand(sql, connection))
                {
                    command.Parameters.Add(new SqlParameter("@productName", SqlDbType.NVarChar) { Value = product.ProductName });
                    command.Parameters.Add(new SqlParameter("@supplierId", SqlDbType.Int) { Value = product.SupplierId });
                    command.Parameters.Add(new SqlParameter("@categoryId", SqlDbType.Int) { Value = product.CategoryId });

                    await command.ExecuteNonQueryAsync(cancellationToken);
                }
            }

        }
    }
}