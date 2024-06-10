using MediatR;
using Microsoft.EntityFrameworkCore;
using MinimalAPIwithCQRS_Dapper_EF.Models;

namespace MinimalAPIwithCQRS_Dapper_EF.CQRS.Commands.DeleteProductCommand
{
    public class DeleteProductCommandHandler : IRequestHandler<DeleteProductCommand>
    {
        private readonly SalesContext _context;
        public DeleteProductCommandHandler(SalesContext context)
        {
            _context = context;
        }
        public async Task Handle(DeleteProductCommand request, CancellationToken cancellationToken)
        {
            var product = await _context.Products.OrderByDescending(x => x.ProductId).FirstOrDefaultAsync();

            _context.Products.Remove(product);
            await _context.SaveChangesAsync();
        }
    }
}
