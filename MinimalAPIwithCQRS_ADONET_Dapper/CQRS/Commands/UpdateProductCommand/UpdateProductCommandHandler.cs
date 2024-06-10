using Dapper;
using MediatR;
using System.Data;

namespace MinimalAPIwithCQRS_ADONET_Dapper.CQRS.Commands.UpdateProductCommand
{
    public class UpdateProductCommandHandler : IRequestHandler<UpdateProductCommand, Unit>
    {
        private readonly IDbConnection _dbConnection;
        public UpdateProductCommandHandler(IDbConnection dbConnection)
        {
            _dbConnection = dbConnection;
        }
        public async Task<Unit> Handle(UpdateProductCommand request, CancellationToken cancellationToken)
        {
            var product = request.Product;

            const string sql = @"update Products 
                                 set ProductName = @productName,
                                 SupplierId = @supplierId, 
                                 CategoryId = @categoryId
                                 where ProductId = @productId";

            await _dbConnection.ExecuteAsync(sql, new
            {
                productId = product.ProductId,
                productName = product.ProductName,
                supplierId = product.SupplierId,
                categoryId = product.CategoryId
            });

            return Unit.Value;
        }
    }
}
