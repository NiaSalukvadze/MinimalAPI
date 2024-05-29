using MediatR;
using MinimalAPIwithCQRS_Dapper_EF.Models;

namespace MinimalAPIwithCQRS_Dapper_EF.CQRS.Commands.CreateProductCommand
{
    public class CreateProductCommandHandler : IRequestHandler<CreateProductCommand>
    {
        private readonly SalesContext _context;
        public CreateProductCommandHandler(SalesContext context)
        {
            _context = context;
        }
        public async Task Handle(CreateProductCommand request, CancellationToken cancellationToken)
        {
            var product = request.Product;

            _context.Products.Add(product);
            await _context.SaveChangesAsync();

        }
    }
}