using FluentValidation;
using GainFlow.Api.Shared;

WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

builder.Services
    .AddEndpoints()
    .AddPersistence(builder.Configuration)
    .AddIdentity(builder.Environment)
    .AddValidatorsFromAssemblyContaining<Program>()
    .AddQueryHandlers()
    .AddCommandHandlers()
    .AddErrorHandling()
    .AddAuthorization()
    .AddAuthentication();

WebApplication app = builder.Build();

app.UseAuthentication();
app.UseAuthorization();

if (app.Environment.IsDevelopment())
{
    await app.ApplyMigrations();
    await app.SeedDatabase();
    app.UseDeveloperExceptionPage();
}
else
{
    app.UseExceptionHandler();
}

app.UseStatusCodePages();

app.UseEndpoints();

await app.RunAsync();
