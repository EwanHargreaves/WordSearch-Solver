using System;
using System.Collections.Generic;
using Wordsearch;

namespace Wordsearch_Solver
{
    public class WordsearchData
    {
        public int length { get; }
        public char[] grid { get; }
        public List<string> simpleDictionary { get; }
        public List<Cell> advancedDictionary { get; }
        public string loadTime { get; set; }

        public WordsearchData(char[] _grid, List<string> _simpleDictionary, int _length)
        {
            grid = _grid;
            length = _length;
            simpleDictionary = _simpleDictionary;
            advancedDictionary = new List<Cell>();
            loadTime = "";
        }

        public void LoadAdvancedDictionary()
        {
            foreach(string word in simpleDictionary)
            {
                AddWord(word);
            }
        }

        private void AddWord(string word)
        {
            bool match = false;

            foreach(Cell root in advancedDictionary)
            {
                if(root.letter == word[0])
                {
                    AddToRoot(ref match, root, word);
                }
            }
            if (match == false)
                AddNewRoot(word);
        }

        private void AddNewRoot(string word)
        {
            Cell root = new Cell(word[0]);
            Cell current = root;

            for (int i = 1; i < word.Length - 1; i++)
            {
                current = AddNextCell(word[i], current);
            }
            AddLastCell(word[word.Length - 1], current, word);
            advancedDictionary.Add(root);
            
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
                    if(next.letter == word[depth + 1])
                    {
                        current = next;
                        match = true;
                        depth++;

                        if(depth + 1 >= wordSize)
                            depth--;

                        if(depth == wordSize)
                        {
                            next.word = word;
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

        private void AddLastCell(char letter, Cell current, string word)
        {
            Cell lastcell = new Cell(letter, word);
            current.AddCell(lastcell);
        }

        private Cell AddNextCell(char letter, Cell current)
        {
            Cell newCell = new Cell(letter);
            current.AddCell(newCell);
            return newCell;
        }

    }
}
