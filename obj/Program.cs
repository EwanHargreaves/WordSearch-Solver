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

                WordsearchData? wordsearch = loadPuzzle(filepath);

                if (wordsearch == null)
                    break;

                ISolver sSolver = new SimpleSolver(wordsearch);
                solveWith(sSolver, wordsearch.getLoadTime(), filepath);

                ISolver aSolver = new AdvancedSolver(wordsearch);
                solveWith(aSolver, wordsearch.getLoadTime(), filepath);
                
            }
        }

        static string[] puzzles = {
            "puzzle 1",
            //"puzzle 2",
            //"puzzle 3",
            //"puzzle 4",
            //"puzzle 5",
            "Large dictionary",
            "Large dictionary - 3 Letter min",
            "Small dictionary"
        };

        static WordsearchData? loadPuzzle(string filepath)
        {
            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();
            PuzzleLoader loader = new PuzzleLoader(filepath);
            WordsearchData? wordsearch = loader.getWordsearch();
            stopwatch.Stop();
            string loadTime = stopwatch.Elapsed.TotalMilliseconds.ToString();
            if (wordsearch != null)
                wordsearch.setLoadTime(loadTime);
            return wordsearch;
        }

        static void solveWith(ISolver solver, string loadTime, string filepath)
        {
            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();
            solver.solve();
            stopwatch.Stop();

            string solveTime = stopwatch.Elapsed.TotalMilliseconds.ToString();
            solver.writeResults(loadTime, solveTime, filepath);
        }
    }
   
}
