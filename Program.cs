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
            "puzzle 5"
        };
        static void Main(string[] args)
        {
            Stopwatch stopwatch = new Stopwatch();

            foreach (string puzzle in puzzles)
            {
                //Simple solution
                stopwatch.Start();
                Wordsearch.setPuzzle(puzzle);
                stopwatch.Stop();
                string loadTime = stopwatch.Elapsed.TotalMilliseconds.ToString();

                stopwatch.Start();
                Wordsearch.SimpleSolve();
                stopwatch.Stop();
                string solveTime = stopwatch.Elapsed.TotalMilliseconds.ToString();

                Wordsearch.WriteResuts(loadTime, solveTime);

                //Advanced solution
            }
        }
    }
}