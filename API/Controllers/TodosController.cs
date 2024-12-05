using Microsoft.AspNetCore.Mvc;
using Service.Todos;
using Service.Todos.DTOs.Requests;
using Shared.Commons;
using Shared.Extensions;

namespace API.Controllers;

[ApiController]
[Route(DevContants.BASE_ENDPOINT)]
public class TodosController(TodoService _service) : ControllerBase
{
    [HttpGet("{id}")]
    public async Task<IActionResult> Get(string id) =>
        Ok(await _service.GetByIdAsync(id).ToResponseAsync(message: Messages.RECORD_FETCHED));

    [HttpGet]
    public async Task<IActionResult> Get() =>
        Ok(await _service.GetAllAsync().ToResponseAsync(message: Messages.RECORD_FETCHED));

    [HttpPost]
    public async Task<IActionResult> Post(AddTodoDto request) =>
        Ok(await _service.CreateAsync(request).ToResponseAsync(message: Messages.RECORD_INSERTED));

    [HttpPut("{id}")]
    public async Task<IActionResult> Put(string id, UpdateTodoDto request) =>
        Ok(await _service.UpdateAsync(id, request).ToResponseAsync(message: Messages.RECORD_UPDATED));

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(string id) =>
        Ok(await _service.DeleteAsync(id).ToResponseAsync(message: Messages.RECORD_DELETED));
}
