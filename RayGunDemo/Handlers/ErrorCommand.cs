using System.Threading.Tasks;
using Enexure.MicroBus;

namespace RayGunDemo
{
    internal class ErrorCommand : ICommand
    {
    }

    internal class ErrorCommandHandler : ICommandHandler<ErrorCommand>
    {
        public Task Handle(ErrorCommand command)
        {
            throw new WorldIsEndingException("Oh no, what have you done now!!");
        }
    }
}