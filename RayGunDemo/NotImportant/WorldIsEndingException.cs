using System;

namespace RayGunDemo
{
    internal class WorldIsEndingException : Exception
    {
        public WorldIsEndingException(string message)
            : base(message)
        {
        }
    }
}