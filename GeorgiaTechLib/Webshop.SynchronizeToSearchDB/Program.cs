using System.Text;
using System.Text.Json;
using Webshop.Tools.DataWrappers;
using Webshop.Tools.Messaging;

namespace Webshop.SynchronizeToSearchDB
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            builder.Services.AddAuthorization();
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();
            builder.Services.AddHttpClient();

            var app = builder.Build();

            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();
            app.UseAuthorization();

            app.MapPost("/syncronize", async (HttpContext httpContext, Message<CRUDWrapper<List<Book>>> msg, IHttpClientFactory httpClientFactory) =>
            {
                string url = "https://localhost:7016/PostUpdate";

                //Remove price from object, so postupdate only recieves Message<CRUDWrapper<List<Book>>> where book has ISBN, Title, Category.
                var transformedMsg = new Message<CRUDWrapper<List<Book>>>
                {
                    MessageType = msg.MessageType,
                    Content = new CRUDWrapper<List<Book>>(msg.Content.Data.Select(book => new Book { ISBN = book.ISBN, Title = book.Title, Category = book.Category }).ToList(),
                    msg.Content.Type),
                    CorrelationId = msg.CorrelationId
                };

                var jsonContent = new StringContent(
                JsonSerializer.Serialize(transformedMsg, new JsonSerializerOptions { PropertyNamingPolicy = JsonNamingPolicy.CamelCase }),
                Encoding.UTF8,
                "application/json");


                //Send request, return answer.

                var client = httpClientFactory.CreateClient();
                var response = await client.PostAsync(url, jsonContent);

                response.EnsureSuccessStatusCode();
                var responseContent = await response.Content.ReadAsStringAsync();
                return Results.Text(responseContent, "application/json");
            })
            .WithOpenApi();

            app.Run();
        }
    }

    public class Book
    {
        public string ISBN { get; set; }
        public string Title { get; set; }
        public string Category { get; set; }
        public float Price { get; set; }
    }

}
