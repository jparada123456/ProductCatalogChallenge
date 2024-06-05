
using Microsoft.EntityFrameworkCore;
using ProductCatalogChallenge.Application.Commands;
using ProductCatalogChallenge.Application.Queries;
using ProductCatalogChallenge.Domain.Interfaces;
using ProductCatalogChallenge.Infraestructure;
using ProductCatalogChallenge.Infraestructure.Repositories;
using MediatR;
using System.Reflection;
using Microsoft.Extensions.DependencyInjection;
using ProductCatalogChallenge.Application.Interfaces;
using ProductCatalogChallenge.Domain.Entities;
using Microsoft.AspNetCore.Hosting;
using System;
using ProductCatalogChallenge.Infraestructure.Seeds;
using Serilog;
using Serilog.Events;
using ProductCatalogChallenge.Application.Decorators;

namespace ProductCatalogChallenge.Api
{
    public class Program
    {
        public static void Main(string[] args)
        {

            Log.Logger = new LoggerConfiguration()
                .MinimumLevel.Debug()
                .MinimumLevel.Override("Microsoft", LogEventLevel.Information)
                .WriteTo.File("logs/log-.txt", rollingInterval: RollingInterval.Day)
                .CreateLogger();

            try
            {
                var builder = WebApplication.CreateBuilder(args);

                builder.Host.UseSerilog();


                // Add services to the container.

                builder.Services.AddControllers();

                //Connection string configuration
                builder.Services.AddDbContext<ApplicationDbContext>(options => options.UseSqlite(builder.Configuration.GetConnectionString("DefaultConnectionString")));

                //Repositories configutarion
                builder.Services.AddScoped(typeof(IReadRepository<>), typeof(GenericRepository<>));
                builder.Services.AddScoped(typeof(IWriteRepository<>), typeof(GenericRepository<>));

                //Handlers configutarion
                builder.Services.AddScoped<ICommandHandler<CreateProductCommand, int>, CreateProductCommandHandler>();
                builder.Services.AddScoped<ICommandHandler<UpdateProductCommand, bool>, UpdateProductCommandHandler>();
                builder.Services.AddScoped<IQueryHandler<GetAllProductsQuery, IEnumerable<Product>>, GetAllProductsQueryHandler>();
                builder.Services.AddScoped<IQueryHandler<GetProductByIdQuery, Product>, GetProductByIdQueryHandler>();

                // Decorator registration
                builder.Services.Decorate<ICommandHandler<CreateProductCommand, int>, LoggingCommandHandlerDecorator<CreateProductCommand, int>>();
                builder.Services.Decorate<ICommandHandler<UpdateProductCommand, bool>, LoggingCommandHandlerDecorator<UpdateProductCommand, bool>>();
                //builder.Services.Decorate<IQueryHandler<GetAllProductsQuery, IEnumerable<Product>>, LoggingQueryHandlerDecorator<GetAllProductsQuery, IEnumerable<Product>>>();
                //builder.Services.Decorate<IQueryHandler<GetProductByIdQuery, Product>, LoggingQueryHandlerDecorator<GetProductByIdQuery, Product>>();



                // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
                builder.Services.AddEndpointsApiExplorer();
                builder.Services.AddSwaggerGen();

                var app = builder.Build();

                using (var scope = app.Services.CreateScope())
                {
                    var dbContext = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
                    dbContext.Database.Migrate();  // Apply missing migrations

                    DataBaseSeeder.SeedDatabase(dbContext);  // Seeder execution
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
            }
            catch (Exception ex)
            {
                Log.Fatal(ex, "Host terminated unexpectedly");  
            }
            finally
            {
                Log.CloseAndFlush();    
            }
        }
    }
}
