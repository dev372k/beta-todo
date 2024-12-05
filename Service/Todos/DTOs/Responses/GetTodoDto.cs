using Shared.Enumerations;

namespace Service.Todos.DTOs.Responses;
public record GetTodoDto(
    string id,
    string name,
    string description,
    DateTime deadline,
    DateTime createdOn,
    DateTime updatedOn,
    string status,
    string priority
);