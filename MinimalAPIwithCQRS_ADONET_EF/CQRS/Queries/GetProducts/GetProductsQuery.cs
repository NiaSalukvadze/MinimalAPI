using MediatR;
using MinimalAPIwithCQRS_ADONET_EF.Models;

namespace MinimalAPIwithCQRS_ADONET_EF.CQRS.Queries.GetProducts
{
    public sealed record GetProductsQuery() : IRequest<List<Product>>;
}
