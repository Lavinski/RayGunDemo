using System.Threading.Tasks;

namespace RayGunDemo
{
    static class TaskEx
    {
        public static Task CompletedTask {
            get { return Task.FromResult(0); }
        }
    }
}