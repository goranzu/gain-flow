using FluentValidation;
using GainFlow.Api.Shared.Extensions;
using GainFlow.Api.Shared.Persistence;

WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

builder.Services
    .AddHttpContextAccessor()
    .AddEndpoints()
    .AddPersistence(builder.Configuration)
    .AddIdentity(builder.Environment)
    .AddValidatorsFromAssemblyContaining<Program>()
    .AddQueryHandlers()
    .AddCommandHandlers()
    .AddApplicationServices()
    .AddErrorHandling()
    .AddSpa()
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
    app.UseSpaStaticFiles();
}

app.UseStatusCodePages();

app.UseEndpoints();

await app.RunAsync();
