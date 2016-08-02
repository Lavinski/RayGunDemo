using System.Threading.Tasks;
using Enexure.MicroBus;
using Serilog;
using System;

namespace RayGunDemo
{
    internal class ExceptionsCommand : ICommand
    {
    }

    internal class ExceptionsCommandHandler : ICommandHandler<ExceptionsCommand>
    {
        private readonly ILogger _log;

        public ExceptionsCommandHandler(ILogger log)
        {
            _log = log;
        }

        public Task Handle(ExceptionsCommand command)
        {
            for (int i = 0; i < 10; i++) {
                _log.Error(new System.InvalidOperationException(), "Also Badness");
            }

            Console.WriteLine("Things went wrong");
            return TaskEx.CompletedTask;
        }
    }
}