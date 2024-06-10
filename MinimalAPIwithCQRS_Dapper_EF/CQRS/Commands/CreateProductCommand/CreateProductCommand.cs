using MediatR;
using MinimalAPIwithCQRS_Dapper_EF.Models;

namespace MinimalAPIwithCQRS_Dapper_EF.CQRS.Commands.CreateProductCommand
{
    public record CreateProductCommand(Product Product) : IRequest;

}
