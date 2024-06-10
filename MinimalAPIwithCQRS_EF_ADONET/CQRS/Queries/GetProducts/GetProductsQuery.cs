using MediatR;
using MinimalAPIwithCQRS_EF_ADONET.Models;

namespace MinimalAPIwithCQRS_EF_ADONET.CQRS.Queries.GetProducts
{
    public sealed record GetProductsQuery() : IRequest<List<Product>>;
}
