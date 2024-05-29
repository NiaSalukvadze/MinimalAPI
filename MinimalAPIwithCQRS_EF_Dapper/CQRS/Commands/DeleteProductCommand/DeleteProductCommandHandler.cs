using Dapper;
using MediatR;
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
            var productId = request.productId;

            const string sql = @"delete from Products 
                                 where ProductId = @productId";

            await _dbConnection.ExecuteAsync(sql, new
            {
                productId = productId
            });

            return Unit.Value;
        }
    }
}
