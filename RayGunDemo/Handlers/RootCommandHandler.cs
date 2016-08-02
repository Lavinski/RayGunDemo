using System;
using System.Threading.Tasks;
using Enexure.MicroBus;

namespace RayGunDemo
{
    internal class RootCommand : ICommand
    {
    }

    internal class RootCommandHandler : ICommandHandler<RootCommand>
    {
        public Task Handle(RootCommand command)
        {
            Console.WriteLine("Hello World");

            return TaskEx.CompletedTask;
        }
    }
}