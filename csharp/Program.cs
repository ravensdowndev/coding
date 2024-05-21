using System;
using System.Linq;
const char sectionDelimiter = '|';
const char gridLineDelimiter = ';';
const int numberOfSections = 3;

void Assert(bool condition, string message)
{
  if (!condition)
  {
    Console.Error.WriteLine(message);
    Environment.Exit(1);
  }
}

Assert(args.Length == 1, "Program expects exactly one argument.");
var input = args[0];
var sections = input.Split(sectionDelimiter);

Assert(sections.Length == numberOfSections, $"Input should consist of {numberOfSections} sections delimited by a '{sectionDelimiter}' character.");
var plannedGrid = sections[0].Split(gridLineDelimiter);
var actualGrid = sections[2].Split(gridLineDelimiter);

Assert(plannedGrid.Length == actualGrid.Length, "Lengths of planned and actual grid inputs must be the same.");
var totalCellCount = Math.Pow(actualGrid.Length, 2);
var initialFertilizerLevel = int.Parse(sections[1]);
var overSpreadCells = 0;
var underSpreadCells = 0;

foreach (var (plan, actual) in plannedGrid.Zip(actualGrid))
{
  var i = 0;
  var j = 0;

  while (i < plan.Length && j < actual.Length - 1)
  {
    var newFertilizerLevel = int.Parse(actual[j..(j + 2)].ToString());
    var fertilizerDelta = initialFertilizerLevel - newFertilizerLevel;
    var expectedExpenditure = int.Parse(plan[i].ToString());

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
    initialFertilizerLevel = newFertilizerLevel;
  }
}

var incorrectSpreads = overSpreadCells + underSpreadCells;
var accuracy = Math.Round((totalCellCount - incorrectSpreads) / totalCellCount * 100);
var report = $"{incorrectSpreads}|{overSpreadCells}|{underSpreadCells}|{accuracy}";
Console.WriteLine(report);
