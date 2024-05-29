using MediatR;
using MinimalAPIwithCQRS_ADONET_EF.Models;

namespace MinimalAPIwithCQRS_ADONET_EF.CQRS.Commands.UpdateProductCommand
{
    public record UpdateProductCommand(Product Product) : IRequest;
}
