using BackgroundWorkerService;

var builder = Host.CreateApplicationBuilder(args);
builder.Services.AddHostedService<BackgroundWorker>();

var host = builder.Build();
host.Run();
