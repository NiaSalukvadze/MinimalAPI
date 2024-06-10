using MediatR;
using MinimalAPIwithCQRS_EF_ADONET.Models;
using Microsoft.EntityFrameworkCore;

namespace MinimalAPIwithCQRS_EF_ADONET.CQRS.Queries.GetProducts
{
    public class GetProductsQueryHandler : IRequestHandler<GetProductsQuery, List<Product>>
    {
        private readonly SalesContext _context;

        public GetProductsQueryHandler(SalesContext context)
        {
            _context = context;
        }
        public async Task<List<Product>> Handle(GetProductsQuery request, CancellationToken cancellationToken)
        {
            return await _context.Products.ToListAsync(cancellationToken);
        }
    }
}
