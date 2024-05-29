using MediatR;
using System.Data.SqlClient;
using System.Data;

namespace MinimalAPIwithCQRS_Dapper_ADONET.CQRS.Commands.UpdateProductCommand
{
    public class UpdateProductCommandHandler : IRequestHandler<UpdateProductCommand>
    {
        private readonly string _connectionString;
        public UpdateProductCommandHandler(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("DefaultConnection");
        }
        public async Task Handle(UpdateProductCommand request, CancellationToken cancellationToken)
        {
            var product = request.Product;

            const string sql = @"update Products 
                                 set ProductName = @productName,
                                 SupplierId = @supplierId, 
                                 CategoryId = @categoryId
                                 where ProductId = @productId";

            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync(cancellationToken);

                using (var command = new SqlCommand(sql, connection))
                {
                    command.Parameters.Add(new SqlParameter("@productId", SqlDbType.Int) { Value = product.ProductId });
                    command.Parameters.Add(new SqlParameter("@productName", SqlDbType.NVarChar) { Value = product.ProductName });
                    command.Parameters.Add(new SqlParameter("@supplierId", SqlDbType.Int) { Value = product.SupplierId });
                    command.Parameters.Add(new SqlParameter("@categoryId", SqlDbType.Int) { Value = product.CategoryId });

                    await command.ExecuteNonQueryAsync(cancellationToken);
                }
            }
        }
    }
}
