using Shared.Enumerations;

namespace Service.Todos.DTOs.Requests;

public record UpdateTodoDto(
    string name,
    string description,
    DateTime deadline,
    enTodoStatus status,
    enPriority priority
);
