using System;
using System.Collections.Generic;
using Wordsearch;

namespace Wordsearch_Solver
{
    public class WordsearchData
    {
        public int Length { get; }
        public char[] Grid { get; }
        public List<string> SimpleDictionary { get; }
        public List<Cell> AdvancedDictionary { get; }
        public string LoadTime { get; set; }

        public WordsearchData(char[] grid, List<string> simpleDictionary, int length)
        {
            Grid = grid;
            Length = length;
            SimpleDictionary = simpleDictionary;
            AdvancedDictionary = new List<Cell>();
            LoadTime = "";
        }

        public void LoadAdvancedDictionary()
        {
            foreach(string word in SimpleDictionary)
            {
                AddWord(word);
            }
        }

        private void AddWord(string word)
        {
            bool match = false;

            foreach(Cell root in AdvancedDictionary)
            {
                if(root.Letter == word[0])
                {
                    AddToRoot(ref match, root, word);
                }
            }
            if (match == false)
                AddNewRoot(word);
        }

        private void AddNewRoot(string word)
        {
            Cell root = new(word[0]);
            Cell current = root;

            for (int i = 1; i < word.Length - 1; i++)
            {
                current = AddNextCell(word[i], current);
            }
            AddLastCell(word[word.Length - 1], current, word);
            AdvancedDictionary.Add(root);
            
        }

        private void AddToRoot(ref bool match, Cell root, string word)
        {
            int wordSize = word.Length;
            match = true;
            Cell current = root;
            int depth = 0;

            while (match && depth < wordSize - 1)
            {
                match = false;
                foreach(Cell next in current.nextLetters)
                {
                    if(next.Letter == word[depth + 1])
                    {
                        current = next;
                        match = true;
                        depth++;

                        if(depth + 1 >= wordSize)
                            depth--;

                        if(depth == wordSize)
                        {
                            next.Word = word;
                            break;
                        }
                        break;
                    }
                }
            }

            for(int j= depth + 1; j < wordSize - 1; j++)
            {
                current = AddNextCell(word[j], current);
            }
            AddLastCell(word[wordSize - 1], current, word);
            match = true;

        }

        private static void AddLastCell(char letter, Cell current, string word)
        {
            Cell lastcell = new(letter, word);
            current.AddCell(lastcell);
        }

        private static Cell AddNextCell(char letter, Cell current)
        {
            Cell newCell = new(letter);
            current.AddCell(newCell);
            return newCell;
        }

    }
}
