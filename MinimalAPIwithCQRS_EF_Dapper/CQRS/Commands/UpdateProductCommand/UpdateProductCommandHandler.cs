using MediatR;
using Microsoft.EntityFrameworkCore;
using MinimalAPIwithCQRS_EF_Dapper.Models;

namespace MinimalAPIwithCQRS_EF_Dapper.CQRS.Commands.UpdateProductCommand
{
    public class UpdateProductCommandHandler : IRequestHandler<UpdateProductCommand>
    {
        private SalesContext _salesContext;
        public UpdateProductCommandHandler(SalesContext salesContext)
        {
            _salesContext = salesContext;
        }
        public async Task Handle(UpdateProductCommand request, CancellationToken cancellationToken)
        {
            var productId = request.productId;

            Product? product = await _salesContext.Products.FirstOrDefaultAsync(x => x.ProductId == productId);

            product = request.product;

            await _salesContext.SaveChangesAsync(cancellationToken);
        }
    }
}
