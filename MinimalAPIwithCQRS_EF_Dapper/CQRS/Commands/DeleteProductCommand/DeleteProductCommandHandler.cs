using Dapper;
using MediatR;
using Microsoft.AspNetCore.Http;
using System.Data;

namespace MinimalAPIwithCQRS_EF_Dapper.CQRS.Commands.DeleteProductCommand
{
    public class DeleteProductCommandHandler : IRequestHandler<DeleteProductCommand, Unit>
    {
        private readonly IDbConnection _dbConnection;
        public DeleteProductCommandHandler(IDbConnection dbConnection)
        {
            _dbConnection = dbConnection;
        }
        public async Task<Unit> Handle(DeleteProductCommand request, CancellationToken cancellationToken)
        {
            const string sql = "DELETE FROM Products WHERE ProductID = (SELECT TOP 1 ProductID FROM Products ORDER BY ProductID DESC)";

            await _dbConnection.ExecuteAsync(sql);

            return Unit.Value;
        }
    }
}
