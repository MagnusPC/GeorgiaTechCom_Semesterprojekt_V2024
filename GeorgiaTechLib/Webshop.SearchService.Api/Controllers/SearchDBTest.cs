using Microsoft.AspNetCore.Mvc;
using Webshop.Search.Domain;
using Webshop.Search.Persisstence;

namespace Webshop.SearchService.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SearchDBController : ControllerBase
    {

        private readonly SearchRepository _searchRepository;

        public SearchDBController(SearchRepository searchRepository)
        {
            _searchRepository = searchRepository;
        }
        // GET api/search/textinput
        [HttpGet("textinput")]
        public async Task<ActionResult<IEnumerable<SearchResult>>> GetFromTextInput(string searchtext)
        {
            if (string.IsNullOrEmpty(searchtext))
            {
                return BadRequest("Search text cannot be empty.");
            }

            var results = await _searchRepository.GetFromTextInput(searchtext);



            return Ok(results);
        }

        // GET api/search/category/{categoryID}
        [HttpGet("category/{categoryID}")]
        public async Task<ActionResult<IEnumerable<SearchResult>>> GetAllFromCategory(int categoryID)
        {
            var results = await _searchRepository.GetAllFromCategory(categoryID);

            

            return Ok(results);
        }
    }
}
