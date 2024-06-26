﻿using MediatR;
using MinimalAPIwithCQRS_EF_Dapper.Models;

namespace MinimalAPIwithCQRS_EF_Dapper.CQRS.Commands.UpdateProductCommand
{
    public record UpdateProductCommand(Product Product) : IRequest<Unit>;
}
