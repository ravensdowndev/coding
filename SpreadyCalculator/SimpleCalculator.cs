namespace SpreadyCalculator
{
    /// <summary>
    /// Separate class to encapsulate import, calculation and output formatting.
    /// 
    /// This is in a separate assembly for easier unit testing.
    /// </summary>
    public class SimpleCalculator
    {
        public int InitialAmount { get; set; } = 0;

        // Statistics properties
        private int PreviousAmount { get; set;} = 0;
        private int OverSpreadCount{ get; set; } = 0;
        private int UnderSpreadCount { get; set; } = 0;
        private int CorrectSpreadCount { get; set; } = 0;




        public string CalculateStatistics(string inputData)
        {
            // Split the raw input into planned, starting and remaining sections
            var sections = SplitSections(inputData);

            // Extract the starting amount 
            ExtractStartingAmount(sections[1]);

            // Extract a list of planned rows (raw string)
            var plannedRowStrings = sections[0].Split(";");
            var remainingRowStrings = sections[2].Split(";");

            //Check these are matching sizes and square
            ValidateRowStrings(plannedRowStrings, remainingRowStrings);

            ClearStatistics();

            CalculateStatistics(plannedRowStrings, remainingRowStrings);

            return FormatResults(); 
        }

        private string FormatResults()
        {
            int incorrectCount = OverSpreadCount + UnderSpreadCount;
            int totalCount = incorrectCount + CorrectSpreadCount;
            
            int roundedPercentageCorrect = (int)Math.Ceiling((double)(CorrectSpreadCount * 100.0/ totalCount));
            var result = $"{incorrectCount}|{OverSpreadCount}|{UnderSpreadCount}|{roundedPercentageCorrect}";

            return result;
        }

        private void CalculateStatistics(string[] plannedRowStrings, string[] remainingRowStrings)
        {
            for(int i = 0; i < plannedRowStrings.Length; i++)
            {
                CalculateStatisticsForRow(plannedRowStrings[i], remainingRowStrings[i]);
            }
        }

        private void CalculateStatisticsForRow(string plannedRowString, string remainingRowString)
        {
            // Extract / parse arrays of numbers from each row string
            var plannedAmounts = ParsePlannedRowString(plannedRowString, 1);
            var RemainingAmounts = ParsePlannedRowString(remainingRowString, 2);
            for(int i = 0; i < plannedAmounts.Length; i++)
            {
                var dispensed = PreviousAmount - RemainingAmounts[i];
                if (plannedAmounts[i] == dispensed)
                {
                    CorrectSpreadCount++;
                }
                else if (dispensed < plannedAmounts[i])
                {
                    UnderSpreadCount++;
                }
                else
                {
                    OverSpreadCount++;
                }
                PreviousAmount = RemainingAmounts[i];
            }

        }

        private int[] ParsePlannedRowString(string inputString, int fieldWidth)
        {
            int resultLength = inputString.Length / fieldWidth;
            var result = new int[resultLength];

            for(int i = 0; i < resultLength; i++)
            {
                int startIndex = i * fieldWidth;

                var ok = int.TryParse(inputString.Substring(startIndex, fieldWidth), out int cellValue);
                if(!ok)
                {
                    throw new ArgumentException("Error parsing data.");
                }
                result[i] = cellValue;
            }

            return result;
        }


        private void ClearStatistics()
        {
            PreviousAmount = InitialAmount;
            OverSpreadCount = 0;
            UnderSpreadCount = 0;
            CorrectSpreadCount = 0;
        }

        private void ValidateRowStrings(string[] plannedRowStrings, string[] remainingRowStrings)
        {
            if(plannedRowStrings.Length !=  remainingRowStrings.Length)
            {
                throw new ArgumentException("Planned and remaining rows do not match.");
            }

            int expectedRowLength = plannedRowStrings.Length;
            for(int i = 0; i <  expectedRowLength; i++)
            {
                if (plannedRowStrings[i].Length != expectedRowLength ||
                    remainingRowStrings[i].Length != expectedRowLength * 2)
                {
                    throw new ArgumentException("Planned and remaining cells do not align.");
                }
            }
        }

        private string[] SplitSections(string inputData)
        {
            var sections = inputData.Split("|");

            if (sections.Length != 3)
            {
                throw new ArgumentException("Invalid input - sections");
            }

            return sections;
        }

        private void ExtractStartingAmount(string inputData)
        {
            // try to parse string into int
            var ok = int.TryParse(inputData, out var amount);
            if(!ok)
            {
                throw new ArgumentException("Invalid input - couldn't read initial amount.");
            }

            InitialAmount = amount;
        }

        public string GetHello()
        {
            return "Hello world.";
        }
    }
}