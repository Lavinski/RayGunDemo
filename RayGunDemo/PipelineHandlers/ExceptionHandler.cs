using System;
using System.Threading.Tasks;
using Enexure.MicroBus;
using Serilog;

namespace RayGunDemo
{
    internal class ExceptionHandler : IDelegatingHandler
    {
        private readonly ILogger _log;

        public ExceptionHandler(ILogger log)
        {
            _log = log;
        }

        public async Task<object> Handle(INextHandler next, object message)
        {
            try
            {
                return await next.Handle(message);
            }
            catch (Exception ex)
            {
                _log.Error(ex, "Error while handling request");
            }

            return Unit.Unit;
        }
    }
}