using MediatR;
using Microsoft.EntityFrameworkCore;
using MinimalAPIwithCQRS_ADONET_EF.Models;

namespace MinimalAPIwithCQRS_ADONET_EF.CQRS.Commands.UpdateProductCommand
{
    public class UpdateProductCommandHandler : IRequestHandler<UpdateProductCommand>
    {
        private readonly SalesContext _context;
        public UpdateProductCommandHandler(SalesContext context)
        {
            _context = context;
        }
        public async Task Handle(UpdateProductCommand request, CancellationToken cancellationToken)
        {
            var productId = request.Product.ProductId;

            var product = await _context.Products.FirstOrDefaultAsync(p => p.ProductId == productId);

            product.ProductName = request.Product.ProductName;
            product.SupplierId = request.Product.SupplierId;
            product.CategoryId = request.Product.CategoryId;

            await _context.SaveChangesAsync();
        }
    }
}
