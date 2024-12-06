
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Webshop.Tools.DataWrappers;
using Webshop.Tools.Messaging;

namespace Webshop.SearchDBAccess.API
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            builder.Services.AddAuthorization();
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();
            var app = builder.Build();

            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();
            app.UseAuthorization();

            app.MapPost("/PostUpdate", (HttpContext httpContext, Message<CRUDWrapper<List<Book>>> msg) =>
            {
                Console.WriteLine("Recieved post: " + msg);
                foreach (var item in msg.Content.Data)
                {
                    Console.WriteLine($"{item}");
                }

                DBAccess dBAccess = new DBAccess();

                dBAccess.UpdateDatabase(msg);

                return Results.Ok();
            })
            .WithOpenApi();

            app.Run();
        }
    }
}
