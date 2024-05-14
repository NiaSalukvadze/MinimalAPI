using MediatR;
using Microsoft.EntityFrameworkCore;
using MinimalAPIwithCQRS_EF_Dapper.Models;

namespace MinimalAPIwithCQRS_EF_Dapper.CQRS.Commands.DeleteProductCommand
{
    public class DeleteProductCommandHandler : IRequestHandler<DeleteProductCommand>
    {
        private SalesContext _salesContext;
        public DeleteProductCommandHandler(SalesContext salesContext)
        {
            _salesContext = salesContext;
        }
        public async Task Handle(DeleteProductCommand request, CancellationToken cancellationToken)
        {
            var productId = request.productId;

            Product? product = await _salesContext.Products.FirstOrDefaultAsync(x => x.ProductId == productId);

            _salesContext.Products.Remove(product!);

            await _salesContext.SaveChangesAsync(cancellationToken);
        }
    }
}
