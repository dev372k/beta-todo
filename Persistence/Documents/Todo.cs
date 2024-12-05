using Shared.Enumerations;

namespace Persistence.Documents;

public class Todo : Base
{
    public required string Name { get; set; }
    public required string Description { get; set; }
    public required DateTime Deadline { get; set; }
    public required enTodoStatus Status { get; set; }
    public required enPriority Priority { get; set; }
}
