using System;
using System.Threading.Tasks;
using Enexure.MicroBus;

namespace RayGunDemo
{
    internal class GoodCommand : ICommand
    {
    }

    internal class GoodCommandHandler : ICommandHandler<GoodCommand>
    {
        public Task Handle(GoodCommand command)
        {
            Console.WriteLine("Did good things");

            return TaskEx.CompletedTask;
        }
    }
}