using Microsoft.Extensions.Logging;
using ProductCatalogChallenge.Application.Interfaces;
using Serilog;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductCatalogChallenge.Application.Decorators
{
    public class LoggingCommandHandlerDecorator<TCommand, TResult> : ICommandHandler<TCommand, TResult>
    {
        private readonly ICommandHandler<TCommand, TResult> _inner;
        private readonly ILogger<LoggingCommandHandlerDecorator<TCommand, TResult>> _logger;

        public LoggingCommandHandlerDecorator(ICommandHandler<TCommand, TResult> inner, ILogger<LoggingCommandHandlerDecorator<TCommand, TResult>> logger)
        {
            _inner = inner;
            _logger = logger;
        }

        public async Task<TResult> HandleAsync(TCommand command)
        {
            var commandName = typeof(TCommand).Name;
            _logger.LogInformation($"Handling {commandName} command.");

            var stopwatch = System.Diagnostics.Stopwatch.StartNew();
            var result = await _inner.HandleAsync(command);
            stopwatch.Stop();

            _logger.LogInformation($"{commandName} command handled in {stopwatch.ElapsedMilliseconds}ms.");

            return result;
        }
    }
}
