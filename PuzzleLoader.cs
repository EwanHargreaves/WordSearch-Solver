using System;
using System.Collections.Generic;
using System.IO;

namespace Wordsearch_Solver
{
    class PuzzleLoader
    {
        public WordsearchData? Wordsearch { get; private set; }
        private int length = 0;
        public PuzzleLoader(string path)
        {
            StreamReader[] readers = GetReaders(path);

            if (CheckReaders(readers))
            {
                char[] grid = ReadGrid(readers[0]);
                List<string> dictionary = ReadDictionary(readers[1]);

                Wordsearch = new WordsearchData(grid, dictionary, length);
            }
        }

        private static StreamReader[] GetReaders(string path)
        {
            const string GRID_PATH = "wordsearch_grid.txt";
            const string DICTIONARY_PATH = "dictionary.txt";
            StreamReader[] readers = new StreamReader[2];
   
            try
            {
                readers[0] = new StreamReader(Path.Combine(path, GRID_PATH));
                readers[1] = new StreamReader(Path.Combine(path, DICTIONARY_PATH));

            }
            catch (Exception e)
            {
                Console.WriteLine("A file could not be found:");
                Console.WriteLine(e.Message);
            }
            return readers;
        }

        private static bool CheckReaders(StreamReader[] readers)
        {
            return readers[0] != null && readers[1] != null;
        }

        private char[] ReadGrid(StreamReader gridReader)
        {
            char[] grid = Array.Empty<char>();
            try
            {
                string? line = gridReader.ReadLine() ?? throw new Exception("Null file");
                length = int.Parse(line);
                grid = new char[length * length];

                int row = 0;
                while (gridReader.EndOfStream == false)
                {
                    line = gridReader.ReadLine() ?? throw new Exception("File ended unexpectedly");

                    for (int i = 0; i < length; i++)
                    {
                        grid[i + (row * length)] = line[i * 2];
                    }
                    row++;
                }
                gridReader.Close();
            }
            catch (Exception e)
            {
                Console.WriteLine("Grid not loaded");
                Console.WriteLine(e.Message);
            }
            return grid;
        }

        private static List<string> ReadDictionary(StreamReader readDictionary)
        {
            List<string> dictionary = new List<string>();
            string? line = readDictionary.ReadLine();
            while (line != null)
            {
                dictionary.Add(line.ToUpper());
                line = readDictionary.ReadLine();
            }
            return dictionary;
        }
    }
}
