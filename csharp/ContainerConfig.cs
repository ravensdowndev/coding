using Autofac;
using SpreadyMcSpreader.Services;

namespace SpreadyMcSpreader
{
    public static class ContainerConfig
    {
        public static IContainer Configure()
        {
            var builder = new ContainerBuilder();
            builder.RegisterType<SpreadCalculator>().As<ISpreadCalculator>();
            return builder.Build();
        }
    }
}