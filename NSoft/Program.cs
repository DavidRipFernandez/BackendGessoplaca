using Microsoft.OpenApi.Models;
using NSoft.Configurations;
using NSoft.Middleware;
using NSoft.Extensions; 

var corsPolicy = "_myAllowSpecificOrigins";
var builder = WebApplication.CreateBuilder(args);

// Config por entorno (como ya lo tenías)
var environment = builder.Environment.EnvironmentName;
Console.WriteLine($"Ejecutando en entorno: {environment}");

builder.Configuration
    .SetBasePath(Directory.GetCurrentDirectory())
    .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
    .AddJsonFile($"appsettings.{environment}.json", optional: true, reloadOnChange: true)
    .AddEnvironmentVariables();

// ⬇️ Llamadas “compactas”
builder.Services
    .AddAppCors(corsPolicy, "http://localhost:5173") //url del frontend
    .AddAppDb(builder.Configuration)
    .AddAppDI()
    .AddAppJwtAuth(builder.Configuration)
    .AddAppSwaggerWithJwt();

var app = builder.Build();

Console.WriteLine($"Ejecutando en entorno : {environment}");
if (environment == "Development")
{
    app.UseDeveloperExceptionPage();
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "SalesSystem API v1");
    });
}

app.UseHttpsRedirection();
app.UseCors(corsPolicy);

app.UseAuthentication();
app.UseMiddleware<SecurityStampMiddleware>();
app.UseAuthorization();

app.MapControllers();
app.Run();
