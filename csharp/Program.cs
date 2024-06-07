using System;
using SpreadyCalculator;

namespace SpreadyMcSpreader
{
    class Program
    {
        static void Main(string[] args)
        {
            var calculator = new SimpleCalculator();
            try
            {
                Console.WriteLine(calculator.CalculateStatistics(args[0]));
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An Error occurred: {ex.Message}");
            }
        }
    }
}
