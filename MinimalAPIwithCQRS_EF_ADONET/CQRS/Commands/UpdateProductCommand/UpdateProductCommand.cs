using MediatR;
using MinimalAPIwithCQRS_EF_ADONET.Models;

namespace MinimalAPIwithCQRS_EF_ADONET.CQRS.Commands.UpdateProductCommand
{
    public record UpdateProductCommand(Product Product) : IRequest;
}
