using System;

namespace SpreadyMcSpreader.Services
{
    public class SpreadCalculator : ISpreadCalculator
    {
        const char INPUT_SEPARATOR = '|';
        const char MAP_ROW_SEPARATOR = ';';

        /// <summary>
        /// Calculates the number of incorrectly spread cells, over spread cells, under spread cells and percentage accuracy of the robot spreader
        /// based on the actual amount of fertiliser used against the planned map.
        /// </summary>
        /// <param name="input">A string which contains 3 sections separated by pipe character. Planned map | starting amount of fertilizer that the robot has | the remaining amount of fertilizer the robot has when leaving the cell (2 digits)</param>
        /// <returns>A string showing the number of incorrectlySpread | overSpread | underSpread | accuracy %</returns>
        public string Calculate(string input)
        {
            if (String.IsNullOrEmpty(input))
            {
                throw new ArgumentNullException(input);
            }

            var inputParts = input.Split(INPUT_SEPARATOR);

            if (inputParts.Length < 3)
            {
                throw new ArgumentException("Invalid input. String should have 3 inputs separated by |.");
            }

            try
            {               
                var plannedSpreadMap = inputParts[0].Split(MAP_ROW_SEPARATOR);                
                var actualSpreadMap = inputParts[2].Split(MAP_ROW_SEPARATOR);
                var startingAmtOfFertilizer = int.Parse(inputParts[1]);

                int n = plannedSpreadMap.Length;
                int incorrectlySpread = 0, overSpread = 0, underSpread = 0, correctSpread = 0;

                int previousFertilizer = startingAmtOfFertilizer;

                for (int i = 0; i < n; i++)
                {
                    for (int j = 0; j < n; j++)
                    {
                        int.TryParse(plannedSpreadMap[i][j].ToString(), out int planned);

                        int remainingIndex = j * 2;
                        int.TryParse(actualSpreadMap[i].Substring(remainingIndex, 2), out int remaining);

                        int actualSpread = previousFertilizer - remaining;

                        if (actualSpread != planned)
                        {
                            incorrectlySpread++;
                            if (actualSpread > planned)
                            {
                                overSpread++;
                            }
                            else
                            {
                                underSpread++;
                            }
                        }
                        else
                        {
                            correctSpread++;
                        }
                        previousFertilizer = remaining;
                    }
                }

                int totalCells = n * n;
                var accuracy = Math.Round((double)correctSpread / totalCells * 100);

                string result = $"{incorrectlySpread}|{overSpread}|{underSpread}|{accuracy}";
                return result;
            }
            catch (Exception ex)
            {
                throw new ApplicationException("An error occurred while calculating the fertilizer spread.", ex);
            }
        }
    }
}
