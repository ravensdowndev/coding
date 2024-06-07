using System;
using SpreadyCalculator;

namespace SpreadyMcSpreader
{
    class Program
    {
        static void Main(string[] args)
        {
            var calculator = new SimpleCalculator();
            Console.WriteLine(calculator.CalculateStatistics(args[0]));
        }
    }
}
