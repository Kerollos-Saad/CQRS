
using API.Requests;

using CreateTodo;
using UpdateTodo;
using DeleteTodo;

using GetTodos;
using GetTodoById;

using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class TodosController(IMediator mediator) : ControllerBase
{
    [HttpGet]
    public async Task<IActionResult> Get()
    {
        return Ok(await mediator.Send(new GetTodosQuery()));
    }
    
    [HttpGet("{todoId:guid}", Name = "GetTodoById")]
    public async Task<IActionResult> Get(Guid todoId)
    {
        var todo = await mediator.Send(new GetTodoByIdQuery(todoId));
        return todo is null ? NotFound() : Ok(todo);
    }
    
    [HttpPost]
    public async Task<IActionResult> Post(CreateTodoRequest request)
    {
        var command = new CreateTodoCommand(request.Title);
        var todoId = await mediator.Send(command);
        return CreatedAtRoute("GetTodoById", new { todoId }, null);
    }

    [HttpPut("{todoId:guid}")]
    public async Task<IActionResult> Put(Guid todoId, UpdateTodoRequest request)
    {
        var command = new UpdateTodoCommand(todoId,request.Title, request.IsCompleted);
        await mediator.Send(command);
        return NoContent();
    }

    [HttpDelete("{todoId:guid}")]
    public async Task<IActionResult> Delete(Guid todoId)
    {
        var command = new DeleteTodoCommand(todoId);
        await mediator.Send(command);
        return NoContent();
    }
}
