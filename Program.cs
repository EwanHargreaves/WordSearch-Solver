using System;
using System.Diagnostics;
using Wordsearch;

namespace Wordsearch_Solver
{
    class Program
    {
        static void Main(string[] args)
        {
            foreach (string path in puzzles)
            {
                string filepath = "Puzzles/" + path + "/";

                WordsearchData? wordsearch = LoadPuzzle(filepath);

                if (wordsearch == null)
                    break;

                ISolver sSolver = new SimpleSolver(wordsearch);
                SolveWith(sSolver, wordsearch.loadTime, filepath);

                ISolver aSolver = new AdvancedSolver(wordsearch);
                SolveWith(aSolver, wordsearch.loadTime, filepath);
                
            }
        }

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

        static WordsearchData? LoadPuzzle(string filepath)
        {
            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();
            PuzzleLoader loader = new PuzzleLoader(filepath);
            WordsearchData? wordsearch = loader.wordsearch;
            stopwatch.Stop();
            string loadTime = stopwatch.Elapsed.TotalMilliseconds.ToString();
            if (wordsearch != null)
                wordsearch.loadTime = loadTime;

            return wordsearch;
        }

        static void SolveWith(ISolver solver, string loadTime, string filepath)
        {
            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();
            solver.Solve();
            stopwatch.Stop();

            string solveTime = stopwatch.Elapsed.TotalMilliseconds.ToString();
            solver.WriteResults(loadTime, solveTime, filepath);
        }
    }
   
}
