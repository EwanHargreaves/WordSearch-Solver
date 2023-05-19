using System;
using System.Collections.Generic;

namespace Wordsearch_Solver
{
    public class Cell
    {
        public char letter { get; private set; }
        public string word { get; set; }

        public List<Cell> nextLetters = new List<Cell>();

        public Cell(char pLetter) : this(pLetter, "")
        {
        }

        public Cell(char pLetter, string pWord)
        {
            letter = pLetter;
            word = pWord;
        }

        public void AddCell(Cell cell)
        {
            nextLetters.Add(cell);
        }
    }
}
