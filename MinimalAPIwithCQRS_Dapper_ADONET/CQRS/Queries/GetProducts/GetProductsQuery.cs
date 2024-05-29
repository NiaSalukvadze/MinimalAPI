using MediatR;
using MinimalAPIwithCQRS_Dapper_ADONET.Models;

namespace MinimalAPIwithCQRS_Dapper_ADONET.CQRS.Queries.GetProducts
{
    public sealed record GetProductsQuery() : IRequest<List<Product>>;
}
