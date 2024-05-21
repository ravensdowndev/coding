using System;
using System.Linq;
const char sectionDelimiter = '|';
const char gridLineDelimiter = ';';
const int numberOfSections = 3;

if (args.Length != 1)
{
  Console.Error.WriteLine("Program expects exactly one argument.");
  Environment.Exit(1);
}

var input = args[0];
var sections = input.Split(sectionDelimiter);

if (sections.Length != numberOfSections)
{
  Console.Error.WriteLine($"Input should consist of {numberOfSections} sections delimited by a '{sectionDelimiter}' character.");
  Environment.Exit(1);
}

var plannedGrid = sections[0].Split(gridLineDelimiter);
var actualGrid = sections[2].Split(gridLineDelimiter);

if (plannedGrid.Length != actualGrid.Length)
{
  Console.Error.WriteLine($"Lengths of planned and actual grid inputs must be the same.");
  Environment.Exit(1);
}

var currentFertilizer = int.Parse(sections[1]);
var totalCellCount = Math.Pow(actualGrid.Length, 2);
var overSpreadCells = 0;
var underSpreadCells = 0;

foreach (var line in plannedGrid.Zip(actualGrid))
{
  var (expected, actual) = line;
  var i = 0;
  var j = 0;

  while (i < expected.Length && j < actual.Length - 1)
  {
    var fertilizerAfterLeavingCell = int.Parse(actual[j..(j + 2)].ToString());
    var fertilizerDelta = currentFertilizer - fertilizerAfterLeavingCell;
    var expectedExpenditure = int.Parse(expected[i].ToString());

    if (fertilizerDelta < expectedExpenditure)
    {
      underSpreadCells++;
    }
    else if (fertilizerDelta > expectedExpenditure)
    {
      overSpreadCells++;
    }

    i += 1;
    j += 2;
    currentFertilizer = fertilizerAfterLeavingCell;
  }
}

var incorrectSpreads = overSpreadCells + underSpreadCells;
var accuracy = Math.Round((totalCellCount - incorrectSpreads) / totalCellCount * 100);
var report = $"{incorrectSpreads}|{overSpreadCells}|{underSpreadCells}|{accuracy}";
Console.WriteLine(report);
