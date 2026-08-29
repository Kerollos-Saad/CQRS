using FluentValidation;

namespace DeleteTodo;
public class DeleteTodoCommandValidator : AbstractValidator<DeleteTodoCommand>
{
    public DeleteTodoCommandValidator()
    {
    }
}