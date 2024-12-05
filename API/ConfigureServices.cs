using MassTransit;
using Persistence;
using Persistence.Externals;
using Service.Todos;
using System.Reflection;

namespace API;

public static class ConfigureServices
{
    public static void ServicesRegistry(this IServiceCollection services, IConfiguration configuration)
    {
        services.Services(configuration);
        services.Database(configuration);
        services.Misc(configuration);
    }

    public static void Services(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddScoped<TodoService>();

        services.AddScoped<PusherService>();
    }

    public static void Misc(this IServiceCollection services, IConfiguration configuration)
    {

        services.AddCors(opt =>
        {
            opt.AddPolicy(name: "CorsPolicy", builder =>
            {
                builder.AllowAnyOrigin()
                    .AllowAnyHeader()
                    .AllowAnyMethod();
            });
        });

        services.AddMassTransit(x =>
        {
            x.AddConsumers(Assembly.GetExecutingAssembly());
            x.UsingRabbitMq((context, cfg) =>
            {
                cfg.Host(new Uri(configuration["RabbitMQ"]!));
                cfg.ConfigureEndpoints(context);
            });
        });

        // Register AutoMapper
        //var mapperConfig = new MapperConfiguration(mc =>
        //{
        //    mc.AddProfile(new MappingProfile());
        //});
        //IMapper mapper = mapperConfig.CreateMapper();
        //services.AddSingleton(mapper);

        services.AddSwaggerGen();
    }

    public static void Database(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddSingleton<ApplicationDBContext>();
    }

}
