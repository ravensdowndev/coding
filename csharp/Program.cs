using System;

namespace SpreadyMcSpreader;

internal class Program
{
    private static void Main(string[] args)
    {
        if (args.Length == 0)
        {
            Console.WriteLine("No input provided");
            return;
        }

        var input = args[0];
        
        // Split input into sections
        var sections = input.Split('|');
        if (sections.Length != 3)
        {
            Console.WriteLine("Invalid input format");
            return;
        }

        // Parsing the planned spreading map
        var plannedMapRows = sections[0].Split(';');
        var size = plannedMapRows.Length;
        var plannedMap = new int[size, size];

        for (var row = 0; row < size; row++)
        {
            for (var cell = 0; cell < size; cell++)
            {
                var cellValue = plannedMapRows[row][cell].ToString();
                plannedMap[row, cell] = int.Parse(cellValue);
            }
        }

        // Parsing the remaining fertilizer map
        var remainingRows = sections[2].Split(';');
        var remainingMap = new int[size, size];

        for (var row = 0; row < size; row++)
        {
            for (var cell = 0; cell < size; cell++)
            {
                var fertilizerCellValue = remainingRows[row].Substring(cell * 2, 2);
                remainingMap[row, cell] = int.Parse(fertilizerCellValue);
            }
        }

        // The the starting amount of fertilizer units the spreading robot has
        var startAmount = int.Parse(sections[1]);
        
        var totalCountIncorrectCells = 0;
        var totalCountOverCells = 0;
        var totalCountUnderCells = 0;
        var totalCountCorrectCells = 0;
        
        var totalCells = size * size;

        for (int row = 0; row < size; row++)
        {
            for (int cell = 0; cell < size; cell++)
            {
                // Calculate the actual spread for the current cell
                int actualSpreadForCell = startAmount - remainingMap[row, cell];
                
                // Compare the actual spread with the planned spread
                if (actualSpreadForCell > plannedMap[row, cell])
                {
                    totalCountOverCells++;
                    totalCountIncorrectCells++;
                }
                else if (actualSpreadForCell < plannedMap[row, cell])
                {
                    totalCountUnderCells++;
                    totalCountIncorrectCells++;
                }
                else
                {
                    totalCountCorrectCells++;
                }

                // Update starting amount of fertilizer units for the next cell
                startAmount = remainingMap[row, cell];
            }
        }

        // Calculate the percentage of the cells spread correctly (rounded up to the nearest whole number)
        var percentageAccuracy = (int)Math.Ceiling((double)totalCountCorrectCells * 100 / totalCells);

        var output = $"{totalCountIncorrectCells}|{totalCountOverCells}|{totalCountUnderCells}|{percentageAccuracy}";
        Console.WriteLine(output);
    }
}