using Domain.Todos;
using MediatR;

namespace GetTodoById;

public sealed record GetTodoByIdQuery(Guid Id) : IRequest<Todo?>;
