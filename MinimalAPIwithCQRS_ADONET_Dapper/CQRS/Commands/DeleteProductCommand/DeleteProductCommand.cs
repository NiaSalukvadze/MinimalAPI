using MediatR;

namespace MinimalAPIwithCQRS_ADONET_Dapper.CQRS.Commands.DeleteProductCommand
{
    public record DeleteProductCommand(int productId) : IRequest<Unit>;

}
