using GainFlow.Api;

WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpoints();

WebApplication app = builder.Build();

app.UseEndpoints();

await app.RunAsync();
