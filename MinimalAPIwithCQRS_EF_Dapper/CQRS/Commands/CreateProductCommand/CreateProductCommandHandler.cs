using Dapper;
using MediatR;
using System.Data;

namespace MinimalAPIwithCQRS_EF_Dapper.CQRS.Commands.CreateProductCommand
{
    public class CreateProductCommandHandler : IRequestHandler<CreateProductCommand>
    {
        private readonly IDbConnection _dbConnection;
        public CreateProductCommandHandler(IDbConnection dbConnection)
        {
            _dbConnection = dbConnection;
        }
        public async Task Handle(CreateProductCommand request, CancellationToken cancellationToken)
        {
            var product = request.Product;

            const string sql = @"INSERT INTO Products (ProductName, SupplierId, CategoryId) 
                             VALUES (@productName, @supplierId, @categoryId)";

            await _dbConnection.ExecuteAsync(sql, new
            {
                productName = product.ProductName,
                supplierId = product.SupplierId,
                categoryId = product.CategoryId
            });

        }
    }
}