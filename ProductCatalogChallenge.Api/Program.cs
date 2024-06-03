
using Microsoft.EntityFrameworkCore;
using ProductCatalogChallenge.Application.Commands;
using ProductCatalogChallenge.Application.Queries;
using ProductCatalogChallenge.Domain.Interfaces;
using ProductCatalogChallenge.Infraestructure;
using ProductCatalogChallenge.Infraestructure.Repositories;
using MediatR;
using System.Reflection;

namespace ProductCatalogChallenge.Api
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers();


            //Connection string configuration
            builder.Services.AddDbContext<ApplicationDbContext>(options => options.UseSqlite(builder.Configuration.GetConnectionString("DefaultConnectionString")));

            //Repositories configutarion
            builder.Services.AddScoped(typeof(IReadRepository<>), typeof(GenericRepository<>));
            builder.Services.AddScoped(typeof(IWriteRepository<>), typeof(GenericRepository<>));

            // Add MediatR
            builder.Services.AddMediatR(Assembly.GetExecutingAssembly());
            builder.Services.AddMediatR(typeof(CreateProductCommandHandler).Assembly);
            builder.Services.AddMediatR(typeof(GetAllProductsQueryHandler).Assembly);


            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            var app = builder.Build();

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
    }
}
