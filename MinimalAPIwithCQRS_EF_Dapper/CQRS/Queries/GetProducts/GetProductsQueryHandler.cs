using MediatR;
using MinimalAPIwithCQRS_EF_Dapper.Models;
using System.Data;
using Dapper;

namespace MinimalAPIwithCQRS_EF_Dapper.CQRS.Queries.GetProducts
{
    public class GetProductsQueryHandler : IRequestHandler<GetProductsQuery, List<Product>>
    {
        private readonly IDbConnection _dbConnection;

        public GetProductsQueryHandler(IDbConnection dbConnection)
        {
            _dbConnection = dbConnection;
        }
        public async Task<List<Product>> Handle(GetProductsQuery request, CancellationToken cancellationToken)
        {
            const string sql = @"SELECT * FROM ""Products""";

            var products = await _dbConnection.QueryAsync<Product>(sql);

            return products.ToList();
        }
    }
}
