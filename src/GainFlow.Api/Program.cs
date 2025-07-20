using GainFlow.Api;

WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

builder.Services
    .AddEndpoints()
    .AddPersistence(builder.Configuration)
    .AddIdentity(builder.Environment);

WebApplication app = builder.Build();

if (app.Environment.IsDevelopment())
{
    await app.ApplyMigrations();
}

app.UseEndpoints();

await app.RunAsync();
