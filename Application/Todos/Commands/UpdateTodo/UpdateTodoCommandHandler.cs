using Domain.Todos;
using Exceptions;
using Interfaces;
using MediatR;

namespace UpdateTodo;

public sealed class UpdateTodoCommandHandler(IAppDbContext context) : IRequestHandler<UpdateTodoCommand, Guid>
{
    public async Task<Guid> Handle(UpdateTodoCommand request, CancellationToken cancellationToken)
    {
        var todo = await context.Todos.FindAsync([request.Id], cancellationToken);
        if (todo is null)
        {
            throw new NotFoundException(nameof(Todo), request.Id);
        }
        todo.Title = request.Title;
        todo.Completed = request.IsCompleted;
        await context.SaveChangesAsync(cancellationToken);
        return todo.Id;
    }
}