using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Autofac;
using Enexure.MicroBus;

namespace RayGunDemo
{
    internal class AutofacDependencyResolver : IDependencyResolver
    {
        private readonly ILifetimeScope lifetimeScope;

        public AutofacDependencyResolver(ILifetimeScope lifetimeScope)
        {
            this.lifetimeScope = lifetimeScope;
        }

        public IDependencyScope BeginScope()
        {
            var scope = (lifetimeScope.Tag as string == "MicroBus")
                ? lifetimeScope
                : lifetimeScope.BeginLifetimeScope("MicroBus");

            return new AutofacDependencyScope(scope);
        }
    }

    internal class AutofacDependencyScope : AutofacDependencyResolver, IDependencyScope
    {
        private readonly ILifetimeScope lifetimeScope;

        public AutofacDependencyScope(ILifetimeScope lifetimeScope)
            : base(lifetimeScope)
        {
            this.lifetimeScope = lifetimeScope;
        }

        public object GetService(Type serviceType)
        {
            return lifetimeScope.Resolve(serviceType);
        }

        public IEnumerable<object> GetServices(Type serviceType)
        {
            return ((IEnumerable)lifetimeScope.Resolve(typeof(IEnumerable<>).MakeGenericType(serviceType))).Cast<object>();
        }

        public T GetService<T>()
        {
            return (T)GetService(typeof(T));
        }

        public IEnumerable<T> GetServices<T>()
        {
            return (IEnumerable<T>)GetService(typeof(IEnumerable<T>));
        }

        public void Dispose()
        {
            lifetimeScope.Dispose();
        }
    }
}
