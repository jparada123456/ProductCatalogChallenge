using Microsoft.Extensions.Logging;
using ProductCatalogChallenge.Application.Interfaces;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductCatalogChallenge.Application.Decorators
{
    public class LoggingQueryHandlerDecorator<TQuery,TResult> : IQueryHandler<TQuery, TResult>
    {
        private readonly IQueryHandler<TQuery, TResult> _inner;
        private readonly ILogger<LoggingQueryHandlerDecorator<TQuery, TResult>> _logger;

        public LoggingQueryHandlerDecorator(IQueryHandler<TQuery, TResult> inner, ILogger<LoggingQueryHandlerDecorator<TQuery, TResult>> logger)
        {
            _inner = inner;
            _logger = logger;
        }

        public async Task<TResult> HandleAsync(TQuery query)
        {
            var stopwatch = Stopwatch.StartNew();
            var result = await _inner.HandleAsync(query);
            stopwatch.Stop();

            _logger.LogInformation($"Handled {typeof(TQuery).Name} in {stopwatch.ElapsedMilliseconds}ms");

            return result;
        }
    }
}
