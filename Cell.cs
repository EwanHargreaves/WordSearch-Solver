using System;
using System.Collections.Generic;

namespace Wordsearch_Solver
{
    public class Cell
    {
        public char Letter { get; private set; }
        public string Word { get; set; }

        public List<Cell> nextLetters = new();

        public Cell(char pLetter) : this(pLetter, "")
        {
        }

        public Cell(char pLetter, string pWord)
        {
            Letter = pLetter;
            Word = pWord;
        }

        public void AddCell(Cell cell)
        {
            nextLetters.Add(cell);
        }
    }
}
