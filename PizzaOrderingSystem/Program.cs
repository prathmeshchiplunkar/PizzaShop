

using AutoMapper;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using NLog;
using NLog.Web;
using PizzaOrderingSystem.DataAccess.Configuration;
using PizzaOrderingSystem.DataAccess.Data;
using PizzaOrderingSystem.DataAccess.Models;
using PizzaOrderingSystem.DTO.IService;
using PizzaOrderingSystem.DTO.Models;
using PizzaOrderingSystem.DTO.Services;
using PizzaOrderingSystem.Utils.Filters;
using PizzaOrderingSystem.Utils.Jwt;
using PizzaOrderingSystem.Utils.Middlewares;
using System;
using System.Reflection.Metadata;
using System.Text;

var logger = NLog.LogManager.Setup().LoadConfigurationFromAppSettings().GetCurrentClassLogger();
logger.Debug("Initializing Project : Main method");
try
{
    var builder = WebApplication.CreateBuilder(args);
    IConfiguration configuration = builder.Configuration;


    #region Services
    // Add services to the container.

    //  *** SQL Server ***
    //builder.Services.AddDbContext<PizzaDbContext>(options =>
    //options.UseSqlServer(builder.Configuration.GetConnectionString("")));

    //  *** Inmemory ***
    builder.Services.AddDbContext<PizzaDbContext>(options => options.UseInMemoryDatabase(databaseName: "PizzaDb"));

    

    builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
    builder.Services.AddScoped<IPizzaInventoryService, PizzaInventoryService>();
    builder.Services.AddTransient<IUserService, UserService>();
    builder.Services.AddTransient<IUserRoleService, UserRoleService>();
    builder.Services.AddTransient<IJwtUtils, JwtUtils>();

    //To Register FIlter in pipeline
    builder.Services.AddTransient<JWTActionFilter>();


    // *** Automapper Configuration ***
    var config = new MapperConfiguration(cfg =>
                {
                    cfg.CreateMap<PizzaInventory, PizzaInventoryDTO>();
                    cfg.CreateMap<User, UserDTO>();
                    cfg.CreateMap<UserRole, UserRoleDTO>();
                }
            );
    var mapper = config.CreateMapper();

    builder.Services.AddSingleton(mapper);

    #region JWT Authentication
    // Authentication
    builder.Services.AddAuthentication(options =>
    {
        options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
        options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
        options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
    })

    // Adding Jwt Bearer
    .AddJwtBearer(options =>
     {
         options.SaveToken = true;
         options.RequireHttpsMetadata = false;
         options.TokenValidationParameters = new TokenValidationParameters()
         {
             ValidateIssuer = false,
             ValidateAudience = false,
             ValidateLifetime = true,
             ValidateIssuerSigningKey = true,
             ClockSkew = TimeSpan.Zero,
             ValidAudience = configuration["JWT:ValidAudience"],
             ValidIssuer = configuration["JWT:ValidIssuer"],
             IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["JWT:Secret"]))
         };
     });

    builder.Services.AddAuthorization();

    #endregion


    builder.Services.AddControllers();
    // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
    builder.Services.AddEndpointsApiExplorer();
    builder.Services.AddSwaggerGen(option =>
    {
        option.SwaggerDoc("v1", new OpenApiInfo
        {
            Title = "Pizza Ordering System",
            Version = "v1"
        });
        option.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme()
        {
            Name = "Authorization",
            Type = SecuritySchemeType.ApiKey,
            Scheme = "Bearer",
            BearerFormat = "JWT",
            In = ParameterLocation.Header,
            Description = "JWT Authorization header using the Bearer scheme. \r\n\r\n Enter 'Bearer' [space] and then your token in the text input below.\r\n\r\nExample: \"Bearer 1safsfsdfdfd\"",
        });
        option.AddSecurityRequirement(new OpenApiSecurityRequirement {
        {
            new OpenApiSecurityScheme {
                Reference = new OpenApiReference {
                    Type = ReferenceType.SecurityScheme,
                        Id = "Bearer"
                }
            },
            new string[] {}
        }
        });
    });
    #endregion

    #region NLog
    // NLog: Setup NLog for Dependency injection
    builder.Logging.ClearProviders();
    builder.Logging.SetMinimumLevel(Microsoft.Extensions.Logging.LogLevel.Trace);
    builder.Host.UseNLog();
    #endregion

    var app = builder.Build();

    #region Middleware Pipeline
    // Configure the HTTP request pipeline.
    if (app.Environment.IsDevelopment())
    {
        app.UseSwagger();
        app.UseSwaggerUI();
    }

    app.UseCors();
    //exception middleware
    app.UseMiddleware<GlobalErrorHandlingMiddleware>();
    //app.UseMiddleware<JwtMiddleware>();

    app.UseHttpsRedirection();

    app.UseAuthentication();
    app.UseAuthorization();

    app.MapControllers();

    app.Run();
    #endregion
}
catch (Exception exception)
{
    logger.Error(exception, "Exception occured in .net COre Lifecycle");
    throw;
}
finally
{
    logger.Error("Shutting down NLog..");
    NLog.LogManager.Shutdown();
}
