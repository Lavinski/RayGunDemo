using System;
using System.Diagnostics;
using System.Threading.Tasks;
using Enexure.MicroBus;
using Serilog;

namespace RayGunDemo
{
    internal class LoggingHandler : IDelegatingHandler
    {
        private readonly ILogger _log;

        public LoggingHandler(ILogger log)
        {
            _log = log;
        }

        public async Task<object> Handle(INextHandler next, object message)
        {
            var watch = new Stopwatch();

            _log.Information("Started handling message {MessageType}", message.GetType());

            watch.Start();

            var result = await next.Handle(message);

            watch.Stop();

            _log.Information("Finished handling {MessageType}", message.GetType());

            if (watch.Elapsed > TimeSpan.FromSeconds(1))
            {
                _log.Warning("Processing of {MessageType} took too long {Duration}", message.GetType(), watch.Elapsed);
            }

            return result;
        }
    }
}