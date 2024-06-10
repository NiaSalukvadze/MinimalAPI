using MediatR;
using MinimalAPIwithCQRS_EF_ADONET.Models;

namespace MinimalAPIwithCQRS_EF_ADONET.CQRS.Commands.CreateProductCommand
{
    public record CreateProductCommand(Product Product) : IRequest;

}
