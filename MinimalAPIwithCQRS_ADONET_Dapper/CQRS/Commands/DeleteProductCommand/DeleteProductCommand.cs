using MediatR;

namespace MinimalAPIwithCQRS_ADONET_Dapper.CQRS.Commands.DeleteProductCommand
{
    public record DeleteProductCommand() : IRequest<Unit>;

}
