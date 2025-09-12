
using NSoft.Data;
using Microsoft.EntityFrameworkCore;
using NSoft.Repositories;
using NSoft.Services;
using Microsoft.IdentityModel.Tokens;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using System.Text;
using NSoft.Middleware;
using NSoft.Services.IServices;
using NSoft.Repositories.IRepositories;
using Microsoft.OpenApi.Models;
var corsPolicy = "_myAllowSpecificOrigins";
var builder = WebApplication.CreateBuilder(args);

// Habilitar CORS
builder.Services.AddCors(options =>
{
    options.AddPolicy(name: corsPolicy,
        policy =>
        {
            policy.WithOrigins("http://localhost:5173") // URL del frontend
                  .AllowAnyHeader()
                  .AllowAnyMethod();
        });
});

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
//var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");  before

builder.Services.AddDbContext<ApplicationDbContext>(options =>
options.UseSqlServer(builder.Configuration.GetConnectionString("ConexionSql")));

//Opcional Mostrar todas las variables de entorno dentro del contenedor
foreach (var key in Environment.GetEnvironmentVariables().Keys)
{
    Console.WriteLine($"{key} ={Environment.GetEnvironmentVariable(key!.ToString()!)}");
}

/*  BEFORE
Mostrar todas las variables de entorno dentro del contenedor
foreach (var env in Environment.GetEnvironmentVariables().Keys)
{
    Console.WriteLine($"{env} = {Environment.GetEnvironmentVariable(env.ToString())}");
}
*/

//configurar el archivo de configuracion
//categoria
builder.Services.AddScoped<ICategoriaMaterialRepository, CategoriaMaterialRepository>();
builder.Services.AddScoped<ICategoriaMaterialService, CategoriaMaterialService>();
//ProveedorMarca
builder.Services.AddScoped<IProveedorMarcaRepository, ProveedorMarcaRepository>();
builder.Services.AddScoped<IProveedorMarcaService, ProveedorMarcaService>();
//PrecioTarifa
builder.Services.AddScoped<IPrecioTarifaRepository, PrecioTarifaRepository>();
builder.Services.AddScoped<IPrecioTarifaService, PrecioTarifaService>();
//Marca
builder.Services.AddScoped<IMarcaRepository, MarcaRepository>();
builder.Services.AddScoped<IMarcaService, MarcaService>();
//Material
builder.Services.AddScoped<IMaterialRepository, MaterialRepository>();
builder.Services.AddScoped<IMaterialService, MaterialService>();
//Auth
builder.Services.AddScoped<IAuthRepository, AuthRepository>();
builder.Services.AddScoped<IAuthService, AuthServices>();
//Contacto
builder.Services.AddScoped<IContactRepository, ContactRepository>();
builder.Services.AddScoped<IContactService, ContactService>();
//proveedor
builder.Services.AddScoped<ISupplierRepository, SupplierRepository>();
builder.Services.AddScoped<ISupplierService, SupplierService>();
//User
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<IModulePermissionRepository, ModulePermissionRepository>();
builder.Services.AddScoped<IModulePermissionService, ModulePermissionService>();
builder.Services.AddScoped<IRoleRepository, RoleRepository>();
builder.Services.AddScoped<IRoleService, RoleService>();
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
//builder.Services.AddDbContext<ApplicationDbContext>(options =>options.UseSqlServer(connectionString)); -> before


//autenticacion JWT
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

builder.Services.AddAuthorization();

//Swagger + esquema de seguridad JWT
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "SalesSystem API", Version = "v1" });

    var jwtScheme = new OpenApiSecurityScheme
    {
        Scheme = "bearer",
        BearerFormat = "JWT",
        Name = "Authorization",
        In = ParameterLocation.Header,
        Type = SecuritySchemeType.Http,
        Description = "**Redhat98",
        Reference = new OpenApiReference { Id = "Bearer", Type = ReferenceType.SecurityScheme }
    };

    c.AddSecurityDefinition(jwtScheme.Reference.Id, jwtScheme);
    c.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        { jwtScheme, Array.Empty<string>() }
    });
});


var app = builder.Build();


Console.WriteLine($"Ejecutando en entorno : {environment}");
if (environment == "Development")
{
    app.UseDeveloperExceptionPage();
}

// HTTPS + CORS
app.UseHttpsRedirection();
app.UseCors(corsPolicy);
//Auth
app.UseAuthentication();
app.UseMiddleware<SecurityStampMiddleware>();
app.UseAuthorization();
// Swagger (siempre activo; si quieres solo en Dev, muévelo al if de arriba)
app.UseSwagger();
app.UseSwaggerUI(c =>
{
    c.SwaggerEndpoint("/swagger/v1/swagger.json", "SalesSystem API v1");
    // c.RoutePrefix = string.Empty; // descomenta si quieres Swagger en la raíz "/"
});

// Logging simple de requests/responses (antes de MapControllers)
app.Use(async (context, next) =>
{
    Console.WriteLine($"Request: {context.Request.Method} {context.Request.Path}");
    Console.WriteLine($"Authorization Header: {context.Request.Headers["Authorization"]}");
    await next.Invoke();
    Console.WriteLine($"Response Status Code: {context.Response.StatusCode}");
});



app.MapControllers();
/*
app.Use(async (context, next) =>
{
    Console.WriteLine($"Request: {context.Request.Method} {context.Request.Path}");
    Console.WriteLine($"Authorization Header: {context.Request.Headers["Authorization"]}");
    await next.Invoke();
    Console.WriteLine($"Response Status Code: {context.Response.StatusCode}");
});
//builder.WebHost.UseUrls("http://0.0.0.0:8080", "https://0.0.0.0:8081");
*/

app.Run();

/*
   app.UseRouting();
   app.UseAuthentication(); 
   app.UseAuthorization();
   app.UseEndpoints(endpoints => {
    endpoints.MapControllers();   
   });

 */
