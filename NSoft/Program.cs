
using NSoft.Data;
using Microsoft.EntityFrameworkCore;
using NSoft.Repositories;
using NSoft.Services;

var builder = WebApplication.CreateBuilder(args);

// Detectar el entorno actual (Development, Production, etc.)
var environment = builder.Environment.EnvironmentName;

Console.WriteLine($"Ejecutando en entorno: {environment}");

// Obtener la cadena de conexion desde appsettings.json
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

// Mostrar todas las variables de entorno dentro del contenedor
foreach (var env in Environment.GetEnvironmentVariables().Keys)
{
    Console.WriteLine($"{env} = {Environment.GetEnvironmentVariable(env.ToString())}");
}

//configurar el archivo de configuracion
builder.Services.AddScoped<IMaterialRepository, MaterialRepository>();
builder.Services.AddScoped<IMaterialService, MaterialService>();
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddDbContext<ApplicationDbContext>(options =>options.UseSqlServer(connectionString));  

//configura el archivo de configuración según el entorno
builder.Configuration
    .SetBasePath(Directory.GetCurrentDirectory())
    .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true) //archivo base
    .AddJsonFile($"appsettings.{environment}.json", optional: true, reloadOnChange: true) //sobreescribe con el del entorno 
    .AddEnvironmentVariables(); //permite variables de entorno de docker

var app = builder.Build();

//Mostrando el entorno actual en consola
Console.WriteLine($"Ejecutando en entorno : {environment}");

if (environment == "Development")
{
    //configuraciones adicionales para desarrollo
    app.UseDeveloperExceptionPage();
}

// Configure the HTTP request pipeline.


app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();
app.Run();

/*
   app.UseRouting();
   app.UseAuthentication(); 
   app.UseAuthorization();
   app.UseEndpoints(endpoints => {
    endpoints.MapControllers();   
   });

 */

