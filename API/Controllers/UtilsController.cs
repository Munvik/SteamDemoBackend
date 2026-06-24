using Application.Features.Redis;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Distributed;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UtilsController(IDistributedCache _cache, IMediator _mediator) : ControllerBase
    {

        [HttpGet("version")]
        public IActionResult Version()
        {
            return Ok("v2");
        }

        [HttpGet("test-cache")]
        public async Task<IActionResult> TestCache()
        {
            var result = await _mediator.Send(new TestCacheQuery(), new CancellationToken());
            return Ok(result);
        }
    }
}
