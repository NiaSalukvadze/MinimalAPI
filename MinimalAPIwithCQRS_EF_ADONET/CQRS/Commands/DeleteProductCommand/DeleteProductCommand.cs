using MediatR;

namespace MinimalAPIwithCQRS_EF_ADONET.CQRS.Commands.DeleteProductCommand
{
    public record DeleteProductCommand(int productId) : IRequest;

}
