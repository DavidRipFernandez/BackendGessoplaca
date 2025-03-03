
using NSoft.Data;
using Microsoft.EntityFrameworkCore;
using NSoft.Repositories;
using NSoft.Services;
using Microsoft.IdentityModel.Tokens;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using System.Text;
using NSoft.Middleware;

var builder = WebApplication.CreateBuilder(args);

// Detectar el entorno actual (Development, Production, etc.)
var environment = builder.Environment.EnvironmentName;

Console.WriteLine($"Ejecutando en entorno: {environment}");

//configura el archivo de configuración según el entorno
builder.Configuration
    .SetBasePath(Directory.GetCurrentDirectory())
    .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true) //archivo base
    .AddJsonFile($"appsettings.{environment}.json", optional: true, reloadOnChange: true) //sobreescribe con el del entorno 
    .AddEnvironmentVariables(); //permite variables de entorno de docker

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
builder.Services.AddScoped<IAuthRepository, AuthRepository>();
builder.Services.AddScoped<IAuthService, AuthServices>();
builder.Services.AddScoped<IContactRepository, ContactRepository>();
builder.Services.AddScoped<IContactService, ContactService>();
builder.Services.AddScoped<ISupplierRepository, SupplierRepository>();
builder.Services.AddScoped<ISupplierService, SupplierService>();

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddDbContext<ApplicationDbContext>(options =>options.UseSqlServer(connectionString));

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.RequireHttpsMetadata = false;
        options.SaveToken = true;
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["JwtSettings:Key"])),
            ValidateIssuer = false,
            ValidateAudience = false,
            ValidIssuer = builder.Configuration["JwtSettings:Issuer"],
            ValidAudience = builder.Configuration["JwtSettings:Audience"],
            ClockSkew = TimeSpan.Zero
        };
        //options.TokenValidationParameters.NameClaimType = "userId";
        //options.TokenValidationParameters.RoleClaimType = "rolId";
    });


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
app.UseAuthentication();
app.UseMiddleware<SecurityStampMiddleware>();
app.UseAuthorization();
app.MapControllers();
app.Use(async (context, next) =>
{
    Console.WriteLine($"Request: {context.Request.Method} {context.Request.Path}");
    Console.WriteLine($"Authorization Header: {context.Request.Headers["Authorization"]}");
    await next.Invoke();
    Console.WriteLine($"Response Status Code: {context.Response.StatusCode}");
});
app.Run();

/*
   app.UseRouting();
   app.UseAuthentication(); 
   app.UseAuthorization();
   app.UseEndpoints(endpoints => {
    endpoints.MapControllers();   
   });

 */

