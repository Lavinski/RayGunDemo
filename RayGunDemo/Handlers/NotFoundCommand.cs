using System;
using System.Threading.Tasks;
using Enexure.MicroBus;

namespace RayGunDemo
{
    internal class NotFoundCommand : ICommand
    {
    }

    internal class NotFoundCommandHandler : ICommandHandler<NotFoundCommand>
    {
        public Task Handle(NotFoundCommand command)
        {
            Console.WriteLine("404 Not Found");

            return TaskEx.CompletedTask;
        }
    }
}