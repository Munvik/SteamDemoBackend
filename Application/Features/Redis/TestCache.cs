using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;
using Microsoft.Extensions.Caching.Distributed;

namespace Application.Features.Redis
{
    public record TestCacheQuery() : IRequest<string>;

    public class TestCacheQueryHandler(IDistributedCache _cache) : IRequestHandler<TestCacheQuery, string>
    {
        public async Task<string> Handle(TestCacheQuery request, CancellationToken cancellationToken)
        {
            string? cached = await _cache.GetStringAsync("test-cache");

            if (string.IsNullOrEmpty(cached))
            {
                await _cache.SetStringAsync("test-cache", "Hello from cache database", new DistributedCacheEntryOptions
                {
                    AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(5)
                });
                return "Cache miss";
            }
            return "Cache hit";
        }
    }
}
