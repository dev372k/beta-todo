using Microsoft.Extensions.Configuration;
using PusherServer;

namespace Persistence.Externals;

public class PusherService(IConfiguration config)
{
    public async Task PushAsync(object message) =>
        await new Pusher(
            config["Pusher:appId"],
            config["Pusher:appKey"],
            config["Pusher:appSecret"],
            new PusherOptions
            {
                Cluster = "eu",
                Encrypted = true
            }).TriggerAsync("my-channel", "my-event", message);
}
