using Microsoft.AspNetCore.Mvc;
using System.Text.Json;
using Webshop.Tools.Messaging;

namespace Webshop.SearchService.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SearchTermController : ControllerBase
    {
        Producer producer;
        Consumer<List<Book>> consumer;

        List<State> callbacks;

        public class State
        {
            public string CorrelationId { get; set; }
            public List<Book>? Content { get; set; }

            public bool finished;

            public State(string correlationId, List<Book>? content, bool finished)
            {
                CorrelationId = correlationId;
                Content = content;
                this.finished = finished;
            }
        }

        public class Book
        {
            public string ISBN { get; set; }
            public string Title { get; set; }
            public string Category { get; set; }
        }

        public SearchTermController()
        {
            producer = new Producer("PostSearch");
            producer.Connect().Wait();

            callbacks = new List<State>();

            consumer = new Consumer<List<Book>>(result =>
            {
                State? s = callbacks.Find(x => x.CorrelationId == result.CorrelationId);
                if (s != null)
                {
                    s.finished = true;
                    s.Content = result.Content;
                }

            }, "SearchComplete");

            consumer.Connect().Wait();
        }

        [HttpPost]
        public async Task<IActionResult> GetSearch([FromBody] SearchTerm temp)
        {
            string correlationId = Guid.NewGuid().ToString();
            await producer.SendMessage(new Message<SearchTerm>("Search", temp, correlationId));

            State s = new(correlationId, null, false);
            callbacks.Add(s);

            while (s.finished != true)
            {
                Thread.Sleep(10);
            }


            callbacks.Remove(s);

            if (s.Content != null)
            {
                return Ok(s.Content);
            }
            
            return NoContent();

        }
    }

    public class SearchTerm
    {
        public SearchTerm()
        {
        }

        public SearchTerm(string term, string category)
        {
            Term = term;
            Category = category;
        }

        public string Term { get; set; }
        public string Category { get; set ; }
    }



}
