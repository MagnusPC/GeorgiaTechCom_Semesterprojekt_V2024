using Microsoft.AspNetCore.Mvc;
using System.Text.Json;
using Webshop.Search.Domain;
using Webshop.Tools.Messaging;

using Webshop.Search.Persisstence;
using Webshop.Data.Persistence;

namespace Webshop.SearchService.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SearchTermController : ControllerBase
    {
        Producer producer;
        Consumer<List<SearchResult>> consumer;

        List<State> callbacks;

    public class State
        {
            public string CorrelationId { get; set; }
            public List<SearchResult>? Content { get; set; }

            public bool finished;

            public State(string correlationId, List<SearchResult>? content, bool finished)
            {
                CorrelationId = correlationId;
                Content = content;
                this.finished = finished;
            }
        }


        public SearchTermController()
        {
            producer = new Producer("PostSearch");
            producer.Connect().Wait();

            callbacks = new List<State>();

            consumer = new Consumer<List<SearchResult>>(result =>
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
}

    
