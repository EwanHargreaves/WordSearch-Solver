using System;
using System.Collections.Generic;
using Wordsearch_Solver;

namespace Wordsearch
{
    internal class AdvancedSolver : BaseSolver
    {
        private readonly List<Cell> dictionary;

        public struct Searching
        {
            public Searching(int[] direction, Cell currentCell)
            {
                Direction = direction;
                Cell = currentCell; 
            }

            public int[] Direction { get; }
            public Cell Cell { get; set; }
        }

        public AdvancedSolver(WordsearchData _wordsearch)
        {
            _wordsearch.LoadAdvancedDictionary();
            dictionary = _wordsearch.AdvancedDictionary;
            length = _wordsearch.Length;
            grid = _wordsearch.Grid;
            method = "Advanced";
            notFound = _wordsearch.SimpleDictionary;
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
                if (grid[x + (y * length)] == root.Letter)
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
                    if (letterInDirection == nextCell.Letter)
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
            string word = search.Cell.Word;
            if (!string.IsNullOrEmpty(word))
            {
                notFound.Remove(word);
                string location = $"{x} {y} ";
                found.Add(location + word);
                return;
            }

            char? letterInDirection = NextLetterInDirection(search.Direction, x, y, depth);
            if (letterInDirection == null)
                return;

            foreach (Cell nextCell in search.Cell.nextLetters)
            {
                if (letterInDirection == nextCell.Letter)
                {
                    Searching s = new Searching(search.Direction, nextCell);
                    RecursiveSearch(s, x, y, ++depth);
                }
            }
        }
    }
}