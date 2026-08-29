using MediatR;

namespace CreateTodo;

public sealed record CreateTodoCommand(string Title) : IRequest<Guid>;
