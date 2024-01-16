using Amazon.S3;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using PokeApi.Data;
using PokeApi.Interfaces;
using PokeApi.Middlewares;
using PokeApi.Repository;
using System.Text;
using System.Text.Json.Serialization;

namespace PokeApi.Configs
{
    public class AppConfigurations
    {

        private WebApplicationBuilder builder { get; set; }

        public AppConfigurations()
        {
       
        }

        public void Configure(WebApplicationBuilder builder)
        {
            this.builder = builder;
            AWSConfig();
            ConfigureCORS();
            ConfigureDatabase();
            ConfigureSeeders();
            ConfigureAuthentication();
            ConfigureRepositories();
            ConfigureAutoMapper();
            ConfigureSwagger();
        }

        private void AWSConfig()
        {
            // Serverless config
            builder.Services.AddAWSLambdaHosting(LambdaEventSource.HttpApi);

            // S3 Config
            builder.Services.AddDefaultAWSOptions(builder.Configuration.GetAWSOptions());
            builder.Services.AddAWSService<IAmazonS3>();
        }

        private void ConfigureAutoMapper()
        {
            builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
        }

        private void ConfigureSwagger()
        {
            // Swagger
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();
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

        }

        private void ConfigureAuthentication()
        {
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

                        
                    }
                );

            builder.Services.AddSingleton<IJWTManagerRepository, JWTManagerRepository>();
            builder.Services.AddScoped<ITrainerRepository, TrainerRepository>();
        }

        private void ConfigureRepositories()
        {
            // Controllers Config
            builder.Services.AddControllers(
                    options =>
                    {
                        options.Filters.Add<ApiResponseFilter>();
                    }
                )
                .AddJsonOptions(x => x.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles);

            builder.Services.AddScoped<IPokemonRepository, PokemonRepository>();
            builder.Services.AddScoped<ITypeRepository, TypeRepository>();
            builder.Services.AddScoped<ITrainerRepository, TrainerRepository>();
            builder.Services.AddScoped<IReviewRepository, ReviewRepository>();
            builder.Services.AddScoped<IAbilityRepository, AbilityRepository>();
            builder.Services.AddScoped<IAWSRepository, AWSRepository>();
        }

        private void ConfigureSeeders()
        {
            builder.Services.AddTransient<Seed>();
        }

        private void ConfigureCORS()
        {
            // CORS
            builder.Services.AddCors(options =>
            {
                options.AddDefaultPolicy(
                    policy =>
                    {
                        policy.AllowAnyOrigin()
                            .AllowAnyHeader()
                            .AllowAnyMethod();
                    });
            });

        }

        private void ConfigureDatabase()
        {
            // Db Config
            builder.Services.AddDbContext<DataContext>(options =>
            {
                options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
            }
            );
        }
    }
}
