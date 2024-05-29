using MediatR;

namespace MinimalAPIwithCQRS_Dapper_ADONET.CQRS.Commands.DeleteProductCommand
{
    public record DeleteProductCommand(int productId) : IRequest;

}
