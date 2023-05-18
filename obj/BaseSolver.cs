using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Wordsearch_Solver
{
    public abstract class BaseSolver : ISolver
    {
        protected int length;
        protected char[] grid = new char[0];

        protected int gridCellsVisited;
        protected int dictionaryEntriesVisited;

        protected List<string> notFound = new();
        protected List<string> found = new List<string>();

        protected string method = "";

        abstract public void solve();

        public void writeResults(string loadTime, string solveTime, string filepath)
        {
            filepath = filepath + method + "_Results.txt";
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

        protected char? nextLetterInDirection(int[] direction, int x, int y, int depth)
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
            int[] dir = new int[2] { 1, 1 };
            switch (direction)
            {
                case 1:
                    dir[0] = 0;
                    break;
                case 2:
                    dir[0] = 0;
                    dir[1] = -1;
                    break;
                case 3:
                    dir[0] = -1;
                    dir[1] = 0;
                    break;
                case 4:
                    dir[1] = 0;
                    break;
                case 5:
                    dir[0] = -1;
                    break;
                case 6:
                    break;
                case 7:
                    dir[0] = -1;
                    dir[1] = -1;
                    break;
                case 8:
                    dir[1] = -1;
                    break;

            }
            return dir;
        }

       
    }
}
