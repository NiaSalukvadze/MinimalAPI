using MediatR;
using MinimalAPIwithCQRS_ADONET_EF.Models;

namespace MinimalAPIwithCQRS_ADONET_EF.CQRS.Commands.DeleteProductCommand
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
            var productId = request.productId;

            var product = await _context.Products.FindAsync(productId);

            _context.Products.Remove(product);
            await _context.SaveChangesAsync();
        }
    }
}
