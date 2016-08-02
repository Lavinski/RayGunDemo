using System;
using System.Collections.Generic;
using Autofac;
using Enexure.MicroBus;
using Enexure.MicroBus.Autofac;
using Serilog;
using Serilog.Events;

namespace RayGunDemo
{
    class Program
    {
        private static void Run(IContainer container)
        {
            var routes = new Dictionary<string, ICommand>()
            {
                { "", new RootCommand() },
                { "/", new RootCommand() },
                { "/good", new GoodCommand() },
                { "/error", new ErrorCommand() },
                { "/lengthy", new LengthyCommand() },
            };

            RunWeb(routes, container);
        }

        private static BusBuilder GetBusBuilder()
        {
            return new BusBuilder()
                .RegisterGlobalHandler<LoggingHandler>()
                .RegisterGlobalHandler<ExceptionHandler>()
                .RegisterHandlers(typeof(Program).Assembly);
        }

        private static ILogger CreateLogger()
        {
            return new LoggerConfiguration()
                .WriteTo.Seq("http://localhost:5341/")
                /*.WriteTo.Raygun(
                    applicationKey: null,
                    wrapperExceptions: null,
                    userNameProperty: "UserName",
                    applicationVersionProperty: "ApplicationVersion",
                    restrictedToMinimumLevel: LogEventLevel.Error,
                    formatProvider: null,
                    tags: null,
                    ignoredFormFieldNames: null)*/
                .Enrich.WithProperty("UserName", "dlit")
                .Enrich.WithProperty("ApplicationVersion", "1.0.0")
                .CreateLogger();
        }

        static void Main(string[] args)
        {
            var logger = CreateLogger();
            var containerBuilder = new ContainerBuilder();
            var busBuilder = GetBusBuilder();

            containerBuilder.RegisterMicroBus(busBuilder);
            containerBuilder.RegisterInstance(logger);

            var container = containerBuilder.Build();

            Run(container);
        }

        public static void RunWeb(Dictionary<string, ICommand> routes, ILifetimeScope scope)
        {
            var log = scope.Resolve<ILogger>();

            while (true)
            {
                Console.Write("http://demo");
                var path = Console.ReadLine();

                using (var requestScope = scope.BeginLifetimeScope("MicroBus", builder =>
                {
                    builder.RegisterInstance(log.ForContext("ConversationId", Guid.NewGuid()));
                }))
                {
                    var bus = requestScope.Resolve<IMicroBus>();

                    ICommand command;
                    if (routes.TryGetValue(path, out command))
                    {
                        bus.SendAsync(command).Wait();
                    }
                    else
                    {
                        bus.SendAsync(new NotFoundCommand()).Wait();
                    }
                }
            }
        }
    }
}
