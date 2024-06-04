using Autofac;
using SpreadyMcSpreader.Services;

public static class TestContainerConfig
{
    public static IContainer Configure()
    {
        var builder = new ContainerBuilder();
        builder.RegisterType<SpreadCalculator>().As<ISpreadCalculator>();
        return builder.Build();
    }
}