using Domain.Todos;
using Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace GetTodoById;

public sealed class GetTodoByIdHandler(IAppDbContext context) : IRequestHandler<GetTodoByIdQuery, Todo?>
{
    public async Task<Todo?> Handle(GetTodoByIdQuery request, CancellationToken cancellationToken)
    {
        return await context.Todos.FindAsync([request.Id], cancellationToken);
    }
}