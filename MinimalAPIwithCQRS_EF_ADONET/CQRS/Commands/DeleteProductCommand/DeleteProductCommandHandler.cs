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
            const string sql = @"DELETE FROM Products WHERE ProductID = (SELECT TOP 1 ProductID FROM Products ORDER BY ProductID DESC)"; ;

            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync(cancellationToken);

                using (var command = new SqlCommand(sql, connection))
                {
                    await command.ExecuteNonQueryAsync(cancellationToken);
                }
            }
        }
    }
}
