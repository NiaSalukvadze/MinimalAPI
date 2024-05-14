using MediatR;
using MinimalAPIwithCQRS_EF_Dapper.Models;

namespace MinimalAPIwithCQRS_EF_Dapper.CQRS.Commands.CreateProductCommand
{
    public class CreateProductCommandHandler : IRequestHandler<CreateProductCommand>
    {
        private SalesContext _salesContext;
        public CreateProductCommandHandler(SalesContext salesContext)
        {
            _salesContext = salesContext;
        }
        public async Task Handle(CreateProductCommand request, CancellationToken cancellationToken)
        {
            var product = request.Product;

            _salesContext.Products.Add(product);

            await _salesContext.SaveChangesAsync(cancellationToken);
        }
    }
}
