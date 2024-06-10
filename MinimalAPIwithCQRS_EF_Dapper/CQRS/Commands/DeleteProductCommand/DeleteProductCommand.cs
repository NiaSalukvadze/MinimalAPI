using MediatR;

namespace MinimalAPIwithCQRS_EF_Dapper.CQRS.Commands.DeleteProductCommand
{
    public record DeleteProductCommand() : IRequest<Unit>;

}
