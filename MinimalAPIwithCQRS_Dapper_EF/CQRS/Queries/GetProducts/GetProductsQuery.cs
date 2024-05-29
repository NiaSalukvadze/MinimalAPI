using MediatR;
using MinimalAPIwithCQRS_Dapper_EF.Models;

namespace MinimalAPIwithCQRS_Dapper_EF.CQRS.Queries.GetProducts
{
    public sealed record GetProductsQuery() : IRequest<List<Product>>;
}
