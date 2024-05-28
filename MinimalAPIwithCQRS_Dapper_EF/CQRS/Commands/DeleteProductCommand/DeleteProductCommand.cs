using MediatR;

namespace MinimalAPIwithCQRS_Dapper_EF.CQRS.Commands.DeleteProductCommand
{
    public record DeleteProductCommand(int productId) : IRequest;

}
