using MediatR;
using MinimalAPIwithCQRS_ADONET_Dapper.Models;

namespace MinimalAPIwithCQRS_ADONET_Dapper.CQRS.Queries.GetProducts
{
    public sealed record GetProductsQuery() : IRequest<List<Product>>;
}
