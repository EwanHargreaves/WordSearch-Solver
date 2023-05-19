using System;
using System.Collections.Generic;


namespace Wordsearch_Solver
{
    public abstract class BaseSolver : ISolver
    {
        protected int length;
        protected char[] grid;

        protected int gridCellsVisited;
        protected int dictionaryEntriesVisited;

        protected List<string> notFound = new();
        protected List<string> found = new List<string>();

        protected string method = "";

        abstract public void Solve();

        public void WriteResults(string loadTime, string solveTime, string filepath)
        {
            filepath = $"{filepath}{method}_Results.txt";
            Console.WriteLine("Outputing: " + filepath);
            StreamWriter writer = new StreamWriter(filepath);

            writer.WriteLine("NUMBER_OF_WORDS_MATCHED " + found.Count);

            writer.WriteLine("\nWORDS_MATCHED_IN_GRID");
            WriteList(writer, found);

            writer.WriteLine("\nWORDS_UNMATCHED_IN_GRID");
            WriteList(writer, notFound);

            writer.WriteLine("\nNUMBER_OF_GRID_CELLS_VISITED " + gridCellsVisited);
            writer.WriteLine("NUMBER_OF_DICTIONARY_ENTRIES_VISITED " + dictionaryEntriesVisited);

            writer.WriteLine("\nTIME_TO_POPULATE_GRID_STRUCTURE " + loadTime);
            writer.WriteLine("TIME_TO_SOLVE_PUZZLE " + solveTime);
            writer.Close();
        }

        protected void WriteList(StreamWriter wr, List<string> words)
        {
            foreach (string word in words)
            {
                wr.WriteLine(word);
            }
        }

        protected char? NextLetterInDirection(int[] direction, int x, int y, int depth)
        {
            int newX = x + (direction[0] * depth);
            int newY = y + (direction[1] * depth);

            if (!ValidPos(newX, newY))
                return null;

            return grid[newX + (newY * length)];
        }

        protected bool ValidPos(int x, int y)
        {
            gridCellsVisited++;
            if (x < 0 || x >= length || y < 0 || y >= length)
            {
                return false;
            }
            return true;
        }

        protected int[] GetDirection(int direction)
        {
            return direction switch
            {
                1 => new int[] { -1, -1 }, // Diagonal up-left
                2 => new int[] { -1, 0 },  // Up
                3 => new int[] { -1, 1 },  // Diagonal up-right
                4 => new int[] { 0, -1 },  // Left
                5 => new int[] { 0, 1 },   // Right
                6 => new int[] { 1, -1 },  // Diagonal down-left
                7 => new int[] { 1, 0 },   // Down
                8 => new int[] { 1, 1 },   // Diagonal down-right
                _ => new int[] { 0, 0 },   // Default case for unknown direction
            };
        }
    }
}
