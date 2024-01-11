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
        }
    );

builder.Services.AddSingleton<IJWTManagerRepository, JWTManagerRepository>();
builder.Services.AddScoped<ITrainerRepository, TrainerRepository>();

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

app.UseAuthorization();
app.MapControllers();

app.Run();
