
using Ecommerce_loja_de_Games.Data;
using Ecommerce_loja_de_Games.Model;
using Ecommerce_loja_de_Games.Service;
using Ecommerce_loja_de_Games.Service.Implements;
using Ecommerce_loja_de_Games.Validator;
using FluentValidation;
using Microsoft.EntityFrameworkCore;
using System;

namespace Ecommerce_loja_de_Games
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers()
              .AddNewtonsoftJson(options =>
              {
                  options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore; // evita ficar no loop infinito
              }); // ele fornece todos os recursos para cria��o das classes controladoras




            // Conex�o com o banco de Dados
            var connecetionString = builder.Configuration
                .GetConnectionString("DefaultConnection");
            builder.Services.AddDbContext<AppDbContext>(options =>
                options.UseSqlServer(connecetionString));


            // registrar  a Valida��o das Entidades
            builder.Services.AddTransient<IValidator<Produto>, ProdutosValidator>(); // transiente ele guarda informa��es somente quando aplica��o estiver funcionando
            builder.Services.AddTransient<IValidator<Categoria>, CategoriaValidator>(); // 
            // registrar as classes de servi�o (service)
            builder.Services.AddScoped<IProdutoService, ProdutosService>(); // scoped ele guarda mesmo que aplica��o fecha
            builder.Services.AddScoped<ICategoriaService, CategoriaService>();



            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            // Configura��o do CORS onde fa�o a configura��o onde habilito que o front converse com o backend
            builder.Services.AddCors(options =>
            {
                options.AddPolicy(name: "My policy",
                    policy =>
                    {
                        policy.AllowAnyOrigin() // receber as requisi��es do front
                              .AllowAnyMethod() // receber os pots,get e delete
                              .AllowAnyHeader(); // para receber o token

                    });

            }); ;

            var app = builder.Build();


            using (var scope = app.Services.CreateAsyncScope()) // CreateasyScope cria o banco de dados e as tabelas e consulta os contextos
            {
                var dbContext = scope.ServiceProvider.GetRequiredService<AppDbContext>();
                dbContext.Database.EnsureCreated();
            }

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseCors("Mypolicy");// ele inicializa o CORS
            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}