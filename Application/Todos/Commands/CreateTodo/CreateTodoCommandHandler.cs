using Domain.Todos;
using Interfaces;
using MediatR;

namespace CreateTodo;

public sealed class CreateTodoCommandHandler(IAppDbContext context) : IRequestHandler<CreateTodoCommand, Guid>
{
    public async Task<Guid> Handle(CreateTodoCommand request, CancellationToken cancellationToken)
    {
        var todo = new Todo{
            Id = Guid.NewGuid(),
            Title = request.Title
        };
        await context.Todos.AddAsync(todo, cancellationToken);
        await context.SaveChangesAsync(cancellationToken);
        return todo.Id;
    }
}