using Webshop.Data.Persistence;
using Webshop.Search.Persisstence;

namespace Webshop.SearchService.Api
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Services.AddSingleton<PGDataContext>();
            builder.Services.AddScoped<SearchRepository>();


            // Add services to the container.
            builder.Services.AddAuthorization();

            builder.Services.AddControllers();

            // Swagger/OpenAPI configuration
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
