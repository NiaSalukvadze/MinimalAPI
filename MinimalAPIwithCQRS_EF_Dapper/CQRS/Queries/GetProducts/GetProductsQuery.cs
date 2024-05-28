using MediatR;
using MinimalAPIwithCQRS_EF_Dapper.Models;

namespace MinimalAPIwithCQRS_EF_Dapper.CQRS.Queries.GetProducts
{
    public sealed record GetProductsQuery() : IRequest<List<Product>>;
}
