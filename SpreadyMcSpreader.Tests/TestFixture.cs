using Autofac;
using SpreadyMcSpreader.Services;

namespace SpreadyMcSpreader.Tests
{
    public class TestFixture
    {
        private readonly ISpreadCalculator _spreadCalculator;
        public TestFixture()
        {
            var container = ContainerConfig.Configure();
            using (var scope = container.BeginLifetimeScope())
            {
                _spreadCalculator = scope.Resolve<ISpreadCalculator>();
            }
        }

        public ISpreadCalculator SpreadCalculatorService() => _spreadCalculator;
    }
}
