using MediatR;

namespace DeleteTodo;

public sealed record DeleteTodoCommand(Guid Id) : IRequest;
