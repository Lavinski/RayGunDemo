using System.Threading.Tasks;
using Enexure.MicroBus;
using Serilog;
using System;

namespace RayGunDemo
{
    internal class ErrorsCommand : ICommand
    {
    }

    internal class ErrorsCommandHandler : ICommandHandler<ErrorsCommand>
    {
        private readonly ILogger _log;

        public ErrorsCommandHandler(ILogger log)
        {
            _log = log;
        }

        public Task Handle(ErrorsCommand command)
        {
            for (int i = 0; i < 10; i++) {
                _log.Error("Badness");
            }

            Console.WriteLine("Things went wrong, no exceptions tho");
            return TaskEx.CompletedTask;
        }
    }
}