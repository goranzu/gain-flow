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
    .AddApplicationServices()
    .AddErrorHandling()
    .AddAuthorization()
    .AddAuthentication();

builder.Services.AddHttpContextAccessor();
builder.Services.AddSpaStaticFiles(configuration =>
{
    configuration.RootPath = "wwwroot";
});

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

app.UseSpa(spaOptions =>
{
   spaOptions.Options.SourcePath = "../react-client";
   if (app.Environment.IsDevelopment())
   {
       spaOptions.UseProxyToSpaDevelopmentServer("http://localhost:3000");
   }
});

app.UseStatusCodePages();

app.UseEndpoints();

await app.RunAsync();
