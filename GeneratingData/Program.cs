using Faker;
using GeneratingData;
using GeneratingData.Domain;
using GeneratingData.Domain.QueueSettings;
using GeneratingData.Domain.Services;

var builder = Host.CreateApplicationBuilder(args);

// Adding the configuration of correct environment

builder.Services.AddHostedService<Worker>();

// Getting from appsettings.<env>.json the configuration of RabbitMQ. In the Docker file has: ENV DOTNET_ENVIRONMENT=Docker
builder.Services.Configure<RabbitMQSettings>(builder.Configuration.GetSection("RabbitMQ"));
builder.Services.AddSingleton<RabbitMQService>();

builder.Services.AddTransient<ClientService>();
builder.Services.AddTransient<Execute>();


var host = builder.Build();

host.Run();
