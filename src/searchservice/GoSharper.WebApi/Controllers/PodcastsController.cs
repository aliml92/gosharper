using GoSharper.Domain.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace GoSharper.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PodcastsController : Controller
    {
        private readonly IPodcastsApplication _podcastsApplication;

        public PodcastsController(IPodcastsApplication podcastsApplication)
        {
            _podcastsApplication = podcastsApplication;
        }

        [HttpPost("sample")]
        public async Task<IActionResult> PostSampleData()
        {
            await _podcastsApplication.InsertManyAsync();

            return Ok(new { Result = "Data successfully registered with Elasticsearch" });
        }

        [HttpPost("exception")]
        public IActionResult PostException()
        {
            throw new Exception("Generate sample exception");
        }

        [HttpGet("")]
        public async Task<IActionResult> GetAll()
        {
            var result = await _podcastsApplication.GetAllAsync();

            return Json(result);
        }

        [HttpGet("name-match")]
        public async Task<IActionResult> GetByTitleWithMatch([FromQuery] string title)
        {
            var result = await _podcastsApplication.GetByTitleWithMatch(title);

            return Json(result);
        }

        [HttpGet("name-multimatch")]
        public async Task<IActionResult> GetByTitleAndDescriptionMultiMatch([FromQuery] string term)
        {
            var result = await _podcastsApplication.GetByTitleAndDescriptionMultiMatch(term);

            return Json(result);
        }

        [HttpGet("name-matchphrase")]
        public async Task<IActionResult> GetByTitleWithMatchPhrase([FromQuery] string title)
        {
            var result = await _podcastsApplication.GetByTitleWithMatchPhrase(title);

            return Json(result);
        }

        [HttpGet("name-matchphraseprefix")]
        public async Task<IActionResult> GetByTitleWithMatchPhrasePrefix([FromQuery] string title)
        {
            var result = await _podcastsApplication.GetByTitleWithMatchPhrasePrefix(title);

            return Json(result);
        }

        [HttpGet("name-term")]
        public async Task<IActionResult> GetByTitleWithTerm([FromQuery] string title)
        {
            var result = await _podcastsApplication.GetByTitleWithTerm(title);

            return Json(result);
        }

        [HttpGet("name-wildcard")]
        public async Task<IActionResult> GetByTitleWithWildcard([FromQuery] string title)
        {
            var result = await _podcastsApplication.GetByTitleWithWildcard(title);

            return Json(result);
        }

        [HttpGet("name-fuzzy")]
        public async Task<IActionResult> GetByTitleWithFuzzy([FromQuery] string title)
        {
            var result = await _podcastsApplication.GetByTitleWithFuzzy(title);

            return Json(result);
        }

        [HttpGet("description-match")]
        public async Task<IActionResult> GetByDescriptionMatch([FromQuery] string description)
        {
            var result = await _podcastsApplication.GetByDescriptionMatch(description);

            return Json(result);
        }

        [HttpGet("all-fields")]
        public async Task<IActionResult> SearchAllProperties([FromQuery] string term)
        {
            var result = await _podcastsApplication.SearchInAllFiels(term);

            return Json(result);
        }

        [HttpGet("condiction")]
        public async Task<IActionResult> GetByCondictions([FromQuery] string title, [FromQuery] string description, [FromQuery] DateTime? createdDate)
        {
            var result = await _podcastsApplication.GetPodcastsCondition(title, description, createdDate);

            return Json(result);
        }

        [HttpGet("term")]
        public async Task<IActionResult> GetByAllCondictions([FromQuery] string term)
        {
            var result = await _podcastsApplication.GetPodcastsAllCondition(term);

            return Json(result);
        }

        //[HttpGet("aggregation")]
        //public async Task<IActionResult> GetActorsAggregation()
        //{
        //    var result = await _podcastsApplication.GetPodcastsAggregation();

        //    return Json(result);
        //}
    }
}
