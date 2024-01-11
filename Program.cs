using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using PokeApi;
using PokeApi.Data;
using PokeApi.Interfaces;
using PokeApi.Middlewares;
using PokeApi.Repository;
using System;
using System.Text;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers(
        options =>
        {
            options.Filters.Add<ApiResponseFilter>();
        }
    )
    .AddJsonOptions(x => x.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddTransient<Seed>();
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

builder.Services.AddScoped<IPokemonRepository, PokemonRepository>();
builder.Services.AddScoped<ITypeRepository, TypeRepository>();
builder.Services.AddScoped<ITrainerRepository, TrainerRepository>();
builder.Services.AddScoped<IReviewRepository, ReviewRepository>();
builder.Services.AddScoped<IAbilityRepository, AbilityRepository>();

// Db Config
builder.Services.AddDbContext<DataContext>(options =>
    {
      options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
    }
);

// JWT Config
builder.Services.AddIdentity<IdentityUser, IdentityRole>(
                   options =>
                   {
                       options.Password.RequireUppercase = true;
                       options.Password.RequireLowercase = true;
                       options.Password.RequireDigit = true;
                       options.SignIn.RequireConfirmedEmail = true;
                   }
               )
           .AddEntityFrameworkStores<DataContext>()
           .AddDefaultTokenProviders();

builder.Services.AddAuthentication(
        x =>
        {
            x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
        }
    )
.AddJwtBearer(
        options =>
        {
            var Key = Encoding.UTF8.GetBytes(builder.Configuration.GetValue<string>("JWT:AccessKey"));
            options.SaveToken = true;
            options.TokenValidationParameters = new TokenValidationParameters()
            {
                ValidateIssuer = false,
                ValidateAudience = false,
                ValidateLifetime = true,
                ValidateIssuerSigningKey = true,
                ValidIssuer = builder.Configuration.GetValue<string>("JWT:Issuer"),
                ValidAudience = builder.Configuration.GetValue<string>("JWT:Audience"),
                IssuerSigningKey = new SymmetricSecurityKey(Key),
                ClockSkew = TimeSpan.Zero
            };

            options.Events = new JwtBearerEvents
            {
                OnAuthenticationFailed = context =>
                {
                    context.Response.Headers.Add("AuthenticationFailed", "true");
                    context.Response.Headers.Add("Exception", context.Exception.ToString());
                    Console.WriteLine("Autenticación fallida: " + context.Exception.Message);
                    Console.WriteLine("Error inesperado en la autenticación: {Error}" + context.Exception.ToString());
                    return Task.CompletedTask;
                },
                OnTokenValidated = context =>
                {
                    Console.WriteLine("Token validado para {Username}" + context.Principal.Identity.Name);
                    return Task.CompletedTask;
                },
                OnChallenge = context =>
                {
                    Console.WriteLine("Desafío de autenticación iniciado");
                    return Task.CompletedTask;
                },

            };
        }
    );

builder.Services.AddSingleton<IJWTManagerRepository, JWTManagerRepository>();
builder.Services.AddScoped<ITrainerRepository, TrainerRepository>();

// Swagger
builder.Services.AddSwaggerGen(option =>
{
    option.SwaggerDoc("v1", new OpenApiInfo { Title = "PokeAPI", Version = "v1" });
    option.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        In = ParameterLocation.Header,
        Description = "Please enter a valid token",
        Name = "Authorization",
        Type = SecuritySchemeType.Http,
        BearerFormat = "JWT",
        Scheme = "Bearer"
    });
    option.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type=ReferenceType.SecurityScheme,
                    Id="Bearer"
                }
            },
            new string[]{}
        }
    });
});

var app = builder.Build();

if (args.Length == 1 && args[0].ToLower() == "seeddata")
    SeedData(app);

void SeedData(IHost app)
{
    var scopedFactory = app.Services.GetService<IServiceScopeFactory>();

    using (var scope = scopedFactory.CreateScope())
    {
        var service = scope.ServiceProvider.GetService<Seed>();
        service.SeedDataContext();
    }
}

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();

app.Run();
