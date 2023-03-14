using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Wordsearch_Solver
{
    static class Wordsearch
    {
        //Location of test files
        const string file = "Puzzles/";
        static string filePath = "";
        //Text file names
        const string gridName = "wordsearch_grid.txt";
        const string dictionaryName = "dictionary.txt";

        //grid data
        static int length;
        static char[] grid;

        //Dictionaries
        static List<string> simpleDictionary = new List<string>();
        static List<Cell> advancedDictionary = new List<Cell>();
        
        //Current results
        static List<string> notFound = new List<string>();
        static List<string> found = new List<string>();
        static int gridCellsVisited;
        static int dictionaryEntriesVisited;

        static string method = "";
         
        public static void setPuzzle(string puzzleName)
        {
            filePath = file + puzzleName + "/";
            readGrid();
        }

        public static void readGrid()
        {
            try
            {
                StreamReader reader = new StreamReader(filePath + gridName);
                if (reader.EndOfStream)
                    throw new Exception("File empty");

                string line = reader.ReadLine();

                length = int.Parse(line);
                grid = new char[length * length];

                int row = 0;
                while (reader.EndOfStream == false)
                {
                    line = reader.ReadLine();
                    for (int i = 0; i < length; i++)
                    {
                        grid[i + (row * length)] = line[i * 2];
                    }
                    row++;
                }
                reader.Close();
                Console.WriteLine("Grid loaded");
                Console.WriteLine("length: " + length);
            }
            catch
            {
                Console.WriteLine("grid not loaded");
            }
        }

        public static void SimpleSolve()
        {
            loadSimpleDictionary();
            ResetSolver();
            method = "Simple";
            Simple();
        }

        public static void loadSimpleDictionary()
        {
            try
            {
                StreamReader reader = new StreamReader(filePath + dictionaryName);
                if (reader.EndOfStream)
                    throw new Exception("File empty");
                string line = reader.ReadLine();

                while (reader.EndOfStream == false)
                {
                    simpleDictionary.Add(line);
                    line = reader.ReadLine();
                }
                Console.WriteLine("Simple dictionaty loaded");
                Console.WriteLine("First word: " + simpleDictionary[1]);
            }
            catch
            {
                Console.WriteLine("Simple dictionary failed to load");
            }
        }

        private static void ResetSolver()
        {
            notFound = new List<string>(simpleDictionary);
            gridCellsVisited = 0;
            dictionaryEntriesVisited = 0;
        }

        private static void Simple()
        {
            for (int y = 0; y < length; y++)
            {
                for (int x = 0; x < length; x++)
                {
                    SearchCell(x, y);
                    gridCellsVisited++;
                }
            }
        }

        private static void SearchCell(int x, int y)
        {
            char letter = grid[x + (y * length)];

            foreach (string word in simpleDictionary)
            {
                dictionaryEntriesVisited++;
                if (letter == word[0])
                {
                    List<int> dirToCheck = possibleDirections(x, y, word);

                    if (dirToCheck == new List<int>())
                        continue;

                    foreach (int direction in dirToCheck)
                    {
                        isWord(x, y, direction, word);
                    }
                }
            }
        }

        private static void isWord(int x, int y, int direction, string word)
        {
            for (int depth = 1; depth < word.Length; depth++)
            {
                int[] dir = GetDirection(direction);
                int newX = x + (dir[0] * depth);
                int newY = y + (dir[1] * depth);

                if (!ValidPos(newX, newY))
                    break;

                if (grid[newX + (newY * length)] != word[depth])
                    break;

                if (depth + 1 == word.Length)
                {
                    notFound.Remove(word);
                    string location = x.ToString() + " " + y.ToString() + " ";
                    found.Add(location + word);
                }
            }
        }

        private static List<int> possibleDirections(int x, int y, string word)
        {
            List<int> check = new List<int>();

            for (int i = 1; i <= 8; i++)
            {
                int[] dir = GetDirection(i);
                int hor = x + dir[0];
                int vert = y + dir[1];

                if (!ValidPos(hor, vert))
                    continue;

                if (grid[hor + (vert * length)] == word[1])
                    check.Add(i);
            }
            return check;
        }

        private static bool ValidPos(int x, int y)
        {
            gridCellsVisited++;
            if (x < 0 || x >= length || y < 0 || y >= length)
            {
                return false;
            }
            return true;
        }

        static int[] GetDirection(int direction)
        {
            int[] dir = new int[2] {1,1};
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

        public static void WriteResuts(string loadTime, string solveTime)
        {
            StreamWriter writer = new StreamWriter(filePath + method + "_" + "Results.txt");

            writer.WriteLine("NUMBER_OF_WORDS_MATCHED " + found.Capacity);

            writer.WriteLine("\nWORDS_MATCHED_IN_GRID");
            Output(writer,found);

            writer.WriteLine("\nWORDS_UNMATCHED_IN_GRID");
            Output(writer,notFound);

            writer.WriteLine("\nNUMBER_OF_GRID_CELLS_VISITED " + gridCellsVisited);
            writer.WriteLine("NUMBER_OF_DICTIONARY_ENTRIES_VISITED " + dictionaryEntriesVisited);

            Console.WriteLine("/nTIME_TO_POPULATE_GRID_STRUCTURE " + loadTime);
            Console.WriteLine("TIME_TO_SOLVE_PUZZLE" + solveTime);
            writer.Close();
        }

        private static void Output (StreamWriter wr, List<string> words)
        {
            foreach (string word in words)
            {
                wr.WriteLine(word);
            }
        }
    }
}
