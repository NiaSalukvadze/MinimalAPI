using MediatR;

namespace MinimalAPIwithCQRS_ADONET_EF.CQRS.Commands.DeleteProductCommand
{
    public record DeleteProductCommand() : IRequest;

}
