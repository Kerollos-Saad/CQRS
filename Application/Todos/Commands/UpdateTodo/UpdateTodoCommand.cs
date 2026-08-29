using MediatR;

namespace UpdateTodo;

public sealed record UpdateTodoCommand(Guid Id, string Title, bool IsCompleted) : IRequest<Guid>;
