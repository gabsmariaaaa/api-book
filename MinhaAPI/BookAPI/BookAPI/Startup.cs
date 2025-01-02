using BookAPI.Model;
using BookAPI.Repositories;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using System;

namespace BookAPI
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            // Configuração do DbContext para usar SQLite.
            services.AddDbContext<BookContext>(options =>
                options.UseSqlite(Configuration.GetConnectionString("DefaultConnection")));

            // Registra o repositório e a interface no container de dependência
            services.AddScoped<IBookRepository, BookRepository>();

            // Adiciona suporte para controladores API
            services.AddControllers();

            // Configuração do Swagger para documentar a API
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "BooksAPI", Version = "v1" });
            });

            // Configuração de CORS
            services.AddCors(options =>
            {
                options.AddPolicy("AllowAll", builder =>
                {
                    builder.AllowAnyOrigin()
                           .AllowAnyMethod()
                           .AllowAnyHeader();
                });
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            // Configuração para desenvolvimento com exceções detalhadas
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "BooksAPI v1"));
            }

            // Configuração para redirecionamento HTTPS e configuração de rotas
            app.UseHttpsRedirection();

            // Habilita o CORS com a política "AllowAll"
            app.UseCors("AllowAll");

            app.UseRouting();
            app.UseAuthorization();

            // Mapeamento de controladores no pipeline de requisição
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
