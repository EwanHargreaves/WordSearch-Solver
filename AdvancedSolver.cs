using System;
using System.Collections.Generic;
using Wordsearch_Solver;

namespace Wordsearch
{
    internal class AdvancedSolver : BaseSolver
    {
        private List<Cell> dictionary;

        public struct Searching
        {
            public Searching(int[] _direction, Cell _currentCell)
            {
                direction = _direction;
                cell = _currentCell;
            }

            public int[] direction { get; }
            public Cell cell { get; set; }
        }

        public AdvancedSolver(WordsearchData _wordsearch)
        {
            _wordsearch.LoadAdvancedDictionary();
            dictionary = _wordsearch.advancedDictionary;
            length = _wordsearch.length;
            grid = _wordsearch.grid;
            method = "Advanced";
            notFound = _wordsearch.simpleDictionary;
        }

        override public void Solve()
        {
            for (int y = 0; y < length; y++)
            {
                for (int x = 0; x < length; x++)
                {
                    CheckCell(x, y);
                }
            }
        }

        private void CheckCell(int x, int y)
        {
            foreach (Cell root in dictionary)
            {
                dictionaryEntriesVisited++;
                if (grid[x + (y * length)] == root.letter)
                {
                    MatchingRoot(x, y, root);
                }
            }
        }

        private void MatchingRoot(int x, int y, Cell root)
        {
            List<Searching> possibleDirections = ValidDirections(x, y, root);

            int depth = 2;
            foreach (Searching search in possibleDirections)
            {
                RecursiveSearch(search, x, y, depth);
            }
         }

        //returns all directions in which the next letter matches a possible next letter 
        public List<Searching> ValidDirections(int x, int y, Cell root)
        {
            List<Searching> possibleDirections = new List<Searching>();
            for (int i = 1; i <= 8; i++)
            {
                int[] direction = GetDirection(i);
                char? letterInDirection = NextLetterInDirection(direction, x, y, 1);
                if (letterInDirection == null)
                    continue;

                foreach (Cell nextCell in root.nextLetters)
                {
                    if (letterInDirection == nextCell.letter)
                    {
                        Searching search = new Searching(direction, nextCell);
                        possibleDirections.Add(search);
                        break;
                    }
                }
            }
            return possibleDirections;
        }

        private void RecursiveSearch(Searching search, int x, int y, int depth)
        {
            string word = search.cell.word;
            if (!string.IsNullOrEmpty(word))
            {
                notFound.Remove(word);
                string location = $"{x} {y} ";
                found.Add(location + word);
                return;
            }

            char? letterInDirection = NextLetterInDirection(search.direction, x, y, depth);
            if (letterInDirection == null)
                return;

            foreach (Cell nextCell in search.cell.nextLetters)
            {
                if (letterInDirection == nextCell.letter)
                {
                    Searching s = new Searching(search.direction, nextCell);
                    RecursiveSearch(s, x, y, ++depth);
                }
            }
        }
    }
}