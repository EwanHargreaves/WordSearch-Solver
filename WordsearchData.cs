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

        public int getLength()
        {
            return length;
        }

        public char[] getGrid()
        {
            return grid;
        }

        public List<string> getSimpleDictionary()
        {
            return simpleDictionary;
        }

        public void setLoadTime(string _loadTime)
        {
            loadTime = _loadTime;
        }

        public string getLoadTime()
        {
            return loadTime;
        }

        public List<Cell> getAdvancedDictionary()
        {
            return advancedDictionary;
        }

        public void loadAdvancedDictionary()
        {
            int dictionarySize = simpleDictionary.Count;

            for (int i =0; i< dictionarySize; i++)
            {
                addWord(i);
            }
        }

        private void addWord(int i)
        {
            string word = simpleDictionary[i];

            bool match = false;

            foreach(Cell root in advancedDictionary)
            {
                if(root.letter == word[0])
                {
                    addToRoot(ref match, root, word);
                }
            }
            if (match == false)
                addNewRoot(word);
        }

        private void addNewRoot(string word)
        {
            int wordSize = word.Length;
            Cell root = new Cell(word[0]);
            Cell current = root;

            for (int j = 1; j < wordSize - 1; j++)
            {
                current = addNextCell(word, current, j);
            }
            addLastCell(word, current);
            advancedDictionary.Add(root);
            
        }

        private void addToRoot(ref bool match, Cell root, string word)
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
                current = addNextCell(word, current, j);
            }
            addLastCell(word, current);
            match = true;

        }

        private void addLastCell(string word, Cell current)
        {
            int wordSize = word.Length;
            Cell lastcell = new Cell(word[wordSize - 1], word);
            current.AddCell(lastcell);
        }

        private Cell addNextCell(string word, Cell current, int depth)
        {
            Cell newCell = new Cell(word[depth]);
            current.AddCell(newCell);
            return newCell;
        }

    }
}
