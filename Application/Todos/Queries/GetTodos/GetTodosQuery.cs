using Domain.Todos;
using MediatR;

namespace GetTodos;

public sealed record GetTodosQuery : IRequest<List<Todo>>;
