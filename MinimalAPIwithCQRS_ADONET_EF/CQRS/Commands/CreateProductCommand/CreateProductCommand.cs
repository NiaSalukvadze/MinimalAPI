using MediatR;
using MinimalAPIwithCQRS_ADONET_EF.Models;

namespace MinimalAPIwithCQRS_ADONET_EF.CQRS.Commands.CreateProductCommand
{
    public record CreateProductCommand(Product Product) : IRequest;

}
