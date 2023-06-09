﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Wordsearch
{
    public class Cell
    {
        private char letter;
        private string word;
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

        public char GetLetter()
        {
            return letter;
        }

        public void SetWord(string pWord)
        {
            word = pWord;
        }

        public string GetWord()
        {
            return word;
        }
    }
}
