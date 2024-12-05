using Shared.Enumerations;

namespace Service.Todos.DTOs.Requests;

public record AddTodoDto(
    string name,
    string description,
    DateTime deadline,
    enTodoStatus status,
    enPriority priority
);
