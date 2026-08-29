using Domain.Todos;
using Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace GetTodos;

public sealed class GetTodoByIdHandler(IAppDbContext context) : IRequestHandler<GetTodosQuery, List<Todo>>
{
    public async Task<List<Todo>> Handle(GetTodosQuery request, CancellationToken cancellationToken)
    {
        return await context.Todos.ToListAsync(cancellationToken);
    }
}