using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Wordsearch;

namespace Wordsearch_Solver
{
    public class WordsearchData
    {
        int length;
        char[] grid;
        List<string> simpleDictionary = new List<string>();
        List<Cell> advancedDictionary = new List<Cell>();

        string loadTime = "";

        public WordsearchData(char[] _grid, List<string> _simpleDictionary, int _length)
        {
            grid = _grid;
            simpleDictionary = _simpleDictionary;
            length = _length;
        }

        public int GetLength()
        {
            return length;
        }

        public char[] GetGrid()
        {
            return grid;
        }

        public List<string> GetSimpleDictionary()
        {
            return simpleDictionary;
        }

        public void SetLoadTime(string _loadTime)
        {
            loadTime = _loadTime;
        }

        public string GetLoadTime()
        {
            return loadTime;
        }

        public List<Cell> GetAdvancedDictionary()
        {
            return advancedDictionary;
        }

        public void LoadAdvancedDictionary()
        {
            int dictionarySize = simpleDictionary.Count;

            for (int i =0; i< dictionarySize; i++)
            {
                AddWord(i);
            }
        }

        private void AddWord(int i)
        {
            string word = simpleDictionary[i];

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

        private void AddToRoot(ref bool match, Cell root, string word)
        {
            int wordSize = word.Length;
            match = true;

            Cell current = root;
            int depth = 0;

            while (match)
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
                current = AddNextCell(word, current, j);
            }
            AddLastCell(word, current);
            match = true;

        }

        private void AddLastCell(string word, Cell current)
        {
            int wordSize = word.Length;
            Cell lastcell = new Cell(word[wordSize - 1], word);
            current.AddCell(lastcell);
        }

        private Cell AddNextCell(string word, Cell current, int depth)
        {
            Cell newCell = new Cell(word[depth]);
            current.AddCell(newCell);
            return newCell;
        }

    }
}
