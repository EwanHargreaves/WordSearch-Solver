using System;

namespace Wordsearch_Solver
{
    class PuzzleLoader
    {
        WordsearchData? wordsearch;
        int length = 0;
        public PuzzleLoader(string path)
        {
            StreamReader[] readers = getReaders(path);

            if (checkReaders(readers))
            {
                char[] grid = readGrid(readers[0]);
                List<string> dictionary = readDictionary(readers[1]);

                wordsearch = new WordsearchData(grid, dictionary, length);
            }
        }

        public WordsearchData? getWordsearch()
        {
            return wordsearch;
        }

        StreamReader[] getReaders(string path)
        {
            const string GRID_PATH = "wordsearch_grid.txt";
            const string DICTIONARY_PATH = "dictionary.txt";
            StreamReader[] readers = new StreamReader[2];
   
            try
            {
                readers[0] = new StreamReader(path + GRID_PATH);
                readers[1] = new StreamReader(path + DICTIONARY_PATH);

            }
            catch (Exception e)
            {
                Console.WriteLine("A file could not be found:");
                Console.WriteLine(e.Message);
            }
            return readers;
        }

        bool checkReaders(StreamReader[] readers)
        {
            if (readers[0] == null || readers[1] == null)
                return false;
            return true;
        }

        char[] readGrid(StreamReader gridReader)
        {
            char[] grid = new char[0];
            try
            {
                string? line = gridReader.ReadLine();

                if (line == null)
                {
                    throw new Exception("null file");
                }

                length = int.Parse(line);
                grid = new char[length * length];

                int row = 0;
                while (gridReader.EndOfStream == false)
                {
                    line = gridReader.ReadLine();
                    if (line == null)
                    {
                        throw new Exception("File ended unexpectedly");
                    }

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

        List<string> readDictionary(StreamReader readDictionary)
        {
            List<string> dictionary = new List<string>();
            string? line = readDictionary.ReadLine();
            while (line != null)
            {
                dictionary.Add(line);
                line = readDictionary.ReadLine();
            }
            return dictionary;
        }
    }
}
