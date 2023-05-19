using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Wordsearch_Solver;

namespace Wordsearch
{
    public class AdvancedSolver : BaseSolver
    {
        List<Cell> dictionary = new List<Cell>();

        public struct Searching
        {
            public Searching(int[] pDirection, Cell currentCell)
            {
                direction = pDirection;
                cell = currentCell;
            }

            public int[] direction { get; }
            public Cell cell { get; set; }
        }

        public AdvancedSolver(WordsearchData _wordsearch)
        {
            _wordsearch.loadAdvancedDictionary();
            dictionary = _wordsearch.getAdvancedDictionary();
            length = _wordsearch.getLength();
            grid = _wordsearch.getGrid();
            method = "Advanced";
            notFound = _wordsearch.getSimpleDictionary();
        }

        override public void solve()
        {
            for (int y = 0; y < length; y++)
            {
                for (int x = 0; x < length; x++)
                {
                    checkCell(x, y);
                }
            }
        }

        private void checkCell(int x, int y)
        {
            foreach (Cell root in dictionary)
            {
                dictionaryEntriesVisited++;
                matchingRoot(x, y, root);
            }
        }

        private void matchingRoot(int x, int y, Cell root)
        {
            if (grid[x + (y * length)] != root.letter)
                return;

            List<Searching> possibleDirections = validDirections(x, y, root);

            int depth = 2;
            foreach (Searching search in possibleDirections)
            {
                recursiveSearch(search, x, y, depth);
            }
         }

        //returns all directions in which the next letter matches a possible next letter 
        public List<Searching> validDirections(int x, int y, Cell root)
        {
            List<Searching> possibleDirections = new List<Searching>();
            for (int i = 1; i <= 8; i++)
            {
                int[] direction = GetDirection(i);
                char? letterInDirection = nextLetterInDirection(direction, x, y, 1);
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

        private void recursiveSearch(Searching search, int x, int y, int depth)
        {
            string word = search.cell.word;
            if (word != "")
            {
                notFound.Remove(word);
                string location = x + " " + y + " ";
                found.Add(location + word);
                return;
            }

            char? letterInDirection = nextLetterInDirection(search.direction, x, y, depth);
            if (letterInDirection == null)
                return;

            foreach (Cell nextCell in search.cell.nextLetters)
            {
                if (letterInDirection == nextCell.letter)
                {
                    Searching s = new Searching(search.direction, nextCell);
                    recursiveSearch(s, x, y, ++depth);
                }
            }
        }
    }
}