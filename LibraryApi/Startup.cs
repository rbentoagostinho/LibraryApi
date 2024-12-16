using LibraryApi.Data;
using LibraryApi.Mappings;
using LibraryApi.Services;
using LibraryApi.Services.Interfaces;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using Newtonsoft.Json;

namespace LibraryApi
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // Adiciona serviços ao contêiner
        public void ConfigureServices(IServiceCollection services)
        {
            // Configura o DbContext com SQL Server
            services.AddDbContext<LibraryContext>(options =>
                options.UseSqlServer(Configuration.GetConnectionString("LibraryDb")));

            // Registra serviços de dependência
            services.AddScoped<IGenreService, GenreService>();
            services.AddScoped<IAuthorService, AuthorService>();
            services.AddScoped<IBookService, BookService>();

            // Adiciona controladores
            services.AddControllers()
                    .AddNewtonsoftJson(options =>
                    {
                        options.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
                    });

            // Configura Swagger
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo
                {
                    Title = "Library API",
                    Version = "v1",
                    Description = "API para gerenciar gêneros, autores e livros.",
                    Contact = new OpenApiContact
                    {
                        Name = "Seu Nome",
                        Email = "seuemail@exemplo.com"
                    }
                });
                // (Opcional) Inclui comentários XML no Swagger, caso ativados
                //var xmlFile = $"{System.Reflection.Assembly.GetExecutingAssembly().GetName().Name}.xml";
                //var xmlPath = System.IO.Path.Combine(AppContext.BaseDirectory, xmlFile);
                //c.IncludeXmlComments(xmlPath);
            });

            // Registrar AutoMapper
            services.AddAutoMapper(typeof(MappingProfile));

            // Configura o CORS (Cross-Origin Resource Sharing)
            services.AddCors(options =>
            {
                options.AddPolicy("AllowAngularOrigins", builder =>
                {
                    builder.WithOrigins("http://localhost:4200")  // URL do seu frontend Angular
                           .AllowAnyMethod()
                           .AllowAnyHeader();
                });
            });
        }

        // Configura o pipeline de requisições HTTP
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();

                // Ativa o Swagger no ambiente de desenvolvimento
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Library API v1"));
            }

            // Redireciona HTTP para HTTPS
            app.UseHttpsRedirection();

            // Configura CORS
            app.UseCors("AllowAngularOrigins");

            // Configura roteamento
            app.UseRouting();

            // Configura autorização (se necessário)
            app.UseAuthorization();

            // Mapeia endpoints dos controladores
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
