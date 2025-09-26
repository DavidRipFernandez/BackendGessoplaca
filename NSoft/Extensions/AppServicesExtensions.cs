
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using NSoft.Configurations;
using NSoft.Data;
using NSoft.Repositories;
using NSoft.Repositories.IRepositories;
using NSoft.Services;
using NSoft.Services.IServices;
using System.Text;


namespace NSoft.Extensions
{
    public static class AppServicesExtensions
    {
        public static IServiceCollection AddAppCors(
            this IServiceCollection services,
            string policyName,
            params string[] allowedOrigins)
        {
            services.AddCors(options =>
            {
                options.AddPolicy(policyName, policy =>
                {
                    policy.WithOrigins(allowedOrigins.Length > 0 ? allowedOrigins : new[] { "http://localhost:5173" })
                          .AllowAnyHeader()
                          .AllowAnyMethod();
                });
            });
            return services;
        }

        public static IServiceCollection AddAppDb(
            this IServiceCollection services,
            IConfiguration config)
        {
            services.AddDbContext<ApplicationDbContext>(opt =>
                opt.UseSqlServer(config.GetConnectionString("ConexionSql")));
            return services;
        }

        public static IServiceCollection AddAppDI(this IServiceCollection services)
        {
            // Repos & Services (lo mismo que tenías)
            services.AddScoped<ICategoriaMaterialRepository, CategoriaMaterialRepository>();
            services.AddScoped<ICategoriaMaterialService, CategoriaMaterialService>();
            services.AddScoped<IProveedorMarcaRepository, ProveedorMarcaRepository>();
            services.AddScoped<IProveedorMarcaService, ProveedorMarcaService>();
            services.AddScoped<IPrecioTarifaRepository, PrecioTarifaRepository>();
            services.AddScoped<IPrecioTarifaService, PrecioTarifaService>();
            services.AddScoped<IMarcaRepository, MarcaRepository>();
            services.AddScoped<IMarcaService, MarcaService>();
            services.AddScoped<IMaterialRepository, MaterialRepository>();
            services.AddScoped<IMaterialService, MaterialService>();
            services.AddScoped<IAuthRepository, AuthRepository>();
            services.AddScoped<IAuthService, AuthServices>();
            services.AddScoped<IContactRepository, ContactRepository>();
            services.AddScoped<IContactService, ContactService>();
            services.AddScoped<ISupplierRepository, SupplierRepository>();
            services.AddScoped<ISupplierService, SupplierService>();
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IModulePermissionRepository, ModulePermissionRepository>();
            services.AddScoped<IModulePermissionService, ModulePermissionService>();
            services.AddScoped<IRoleRepository, RoleRepository>();
            services.AddScoped<IRoleService, RoleService>();
            services.AddScoped<IUserService, UserService>();

            services.AddControllers();
            services.AddEndpointsApiExplorer();
            return services;
        }

        public static IServiceCollection AddAppJwtAuth(
            this IServiceCollection services,
            IConfiguration config)
        {
            services.Configure<JwtSettings>(config.GetSection("JwtSettings"));

            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(options =>
                {
                    options.RequireHttpsMetadata = false;
                    options.SaveToken = true;
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuerSigningKey = true,
                        IssuerSigningKey = new SymmetricSecurityKey(
                            Encoding.UTF8.GetBytes(config["JwtSettings:Key"])),

                        // De momento mantienes relajado:
                        ValidateIssuer = false,
                        ValidateAudience = false,

                        // Ya configurados por si luego activas validación estricta:
                        ValidIssuer = config["JwtSettings:Issuer"],
                        ValidAudience = config["JwtSettings:Audience"],

                        ClockSkew = TimeSpan.Zero
                    };
                });

            services.AddAuthorization();
            return services;
        }

        public static IServiceCollection AddAppSwaggerWithJwt(this IServiceCollection services)
        {
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "SalesSystem API", Version = "v1" });

                var jwtScheme = new OpenApiSecurityScheme
                {
                    Scheme = "bearer",
                    BearerFormat = "JWT",
                    Name = "Authorization",
                    In = ParameterLocation.Header,
                    Type = SecuritySchemeType.Http,
                    Description = "Escribe: Bearer {tu_token_jwt}",
                    Reference = new OpenApiReference { Id = "Bearer", Type = ReferenceType.SecurityScheme }
                };

                c.AddSecurityDefinition(jwtScheme.Reference.Id, jwtScheme);
                c.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    { jwtScheme, Array.Empty<string>() }
                });
            });

            return services;
        }
    }
}
