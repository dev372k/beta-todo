using MassTransit;
using MongoDB.Driver;
using Persistence;
using Persistence.Documents;
using Service.Todos.DTOs.Requests;
using Service.Todos.DTOs.Responses;
using Shared.Extensions;

namespace Service.Todos;

public class TodoService
{
    private readonly IMongoCollection<Todo> _context;
    private readonly IPublishEndpoint _publishEndpoint;

    public TodoService(ApplicationDBContext context, IPublishEndpoint publishEndpoint)
    {
        _context = context.GetCollection<Todo>("todos");
        _publishEndpoint = publishEndpoint;
    }

    public async Task<List<GetTodoDto>> GetAllAsync()
    {
        var todos = await _context.Find(_ => true).ToListAsync();

        var todoList = new List<GetTodoDto>();
        foreach(var todo in todos)
            todoList.Add(new GetTodoDto(todo.Id, todo.Name, todo.Description, todo.Deadline, todo.CreatedOn, todo.UpdatedOn.GetValueOrDefault(), todo.Status.GetString(), todo.Priority.GetString()));
        return todoList.OrderByDescending(_ => _.id).ToList();
    }

    public async Task<GetTodoDto?> GetByIdAsync(string id)
    {
        var todo = await _context.Find(item => item.Id == id).FirstOrDefaultAsync();
        return new GetTodoDto(todo.Id, todo.Name, todo.Description, todo.Deadline, todo.CreatedOn, todo.UpdatedOn.GetValueOrDefault(), todo.Status.GetString(), todo.Priority.GetString());
    }

    public async Task CreateAsync(AddTodoDto dto)
    {
        await _context.InsertOneAsync(new Todo
        {
            Name = dto.name,
            Description = dto.description,
            Deadline = dto.deadline,
            Priority = dto.priority,
            Status = dto.status
        });

        await _publishEndpoint.Publish(new GetTodoAckDto(true, "added"));
    }

    public async Task UpdateAsync(string id, UpdateTodoDto dto)
    {
        await _context.ReplaceOneAsync(_ => _.Id == id, new Todo
        {
            Id = id,
            Name = dto.name,
            Description = dto.description,
            Deadline = dto.deadline,
            Priority = dto.priority,
            Status = dto.status
        });

        await _publishEndpoint.Publish(new GetTodoAckDto(true, "updated"));
    }

    public async Task DeleteAsync(string id)
    {
        await _context.DeleteOneAsync(_ => _.Id == id);
        await _publishEndpoint.Publish(new GetTodoAckDto(true, "deleted"));
    }
}


