﻿using System;
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
            simpleDictionary = new List<string>();
            try
            {
                StreamReader reader = new StreamReader(filePath + dictionaryName);
                if (reader.EndOfStream)
                    throw new Exception("File empty");
                string line = reader.ReadLine();

                while (reader.EndOfStream == false)
                {
                    simpleDictionary.Add(line);
                    line = reader.ReadLine().ToUpper();
                }
                simpleDictionary.Add(line);
                Console.WriteLine("Simple dictionaty loaded");
                Console.WriteLine("First word: " + simpleDictionary[0]);
            }
            catch
            {
                Console.WriteLine("Simple dictionary failed to load");
            }
        }

        private static void ResetSolver()
        {
            notFound = new List<string>(simpleDictionary);
            found = new List<string>();
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
                int[] direction = GetDirection(i);
                char? next = nextLetterInDirection(direction, x, y, 1);
                if (next == null)
                    continue;

                if (next == word[1])
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
            Console.WriteLine("Outputing: " + method + "_" + "Results.txt\n");
            StreamWriter writer = new StreamWriter(filePath + method + "_" + "Results.txt");

            writer.WriteLine("NUMBER_OF_WORDS_MATCHED " + found.Count);

            writer.WriteLine("\nWORDS_MATCHED_IN_GRID");
            Output(writer,found);

            writer.WriteLine("\nWORDS_UNMATCHED_IN_GRID");
            Output(writer,notFound);

            writer.WriteLine("\nNUMBER_OF_GRID_CELLS_VISITED " + gridCellsVisited);
            writer.WriteLine("NUMBER_OF_DICTIONARY_ENTRIES_VISITED " + dictionaryEntriesVisited);

            writer.WriteLine("\nTIME_TO_POPULATE_GRID_STRUCTURE " + loadTime);
            writer.WriteLine("TIME_TO_SOLVE_PUZZLE " + solveTime);
            writer.Close();
        }

        private static void Output (StreamWriter wr, List<string> words)
        {
            foreach (string word in words)
            {
                wr.WriteLine(word);
            }
        }

        public static void AdvancedSolve()
        {
            loadAdvancedDictionary();
            Console.WriteLine("Advanced dictionary loaded");
            ResetSolver();
            method = "Advanced";
            Advanced();
        }

        private static void loadAdvancedDictionary()
        {
            loadSimpleDictionary();
            int dictionarySize = simpleDictionary.Count;

            for(int i = 0; i < dictionarySize; i++)
            {
                addWord(i);
            }
        }

        static void addWord(int i)
        {
            string word = simpleDictionary[i];
            //int wordSize = word.Length;

            bool match = false;
            foreach (Cell root in advancedDictionary)
            {
                if (root.GetLetter() == word[0])
                {
                    addToRoot(ref match, root, word);
                }
            } 
            if (match == false)
            {
                addNewRoot(word);
            }
        }

        private static void addNewRoot(string word)
        {
            int wordSize = word.Length;
            Cell root = new Cell(word[0]);
            Cell current = root;

            for (int j = 1; j < wordSize - 1; j++)
            {
                current = AddNextCell(word, current, j);
            }
            AddLastCell(word, current);
            advancedDictionary.Add(root);
        }

        private static void addToRoot(ref bool match, Cell root, string word)
        {
            int wordSize = word.Length;
            match = true;

            Cell current = root;
            int depth = 0;

            while (match)
            {
                match = false;
                foreach (Cell next in current.nextLetters)
                {
                    if (next.GetLetter() == word[depth + 1])
                    {
                        current = next;
                        match = true;
                        depth++;

                        if (depth + 1 >= wordSize)
                            depth--;


                        if (depth == wordSize)
                        {
                            next.SetWord(word);
                            break;
                        }
                        break;
                    }
                }
            }
            for (int j = depth + 1; j < wordSize - 1; j++)
            {
                current = AddNextCell(word, current, j);
            }
            AddLastCell(word, current);
            match = true;
            //break;  return?
        }

        private static void AddLastCell(string word,Cell current)
        {
            int wordSize = word.Length;
            Cell lastCell = new Cell(word[wordSize - 1], word);
            current.AddCell(lastCell);
        }

        private static Cell AddNextCell(string word, Cell current, int depth)
        {
            Cell newCell = new Cell(word[depth]);
            current.AddCell(newCell);
            return newCell;
        }

        /*
        private struct Search
        {
            public Search(int pDirection, Cell pNextCell)
            {
                direction = pDirection;
                nextInLine = pNextCell;
            }

            public int direction { get; }
            public Cell nextInLine { get; set; }
        }
        */

        /*
        private static void Advanced()
        {
            for (int y = 0; y < length; y++)
            {
                for (int x = 0; x < length; x++)
                {
                    foreach (Cell root in advancedDictionary)
                    {
                        dictionaryEntriesVisited++;
                        if (grid[x + (y * length)] == root.GetLetter())
                        {
                            List<Search> directionsToCheck = new List<Search>();
                            foreach (Cell nextLetter in root.nextLetters)
                            {
                                for (int i = 0; i < 8; i++)
                                {
                                    int[] direction = GetDirection(i);
                                    char? next = nextLetterInDirection(direction,x,y,1);

                                    gridCellsVisited++;
                                    if(next == nextLetter.GetLetter())
                                    {
                                        Search possibleWord = new Search(i,nextLetter);
                                        directionsToCheck.Add(possibleWord);
                                    }
                                }
                            }

                            if (directionsToCheck.Count == 0)
                                continue;


                            for (int i = 0; i < directionsToCheck.Count; i++)
                            {
                                Search path = directionsToCheck[i];
                                int depth = 2;
                                int[] direction = GetDirection(path.direction);
                                bool match = true;

                                do
                                {
                                    //Reached end of word
                                    if (path.nextInLine.GetWord() != "")
                                    {
                                        string word = path.nextInLine.GetWord();

                                        notFound.Remove(word);
                                        string location = x + " " + y + " ";
                                        found.Add(location + word);
                                    }

                                    if (path.nextInLine.nextLetters.Count == 0)
                                        break;

                                    char? next = nextLetterInDirection(direction, x, y, depth);
                                    if (next == null)
                                        break;

                                    match = false;
                                    foreach (Cell nextLetter in path.nextInLine.nextLetters)
                                    {
                                        if (next == nextLetter.GetLetter())
                                        {
                                            path.nextInLine = nextLetter;
                                            match = true;
                                            depth++;
                                        }
                                    }

                                } while (match);
                            }                          
                        }
                    }
                }
            }
        }
        */

        private static char? nextLetterInDirection(int[] direction , int x, int y, int depth)
        {
            int newX = x + (direction[0] * depth);
            int newY = y + (direction[1] * depth);

            if (!ValidPos(newX, newY))
                return null;

            return grid[newX + (newY * length)];
        }


        private static void Advanced()
        {
            for (int y = 0; y < length; y++)
            {
                for (int x = 0; x < length; x++)
                {
                    checkCell(x,y);
                }
            }
        }

        private static void checkCell(int x, int y)
        {
            foreach (Cell root in advancedDictionary)
            {
                dictionaryEntriesVisited++;
                matchingRoot(x, y, root);
            }
        }

        public struct Searching
        {
            public Searching(int[] pDirection, Cell currentCell)
            {
                direction = pDirection;
                cell = currentCell;
            }

            public int[] direction { get; } 
            public Cell cell { get; set; }
        }

        private static void matchingRoot(int x, int y, Cell root)
        {
            if (grid[x + (y * length)] != root.GetLetter())
                return;

            List<Searching> possibleDirections = validDirections(x,y,root);

            int depth = 2;
            foreach (Searching search in possibleDirections)
            {
                recursiveSearch(search,x,y,depth);
            }
        }

        //returns all directions in which the next letter matches a possible next letter from the root
        public static List<Searching> validDirections(int x, int y, Cell root)
        {
            List<Searching> possibleDirections = new List<Searching>();
            for (int i = 1; i <= 8; i++)
            {
                int[] direction = GetDirection(i);
                char? letterInDirection = nextLetterInDirection(direction, x, y, 1);
                if (letterInDirection == null)
                    continue;

                foreach (Cell nextCell in root.nextLetters)
                {
                    if (letterInDirection == nextCell.GetLetter())
                    {
                        Searching search = new Searching(direction, nextCell);
                        possibleDirections.Add(search);
                        break;
                    }
                }
            }
            return possibleDirections;
        }

        private static void recursiveSearch(Searching search, int x, int y, int depth)
        {
            string word = search.cell.GetWord();
            if (word != "")
            {
                notFound.Remove(word);
                string location = x + " " + y + " ";
                found.Add(location + word);
                return;
            }

            char? letterInDirection = nextLetterInDirection(search.direction, x, y, depth);
            if (letterInDirection == null)
                return;

            foreach (Cell nextCell in search.cell.nextLetters)
            {
                if (letterInDirection == nextCell.GetLetter())
                {
                    Searching s = new Searching(search.direction, nextCell);
                    recursiveSearch(s, x, y, ++depth);
                }
            }
        }
    }
}
