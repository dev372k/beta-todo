using MassTransit;
using Persistence.Externals;
using Service.Todos.DTOs.Responses;

namespace API.Consumers;

public class TodoConsumer(PusherService pusher) : IConsumer<GetTodoAckDto>
{
    public async Task Consume(ConsumeContext<GetTodoAckDto> context)
    {
        Console.WriteLine("Packet received:");
        await pusher.PushAsync(context.Message);
        //if (context.Message.type == "deleted")
        //    await context.Publish(new GetTodoAckDto(true, "changed"));
        Console.WriteLine("Packet delivered:");
    }
}
