using Azure.Messaging.ServiceBus;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using ProductProvider.Infrastructure.Services;

var host = new HostBuilder()
    .ConfigureFunctionsWebApplication()
    .ConfigureServices(services =>
    {
        services.AddApplicationInsightsTelemetryWorkerService();
        services.ConfigureFunctionsApplicationInsights();


        services.AddSingleton(new ServiceBusClient(Environment.GetEnvironmentVariable("SERVICEBUS_CONNECTION")));
        services.AddScoped<ProductService>();

    })
    .Build();

host.Run();
