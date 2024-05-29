using MediatR;
using System.Data;
using System.Data.SqlClient;

namespace MinimalAPIwithCQRS_EF_ADONET.CQRS.Commands.DeleteProductCommand
{
    public class DeleteProductCommandHandler : IRequestHandler<DeleteProductCommand>
    {
        private readonly string _connectionString;
        public DeleteProductCommandHandler(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("DefaultConnection");
        }
        public async Task Handle(DeleteProductCommand request, CancellationToken cancellationToken)
        {
            var productId = request.productId;

            const string sql = @"DELETE FROM Products WHERE ProductId = @productId";

            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync(cancellationToken);

                using (var command = new SqlCommand(sql, connection))
                {
                    command.Parameters.Add(new SqlParameter("@productId", SqlDbType.Int) { Value = productId });

                    await command.ExecuteNonQueryAsync(cancellationToken);
                }
            }
        }
    }
}
