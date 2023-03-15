using System;
using System.Diagnostics;

namespace Wordsearch_Solver
{
    class Program
    {
        static string[] puzzles = { 
            "puzzle 1",
            "puzzle 2",
            "puzzle 3",
            "puzzle 4",
            "puzzle 5",
            "Large dictionary",
            "Large dictionary - 3 Letter min",
            "Small dictionary"
        };
        static void Main(string[] args)
        {
            Stopwatch stopwatch = new Stopwatch();

            foreach (string puzzle in puzzles)
            {
                //Setup
                stopwatch.Start();
                Wordsearch.setPuzzle(puzzle);
                stopwatch.Stop();
                string loadTime = stopwatch.Elapsed.TotalMilliseconds.ToString();
                stopwatch.Restart();

                //Simple solution
                stopwatch.Start();
                Wordsearch.SimpleSolve();
                stopwatch.Stop();
                string solveTime = stopwatch.Elapsed.TotalMilliseconds.ToString();
                stopwatch.Restart();
                Wordsearch.WriteResuts(loadTime, solveTime);

                //Advanced solution
                stopwatch.Start();
                Wordsearch.AdvancedSolve();
                stopwatch.Stop();
                solveTime = stopwatch.Elapsed.TotalMilliseconds.ToString();
                Wordsearch.WriteResuts(loadTime,solveTime);

            }
        }
    }
}