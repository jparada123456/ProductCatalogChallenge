using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Caching.Memory;
using Newtonsoft.Json;
using ProductCatalogChallenge.Application.Interfaces;

namespace ProductCatalogChallenge.Application.Decorators
{
    public class CachingQueryHandlerDecorator<TQuery, TResult> : IQueryHandler<TQuery, TResult>
    {
        private readonly IQueryHandler<TQuery, TResult> _handler;
        private readonly IMemoryCache _cache;

        public CachingQueryHandlerDecorator(IQueryHandler<TQuery, TResult> handler, IMemoryCache cache)
        {
            _handler = handler;
            _cache = cache;
        }

        public async Task<TResult> HandleAsync(TQuery query)
        {
           
            var cacheKey = JsonConvert.SerializeObject(query);

            if (_cache.TryGetValue(cacheKey, out TResult result))
            {
                return result;
            }
            else
            {
                result = await _handler.HandleAsync(query);
                _cache.Set(cacheKey, result, TimeSpan.FromMinutes(5));

                return result;
            }
        }
    }
}
