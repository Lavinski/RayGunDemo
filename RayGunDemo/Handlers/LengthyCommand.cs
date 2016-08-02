using System;
using System.Threading.Tasks;
using Enexure.MicroBus;

namespace RayGunDemo
{
    internal class LengthyCommand : ICommand
    {
    }

    internal class LengthyCommandHandler : ICommandHandler<LengthyCommand>
    {
        public async Task Handle(LengthyCommand command)
        {
            var total = 11;
            for (int i = 0; i < total; i++)
            {
                await Task.Delay(TimeSpan.FromSeconds(0.1));
                Console.WriteLine("Processing {0:0.0}%", (double)total / (double)i);
            }

            Console.WriteLine("Done Processing");
        }
    }
}