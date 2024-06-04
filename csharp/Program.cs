using Autofac;
using SpreadyMcSpreader.Services;
using System;

namespace SpreadyMcSpreader
{
    class Program
    {
        static void Main(string[] args)
        {
            var container = ContainerConfig.Configure();
            using (var scope = container.BeginLifetimeScope())
            {
                var spreadCalculatorService = scope.Resolve<ISpreadCalculator>();
                try
                {
                    var result = spreadCalculatorService.Calculate(args[0]);
                    Console.WriteLine(result);
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"An error occurred: {ex.Message}");
                }
                finally
                {
                    Console.ReadKey();
                }
            }
        }
    }
}
