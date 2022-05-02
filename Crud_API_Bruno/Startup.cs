using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerUI;
using Crud_API_Bruno.Infrastructure.Context;
using Crud_API_Bruno.Domain.Users;
using Crud_API_Bruno.Application.Services;
using Crud_API_Bruno.Infrastructure.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using Crud_API_Bruno.Domain.Products;
using Crud_API_Bruno.Application.Products;
using System.IO;
using System.Reflection;

namespace Crud_API_Bruno
{
    public class Startup
    {
        public IConfiguration Configuration { get; }
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            // Banco de dados
            services
                .AddDbContext<MainContext>((opt) => opt.UseSqlServer(GetConnectionString()));

            // Segurança
            services
                .AddDefaultIdentity<User>(options =>
                {
                    options.Password.RequireDigit = false;
                    options.Password.RequireLowercase = false;
                    options.Password.RequireUppercase = false;
                    options.Password.RequireNonAlphanumeric = false;
                    options.Password.RequiredLength = 1;
                })
                .AddEntityFrameworkStores<MainContext>()
                .AddDefaultTokenProviders();



            services
                .AddHttpContextAccessor()
                .AddAuthentication(x =>
                {
                    x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                    x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                })

                .AddJwtBearer(x =>
                {
                    x.RequireHttpsMetadata = true;
                    x.SaveToken = true;
                    x.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuerSigningKey = true,
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(Environment.GetEnvironmentVariable("EncryptKey"))),
                        ValidateAudience = false,
                        ValidateIssuer = false,
                        ValidateLifetime = false
                    };
                });

            // Documentação
            services.AddSwaggerGen(cfg =>
            {
                var contact = new OpenApiContact
                {
                    Name = "Crud - Avaliação"                                  
                };

                cfg.SwaggerDoc("v1", new OpenApiInfo
                {
                    Version = "v1",
                    Title = "CRUD API 1.0",
                    Contact = contact
                });

                var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
                cfg.IncludeXmlComments(xmlPath);
          

            cfg.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    Name = "Authorization",
                    Type = SecuritySchemeType.ApiKey,
                    Scheme = "Bearer",
                    BearerFormat = "JWT",
                    In = ParameterLocation.Header
                });

                cfg.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    {
                        new OpenApiSecurityScheme
                        {
                            In = ParameterLocation.Header,
                            Description = "Copie 'Bearer ' + token'",
                            Reference = new OpenApiReference
                            {
                                Type = ReferenceType.SecurityScheme,
                                Id = "Bearer"
                            }
                        },
                        new string[] {}

                    }
                });

            });

            // Configurações gerais da api
            services
                .Configure<ApiBehaviorOptions>(o =>
                {
                    o.InvalidModelStateResponseFactory = actionContext => new BadRequestObjectResult(new
                    {
                        ReasonOfFail = "Dados inválidos",
                        Errors = actionContext.ModelState.SelectMany(x => x.Value.Errors)
                    });
                })
                .AddControllers()
                .AddJsonOptions((opt) =>
                {
                    opt.JsonSerializerOptions.IgnoreNullValues = true;
                    opt.JsonSerializerOptions.PropertyNamingPolicy = JsonNamingPolicy.CamelCase;
                });

            // Contratos e classes concretas da aplicação
            services.AddScoped<IAuthService, JwtIdentityAuthService>();

            services.AddScoped<IProdutoRepository, ProdutoRepository>();
            services.AddScoped<IProdutoService, ProdutoService>();

            services.AddScoped<ICategoriaRepository, CategoriaRepository>();
            services.AddScoped<ICategoriaService, CategoriaService>();


            services.AddScoped<IProdutosCategoriasRepository, ProdutosCategoriasRepository>();
            services.AddScoped<IProdutosCategoriasService, ProdutosCategoriasService>();
        }

        private string GetConnectionString() => Configuration.GetConnectionString("MainDB");

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseCors(c =>
            {
                c.AllowAnyHeader();
                c.AllowAnyMethod();
                c.AllowAnyOrigin();
            });

            app.UseAuthentication();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

            app
              .UseSwagger()
              .UseSwaggerUI(s =>
              {
                  s.SwaggerEndpoint("/swagger/v1/swagger.json", "V1");
                  s.DisplayRequestDuration();
                  s.DisplayOperationId();
                  s.EnableValidator();
                  s.DefaultModelExpandDepth(2);
                  s.DefaultModelRendering(ModelRendering.Model);
                  s.DefaultModelsExpandDepth(-1);
                  s.DocExpansion(DocExpansion.None);
                  s.EnableDeepLinking();
                  s.ShowExtensions();

                  s.RoutePrefix = "swagger";
                  s.DocumentTitle = "Avaliação - Crud Api";
              });
        }
    }
}
